using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Theater_ticket_booking.Models;
using Theater_ticket_booking.Models.DB;
using Theater_ticket_booking.ModelsView;
using Theater_ticket_booking.Repositories;

namespace Theater_ticket_booking.Controllers.API
{
    [Route("api/")]
    [ApiController]
    [Authorize]
    public class PerformanceApiController : _BaseApiController
    {

        TheaterContext _db;

        public PerformanceApiController(UsersRepository userRepository, 
            TheaterContext context)
            : base(userRepository)
        {
            _db = context;
        }

        /// <summary>
        /// Возвращает список всех спектаклей
        /// </summary>
        [HttpGet("GetAllPerformances")]
        [DisableRequestSizeLimit]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<PerformancesView>), 200)]
        [ProducesResponseType(typeof(Exception), 400)]
        public IActionResult Performances() 
        {
            try
            {
                var performances = _db.Performances.ToList();
                if (performances == null)
                    return StatusCode(404, "Не удалось найди спектакли");

                List<PerformancesView> result = new List<PerformancesView>();

                foreach (var performance in performances)
                {
                    result.Add(new PerformancesView
                    {
                        Id = performance.Id,
                        Name = performance.Name,
                        Description = performance.MiniDescription,
                        Photo = performance.Photo
                    });
                }

                return Ok(result ?? new List<PerformancesView>());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Не удалось получить список спектаклей. Сообщите нам об это ошибке.");
            }
        }

        /// <summary>
        /// Получить детальную информацию о спектакле
        /// </summary>
        /// <param name="performanceId"></param> - id спектакля
        /// <returns></returns>
        [HttpGet("GetPerformance")]
        [DisableRequestSizeLimit]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PerformanceView), 200)]
        [ProducesResponseType(typeof(Exception), 400)]
        public IActionResult Performance(int performanceId)   
        {
            try
            {
                // получить спектакль
                var performance = _db.Performances.Where(p => p.Id == performanceId).FirstOrDefault();
                if (performance == null)
                    return StatusCode(404, "Не удалось найди спектакль с данным идентификатором");

                // получения списка актеров
                List<ActorPerformance> actorPerformances = _db.ActorPerformances.Where(p => p.PerformanceId == performance.Id).ToList();
                string actors = "";

                foreach (var it in actorPerformances)
                    actors += _db.Actors.Where(p => p.Id == it.ActorId).FirstOrDefault().Name + ", ";
                actors = actors.Substring(0, actors.Length - 2);

                // получения списка режиссеров
                List<ProducerPerformance> producerPerformance = _db.ProducerPerformances.Where(p => p.PerformanceId == performance.Id).ToList();
                string producers = "";

                foreach (var it in producerPerformance)
                    producers += _db.Producers.Where(p => p.Id == it.PerformanceId).FirstOrDefault().Name + ", ";
                producers = producers.Substring(0, producers.Length - 2);

                PerformanceView result = new PerformanceView
                {
                    Id = performance.Id,
                    Name = performance.Name,
                    Description = performance.Description,
                    Actors = actors,
                    Producers = producers,
                    Photo = performance.Photo
                };

                return Ok(result ?? new PerformanceView());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Не удалось получить информация о спектакле. Сообщите нам об это ошибке.");
            }
        }
    }
}
