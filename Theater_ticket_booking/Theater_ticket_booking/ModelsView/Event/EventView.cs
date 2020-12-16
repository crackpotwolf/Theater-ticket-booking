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
    public class EventView 
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
        public string Description { get; set; }

        /// <summary>
        /// Актеры
        /// </summary>
        public string Actors { get; set; }

        /// <summary>
        /// Режиссеры
        /// </summary>
        public string Producers { get; set; }

        /// <summary>
        /// Дата и время
        /// </summary>
        public string DateTime { get; set; }

        /// <summary>
        /// Номер мероприятия
        /// </summary>
        [ForeignKey("Performance")]
        public int PerformanceId { get; set; }
    }
}
