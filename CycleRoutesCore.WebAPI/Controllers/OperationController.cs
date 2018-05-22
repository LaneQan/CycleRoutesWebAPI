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
            var user = await _userRepository.GetUserByCredentials(credentials.Login, credentials.Email,
                credentials.Password);
            if (user == null)
                return Unauthorized();

            return Ok(new JWTAuthorizeHandler(_config).BuildToken(user));
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] CredentialsViewModel viewModel)
        {
            User user = new User();

            if (await _userRepository.Create(viewModel.MapToUser(user)) == null)
                return BadRequest();

            return Ok(new JWTAuthorizeHandler(_config).BuildToken(user));
        }
    }
}