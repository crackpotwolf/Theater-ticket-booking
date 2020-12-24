using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Theater_ticket_booking.Models.DB
{
    /// <summary>
    /// Спектакль
    /// </summary>
    public class Performance : BaseEntity
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
        /// Описание спектакля
        /// </summary>
        public string MiniDescription { get; set; } 

        /// <summary>
        /// Фото спектакля
        /// </summary>
        public string Photo { get; set; }
    }    
}