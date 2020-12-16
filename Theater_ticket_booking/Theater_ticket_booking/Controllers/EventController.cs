using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        public List<EventView> GetEvent(string date)
        {
            var event_view = new List<EventView>(); 

            try
            {
                var events = _db.Events.Where(p => p.Date == date).ToList(); 


                foreach (var item in events)
                {

                    if (event_view.Where(p => p.PerformanceId == item.PerformanceId).FirstOrDefault() == null)
                    {
                        Performance performance = _db.Performances.Where(p => p.Id == item.PerformanceId).FirstOrDefault();

                        List<ActorPerformance> actorPerformances = _db.ActorPerformances.Where(p => p.PerformanceId == performance.Id).ToList();
                        List<Actor> actors = new List<Actor>();

                        foreach (var it in actorPerformances)
                            actors.Add(_db.Actors.Where(p => p.Id == it.ActorId).FirstOrDefault());

                        List<ProducerPerformance> producerPerformance = _db.ProducerPerformances.Where(p => p.PerformanceId == performance.Id).ToList();
                        List<Producer> producers = new List<Producer>();

                        foreach (var it in producerPerformance)
                            producers.Add(_db.Producers.Where(p => p.Id == it.PerformanceId).FirstOrDefault());

                        event_view.Add(new EventView()
                        {
                            Name = performance.Name,
                            Description = performance.Description,
                            MiniDescription = performance.MiniDescription,
                            Actors = actors,
                            Producers = producers,
                            Photo = performance.Photo,
                            Date = new List<string> { item.Date },
                            Time = new List<string> { item.Time }

                        });
                    }
                    else
                    {
                        var indexevent = event_view.FindIndex(p => p.PerformanceId == item.PerformanceId);

                        event_view[indexevent].Date.Add(item.Date);
                        event_view[indexevent].Date.Add(item.Time);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            return event_view;
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
                            Date = new List<string> { item.Date },
                            Time = new List<string> { item.Time }

                        });
                    }
                    else
                    {
                        var indexevent = event_view.FindIndex(p => p.PerformanceId == item.PerformanceId);

                        event_view[indexevent].Date.Add(item.Date);
                        event_view[indexevent].Date.Add(item.Time);
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
