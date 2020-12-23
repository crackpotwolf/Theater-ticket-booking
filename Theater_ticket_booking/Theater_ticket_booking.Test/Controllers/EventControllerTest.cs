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
    class EventControllerTest
    {

        Mock<TheaterContext> DbContext;

        //DialogsApiController
        EventController eventController; 

    }
}
