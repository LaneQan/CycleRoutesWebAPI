using CycleRoutesCore.Domain.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;

namespace CycleRoutesCore.WebAPI.Auth
{
    public class AuthUser : ClaimsPrincipal
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public bool IsAdministrator { get; set; }
        public string Image { get; set; }
        public string Test { get; set; }

        public void MapToSource(User user)
        {
            this.UserId = user.Id;
            this.Email = user.Email;
            this.IsAdministrator = user.IsAdministrator;
            this.Image = user.Image;
        }
    }
}