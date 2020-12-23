using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Theater_ticket_booking.Models;
using Theater_ticket_booking.Models.DB;

namespace Theater_ticket_booking.Repositories
{
    public class ProducerRepository : BaseRepository<Producer>
    {
        public ProducerRepository(TheaterContext db) : base(db)
        {
        }


        public async Task<string> GetListProducers(int performanceId) 
        {
            try
            {
                List<ProducerPerformance> producerPerformance = _db.ProducerPerformances.Where(p => p.PerformanceId == performanceId).ToList();
                string producers = "";

                foreach (var it in producerPerformance)
                    producers += _db.Producers.Where(p => p.Id == it.PerformanceId).FirstOrDefault().Name + ", ";
                producers = producers.Substring(0, producers.Length - 2);

                return producers;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
