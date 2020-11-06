using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MS_Lab_2.Models.db
{
    public class Performance
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string miniPhoto { get; set; } 
        public List<string> Actors { get; set; }
        public List<string> Producers { get; set; }
        public List<string> Photo { get; set; }
    }
}
