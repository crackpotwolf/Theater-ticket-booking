using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Theater_ticket_booking.Models.DB;

namespace Theater_ticket_booking.Models
{
    public class TheaterContext : DbContext
    {
        public virtual DbSet<Performance> Performances { get; set; }
        public virtual DbSet<Producer> Producers { get; set; }
        public virtual DbSet<ProducerPerformance> ProducerPerformances { get; set; }
        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<ActorPerformance> ActorPerformances { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Seat> Seats { get; set; }
        public virtual DbSet<Event> Events { get; set; }  
        public virtual DbSet<Order> Orders { get; set; }

        public TheaterContext(DbContextOptions<TheaterContext> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
    }
}
