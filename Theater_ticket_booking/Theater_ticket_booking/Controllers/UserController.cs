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
using Theater_ticket_booking.Models.DB;
using Theater_ticket_booking.Models;
using Theater_ticket_booking.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Theater_ticket_booking.Controllers
{
    public class UserController : BaseController
    {
        public UserController(UsersRepository userRepository) : base(userRepository)
        {
        }

        [HttpGet("")]
        public async Task<IActionResult> Index(int id)
        {
            try
            {
                var user = await _userRepository.GetList()
                    .FirstOrDefaultAsync(p => p.Id == id);
                return View(user);
            }
            catch (Exception)
            {

                throw;
            }           
        }
    }
}
