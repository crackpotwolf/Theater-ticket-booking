using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Theater_ticket_booking.Models.DB;

namespace Theater_ticket_booking.Models
{
    public class InitDB
    {
        public static void Initialize(TheaterContext context)
        {
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User
                    {
                        Login = "Test",
                        Password = "Test",
                        FirstName = "Test",
                        LastName = "Test",
                        Phone = "89999999999",
                        Email = "test@test.ru"
                    }
                );
                context.SaveChanges();
            }

            if (!context.Performances.Any())
            {
                context.Performances.AddRange(
                    new Performance
                    {
                        Id = 1,
                        Name = "Мастер и Маргарита",
                        Description = "Спектаклю «Мастер и Маргарита» уже больше 10 лет. И он давно заслужил репутацию самого популярного спектакля в странах СНГ и зарубежья, побившего все известные рекорды зрительского спроса и кассовых сборов.",
                        MiniDescription = "«Мастер и Маргарита» — подлинный шедевр искусства театра двадцать первого века.",
                        Photo = "https://sun9-56.userapi.com/impg/cHPDwO7FDtki4BZ6_vAD1fFZgewxKd7BMjCoWw/O2QokcLexM0.jpg?size=833x500&quality=96&proxy=1&sign=37461966fa37122559137c286269c4cb&type=album"
                    }
                );
                context.SaveChanges();
            }

            if (!context.Producers.Any())
            {
                context.Producers.AddRange(
                    new Producer
                    {
                        Id = 1,
                        Name = "Валерий Белякович"
                    }
                );
                context.SaveChanges();
            }

            if (!context.ProducerPerformances.Any())
            {
                context.ProducerPerformances.AddRange(
                    new ProducerPerformance
                    {
                        Id = 1,
                        PerformanceId = 1,
                        ProducerId = 1,
                    }
                );
                context.SaveChanges();
            }

            if (!context.Actors.Any())
            {
                context.Actors.AddRange(
                    new Actor
                    {
                        Id = 1,
                        Name = "Владимир Филатов"
                    }
                );

                context.Actors.AddRange(
                    new Actor
                    {
                        Id = 2,
                        Name = "Михаил Гудошников"
                    }
                );

                context.SaveChanges();
            }

            if (!context.ActorPerformances.Any())
            {
                context.ActorPerformances.AddRange(
                    new ActorPerformance
                    {
                        Id = 1,
                        PerformanceId = 1,
                        ActorId = 1
                    }
                );

                context.ActorPerformances.AddRange(
                    new ActorPerformance
                    {
                        Id = 2,
                        PerformanceId = 1,
                        ActorId = 2
                    }
                );

                context.SaveChanges();
            }

            if (!context.Events.Any())
            {
                context.Events.AddRange(
                    new Event
                    {
                        Id = 1,
                        PerformanceId = 1,
                        Date = "17.12.2020",
                        Time = "18:00",
                    }
                );

                context.Events.AddRange(
                    new Event
                    {
                        Id = 2,
                        PerformanceId = 1,
                        Date = "17.12.2020",
                        Time = "21:30",
                    }
                );

                context.SaveChanges();
            }

            if (!context.Seats.Any())
            {
                context.Seats.AddRange(
                    new Seat
                    {
                        EventId = 1,
                        Row = "1",
                        Place = "1",
                        Status = true,
                        Price = 100,
                    }
                );

                context.Seats.AddRange(
                    new Seat
                    {
                        EventId = 1,
                        Row = "1",
                        Place = "2",
                        Status = true,
                        Price = 100,
                    }
                );

                context.Seats.AddRange(
                    new Seat
                    {
                        EventId = 1,
                        Row = "1",
                        Place = "3",
                        Status = true,
                        Price = 100,
                    }
                );

                context.Seats.AddRange(
                    new Seat
                    {
                        EventId = 1,
                        Row = "1",
                        Place = "4",
                        Status = true,
                        Price = 100,
                    }
                );

                context.Seats.AddRange(
                    new Seat
                    {
                        Id = 6,
                        EventId = 1,
                        Row = "1",
                        Place = "5",
                        Status = true,
                        Price = 100,
                    }
                );

                context.Seats.AddRange(
                    new Seat
                    {
                        EventId = 1,
                        Row = "2",
                        Place = "1",
                        Status = true,
                        Price = 100,
                    }
                );

                context.Seats.AddRange(
                    new Seat
                    {
                        EventId = 1,
                        Row = "2",
                        Place = "2",
                        Status = true,
                        Price = 100,
                    }
                );

                context.Seats.AddRange(
                    new Seat
                    {
                        EventId = 1,
                        Row = "2",
                        Place = "3",
                        Status = true,
                        Price = 100,
                    }
                );

                context.Seats.AddRange(
                    new Seat
                    {
                        EventId = 1,
                        Row = "2",
                        Place = "4",
                        Status = true,
                        Price = 100,
                    }
                );

                context.Seats.AddRange(
                    new Seat
                    {
                        EventId = 1,
                        Row = "2",
                        Place = "5",
                        Status = true,
                        Price = 100,
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
