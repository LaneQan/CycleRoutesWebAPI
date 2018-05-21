using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Web;

namespace CycleRoutesCore.WebAPI.Auth
{
    public class AuthorizeRequirement : IAuthorizationRequirement { }

    public class JWTAuthorizeHandler : AuthorizationHandler<AuthorizeRequirement>
    {
        private IConfiguration _config;

        public JWTAuthorizeHandler(IConfiguration config)
        {
            _config = config;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            AuthorizeRequirement requirement)
        {
            string authHeader = null;
            var httpContext = (context.Resource as
                Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext).HttpContext;
            var request = httpContext.Request;
            var auth = request.Headers["Authorization"];

            if (auth.Count > 0 && auth[0] != "undefined" && auth[0].StartsWith("Bearer "))
                authHeader = auth[0].Substring("Bearer ".Length);

            if (string.IsNullOrEmpty(authHeader))
            {
                if (request.Query.ContainsKey("api_key"))
                    authHeader = HttpUtility.UrlDecode(request.Query["api_key"]);
            }
            if (!string.IsNullOrEmpty(authHeader))
            {
                string key = _config["Data:jwtKey"];
                try
                {
                    httpContext.User =
                        JwtCore.JsonWebToken.DecodeToObject<AuthUser>(authHeader, key);
                    context.Succeed(requirement);
                }
                catch (JwtCore.SignatureVerificationException)
                {
                }
            }
        }
    }
}