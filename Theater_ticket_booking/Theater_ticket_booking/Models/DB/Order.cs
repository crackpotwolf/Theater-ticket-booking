using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MS_Lab_2.Models.db
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        [Display(Name = "UserId")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public List<Seat> Seats { get; set; } 
    }
}
