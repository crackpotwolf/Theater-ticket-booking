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
        Mock<TheaterContext> dbContext = new Mock<TheaterContext>();

        //OrderController
        OrderController orderController;

        public OrderControllerTest()
        {
            dbContext = new Mock<TheaterContext>();
            InitDB.Initialize(dbContext.Object);

            //Controller
            orderController = new OrderController(dbContext.Object);
        }

        [Fact]
        public void GetOrders() 
        {
            // Act
            var result = orderController.GetOrders();

            // Assert
            Assert.IsType<List<OrderView>>(result);
        }


    }
}
