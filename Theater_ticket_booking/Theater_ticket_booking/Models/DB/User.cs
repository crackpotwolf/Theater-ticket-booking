using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MS_Lab_2.Models.db
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Login { get; set; } 
        public string Password { get; set; } 
        public string Name { get; set; }
        public string Phone { get; set; } 
        public string Email { get; set; }
    }
}
