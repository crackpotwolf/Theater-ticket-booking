using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Theater_ticket_booking.Models;
using Theater_ticket_booking.Models.DB;

namespace Theater_ticket_booking.Repositories
{
    public class PerformanceRepository : BaseRepository<Performance>
    {
        public PerformanceRepository(TheaterContext db) : base(db)
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
    }
}
