using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Theater_ticket_booking.ModelsView.UserModels;
using Theater_ticket_booking.Repositories;

namespace Theater_ticket_booking.Controllers.API
{

    [Route("api/Users")]
    [ApiController]
    [Authorize]
    public class UsersApi : _BaseApi
    {
        public UsersApi(UsersRepository userRepository) : base(userRepository)
        {
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> UserInfo(int id)
        {
            var user = await _userRepository.GetList()
                .FirstOrDefaultAsync(p => p.Id == id);
            if (user == null)
                return StatusCode(404, "Пользователь не найден");

            return Ok(new UserView(user));
        }
    }
}
