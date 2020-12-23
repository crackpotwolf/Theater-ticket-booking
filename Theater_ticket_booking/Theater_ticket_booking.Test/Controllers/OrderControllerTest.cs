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

namespace Theater_ticket_booking.Test.Controllers
{
    public class OrderControllerTest
    {

        Mock<TheaterContext> dbContext;

        //Repos
        Mock<UsersRepository> usersRepository;
        Mock<EventRepository> eventRepository;
        Mock<ProducerRepository> producerRepository;
        Mock<ActorRepository> actorRepository;

        //OrderController
        EventController eventController;

        public OrderControllerTest()
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
    }
}