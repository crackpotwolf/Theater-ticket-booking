using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Theater_ticket_booking.Models.DB
{
    /// <summary>
    /// Фото
    /// </summary>
    public class Photo
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Путь к фото
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// Фото для данного спектакля
    /// </summary>
    public class PhotoPerformance
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Key]
        public int Id { get; set; }
        public int PerformanceId { get; set; }


        /// <summary>
        /// Номер фото
        /// </summary>
        [ForeignKey("Photo")]
        [Display(Name = "PhotoId")]
        public int PhotoId { get; set; }
        public virtual Photo Photo { get; set; }
    }
}
