using System.Collections.Generic;
using System.Security.Principal;

namespace CycleRoutesCore.WebAPI.Auth
{
    public class AuthIdentity : IIdentity
    {
        public AuthIdentity(int id, List<string> roles)
        {
            this.Name = id.ToString();
            this.Roles = roles;
        }

        public string AuthenticationType
        {
            get { return "JWT"; }
        }

        public bool IsAuthenticated
        {
            get { return true; }
        }

        public string Name
        {
            get;
            private set;
        }

        public List<string> Roles { get; set; }
    }
}