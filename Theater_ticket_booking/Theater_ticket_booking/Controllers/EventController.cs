using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Theater_ticket_booking.Models;
using Theater_ticket_booking.Models.DB;
using Theater_ticket_booking.ModelsView;

namespace Theater_ticket_booking.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        TheaterContext _db;

        public EventController(TheaterContext context)
        {
            _db = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public virtual IActionResult EventView()
        {
            return PartialView("_EventPartial");
        }

        /// <summary>
        /// Нахождение списка мест для определенного мероприятия
        /// </summary>
        /// <param name="row"></param> - номер ряда
        /// <param name="eventId"></param> - id события
        /// <returns></returns>
        [HttpGet]
        public Dictionary<int, string> GetSeats(string row, int eventId)
        {
            var seats = _db.Seats.Where(p => p.Row == row && p.Status == true && p.EventId == eventId).ToList();

            Dictionary<int, string> result = new Dictionary<int, string> ();
            foreach (var item in seats)
                result.Add(item.Id, item.Place + " (" + item.Price.ToString() + " р.)");

            return result;
        }

        /// <summary>
        /// Расчет суммы денег для выбранных мест
        /// </summary>
        /// <param name="sumSeats"></param> - id мест
        /// <returns></returns>
        [HttpGet]
        public string GetSum(int[] sumSeats)
        {
            int result = 0;
            foreach (var item in sumSeats)
                result += _db.Seats.Where(p => p.Id == item).Select(p => p.Price).FirstOrDefault();

            return result.ToString();
        }

        /// <summary>
        /// Получение полной информации для определенного мероприятия
        /// </summary>
        /// <param name="eventId"></param> - id мероприятия
        /// <returns></returns>
        [HttpGet]
        public virtual IActionResult GetEvent(int eventId)
        {
            var event_view = new EventView();
            ViewBag.Seats = new List<string>();

            ViewBag.Seats = _db.Seats.Select(m => m.Row).Distinct().ToList();

            try
            {
                // получение мероприятия
                var events = _db.Events.Where(p => p.Id == eventId).FirstOrDefault();
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

                event_view = new EventView
                {
                    EventId = events.Id,
                    Name = performance.Name,
                    Description = performance.Description,
                    Actors = actors,
                    Producers = producers,
                    PerformanceId = performance.Id,
                    DateTime = events.Date + ", " + events.Time
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            return PartialView("_OrderPartial", event_view);
        }

        /// <summary>
        /// Получение всех мероприятий для текущей даты
        /// </summary>
        /// <param name="date"></param> - дата
        /// <returns></returns>
        [HttpGet]
        public List<ShotEventView> GetEvents(string date) 
        {
            var event_view = new List<ShotEventView>();

            try
            {
                // получение мероприятий для данной даты
                var events = _db.Events.Where(p => p.Date == date).ToList();

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
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            return event_view;
        }
    }
}
