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

        public void CancelOrder(int orderid)
        {
            var orders = _db.Orders.Where(p => p.Id == orderid).FirstOrDefault();
            var seats = _db.Seats.Where(p => p.OrderId == orders.Id).ToList();

            foreach (var item in seats)
            {
                Seat seat = item;
                seat.Status = true;
                seat.OrderId = null;

                _db.Update(seat);
            }

            _db.SaveChanges();

            _db.Remove(orders);

            _db.SaveChanges();
        }

        public List<OrderView> GetOrders() 
        {
            List<OrderView> orderView = new List<OrderView>();

            var UserId = CurrentUserId();
            var orders = _db.Orders.Where(p => p.UserId == UserId).ToList();

            foreach (var item in orders)
            {
                OrderView temp = new OrderView();
                var seats = _db.Seats.Where(p => p.OrderId == item.Id).ToList();

                string seat = "";
                foreach (var it in seats)
                    seat += it.Place + ", ";
                seat = seat.Substring(0, seat.Length - 2);

                var events = _db.Events.Where(p => p.Id == seats.FirstOrDefault().EventId).FirstOrDefault();
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

        public void BookSeats(int performanceId, int[] sumSeats) 
        {
            int result = 0;
            foreach (var item in sumSeats)
                result += _db.Seats.Where(p => p.Id == item).Select(p => p.Price).FirstOrDefault();

            Order order = new Order
            {
                UserId = CurrentUserId(),
                PerformanceId = performanceId,
                Price = result
            };

            _db.Add(order);
            _db.SaveChanges();

            var lastorder = _db.Orders.ToList().LastOrDefault();

            foreach (var item in sumSeats) {

                Seat seats = _db.Seats.Where(p => p.Id == item).FirstOrDefault();
                seats.Status = false;
                seats.OrderId = lastorder.Id;

                _db.Update(order);
            }

            _db.SaveChanges();
        }


        protected int CurrentUserId()
        {
            var val = User?.Claims?.FirstOrDefault(p => p.Type == "Id")?.Value;
            if (val == null)
                return -1;
            return int.Parse(val);
        }
    }
}
