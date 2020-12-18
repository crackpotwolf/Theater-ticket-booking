using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Theater_ticket_booking.ModelsView
{
    public class PerformanceView
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
        /// Фото спектакля
        /// </summary>
        public string Photo { get; set; }

    }
}
