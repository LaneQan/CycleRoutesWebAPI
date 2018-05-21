using CycleRoutesCore.Domain.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;

namespace CycleRoutesCore.WebAPI.Auth
{
    public class AuthUser : ClaimsPrincipal
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public bool IsAdministrator { get; set; }
        public string Image { get; set; }

        public List<string> Roles { get; set; }

        public override IIdentity Identity
        {
            get { return new AuthIdentity(this.Id, this.Roles); }
        }

        public void MapToSource(User user)
        {
            this.Id = user.Id;
            this.Email = user.Email;
            this.IsAdministrator = user.IsAdministrator;
            this.Image = user.Image;
            this.Roles = new List<string> {user.IsAdministrator ? "Administrator" : "User"};
        }
    }
}