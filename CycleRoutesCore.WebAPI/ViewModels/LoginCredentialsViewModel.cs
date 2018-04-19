using CycleRoutesCore.Domain.Models;

namespace CycleRoutesCore.WebAPI.ViewModels
{
    public class CredentialsViewModel
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


        public User MapToUser(User user)
        {
            user.Email = this.Email;
            user.Password = this.Password;
            user.Login = this.Login;

            return user;
        }
    }
}