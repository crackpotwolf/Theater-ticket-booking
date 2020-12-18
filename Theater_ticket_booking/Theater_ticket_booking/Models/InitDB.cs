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
                context.Performances.AddRange(
                    new Performance
                    {
                        Id = 1,
                        Name = "Мастер и Маргарита",
                        Description = "Вообще «Мастер и Маргарита» не особо охотно поддается постановке. Не так то это просто — внести довольно крупное произведение в рамки спектакля. Задачу усложняют тесно-тонкое переплетение сюжетных линий, да и вообще роман сам по себе окутан мистическим ореолом. Главный режиссер театра на «Юго-Западе» Валерий Белякович не побоялся осуществить постановку (нехорошего романа) Михаила Булгакова. Необычно, оригинально, блестяще. Такие эмоции переполняли зрителей, когда на сцене разыгрывалось настоящее мистическое действо. И действительно удачно продуманные режиссером Валерием Беляковичем мизансцены, множество танцев, роскошные костюмы, лазерным шоу, мастерство художника по свету и блестящая игра актеров раздвинули тесную черноту и сделали, как казалось раньше, невозможное: самый знаменитый роман Булгакова наконец-то ожил на сцене.",
                        MiniDescription = "«Мастер и Маргарита» — подлинный шедевр искусства театра двадцать первого века. Драма о любви и предательстве.",
                        Photo = "https://sun9-56.userapi.com/impg/cHPDwO7FDtki4BZ6_vAD1fFZgewxKd7BMjCoWw/O2QokcLexM0.jpg?size=833x500&quality=96&proxy=1&sign=37461966fa37122559137c286269c4cb&type=album"
                    }
                );

                context.Performances.AddRange(
                    new Performance
                    {
                        Id = 2,
                        Name = "Юнона и Авось",
                        Description = "«Юнона» и «Авось» – самая известная рок-опера на российской сцене. Авторы – выдающийся русский композитор Алексей Рыбников и поэт Андрей Вознесенский. Постановку рок-оперы «Юнона и Авось» в авторской версии осуществил Государственный театр под руководством Народного артиста РФ, Лауреата Государственной премии РФ, композитора Алексея Львовича Рыбникова.",
                        MiniDescription = " «Юнона» и «Авось» – самая известная рок-опера на российской сцене, в авторской версии Алексея Рыбникова.",
                        Photo = "https://sun9-31.userapi.com/impg/a9qwQ0Ii6-Kbx7X-ptTYd_GYI9qEa_SzA1t_0g/Se_Caw9Jue8.jpg?size=833x500&quality=96&proxy=1&sign=aae7351d145185d0f693f4c16304323c&type=album"
                    }
                );

                context.Performances.AddRange(
                    new Performance
                    {
                        Id = 3,
                        Name = "Искуситель",
                        Description = "«Искуситель» - это авантюрная комедия с интригующим сюжетом. За основу спектакля был взят сюжет пьесы Валерия Шашина «Поджигатель», которая была написана им 80-е. В обычную, ничем непримечательную жизнь, молодой супружеской пары вторгается некий третий. Этот острый на язык обманщик не только рушит своим присутствием все привычные устои семьи, но и заставляет в общем-то милых и честных людей действовать так, как прежде им бы и в голову не пришло.",
                        MiniDescription = "«Искуситель» - это авантюрная комедия с интригующим сюжетом, с Ароновой и Спиваковским",
                        Photo = "https://sun9-3.userapi.com/impg/I2P7IOh7PDScBEK9pPP-_Vjmqm1GEYnpB0edOQ/Dv0uaY3797Y.jpg?size=833x500&quality=96&proxy=1&sign=add8f2baded78c3dd97116fd0f35f274&type=album"
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

                context.Producers.AddRange(
                    new Producer
                    {
                        Id = 2,
                        Name = "Александр Рыхлов"
                    }
                );

                context.Producers.AddRange(
                    new Producer
                    {
                        Id = 3,
                        Name = "Александр Горбань"
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

                context.ProducerPerformances.AddRange(
                    new ProducerPerformance
                    {
                        Id = 2,
                        PerformanceId = 2,
                        ProducerId = 2,
                    }
                );

                context.ProducerPerformances.AddRange(
                    new ProducerPerformance
                    {
                        Id = 3,
                        PerformanceId = 3,
                        ProducerId = 3,
                    }
                );
                context.SaveChanges();
            }

            if (!context.Actors.Any())
            {

                context.Actors.AddRange(NewActor(1, "Владимир Филатов"));
                context.Actors.AddRange(NewActor(2, "Михаил Гудошников"));
                context.Actors.AddRange(NewActor(3, "Сергей Поздняков"));
                context.Actors.AddRange(NewActor(4, "Ивар Калныньш"));
                context.Actors.AddRange(NewActor(5, "Александр Бреньков"));

                context.Actors.AddRange(NewActor(6, "Игорь Кирилюк"));
                context.Actors.AddRange(NewActor(7, "Николай Дроздовский"));
                context.Actors.AddRange(NewActor(8, "Наталья Крестьянских"));
                context.Actors.AddRange(NewActor(9, "Екатерина Соловьева"));
                context.Actors.AddRange(NewActor(10, "Никита Поздняков"));

                context.Actors.AddRange(NewActor(11, "Даниил Спиваковский"));
                context.Actors.AddRange(NewActor(12, "Александр Феклистов"));
                context.Actors.AddRange(NewActor(13, "Мария Аронова"));

                context.SaveChanges();
            }

            if (!context.ActorPerformances.Any())
            {
                context.ActorPerformances.AddRange(NewActorPerformance(1, 1));
                context.ActorPerformances.AddRange(NewActorPerformance(1, 2));
                context.ActorPerformances.AddRange(NewActorPerformance(1, 3));
                context.ActorPerformances.AddRange(NewActorPerformance(1, 4));
                context.ActorPerformances.AddRange(NewActorPerformance(1, 5));

                context.ActorPerformances.AddRange(NewActorPerformance(2, 6));
                context.ActorPerformances.AddRange(NewActorPerformance(2, 7));
                context.ActorPerformances.AddRange(NewActorPerformance(2, 8));
                context.ActorPerformances.AddRange(NewActorPerformance(2, 9));
                context.ActorPerformances.AddRange(NewActorPerformance(2, 10));

                context.ActorPerformances.AddRange(NewActorPerformance(3, 11));
                context.ActorPerformances.AddRange(NewActorPerformance(3, 12));
                context.ActorPerformances.AddRange(NewActorPerformance(3, 13));

                context.SaveChanges();
            }

            if (!context.Events.Any())
            {
                context.Events.AddRange(NewEvent(1, 1, DateTime.Now.Date.ToString("dd.MM.yyyy"), "18:00"));
                context.Events.AddRange(NewEvent(2, 1, DateTime.Now.Date.ToString("dd.MM.yyyy"), "20:00"));
                context.Events.AddRange(NewEvent(3, 1, DateTime.Now.Date.ToString("dd.MM.yyyy"), "22:00"));

                context.Events.AddRange(NewEvent(4, 2, DateTime.Now.Date.ToString("dd.MM.yyyy"), "19:00"));
                context.Events.AddRange(NewEvent(5, 2, DateTime.Now.Date.ToString("dd.MM.yyyy"), "21:00"));

                context.Events.AddRange(NewEvent(6, 3, DateTime.Now.Date.ToString("dd.MM.yyyy"), "18:30"));
                context.Events.AddRange(NewEvent(7, 3, DateTime.Now.Date.ToString("dd.MM.yyyy"), "20:00"));

                context.SaveChanges();
            }

            if (!context.Seats.Any())
            {
                context.Seats.AddRange(NewSeat(1, "1", "1", true, 100));
                context.Seats.AddRange(NewSeat(1, "1", "2", true, 150));
                context.Seats.AddRange(NewSeat(1, "1", "3", true, 100));
                context.Seats.AddRange(NewSeat(1, "1", "4", true, 200));
                context.Seats.AddRange(NewSeat(1, "1", "5", true, 100));
                context.Seats.AddRange(NewSeat(1, "2", "1", true, 300));
                context.Seats.AddRange(NewSeat(1, "2", "2", true, 100));
                context.Seats.AddRange(NewSeat(1, "2", "3", true, 100));
                context.Seats.AddRange(NewSeat(1, "2", "4", true, 500));
                context.Seats.AddRange(NewSeat(1, "2", "5", true, 100));

                context.Seats.AddRange(NewSeat(2, "1", "1", true, 100));
                context.Seats.AddRange(NewSeat(2, "1", "2", true, 150));
                context.Seats.AddRange(NewSeat(2, "1", "3", true, 300));
                context.Seats.AddRange(NewSeat(2, "1", "4", true, 200));
                context.Seats.AddRange(NewSeat(2, "1", "5", true, 100));
                context.Seats.AddRange(NewSeat(2, "2", "1", true, 300));
                context.Seats.AddRange(NewSeat(2, "2", "2", true, 400));
                context.Seats.AddRange(NewSeat(2, "2", "3", true, 100));
                context.Seats.AddRange(NewSeat(2, "2", "4", true, 500));
                context.Seats.AddRange(NewSeat(2, "2", "5", true, 700));

                context.Seats.AddRange(NewSeat(3, "1", "1", true, 900));
                context.Seats.AddRange(NewSeat(3, "1", "2", true, 150));
                context.Seats.AddRange(NewSeat(3, "1", "3", true, 800));
                context.Seats.AddRange(NewSeat(3, "1", "4", true, 200));
                context.Seats.AddRange(NewSeat(3, "1", "5", true, 700));
                context.Seats.AddRange(NewSeat(3, "2", "1", true, 300));
                context.Seats.AddRange(NewSeat(3, "2", "2", true, 500));
                context.Seats.AddRange(NewSeat(3, "2", "3", true, 100));
                context.Seats.AddRange(NewSeat(3, "2", "4", true, 500));
                context.Seats.AddRange(NewSeat(3, "2", "5", true, 300));

                context.Seats.AddRange(NewSeat(4, "1", "1", true, 800));
                context.Seats.AddRange(NewSeat(4, "1", "2", true, 850));
                context.Seats.AddRange(NewSeat(4, "1", "3", true, 800));
                context.Seats.AddRange(NewSeat(4, "1", "4", true, 600));
                context.Seats.AddRange(NewSeat(4, "1", "5", true, 200));
                context.Seats.AddRange(NewSeat(4, "2", "1", true, 300));
                context.Seats.AddRange(NewSeat(4, "2", "2", true, 400));
                context.Seats.AddRange(NewSeat(4, "2", "3", true, 600));
                context.Seats.AddRange(NewSeat(4, "2", "4", true, 700));
                context.Seats.AddRange(NewSeat(4, "2", "5", true, 700));

                context.Seats.AddRange(NewSeat(5, "1", "1", true, 300));
                context.Seats.AddRange(NewSeat(5, "1", "2", true, 150));
                context.Seats.AddRange(NewSeat(5, "1", "3", true, 100));
                context.Seats.AddRange(NewSeat(5, "1", "4", true, 200));
                context.Seats.AddRange(NewSeat(5, "1", "5", true, 900));
                context.Seats.AddRange(NewSeat(5, "2", "1", true, 300));
                context.Seats.AddRange(NewSeat(5, "2", "2", true, 400));
                context.Seats.AddRange(NewSeat(5, "2", "3", true, 700));
                context.Seats.AddRange(NewSeat(5, "2", "4", true, 500));
                context.Seats.AddRange(NewSeat(5, "2", "5", true, 400));

                context.Seats.AddRange(NewSeat(6, "1", "1", true, 1000));
                context.Seats.AddRange(NewSeat(6, "1", "2", true, 1500));
                context.Seats.AddRange(NewSeat(6, "1", "3", true, 3000));
                context.Seats.AddRange(NewSeat(6, "1", "4", true, 2000));
                context.Seats.AddRange(NewSeat(6, "1", "5", true, 1000));
                context.Seats.AddRange(NewSeat(6, "2", "1", true, 3000));
                context.Seats.AddRange(NewSeat(6, "2", "2", true, 4000));
                context.Seats.AddRange(NewSeat(6, "2", "3", true, 1000));
                context.Seats.AddRange(NewSeat(6, "2", "4", true, 5000));
                context.Seats.AddRange(NewSeat(6, "2", "5", true, 7000));

                context.Seats.AddRange(NewSeat(7, "1", "1", true, 1000));
                context.Seats.AddRange(NewSeat(7, "1", "2", true, 1500));
                context.Seats.AddRange(NewSeat(7, "1", "3", true, 1000));
                context.Seats.AddRange(NewSeat(7, "1", "4", true, 2000));
                context.Seats.AddRange(NewSeat(7, "1", "5", true, 1000));
                context.Seats.AddRange(NewSeat(7, "2", "1", true, 3000));
                context.Seats.AddRange(NewSeat(7, "2", "2", true, 1000));
                context.Seats.AddRange(NewSeat(7, "2", "3", true, 1000));
                context.Seats.AddRange(NewSeat(7, "2", "4", true, 5000));
                context.Seats.AddRange(NewSeat(7, "2", "5", true, 1000));

                context.SaveChanges();
            }
        }

        public static Seat NewSeat(int EventId, string Row, string Place, bool Status, int Price) 
        {
            return new Seat
            {
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
    }
}
