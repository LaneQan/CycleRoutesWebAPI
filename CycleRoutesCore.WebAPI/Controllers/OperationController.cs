using CycleRoutesCore.Domain.Interfaces;
using CycleRoutesCore.Domain.Models;
using CycleRoutesCore.WebAPI.Auth;
using CycleRoutesCore.WebAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace CycleRoutesCore.WebAPI.Controllers
{
    [EnableCors("AllowAny")]
    [Route("[controller]")]
    public class OperationController : Controller
    {
        private IUserRepository _userRepository;
        private IConfiguration _config;

        public OperationController(IUserRepository userRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] CredentialsViewModel credentials)
        {
            var user = await _userRepository.GetUserByCredentials(credentials.Email, credentials.Password);
            if (user == null)
                return Unauthorized();

            AuthUser authUser = new AuthUser();
            authUser.MapToSource(user);

            string key = _config["Data:jwtKey"];

            var token = JwtCore.JsonWebToken.Encode(authUser, key, JwtCore.JwtHashAlgorithm.HS256);

            return Ok(token);
        }
    }
}