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


        }


        [Fact]
        public void CancelOrder()
        {
            // Arrange
            #region Ожидаемые данные
            
            #endregion

            // Act
            

            // Assert

        }

        [Fact]
        public void GetOrders()
        {
            // Arrange
            #region Ожидаемые данные

            #endregion

            // Act


            // Assert

        }

        [Fact]
        public void BookSeats() 
        {
            // Arrange
            #region Ожидаемые данные

            #endregion

            // Act


            // Assert

        }
    }
}