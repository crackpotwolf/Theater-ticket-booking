using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Theater_ticket_booking.Models.DB
{
    /// <summary>
    /// Режиссер
    /// </summary>
    public class Producer : BaseEntity
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// Режиссер для мероприятия 
    /// </summary>
    public class ProducerPerformance
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Номер спектакля
        /// </summary>
        public int PerformanceId { get; set; }

        /// <summary>
        /// Режиссер
        /// </summary>     
        [ForeignKey("Producer")]
        public int ProducerId { get; set; }
    }
}
