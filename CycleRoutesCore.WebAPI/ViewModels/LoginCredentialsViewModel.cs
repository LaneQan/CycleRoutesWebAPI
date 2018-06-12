using CycleRoutesCore.Domain.Models;

namespace CycleRoutesCore.WebAPI.ViewModels
{
    public class CredentialsViewModel
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CurrentCity { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Phone { get; set; }


        public User MapToUser(User user)
        {
            user.Email = this.Email;
            user.Password = this.Password;
            user.Login = this.Login;
            user.CurrentCity = this.CurrentCity;
            user.Firstname = this.Firstname;
            user.Lastname = this.Lastname;
            user.Phone = this.Phone;

            return user;
        }
    }
}