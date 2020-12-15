using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Theater_ticket_booking.Helpers;
using Theater_ticket_booking.Models;
using Theater_ticket_booking.Models.DB;
using Theater_ticket_booking.ModelsView.UserModels;
using Theater_ticket_booking.Repositories;

namespace Theater_ticket_booking.Controllers.API
{
    [Route("api/User")]
    [ApiController]
    [Authorize]
    public class UserApi : _BaseApi
    {
        public UserApi(UsersRepository userRepository) : base(userRepository)
        {
        }

        /// <summary>
        /// Получение токена доступа (Авторизация)
        /// </summary>
        /// <param name="username">Имя учетной записи (Email/Login/Номер телефона)</param>
        /// <param name="password">Пароль учетной записи</param>
        /// <returns>Токен доступа</returns>
        [HttpGet("")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return StatusCode(400, "Не указан логин и/или пароль");
            var token = await Authorize(username, password);
            if (token == null)
                return BadRequest(new { errorText = "Не правильный логин или пароль." });

            return Ok(token);
        }

        /// <summary>
        /// Регистрация новой учетной записи
        /// </summary>
        /// <param name="model">Учетный данные</param>
        /// <returns></returns>
        [HttpPost("")]
        [AllowAnonymous]
        public async Task<IActionResult> Registration(RegistrationModel model)
        {
            if (!model.Validate(out string errors))
                return StatusCode(400, errors);

            if (await _userRepository.Any(p => p.Email == model.Email))
                return StatusCode(400, "Пользователь с данным Email уже зарегистрирован");

            if (await _userRepository.Any(p => p.Login == model.Login))
                return StatusCode(400, "Выбранный логин уже занят");

            var user = model.ToDbModel();
            if (!await _userRepository.Add(user))
                return StatusCode(500, "При регистрации произошла ошибка. Сообщите нам о ней");

            return Ok(await Authorize(model.Email, model.Password));
        }

        /// <summary>
        /// Обновление информации об учетной записи
        /// </summary>
        /// <param name="model">Обновленная информация о пользователе</param>
        /// <returns></returns>
        [HttpPut("")]
        public async Task<IActionResult> UpdateUser(UpdateUserView model)
        {
            try
            {
                //Поиск текущего пользователя
                var user = await CurrentUser();
                if (user == null)
                    throw new Exception("Не найден пользователь");

                //Проверка пароля
                var heshOldPassword = MD5Helper.GetMD5Hash(model.OldPassword);
                if (user.PasswordHash != heshOldPassword)
                    return StatusCode(401, "Не правильный пароль");

                if (!model.Validate(out string errors))
                    return StatusCode(400, errors);

                //Обновление пароля пользователя
                model.UpdateDbModel(user);
                await _userRepository.Update(user);

                var token = await Authorize(user.Email, model.NewPassword);
                if (token == null)
                    throw new Exception("Не удалось получить новый токен");
                //Выдача нового токена доступа
                return Ok(await Authorize(user.Email, model.NewPassword));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Не удалось обновить учетную запись. СОобщите нам об этой ошибке.");
            }
        }


        #region Вспомогательные методы

        /// <summary>
        /// Выдача токена по Email и паролю
        /// </summary>
        /// <param name="username">Email/Login/номер телефона</param>
        /// <param name="password">Пароль</param>
        /// <returns>Токен</returns>
        private async Task<Token> Authorize(string username, string password)
        {
            var identity = await GetIdentity(username, password);
            if (identity == null)
                return null;


            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new Token(encodedJwt, identity.Name);
        }

        /// <summary>
        /// Аутендификация пользователя
        /// </summary>
        /// <param name="username">Логин/Email/Телефон</param>
        /// <param name="password">Пароль</param>
        /// <returns></returns>
        private async Task<ClaimsIdentity> GetIdentity(string username, string password)
        {
            var paswordHash = MD5Helper.GetMD5Hash(password);
            User user = await _userRepository.FirstOrDefault(x => (x.Email == username || x.Login == username || x.Phone == username)
                && x.PasswordHash == paswordHash);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim("Email", user.Email),
                    new Claim("Id", user.Id.ToString()),
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }

        #endregion

    }
}