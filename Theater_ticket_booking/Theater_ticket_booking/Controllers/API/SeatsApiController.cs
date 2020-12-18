using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Theater_ticket_booking.Models;
using Theater_ticket_booking.Models.DB;
using Theater_ticket_booking.Repositories;

namespace Theater_ticket_booking.Controllers.API
{
    [Route("api/")]
    [ApiController]
    [Authorize]
    public class SeatsApiController : _BaseApiController
    {
        TheaterContext _db;

        public SeatsApiController(UsersRepository userRepository, 
            TheaterContext context)
            : base(userRepository)
        {
            _db = context;
        }

        /// <summary>
        /// Нахождение списка свободных мест для определенного мероприятия
        /// </summary>
        /// <param name="row"></param> - номер ряда
        /// <param name="eventId"></param> - id события
        /// <returns></returns>
        [HttpGet("GetSeats")]
        [DisableRequestSizeLimit]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Seat), 200)]
        [ProducesResponseType(typeof(Exception), 400)]
        public IActionResult GetSeats(string row, int eventId)
        {
            try
            {
                return Ok(_db.Seats.Where(p => p.Row == row && p.Status == true && p.EventId == eventId).ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Не удалось получить список мест. Сообщите нам об это ошибке.");
            }
        }

        /// <summary>
        /// Нахождение списка свободных мест для определенного мероприятия вместе с ценой
        /// </summary>
        /// <param name="row"></param> - номер ряда
        /// <param name="eventId"></param> - id события
        /// <returns></returns>
        [HttpGet("GetSeatsWithSum")]
        [DisableRequestSizeLimit]
        [Produces("application/json")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(Exception), 400)]
        public IActionResult GetSeatsWithSum(string row, int eventId)
        {
            try
            {
                var seats = _db.Seats.Where(p => p.Row == row && p.Status == true && p.EventId == eventId).ToList();
                if (seats == null)
                    return StatusCode(404, "Не удалось найди места");

                Dictionary<int, string> result = new Dictionary<int, string>();
                foreach (var item in seats)
                    result.Add(item.Id, item.Place + " (" + item.Price.ToString() + " р.)");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Не удалось получить список мест. Сообщите нам об это ошибке.");
            }
        }

        /// <summary>
        /// Расчет суммы денег для выбранных мест
        /// </summary>
        /// <param name="sumSeats"></param> - id мест
        /// <returns></returns>
        [HttpGet("GetSumSeats")]
        [DisableRequestSizeLimit]
        [Produces("application/json")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(Exception), 400)]
        public IActionResult GetSumSeats(int[] sumSeats)
        {
            try
            {
                int result = 0;
                foreach (var item in sumSeats)
                    result += _db.Seats.Where(p => p.Id == item).Select(p => p.Price).FirstOrDefault();

                return Ok(result.ToString());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Не удалось получить сумму денег. Сообщите нам об это ошибке.");
            }
        }
    }
}
