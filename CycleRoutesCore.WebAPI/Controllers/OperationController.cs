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
    [Route("api/[controller]")]
    public class OperationController : Controller
    {
        private IUserRepository _userRepository;
        private string _jwtKey;

        public OperationController(IUserRepository userRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _jwtKey = config["Data:jwtKey"];
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] CredentialsViewModel credentials)
        {
            var user = await _userRepository.GetUserByCredentials(credentials.Login, credentials.Email,
                credentials.Password);
            if (user == null)
                return Unauthorized();

            AuthUser authUser = new AuthUser();
            authUser.MapToSource(user);


            var token = JwtCore.JsonWebToken.Encode(authUser, _jwtKey, JwtCore.JwtHashAlgorithm.HS256);

            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] CredentialsViewModel viewModel)
        {
            User user = new User();

            if (await _userRepository.Create(viewModel.MapToUser(user)) == null)
                return BadRequest();

            AuthUser authUser = new AuthUser();
            authUser.MapToSource(user);
            var token = JwtCore.JsonWebToken.Encode(user, _jwtKey, JwtCore.JwtHashAlgorithm.HS256);

            return Ok(token);
        }
    }
}