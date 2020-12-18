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
    public class EventApiController : _BaseApiController
    {
        TheaterContext _db;

        public EventApiController(UsersRepository userRepository,
            TheaterContext context)
            : base(userRepository)
        {
            _db = context;
        }


        /// <summary>
        /// Получение полной информации для определенного мероприятия
        /// </summary>
        /// <param name="eventId"></param> Id мероприятия
        /// <returns></returns> Возвращает информацию о мероприятии
        [HttpGet("GetEvent")]
        [DisableRequestSizeLimit]
        [Produces("application/json")]
        [ProducesResponseType(typeof(EventsView), 200)]
        [ProducesResponseType(typeof(Exception), 400)]
        public IActionResult GetEvent(int eventId)
        {
            try
            {
                var event_view = new EventsView();

                // получение мероприятия
                var events = _db.Events.Where(p => p.Id == eventId).FirstOrDefault();
                if (events == null)
                    return StatusCode(404, "Не удалось найди мероприятие");

                // получение спектакля
                Performance performance = _db.Performances.Where(p => p.Id == events.PerformanceId).FirstOrDefault();

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

                event_view = new EventsView
                {
                    EventId = events.Id,
                    Name = performance.Name,
                    Description = performance.Description,
                    Actors = actors,
                    Producers = producers,
                    PerformanceId = performance.Id,
                    DateTime = events.Date + ", " + events.Time,
                    Rows = _db.Seats.Where(p => p.EventId == eventId).Select(m => m.Row).Distinct().ToList()
                };

                return Ok(event_view ?? new EventsView());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Не удалось получить список мероприятий. Сообщите нам об это ошибке.");
            }
        }

        /// <summary>
        /// Получение всех мероприятий для текущей даты
        /// </summary>
        /// <param name="date"></param> Дата
        /// <returns></returns> Возвращает все мероприятия
        [HttpGet("GetAllEvents")]
        [DisableRequestSizeLimit]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<ShotEventView>), 200)]
        [ProducesResponseType(typeof(Exception), 400)]
        public IActionResult GetEvents(string date)
        {
            try
            {
                var event_view = new List<ShotEventView>();

                // получение мероприятий для данной даты
                var events = _db.Events.Where(p => p.Date == date).ToList();
                if (events == null)
                    return StatusCode(404, "Не удалось найди мероприятия");

                // для каждого мериприятия
                foreach (var item in events)
                {
                    // Если это новый спектакль, то добавить его
                    if (event_view.Where(p => p.PerformanceId == item.PerformanceId).FirstOrDefault() == null)
                    {
                        // получение спектакля
                        Performance performance = _db.Performances.Where(p => p.Id == item.PerformanceId).FirstOrDefault();

                        event_view.Add(new ShotEventView()
                        {
                            Name = performance.Name,
                            MiniDescription = performance.MiniDescription,
                            Photo = performance.Photo,
                            PerformanceId = performance.Id,
                            Event = new List<Event> { item }
                        });
                    }
                    // иначе доавить время к уже существующему
                    else
                    {
                        var indexevent = event_view.FindIndex(p => p.PerformanceId == item.PerformanceId);
                        event_view[indexevent].Event.Add(item);
                    }
                }

                return Ok(event_view ?? new List<ShotEventView>());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Не удалось получить список мероприятий. Сообщите нам об это ошибке.");
            }
        }
    }
}
