using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Theater_ticket_booking.Models;
using Theater_ticket_booking.Models.DB;
using Theater_ticket_booking.ModelsView;

namespace Theater_ticket_booking.Controllers
{
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


        public Dictionary<int, string> GetSeats(string row)
        {
            var seats = _db.Seats.Where(p => p.Row == row && p.Status == true);

            Dictionary<int, string> result = new Dictionary<int, string> ();
            foreach (var item in seats)
                result.Add(item.Id, item.Place + " (" + item.Price.ToString() + " р)");

            return result;
        }

        public string GetSum(int[] sumSeats)
        {
            int result = 0;
            foreach (var item in sumSeats)
                result += _db.Seats.Where(p => p.Id == item).Select(p => p.Price).FirstOrDefault();

            return result.ToString();
        }

        public virtual IActionResult GetEvent(int data)
        {
            var event_view = new EventView();
            ViewBag.Seats = new List<string>();

            ViewBag.Seats = _db.Seats.Select(m => m.Row).Distinct().ToList();

            try
            {
                var events = _db.Events.Where(p => p.Id == data).FirstOrDefault();
                Performance performance = _db.Performances.Where(p => p.Id == events.PerformanceId).FirstOrDefault();

                List<ActorPerformance> actorPerformances = _db.ActorPerformances.Where(p => p.PerformanceId == performance.Id).ToList();
                string actors = "";

                foreach (var it in actorPerformances)
                    actors += _db.Actors.Where(p => p.Id == it.ActorId).FirstOrDefault().Name + ", ";
                actors = actors.Substring(0, actors.Length - 2);

                List<ProducerPerformance> producerPerformance = _db.ProducerPerformances.Where(p => p.PerformanceId == performance.Id).ToList();
                string producers = "";

                foreach (var it in producerPerformance)
                    producers += _db.Producers.Where(p => p.Id == it.PerformanceId).FirstOrDefault().Name + ", ";
                producers = producers.Substring(0, producers.Length - 2);


                event_view = new EventView
                {
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

        public List<ShotEventView> GetShotEvent(string date)
        {
            var event_view = new List<ShotEventView>();

            try
            {
                var events = _db.Events.Where(p => p.Date == date).ToList();

                foreach (var item in events)
                {

                    if (event_view.Where(p => p.PerformanceId == item.PerformanceId).FirstOrDefault() == null) 
                    {
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
