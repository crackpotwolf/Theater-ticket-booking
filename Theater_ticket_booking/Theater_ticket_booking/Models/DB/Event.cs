using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MS_Lab_2.Models.db
{
    public class Event 
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Performance")]
        [Display(Name = "PerformanceId")]
        public int PerformanceId { get; set; }
        public virtual Performance Performance { get; set; }
        public int DateTime { get; set; }
    }
}
