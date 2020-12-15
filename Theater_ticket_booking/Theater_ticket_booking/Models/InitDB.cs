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
        }
    }

}
