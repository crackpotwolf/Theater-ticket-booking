using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MS_Lab_2.Models.db;
using Theater_ticket_booking.Models;

namespace Theater_ticket_booking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        TheaterContext _db;

        public UserController(TheaterContext context)
        {
            _db = context;
        }

        /// <summary>
        /// Аутентификация пользователя
        /// </summary>
        /// <returns></returns>
        /// <response code="400">При выполнении произошла ошибка</response> 
        [HttpPost("Authentication")]
        [DisableRequestSizeLimit]
        [Produces("application/json")]
        public IActionResult Authentication(string username, string password) 
        {
            try
            {
                var identity = GetIdentity(username, password);
                if (identity == null)
                {
                    return BadRequest(new { errorText = "Invalid username or password." });
                }

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

                return Ok(new
                {
                    access_token = encodedJwt,
                    username = identity.Name
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private ClaimsIdentity GetIdentity(string username, string password)
        {
            var paswordHash = GetMD5Hash(password);
            User user = _db.Users.FirstOrDefault(x => x.Email == username);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }

        private string GetMD5Hash(string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            return Encoding.UTF8.GetString(new MD5CryptoServiceProvider().ComputeHash(bytes));
        }
    }
}
