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
    public class OrderController : BaseController
    {
        private readonly OrderRepository _orderRepository;
        private readonly EventRepository _eventRepository;
        private readonly SeatRepository _seatRepository;
        private readonly PerformanceRepository _performanceRepository;

        public OrderController(EventRepository eventRepository,
            UsersRepository userRepository, OrderRepository orderRepository,
            SeatRepository seatRepository,
            PerformanceRepository performanceRepository) : base(userRepository)
        {
            _eventRepository = eventRepository;
            _orderRepository = orderRepository;
            _seatRepository = seatRepository;
            _performanceRepository = performanceRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Отмена брони
        /// </summary>
        /// <param name="orderid"></param> - номер брони
        public async Task CancelOrder(int orderid)
        {
            // получение заказа
            var orders = await _orderRepository.Find(orderid);
            // получение мест данного заказа
            var seats = await _seatRepository.GetList().Where(p => p.OrderId == orders.Id).ToListAsync();

            // для каждого места отменить бронь
            foreach (var item in seats)
            {
                Seat seat = item;
                seat.Status = true;
                seat.OrderId = null;

                await _seatRepository.Update(seat);
            }

            // удалить бронь
            await _orderRepository.Remove(orders);
        }

        /// <summary>
        /// Получение списка броней для данного клиента
        /// </summary>
        /// <returns></returns>
        public async Task<List<OrderView>> GetOrders() 
        {
            List<OrderView> orderView = new List<OrderView>();

            // получение текущего пользователя
            var UserId = CurrentUserId();
            //Получения списка броней
            var orders = await _orderRepository.GetList().Where(p => p.UserId == UserId).ToListAsync();

            // для каждой брони
            foreach (var item in orders)
            {
                OrderView temp = new OrderView();

                // получить список мест
                var seats = await _seatRepository.GetList().Where(p => p.OrderId == item.Id).ToListAsync();

                string seat = "";
                foreach (var it in seats)
                    seat += it.Place + ", ";
                seat = seat.Substring(0, seat.Length - 2);

                // получение мероприятия
                var events = await _eventRepository.Find(seats.FirstOrDefault().EventId);
                // получение спектакля
                var perfomance = await _performanceRepository.Find(events.PerformanceId);

                temp.Id = item.Id;
                temp.Name = perfomance.Name;
                temp.DateTime = events.Date + ", " + events.Time;
                temp.Place = seat;
                temp.Row = seats.FirstOrDefault().Row;
                temp.Price = item.Price;

                orderView.Add(temp);
            }

            return orderView;
        }

        /// <summary>
        /// Оформление брони
        /// </summary>
        /// <param name="eventId"></param> - id мероприятия
        /// <param name="sumSeats"></param> - id выбранных мест
        public async Task BookSeats(int eventId, int[] sumSeats) 
        {
            // расчет общей стоимости брони
            int result = 0;
            foreach (var item in sumSeats)
                result += _seatRepository.GetList().Where(p => p.Id == item).FirstOrDefault().Price;

            // добавление брони
            Order order = new Order
            {
                UserId = CurrentUserId(),
                EventId = eventId,
                Price = result
            };

            await _orderRepository.Add(order);

            // смена статуса мест для текущей брони
            var lastorder = _orderRepository.GetList().ToListAsync().Result.LastOrDefault();

            foreach (var item in sumSeats) {

                Seat seats = await _seatRepository.FirstOrDefault(p => p.Id == item);
                seats.Status = false;
                seats.OrderId = lastorder.Id;

                await _seatRepository.Update(seats);
            }
        }

        /// <summary>
        /// Получение текущего пользователя
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
