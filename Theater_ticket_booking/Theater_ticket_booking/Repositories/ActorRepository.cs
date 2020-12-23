using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Theater_ticket_booking.Models;
using Theater_ticket_booking.Models.DB;

namespace Theater_ticket_booking.Repositories
{
    public class ActorRepository : BaseRepository<Actor>
    {
        public ActorRepository(TheaterContext db) : base(db)
        {
        }


        public async Task<string> GetListActors(int performanceId)
        {
            try
            {
                List<ActorPerformance> actorPerformances = _db.ActorPerformances.Where(p => p.PerformanceId == performanceId).ToList();
                string actors = "";

                foreach (var it in actorPerformances)
                    actors += _db.Actors.Where(p => p.Id == it.ActorId).FirstOrDefault().Name + ", ";
                actors = actors.Substring(0, actors.Length - 2);

                return actors;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}