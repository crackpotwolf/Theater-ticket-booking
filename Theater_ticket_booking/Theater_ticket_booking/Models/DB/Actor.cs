using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Theater_ticket_booking.Models.DB
{
    /// <summary>
    /// Актер
    /// </summary>
    public class Actor
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Имя актера
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// Актер для данного мероприятия
    /// </summary>
    public class ActorPerformance
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
        /// Актер
        /// </summary>     
        [ForeignKey("Actor")]
        [Display(Name = "ActorId")]
        public int ActorId { get; set; }
        public virtual Actor Actor { get; set; }
    }
}
