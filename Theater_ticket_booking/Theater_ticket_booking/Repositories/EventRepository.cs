﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Theater_ticket_booking.Models;
using Theater_ticket_booking.Models.DB;

namespace Theater_ticket_booking.Repositories
{
    public class EventRepository : BaseRepository<Event>
    {
        public EventRepository(TheaterContext db) : base(db)
        {
        }
    }
}
