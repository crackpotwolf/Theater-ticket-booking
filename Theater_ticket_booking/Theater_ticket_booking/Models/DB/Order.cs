﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Theater_ticket_booking.Models.DB
{
    /// <summary>
    /// Заказ
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Номер клиента
        /// </summary>
        [ForeignKey("User")]
        public int UserId { get; set; }

        /// <summary>
        /// Номер мероприятия
        /// </summary>
        [ForeignKey("Performance")]
        public int PerformanceId { get; set; }
        
        /// <summary>
        /// Цена
        /// </summary>
        public int Price { get; set; }
    }
}
