using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Theater_ticket_booking.Models.DB
{
    /// <summary>
    /// Мероприятие
    /// </summary>
    public class Event 
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Номер спектакля
        /// </summary>
        [ForeignKey("Performance")]
        [Display(Name = "PerformanceId")]
        public int PerformanceId { get; set; }
        public virtual Performance Performance { get; set; }
        
        /// <summary>
        /// Двта и время начала мероприятия
        /// </summary>
        public int DateTime { get; set; }
    }
}
