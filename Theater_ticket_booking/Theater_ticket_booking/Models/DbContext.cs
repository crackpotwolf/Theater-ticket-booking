using Microsoft.EntityFrameworkCore;
using MS_Lab_2.Models.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Theater_ticket_booking.Models
{
    public class TheaterContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Seat> Seats { get; set; }
        public virtual DbSet<Program> Programs { get; set; } 
        public virtual DbSet<Performance> Performances { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

        public TheaterContext(DbContextOptions<TheaterContext> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
    }
}
