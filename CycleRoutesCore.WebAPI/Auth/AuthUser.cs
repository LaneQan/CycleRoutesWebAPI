using CycleRoutesCore.Domain.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;

namespace CycleRoutesCore.WebAPI.Auth
{
    public class AuthUser : ClaimsPrincipal
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public bool IsAdministrator { get; set; }

        public List<string> Roles { get; set; }

        public override IIdentity Identity
        {
            get { return new AuthIdentity(this.UserId, this.Roles); }
        }

        public override bool IsInRole(string role)
        {
            return Roles.Contains(role);
        }

        public void MapToSource(User user)
        {
            // to-do
        }
    }
}