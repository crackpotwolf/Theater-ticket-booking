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
using Theater_ticket_booking.Repositories;

namespace Theater_ticket_booking.Controllers
{
    [Authorize]
    public class EventController : BaseController
    {
        private readonly EventRepository _eventRepository;
        private readonly ProducerRepository _producerRepository;
        private readonly ActorRepository _actorRepository;

        public EventController(EventRepository eventRepository,
            UsersRepository userRepository, ProducerRepository producerRepository,
            ActorRepository actorRepository) : base(userRepository)
        {
            _eventRepository = eventRepository;
            _producerRepository = producerRepository;
            _actorRepository = actorRepository;
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
        public async Task<Dictionary<int, string>> GetSeats(string row, int eventId)
        {
            var seats = await _eventRepository.GetSeatsByRow(row, eventId);

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
        public async Task<string> GetSum(int[] sumSeats)
        {
            int result = 0;
            foreach (var item in sumSeats)
                result += await _eventRepository.GetSeat(item);

            return result.ToString();
        }

        /// <summary>
        /// Получение полной информации для определенного мероприятия
        /// </summary>
        /// <param name="eventId"></param> - id мероприятия
        /// <returns></returns>
        public async Task<IActionResult> GetEvent(int eventId)
        {
            var event_view = new EventView();
            ViewBag.Rows = await _eventRepository.GetUniqueRows(eventId);

            try
            {
                // получение мероприятия
                var events = _eventRepository.GetList().Where(p => p.Id == eventId).FirstOrDefault();
                // получение спектакля
                Performance performance = await _eventRepository.GetPerformance(events.PerformanceId);

                // получения списка актеров
                string actors = await _actorRepository.GetListActors(performance.Id);

                // получения списка режиссеров
                string producers = await _producerRepository.GetListProducers(performance.Id);

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
        public async Task<List<ShotEventView>> GetEvents(string date) 
        {
            var event_view = new List<ShotEventView>();

            try
            {
                // получение мероприятий для данной даты
                var events = await _eventRepository.GetList().Where(p => p.Date == date).ToListAsync();

                // для каждого мериприятия
                foreach (var item in events)
                {
                    // Если это новый спектакль, то добавить его
                    if (event_view.Where(p => p.PerformanceId == item.PerformanceId).FirstOrDefault() == null) 
                    {
                        // получение спектакля
                        Performance performance = await _eventRepository.GetPerformance(item.PerformanceId);

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
