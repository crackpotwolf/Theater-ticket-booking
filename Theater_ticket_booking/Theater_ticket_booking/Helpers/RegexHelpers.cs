using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Theater_ticket_booking.Helpers
{
    public static class RegexHelpers
    {
        public static Regex NotInt = new Regex(@"[^\d]");
        public static Regex Email = new Regex(@"[\w\.]+\@\w+\.\w+");

        public static string GetWithoutInt(string value) => NotInt.Replace(value, "");
    }
}
