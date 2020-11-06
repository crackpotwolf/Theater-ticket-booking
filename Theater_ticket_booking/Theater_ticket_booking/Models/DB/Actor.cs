using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Theater_ticket_booking.Models.DB
{
    public class Actor
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ActorPerformance
    {
        [Key]
        public int Id { get; set; }
        public int PerformanceId { get; set; }
        public Actor Actor { get; set; }
    }
}
