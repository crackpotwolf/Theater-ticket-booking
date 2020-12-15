using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Theater_ticket_booking.Helpers;
using Theater_ticket_booking.Models.DB;

namespace Theater_ticket_booking.ModelsView.UserModels 
{
    public class RegistrationModel
    {
        private string _email;
        public string Email { get => _email; set => _email = value.Trim(); }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public bool Validate(out string errors)
        {
            errors = "";

            if (Email == null && RegexHelpers.Email.Match(Email).Value != Email)
                errors += "Не корректный Email\n";

            if (string.IsNullOrEmpty(Password) || Password.Length < 6)
                errors += "Длина пароля меньше 6 символов\n";

            if (string.IsNullOrEmpty(Phone))
                errors += "Не указан номер телефона\n";

            if (string.IsNullOrEmpty(Login) || Login.Length < 3)
                errors += "Длина логина меньше 3 символов\n";

            if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName))
                errors += "Имя и фамилия являются обязательными полями\n";

            return errors == "";
        }

        public User ToDbModel()
        {
            return new User()
            {
                Email = Email,
                PasswordHash = MD5Helper.GetMD5Hash(Password),
                Phone = Phone,
                Login = Login,
                FirstName = FirstName,
                LastName = LastName
            };
        }
    }
}
