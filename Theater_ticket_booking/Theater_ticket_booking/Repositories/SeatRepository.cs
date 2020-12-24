using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Theater_ticket_booking.Models;
using Theater_ticket_booking.Models.DB;

namespace Theater_ticket_booking.Repositories
{
    public class SeatRepository : BaseRepository<Seat>
    {
        public SeatRepository(TheaterContext db) : base(db) 
        {
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
