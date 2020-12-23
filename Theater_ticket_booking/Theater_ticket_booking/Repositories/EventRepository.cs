using System;
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

        public async Task<Performance> GetPerformance(int performanceId)
        {
            try
            {
                return _db.Performances.Where(p => p.Id == performanceId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public virtual async Task<List<string>> GetUniqueRows(int eventId)
        {
            try
            {
                return _db.Seats.Where(p => p.EventId == eventId).Select(m => m.Row).Distinct().ToList();
            }
            catch (Exception ex)
            {
                return new List<string>();
            }
        }

        public virtual async Task<int> GetSeat(int seatId)
        {
            try
            {
                return _db.Seats.Where(p => p.Id == seatId).Select(p => p.Price).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public virtual async Task<List<Seat>> GetSeatsByRow(string row, int eventId)
        {
            try
            {
                return _db.Seats.Where(p => p.Row == row && p.Status == true && p.EventId == eventId).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
