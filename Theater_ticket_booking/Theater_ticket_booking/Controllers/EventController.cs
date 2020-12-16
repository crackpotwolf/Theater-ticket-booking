using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Theater_ticket_booking.Controllers
{
    public class EventController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public virtual IActionResult EventView() 
        {
            return PartialView("_EventrPartial");
        }
    }
}
