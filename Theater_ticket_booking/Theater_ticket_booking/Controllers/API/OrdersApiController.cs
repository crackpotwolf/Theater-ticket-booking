using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class OrdersApiController : _BaseApiController
    {
        TheaterContext _db;

        public OrdersApiController(UsersRepository userRepository,
            TheaterContext context)
            : base(userRepository)
        {
            _db = context;
        }

        /// <summary>
        /// Отмена брони
        /// </summary>
        /// <param name="orderid"></param> Номер брони
        [HttpPost("CancelOrder")]
        [DisableRequestSizeLimit]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), 400)]
        public IActionResult CancelOrder(int orderid)
        {
            try
            {
                // получение заказа
                var orders = _db.Orders.Where(p => p.Id == orderid).FirstOrDefault();
                if (orders == null)
                    return StatusCode(404, "Не удалось найди бронь");

                // получение мест данного заказа
                var seats = _db.Seats.Where(p => p.OrderId == orders.Id).ToList();
                if (seats == null)
                    return StatusCode(404, "Не удалось найди места для данной брони");

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

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Не удалось получить список мест. Сообщите нам об это ошибке.");
            }
        }

        /// <summary>
        /// Получение списка броней для данного клиента
        /// </summary>
        /// <returns></returns> Возвращает спискок броней
        [HttpGet("GetOrders")]
        [DisableRequestSizeLimit]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<OrderView>), 200)]
        [ProducesResponseType(typeof(Exception), 400)]
        public IActionResult GetOrders()
        {
            try
            {
                List<OrderView> orderView = new List<OrderView>();

                // получение текущего пользователя
                var UserId = CurrentUserId();
                if (UserId == null)
                    return StatusCode(404, "Не удалось найди пользовтеля");

                //Получения списка броней
                var orders = _db.Orders.Where(p => p.UserId == UserId).ToList();
                if (orders == null)
                    return StatusCode(404, "Не удалось найди брони");

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

                return Ok(orderView ?? new List<OrderView>());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Не удалось получить получить списсок броней. Сообщите нам об это ошибке.");
            }
        }

        /// <summary>
        /// Оформление брони
        /// </summary>
        /// <param name="eventId"></param> Id мероприятия
        /// <param name="sumSeats"></param> Id выбранных мест
        [HttpPost("BookSeats")]
        [DisableRequestSizeLimit]
        [Produces("application/json")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(Exception), 400)]
        public IActionResult BookSeats(int eventId, int[] sumSeats)
        {
            try
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

                foreach (var item in sumSeats)
                {

                    Seat seats = _db.Seats.Where(p => p.Id == item).FirstOrDefault();
                    seats.Status = false;
                    seats.OrderId = lastorder.Id;

                    _db.Update(order);
                }

                _db.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Не удалось оформить бронь. Сообщите нам об это ошибке.");
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
