using Theater_ticket_booking.Controllers;
using Theater_ticket_booking.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using MockQueryable.Moq;
using Theater_ticket_booking.ModelsView;
using System.Threading.Tasks;
using Theater_ticket_booking.Repositories;
using Theater_ticket_booking.Models.DB;
using Theater_ticket_booking.Controllers.API;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Theater_ticket_booking.Test.Controllers
{
    public class EventControllerTest
    {

        Mock<TheaterContext> dbContext;

        //Repos
        Mock<UsersRepository> usersRepository;
        Mock<EventRepository> eventRepository;
        Mock<ProducerRepository> producerRepository;
        Mock<ActorRepository> actorRepository;

        //OrderController
        EventController eventController; 

        public EventControllerTest() 
        {
            dbContext = new Mock<TheaterContext>();

            InitDB.LoadMockDb(dbContext);

            //Event
            eventRepository = new Mock<EventRepository>(dbContext.Object);
            eventRepository.Setup(repo => repo.GetList()).Returns(InitDB.LoadEvent().AsQueryable().BuildMock().Object);
            eventRepository.Setup(repo => repo.GetUniqueRows(1)).Returns(Task.FromResult(new List<string>(new string[] { "1", "2" })));

            eventRepository.Setup(repo => repo.Add(It.IsAny<Event>())).Returns(Task.FromResult(true));
            eventRepository.Setup(repo => repo.Update(It.IsAny<Event>())).Returns(Task.FromResult(true));
            eventRepository.Setup(repo => repo.Remove(It.IsAny<Event>())).Returns(Task.FromResult(true));

            //Producer
            producerRepository = new Mock<ProducerRepository>(dbContext.Object);
            producerRepository.Setup(repo => repo.GetList()).Returns(new List<Producer>().AsQueryable().BuildMock().Object);
            producerRepository.Setup(repo => repo.Add(It.IsAny<Producer>())).Returns(Task.FromResult(true));
            producerRepository.Setup(repo => repo.Update(It.IsAny<Producer>())).Returns(Task.FromResult(true));
            producerRepository.Setup(repo => repo.Remove(It.IsAny<Producer>())).Returns(Task.FromResult(true));

            //Actors
            actorRepository = new Mock<ActorRepository>(dbContext.Object);
            actorRepository.Setup(repo => repo.GetList()).Returns(new List<Actor>().AsQueryable().BuildMock().Object);
            actorRepository.Setup(repo => repo.Add(It.IsAny<Actor>())).Returns(Task.FromResult(true));
            actorRepository.Setup(repo => repo.Update(It.IsAny<Actor>())).Returns(Task.FromResult(true));
            actorRepository.Setup(repo => repo.Remove(It.IsAny<Actor>())).Returns(Task.FromResult(true));

            //Users
            usersRepository = new Mock<UsersRepository>(dbContext.Object);
            var users = new List<User>() {
                new User() { Id = -1 },
                new User() { Id = 1 },
                new User() { Id = 2 },
                new User() { Id = 3 },
            };
            usersRepository.Setup(repo => repo.GetList()).Returns(users.AsQueryable().BuildMock().Object);

            //Controller
            eventController = new EventController(eventRepository.Object, usersRepository.Object, 
                producerRepository.Object, actorRepository.Object);
        }

        [Fact]
        public void GetEvents()  
        {
            // Arrange
            #region Ожидаемые данные
            var event_view = new List<ShotEventView>
            {
                new ShotEventView
                {
                    Name = "Мастер и Маргарита",
                    MiniDescription = "«Мастер и Маргарита» — подлинный шедевр искусства театра двадцать первого века. Драма о любви и предательстве.",
                    Photo = "https://sun9-56.userapi.com/impg/cHPDwO7FDtki4BZ6_vAD1fFZgewxKd7BMjCoWw/O2QokcLexM0.jpg?size=833x500&quality=96&proxy=1&sign=37461966fa37122559137c286269c4cb&type=album",
                    PerformanceId = 1,
                    Event = new List<Event> 
                    {
                        InitDB.NewEvent(1, 1, DateTime.Now.Date.ToString("dd.MM.yyyy"), "18:00"),
                        InitDB.NewEvent(2, 1, DateTime.Now.Date.ToString("dd.MM.yyyy"), "20:00"),
                        InitDB.NewEvent(3, 1, DateTime.Now.Date.ToString("dd.MM.yyyy"), "22:00")
                    }
                },

                new ShotEventView
                {
                    Name = "Юнона и Авось",
                    MiniDescription = "«Юнона» и «Авось» – самая известная рок-опера на российской сцене, в авторской версии Алексея Рыбникова.",
                    Photo = "https://sun9-31.userapi.com/impg/a9qwQ0Ii6-Kbx7X-ptTYd_GYI9qEa_SzA1t_0g/Se_Caw9Jue8.jpg?size=833x500&quality=96&proxy=1&sign=aae7351d145185d0f693f4c16304323c&type=album",
                    PerformanceId = 2,
                    Event = new List<Event>
                    {
                        InitDB.NewEvent(4, 2, DateTime.Now.Date.ToString("dd.MM.yyyy"), "19:00"),
                        InitDB.NewEvent(5, 2, DateTime.Now.Date.ToString("dd.MM.yyyy"), "21:00")
                    }
                },

                new ShotEventView
                {
                    Name = "Искуситель",
                    MiniDescription = "«Искуситель» - это авантюрная комедия с интригующим сюжетом, с Ароновой и Спиваковским",
                    Photo = "https://sun9-3.userapi.com/impg/I2P7IOh7PDScBEK9pPP-_Vjmqm1GEYnpB0edOQ/Dv0uaY3797Y.jpg?size=833x500&quality=96&proxy=1&sign=add8f2baded78c3dd97116fd0f35f274&type=album",
                    PerformanceId = 3,
                    Event = new List<Event>
                    {
                        InitDB.NewEvent(6, 3, DateTime.Now.Date.ToString("dd.MM.yyyy"), "18:30"),
                        InitDB.NewEvent(7, 3, DateTime.Now.Date.ToString("dd.MM.yyyy"), "20:00")
                    }
                }
            };
            #endregion

            // Act
            var result = eventController.GetEvents(DateTime.Now.Date.ToString("dd.MM.yyyy")).Result;

            // Assert
            Assert.Equal(JsonConvert.SerializeObject(event_view), JsonConvert.SerializeObject(result));
        }

        [Fact]
        public void GetEvent()
        {
            // Arrange
            #region Ожидаемые данные
            var event_view = new EventView
            {
                EventId = 1,
                Name = "Мастер и Маргарита",
                Description = "Вообще «Мастер и Маргарита» не особо охотно поддается постановке. Не так то это просто — внести довольно крупное произведение в рамки спектакля. Задачу усложняют тесно-тонкое переплетение сюжетных линий, да и вообще роман сам по себе окутан мистическим ореолом. Главный режиссер театра на «Юго-Западе» Валерий Белякович не побоялся осуществить постановку (нехорошего романа) Михаила Булгакова. Необычно, оригинально, блестяще. Такие эмоции переполняли зрителей, когда на сцене разыгрывалось настоящее мистическое действо. И действительно удачно продуманные режиссером Валерием Беляковичем мизансцены, множество танцев, роскошные костюмы, лазерным шоу, мастерство художника по свету и блестящая игра актеров раздвинули тесную черноту и сделали, как казалось раньше, невозможное: самый знаменитый роман Булгакова наконец-то ожил на сцене.",
                Actors = "Владимир Филатов, Михаил Гудошников, Сергей Поздняков, Ивар Калныньш, Александр Бреньков",
                Producers = "Валерий Белякович",
                PerformanceId = 1,
                DateTime = DateTime.Now.Date.ToString("dd.MM.yyyy") + ", " + "18:00"
            };
            #endregion

            // Act
            var result = eventController.GetEvent(1).Result as PartialViewResult; 

            // Assert
            Assert.Equal(JsonConvert.SerializeObject(event_view), JsonConvert.SerializeObject(result.Model));
        }

        [Fact]
        public void GetEventViewName()  
        {
            // Act
            var result = eventController.GetEvent(1).Result as PartialViewResult;

            // Assert
            Assert.Equal("_OrderPartial", result?.ViewName);
        }

        [Fact]
        public void GetEventRows() 
        {
            // Act
            var result = eventController.GetEvent(1).Result as PartialViewResult;

            // Assert
            Assert.Equal(new List<string>(new string[] { "1", "2" }), result?.ViewData["Rows"]);
        }

        [Fact]
        public void GetSeats()
        {
            // Arrange
            #region Ожидаемые данные
            List<Seat> seats = new List<Seat>
            {
                InitDB.NewSeat(1, 1, "1", "1", true, 100),
                InitDB.NewSeat(2, 1, "1", "2", true, 150),
                InitDB.NewSeat(3, 1, "1", "3", true, 100),
                InitDB.NewSeat(4, 1, "1", "4", true, 200),
                InitDB.NewSeat(5, 1, "1", "5", true, 100)
            };

            eventRepository.Setup(repo => repo.GetSeatsByRow("1", 1)).Returns(Task.FromResult(seats));

            Dictionary<int, string> searsSum = new Dictionary<int, string> 
            {
                {1, "1 (100 р.)"},
                {2, "2 (150 р.)"},
                {3, "3 (100 р.)"},
                {4, "4 (200 р.)"},
                {5, "5 (100 р.)"}
            };
            #endregion

            // Act
            var result = eventController.GetSeats("1", 1).Result;

            // Assert
            Assert.Equal(JsonConvert.SerializeObject(result), JsonConvert.SerializeObject(searsSum));
        }

        [Fact]
        public void GetSum() 
        {
            // Arrange
            int[] sumSeats = new int[] { 1, 2 };

            string sum = "250";

            eventRepository.Setup(repo => repo.GetSeat(1)).Returns(Task.FromResult(100));
            eventRepository.Setup(repo => repo.GetSeat(2)).Returns(Task.FromResult(150));

            // Act
            var result = eventController.GetSum(sumSeats).Result;

            // Assert
            Assert.Equal(JsonConvert.SerializeObject(sum), JsonConvert.SerializeObject(result));
        }

        
    }
}
