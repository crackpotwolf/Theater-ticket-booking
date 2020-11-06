using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MS_Lab_2.Models.db
{
    public class Seat
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Program")]
        [Display(Name = "ProgramId")]
        public int ProgramId { get; set; }
        public virtual Event Event { get; set; } 
        public string Row { get; set; }
        public string Place { get; set; }
        public bool Status { get; set; }
        public int Price { get; set; } 
    }
}
