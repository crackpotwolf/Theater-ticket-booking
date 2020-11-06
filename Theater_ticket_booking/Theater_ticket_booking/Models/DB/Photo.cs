using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Theater_ticket_booking.Models.DB
{
    public class Photo
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class PhotoPerformance
    {
        [Key]
        public int Id { get; set; }
        public int PerformanceId { get; set; }
        public Photo Photo { get; set; }
    }
}
