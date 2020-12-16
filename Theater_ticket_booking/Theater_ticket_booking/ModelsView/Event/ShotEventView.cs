using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Theater_ticket_booking.Models.DB;

namespace Theater_ticket_booking.ModelsView
{
    /// <summary>
    /// Мероприятие
    /// </summary>
    public class ShotEventView 
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Наименования спектакля
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание спектакля
        /// </summary>
        public string MiniDescription { get; set; }

        /// <summary>
        /// Фото спектакля
        /// </summary>zz
        public string Photo { get; set; }

        /// <summary>
        /// Мероприятия
        /// </summary>
        public List<Event> Event { get; set; }

        /// <summary>
        /// Номер мероприятия
        /// </summary>
        [ForeignKey("Performance")]
        public int PerformanceId { get; set; }
    }
}
