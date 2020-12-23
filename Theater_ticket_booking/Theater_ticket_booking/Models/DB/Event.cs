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
    public class Event : BaseEntity
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
        public int PerformanceId { get; set; }
        
        /// <summary>
        /// Двта начала мероприятия
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// Время начала мероприятия
        /// </summary>
        public string Time { get; set; } 
    }
}
