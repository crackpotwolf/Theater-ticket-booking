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
    public class OrderController : Controller 
    {
        TheaterContext _db;

        public OrderController(TheaterContext context)
        {
            _db = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Отмена брони
        /// </summary>
        /// <param name="orderid"></param> - номер брони
        public void CancelOrder(int orderid)
        {
            // получение заказа
            var orders = _db.Orders.Where(p => p.Id == orderid).FirstOrDefault();
            // получение мест данного заказа
            var seats = _db.Seats.Where(p => p.OrderId == orders.Id).ToList();

            // для каждого места отменить бронь
            foreach (var item in seats)
            {
                Seat seat = item;
                seat.Status = true;
                seat.OrderId = null;

                _db.Update(seat);
            }

            // удалить бронь
            _db.Remove(orders);

            _db.SaveChanges();
        }

        /// <summary>
        /// Получение списка броней для данного клиента
        /// </summary>
        /// <returns></returns>
        public List<OrderView> GetOrders() 
        {
            List<OrderView> orderView = new List<OrderView>();

            // получение текущего пользователя
            var UserId = CurrentUserId();
            //Получения списка броней
            var orders = _db.Orders.Where(p => p.UserId == UserId).ToList();

            // для каждой брони
            foreach (var item in orders)
            {
                OrderView temp = new OrderView();

                // получить список мест
                var seats = _db.Seats.Where(p => p.OrderId == item.Id).ToList();

                string seat = "";
                foreach (var it in seats)
                    seat += it.Place + ", ";
                seat = seat.Substring(0, seat.Length - 2);

                // получение мероприятия
                var events = _db.Events.Where(p => p.Id == seats.FirstOrDefault().EventId).FirstOrDefault();
                // получение спектакля
                var perfomance = _db.Performances.Where(p => p.Id == events.PerformanceId).FirstOrDefault();

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
        public void BookSeats(int eventId, int[] sumSeats) 
        {
            // расчет общей стоимости брони
            int result = 0;
            foreach (var item in sumSeats)
                result += _db.Seats.Where(p => p.Id == item).Select(p => p.Price).FirstOrDefault();

            // добавление брони
            Order order = new Order
            {
                UserId = CurrentUserId(),
                EventId = eventId,
                Price = result
            };

            _db.Add(order);
            _db.SaveChanges();

            // смена статуса мест для текущей брони
            var lastorder = _db.Orders.ToList().LastOrDefault();

            foreach (var item in sumSeats) {

                Seat seats = _db.Seats.Where(p => p.Id == item).FirstOrDefault();
                seats.Status = false;
                seats.OrderId = lastorder.Id;

                _db.Update(order);
            }

            _db.SaveChanges();
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
