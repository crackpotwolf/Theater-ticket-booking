using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Theater_ticket_booking.Helpers
{
    public static class MD5Helper
    {
        /// <summary>
        /// ХЭШ строки
        /// </summary>
        /// <param name="value">Исходная строка</param>
        /// <returns>ХЭШ</returns>
        public static string GetMD5Hash(string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            return Encoding.UTF8.GetString(new MD5CryptoServiceProvider().ComputeHash(bytes));
        }
    }
}
