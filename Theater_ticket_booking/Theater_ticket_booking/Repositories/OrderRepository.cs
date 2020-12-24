using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Theater_ticket_booking.Models;
using Theater_ticket_booking.Models.DB;

namespace Theater_ticket_booking.Repositories
{
    public class OrderRepository : BaseRepository<Order>
    {
        public OrderRepository(TheaterContext db) : base(db)
        {
        }
    }
}
