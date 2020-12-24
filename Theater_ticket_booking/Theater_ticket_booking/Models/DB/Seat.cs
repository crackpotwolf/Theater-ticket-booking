using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Theater_ticket_booking.Models.DB
{
    /// <summary>
    /// Место в зале на спектакле
    /// </summary>
    public class Seat : BaseEntity
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Номер мероприятия
        /// </summary>     
        [ForeignKey("Event")]
        public int EventId { get; set; }
        
        /// <summary>
        /// Номер ряда
        /// </summary>
        public string Row { get; set; }
        
        /// <summary>
        /// Номер места
        /// </summary>
        public string Place { get; set; }
        
        /// <summary>
        /// Статус места
        /// </summary>
        public bool Status { get; set; }
        
        /// <summary>
        /// Цена места
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// Наличие брони
        /// </summary>
        [ForeignKey("Order")]
        public int ?OrderId { get; set; }
    }
}
