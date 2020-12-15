using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Theater_ticket_booking.Models.DB;
using Theater_ticket_booking.Repositories;

namespace Theater_ticket_booking.Controllers
{
    public class BaseController : Controller
    {
        protected readonly UsersRepository _userRepository;
        public BaseController(UsersRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Возвращает Email текущего пользователя
        /// </summary>
        /// <returns></returns>
        protected string CurrentEmail()
        {
            return User?.Claims?.FirstOrDefault(p => p.Type == "Email")?.Value;
        }

        /// <summary>
        /// Возвращает текущего пользователя
        /// </summary>
        /// <returns></returns>
        protected async Task<User> CurrentUser()
        {
            var emailUser = CurrentEmail();
            if (emailUser == null)
                return null;

            var user = await _userRepository.FirstOrDefault(p => p.Email == emailUser);
            return user;
        }

        /// <summary>
        /// Получение идентификатора пользователя
        /// </summary>
        /// <returns></returns>
        protected int CurrentUserId()
        {
            var val = User?.Claims?.FirstOrDefault(p => p.Type == "Id")?.Value;
            if (val == null)
                return -1;
            return int.Parse(val);
        }
    }
}
