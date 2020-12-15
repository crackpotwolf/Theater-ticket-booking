using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Theater_ticket_booking.Helpers;
using Theater_ticket_booking.Models.DB;

namespace Theater_ticket_booking.ModelsView.UserModels
{
    public class UpdateUserView
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string Phone { get; set; }
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public bool Validate(out string errors)
        {
            errors = "";

            if (string.IsNullOrEmpty(NewPassword) || NewPassword.Length < 6)
                errors += "Длина нового пароля меньше 6 символов\n";

            if (string.IsNullOrEmpty(Phone))
                errors += "Не указан номер телефона\n";

            if (string.IsNullOrEmpty(Login) || Login.Length < 3)
                errors += "Длина ника меньше 3 символов\n";

            if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName))
                errors += "Имя и фамилия являются обязательными полями\n";

            return errors == "";
        }

        public void UpdateDbModel(User user)
        {
            user.PasswordHash = MD5Helper.GetMD5Hash(NewPassword);
            user.Login = Login;
            user.Login = Login;
            user.FirstName = FirstName;
            user.LastName = LastName;
        }
    }
}
