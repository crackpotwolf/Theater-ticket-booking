using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Theater_ticket_booking.Models
{
    public class AuthOptions
    {
        public const string ISSUER = "MeetingServer"; // издатель токена
        public const string AUDIENCE = "Meeting"; // потребитель токена
        const string KEY = "1231038dag50734081257fghsdhmdasd860284918585dsh43523462547136728!123";   // ключ для шифрации
        public const int LIFETIME = 24 * 60; // время жизни токена - 24 часа
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }


        public const string UrlLoginPage = "/";
    }
}
