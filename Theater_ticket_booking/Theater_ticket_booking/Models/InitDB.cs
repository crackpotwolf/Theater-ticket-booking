using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Theater_ticket_booking.Models.DB;

namespace Theater_ticket_booking.Models
{
    /// <summary>
    /// Наполнение базы данных
    /// </summary>
    public class InitDB
    {
        public static void Initialize(TheaterContext context)
        {
            if (!context.Performances.Any())
            {
                context.Performances.AddRange(LoadPerformance());
                context.SaveChanges();
            }

            if (!context.Producers.Any())
            {
                context.Producers.AddRange(LoadProducer());
                context.SaveChanges();
            }

            if (!context.ProducerPerformances.Any())
            {
                context.ProducerPerformances.AddRange(LoadProducerPerformance());
                context.SaveChanges();
            }

            if (!context.Actors.Any())
            {

                context.Actors.AddRange(LoadActor());
                context.SaveChanges();
            }

            if (!context.ActorPerformances.Any())
            {
                context.ActorPerformances.AddRange(LoadActorPerformance());
                context.SaveChanges();
            }

            if (!context.Events.Any())
            {
                context.Events.AddRange(LoadEvent());
                context.SaveChanges();
            }

            if (!context.Seats.Any())
            {
                context.Seats.AddRange(LoadSeat());
                context.SaveChanges();
            }
        }

        public static List<Performance> LoadPerformance()
        {
            var result = new List<Performance>
            {
                 new Performance
                 {
                    Id = 1,
                    Name = "Мастер и Маргарита",
                    Description = "Вообще «Мастер и Маргарита» не особо охотно поддается постановке. Не так то это просто — внести довольно крупное произведение в рамки спектакля. Задачу усложняют тесно-тонкое переплетение сюжетных линий, да и вообще роман сам по себе окутан мистическим ореолом. Главный режиссер театра на «Юго-Западе» Валерий Белякович не побоялся осуществить постановку (нехорошего романа) Михаила Булгакова. Необычно, оригинально, блестяще. Такие эмоции переполняли зрителей, когда на сцене разыгрывалось настоящее мистическое действо. И действительно удачно продуманные режиссером Валерием Беляковичем мизансцены, множество танцев, роскошные костюмы, лазерным шоу, мастерство художника по свету и блестящая игра актеров раздвинули тесную черноту и сделали, как казалось раньше, невозможное: самый знаменитый роман Булгакова наконец-то ожил на сцене.",
                    MiniDescription = "«Мастер и Маргарита» — подлинный шедевр искусства театра двадцать первого века. Драма о любви и предательстве.",
                    Photo = "https://sun9-56.userapi.com/impg/cHPDwO7FDtki4BZ6_vAD1fFZgewxKd7BMjCoWw/O2QokcLexM0.jpg?size=833x500&quality=96&proxy=1&sign=37461966fa37122559137c286269c4cb&type=album"
                 },

                 new Performance
                 {
                    Id = 2,
                    Name = "Юнона и Авось",
                    Description = "«Юнона» и «Авось» – самая известная рок-опера на российской сцене. Авторы – выдающийся русский композитор Алексей Рыбников и поэт Андрей Вознесенский. Постановку рок-оперы «Юнона и Авось» в авторской версии осуществил Государственный театр под руководством Народного артиста РФ, Лауреата Государственной премии РФ, композитора Алексея Львовича Рыбникова.",
                    MiniDescription = "«Юнона» и «Авось» – самая известная рок-опера на российской сцене, в авторской версии Алексея Рыбникова.",
                    Photo = "https://sun9-31.userapi.com/impg/a9qwQ0Ii6-Kbx7X-ptTYd_GYI9qEa_SzA1t_0g/Se_Caw9Jue8.jpg?size=833x500&quality=96&proxy=1&sign=aae7351d145185d0f693f4c16304323c&type=album"
                 },
                 
                 new Performance
                 {
                    Id = 3,
                    Name = "Искуситель",
                    Description = "«Искуситель» - это авантюрная комедия с интригующим сюжетом. За основу спектакля был взят сюжет пьесы Валерия Шашина «Поджигатель», которая была написана им 80-е. В обычную, ничем непримечательную жизнь, молодой супружеской пары вторгается некий третий. Этот острый на язык обманщик не только рушит своим присутствием все привычные устои семьи, но и заставляет в общем-то милых и честных людей действовать так, как прежде им бы и в голову не пришло.",
                    MiniDescription = "«Искуситель» - это авантюрная комедия с интригующим сюжетом, с Ароновой и Спиваковским",
                    Photo = "https://sun9-3.userapi.com/impg/I2P7IOh7PDScBEK9pPP-_Vjmqm1GEYnpB0edOQ/Dv0uaY3797Y.jpg?size=833x500&quality=96&proxy=1&sign=add8f2baded78c3dd97116fd0f35f274&type=album"
                 }
            };

            return result;
        }

        public static List<Producer> LoadProducer()
        {
            var result = new List<Producer>
            {
                new Producer
                {
                    Id = 1,
                    Name = "Валерий Белякович"
                },

                new Producer
                {
                    Id = 2,
                    Name = "Александр Рыхлов"
                },

                new Producer
                {
                    Id = 3,
                    Name = "Александр Горбань"
                }
            };

            return result;
        }

        public static List<ProducerPerformance> LoadProducerPerformance()
        {
            var result = new List<ProducerPerformance>
            {
                new ProducerPerformance
                {
                    Id = 1,
                    PerformanceId = 1,
                    ProducerId = 1,
                },

                new ProducerPerformance
                {
                    Id = 2,
                    PerformanceId = 2,
                    ProducerId = 2,
                },

                new ProducerPerformance
                {
                    Id = 3,
                    PerformanceId = 3,
                    ProducerId = 3,
                }
            };

            return result;
        }

        public static List<Actor> LoadActor()
        {
            var result = new List<Actor>
            {
                NewActor(1, "Владимир Филатов"),
                NewActor(2, "Михаил Гудошников"),
                NewActor(3, "Сергей Поздняков"),
                NewActor(4, "Ивар Калныньш"),
                NewActor(5, "Александр Бреньков"),
                NewActor(6, "Игорь Кирилюк"),
                NewActor(7, "Николай Дроздовский"),
                NewActor(8, "Наталья Крестьянских"),
                NewActor(9, "Екатерина Соловьева"),
                NewActor(10, "Никита Поздняков"),
                NewActor(11, "Даниил Спиваковский"),
                NewActor(12, "Александр Феклистов"),
                NewActor(13, "Мария Аронова")
            };

            return result;
        }

        public static List<ActorPerformance> LoadActorPerformance()
        {
            var result = new List<ActorPerformance>
            {
                NewActorPerformance(1, 1),
                NewActorPerformance(1, 2),
                NewActorPerformance(1, 3),
                NewActorPerformance(1, 4),
                NewActorPerformance(1, 5),
                NewActorPerformance(2, 6),
                NewActorPerformance(2, 7),
                NewActorPerformance(2, 8),
                NewActorPerformance(2, 9),
                NewActorPerformance(2, 10),
                NewActorPerformance(3, 11),
                NewActorPerformance(3, 12),
                NewActorPerformance(3, 13)
            };

            return result;
        }

        public static List<Event> LoadEvent()  
        {
            var result = new List<Event>
            {
                NewEvent(1, 1, DateTime.Now.Date.ToString("dd.MM.yyyy"), "18:00"),
                NewEvent(2, 1, DateTime.Now.Date.ToString("dd.MM.yyyy"), "20:00"),
                NewEvent(3, 1, DateTime.Now.Date.ToString("dd.MM.yyyy"), "22:00"),
                NewEvent(4, 2, DateTime.Now.Date.ToString("dd.MM.yyyy"), "19:00"),
                NewEvent(5, 2, DateTime.Now.Date.ToString("dd.MM.yyyy"), "21:00"),
                NewEvent(6, 3, DateTime.Now.Date.ToString("dd.MM.yyyy"), "18:30"),
                NewEvent(7, 3, DateTime.Now.Date.ToString("dd.MM.yyyy"), "20:00")
            };

            return result;
        }

        public static List<Seat> LoadSeat()
        {
            var result = new List<Seat>
            {
                NewSeat(1, 1, "1", "1", true, 100),
                NewSeat(2, 1, "1", "2", true, 150),
                NewSeat(3, 1, "1", "3", true, 100),
                NewSeat(4, 1, "1", "4", true, 200),
                NewSeat(5, 1, "1", "5", true, 100),
                NewSeat(6, 1, "2", "1", true, 300),
                NewSeat(7, 1, "2", "2", true, 100),
                NewSeat(8, 1, "2", "3", true, 100),
                NewSeat(9, 1, "2", "4", true, 500),
                NewSeat(10, 1, "2", "5", true, 100),

                NewSeat(11, 2, "1", "1", true, 100),
                NewSeat(12, 2, "1", "2", true, 150),
                NewSeat(13, 2, "1", "3", true, 300),
                NewSeat(14, 2, "1", "4", true, 200),
                NewSeat(15, 2, "1", "5", true, 100),
                NewSeat(16, 2, "2", "1", true, 300),
                NewSeat(17, 2, "2", "2", true, 400),
                NewSeat(18, 2, "2", "3", true, 100),
                NewSeat(19, 2, "2", "4", true, 500),
                NewSeat(20, 2, "2", "5", true, 700),

                NewSeat(21, 3, "1", "1", true, 900),
                NewSeat(22, 3, "1", "2", true, 150),
                NewSeat(23, 3, "1", "3", true, 800),
                NewSeat(24, 3, "1", "4", true, 200),
                NewSeat(25, 3, "1", "5", true, 700),
                NewSeat(26, 3, "2", "1", true, 300),
                NewSeat(27, 3, "2", "2", true, 500),
                NewSeat(28, 3, "2", "3", true, 100),
                NewSeat(29, 3, "2", "4", true, 500),
                NewSeat(30, 3, "2", "5", true, 300),

                NewSeat(31, 4, "1", "1", true, 800),
                NewSeat(32, 4, "1", "2", true, 850),
                NewSeat(33, 4, "1", "3", true, 800),
                NewSeat(34, 4, "1", "4", true, 600),
                NewSeat(35, 4, "1", "5", true, 200),
                NewSeat(36, 4, "2", "1", true, 300),
                NewSeat(37, 4, "2", "2", true, 400),
                NewSeat(38, 4, "2", "3", true, 600),
                NewSeat(39, 4, "2", "4", true, 700),
                NewSeat(40, 4, "2", "5", true, 700),

                NewSeat(41, 5, "1", "1", true, 300),
                NewSeat(42, 5, "1", "2", true, 150),
                NewSeat(43, 5, "1", "3", true, 100),
                NewSeat(44, 5, "1", "4", true, 200),
                NewSeat(45, 5, "1", "5", true, 900),
                NewSeat(46, 5, "2", "1", true, 300),
                NewSeat(47, 5, "2", "2", true, 400),
                NewSeat(48, 5, "2", "3", true, 700),
                NewSeat(49, 5, "2", "4", true, 500),
                NewSeat(50, 5, "2", "5", true, 400),

                NewSeat(51, 6, "1", "1", true, 1000),
                NewSeat(52, 6, "1", "2", true, 1500),
                NewSeat(53, 6, "1", "3", true, 3000),
                NewSeat(54, 6, "1", "4", true, 2000),
                NewSeat(55, 6, "1", "5", true, 1000),
                NewSeat(56, 6, "2", "1", true, 3000),
                NewSeat(57, 6, "2", "2", true, 4000),
                NewSeat(58, 6, "2", "3", true, 1000),
                NewSeat(59, 6, "2", "4", true, 5000),
                NewSeat(60, 6, "2", "5", true, 7000),

                NewSeat(61, 7, "1", "1", true, 1000),
                NewSeat(62, 7, "1", "2", true, 1500),
                NewSeat(63, 7, "1", "3", true, 1000),
                NewSeat(64, 7, "1", "4", true, 2000),
                NewSeat(65, 7, "1", "5", true, 1000),
                NewSeat(66, 7, "2", "1", true, 3000),
                NewSeat(67, 7, "2", "2", true, 1000),
                NewSeat(68, 7, "2", "3", true, 1000),
                NewSeat(69, 7, "2", "4", true, 5000),
                NewSeat(70, 7, "2", "5", true, 1000)
            };

            return result;
        }

        public static Seat NewSeat(int Id, int EventId, string Row, string Place, bool Status, int Price) 
        {
            return new Seat
            {
                Id = Id,
                EventId = EventId,
                Row = Row,
                Place = Place,
                Status = Status,
                Price = Price,
            };
        }

        public static Actor NewActor(int Id, string Name)  
        {
            return new Actor
            {
                Id = Id,
                Name = Name
            };
        }

        public static ActorPerformance NewActorPerformance(int PerformanceId, int ActorId)
        {
            return new ActorPerformance
            {
                PerformanceId = PerformanceId,
                ActorId = ActorId
            };  
        }

        public static Event NewEvent(int Id, int PerformanceId, string Date, string Time)
        {
            return new Event
            {
                Id = Id,
                PerformanceId = PerformanceId,
                Date = Date,
                Time = Time,
            };
        }

        public static void LoadMockDb(Mock<TheaterContext> dbContext)
        {
            var data = InitDB.LoadPerformance().AsQueryable();
            var mockSet = new Mock<DbSet<Performance>>();
            mockSet.As<IQueryable<Performance>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Performance>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Performance>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Performance>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            dbContext.Setup(c => c.Performances).Returns(mockSet.Object);

            var Actordata = InitDB.LoadActor().AsQueryable();
            var ActormockSet = new Mock<DbSet<Actor>>();
            ActormockSet.As<IQueryable<Actor>>().Setup(m => m.Provider).Returns(Actordata.Provider);
            ActormockSet.As<IQueryable<Actor>>().Setup(m => m.Expression).Returns(Actordata.Expression);
            ActormockSet.As<IQueryable<Actor>>().Setup(m => m.ElementType).Returns(Actordata.ElementType);
            ActormockSet.As<IQueryable<Actor>>().Setup(m => m.GetEnumerator()).Returns(Actordata.GetEnumerator());
            dbContext.Setup(c => c.Actors).Returns(ActormockSet.Object);

            var ActorPerformancedata = InitDB.LoadActorPerformance().AsQueryable();
            var ActorPerformancemockSet = new Mock<DbSet<ActorPerformance>>();
            ActorPerformancemockSet.As<IQueryable<ActorPerformance>>().Setup(m => m.Provider).Returns(ActorPerformancedata.Provider);
            ActorPerformancemockSet.As<IQueryable<ActorPerformance>>().Setup(m => m.Expression).Returns(ActorPerformancedata.Expression);
            ActorPerformancemockSet.As<IQueryable<ActorPerformance>>().Setup(m => m.ElementType).Returns(ActorPerformancedata.ElementType);
            ActorPerformancemockSet.As<IQueryable<ActorPerformance>>().Setup(m => m.GetEnumerator()).Returns(ActorPerformancedata.GetEnumerator());
            dbContext.Setup(c => c.ActorPerformances).Returns(ActorPerformancemockSet.Object);

            var Producerdata = InitDB.LoadProducer().AsQueryable();
            var ProducermockSet = new Mock<DbSet<Producer>>();
            ProducermockSet.As<IQueryable<Producer>>().Setup(m => m.Provider).Returns(Producerdata.Provider);
            ProducermockSet.As<IQueryable<Producer>>().Setup(m => m.Expression).Returns(Producerdata.Expression);
            ProducermockSet.As<IQueryable<Producer>>().Setup(m => m.ElementType).Returns(Producerdata.ElementType);
            ProducermockSet.As<IQueryable<Producer>>().Setup(m => m.GetEnumerator()).Returns(Producerdata.GetEnumerator());
            dbContext.Setup(c => c.Producers).Returns(ProducermockSet.Object);

            var ProducerPerformancedata = InitDB.LoadProducerPerformance().AsQueryable();
            var ProducerPerformancemockSet = new Mock<DbSet<ProducerPerformance>>();
            ProducerPerformancemockSet.As<IQueryable<ProducerPerformance>>().Setup(m => m.Provider).Returns(ProducerPerformancedata.Provider);
            ProducerPerformancemockSet.As<IQueryable<ProducerPerformance>>().Setup(m => m.Expression).Returns(ProducerPerformancedata.Expression);
            ProducerPerformancemockSet.As<IQueryable<ProducerPerformance>>().Setup(m => m.ElementType).Returns(ProducerPerformancedata.ElementType);
            ProducerPerformancemockSet.As<IQueryable<ProducerPerformance>>().Setup(m => m.GetEnumerator()).Returns(ProducerPerformancedata.GetEnumerator());
            dbContext.Setup(c => c.ProducerPerformances).Returns(ProducerPerformancemockSet.Object);

            var Seatdata = InitDB.LoadSeat().AsQueryable();
            var SeatmockSet = new Mock<DbSet<Seat>>();
            SeatmockSet.As<IQueryable<Seat>>().Setup(m => m.Provider).Returns(Seatdata.Provider);
            SeatmockSet.As<IQueryable<Seat>>().Setup(m => m.Expression).Returns(Seatdata.Expression);
            SeatmockSet.As<IQueryable<Seat>>().Setup(m => m.ElementType).Returns(Seatdata.ElementType);
            SeatmockSet.As<IQueryable<Seat>>().Setup(m => m.GetEnumerator()).Returns(Seatdata.GetEnumerator());
            dbContext.Setup(c => c.Seats).Returns(SeatmockSet.Object);
        }
    }
}
