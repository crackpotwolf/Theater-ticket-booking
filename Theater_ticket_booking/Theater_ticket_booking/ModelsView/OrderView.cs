using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Theater_ticket_booking.ModelsView
{
    public class OrderView
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
        /// Номер ряда
        /// </summary>
        public string Row { get; set; }

        /// <summary>
        /// Номер места
        /// </summary>
        public string Place { get; set; }

        /// <summary>
        /// Двта начала мероприятия
        /// </summary>
        public string DateTime { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public int Price { get; set; }
    }
}
