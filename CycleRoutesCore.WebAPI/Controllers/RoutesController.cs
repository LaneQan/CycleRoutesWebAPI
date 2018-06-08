using CycleRoutesCore.Domain.Interfaces;
using CycleRoutesCore.Domain.Models;
using CycleRoutesCore.WebAPI.Auth;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CycleRoutesCore.WebAPI.Controllers
{
    [EnableCors("AllowAny")]
    [Authorize(Policy = "JWTAuthorize")]
    [Route("api/[controller]")]
    public class RoutesController : Controller
    {
        private readonly IRouteRepository _routeRepository;
        private IConfiguration _config;

        public RoutesController(IRouteRepository routeRepository, IConfiguration config)
        {
            _routeRepository = routeRepository;
            _config = config;
        }

        [AllowAnonymous]
        [Route("")]
        [HttpGet]
        public async Task<List<Route>> GetAllRoutes()
        {
            var user = new JWTAuthorizeHandler(_config).DecodeToken(HttpContext.Request.Headers["Authorization"]);
            return await _routeRepository.GetAllRoutes( user?.Id);
        }

        [AllowAnonymous]
        [Route("{routeId:int}")]
        [HttpGet]
        public async Task <Route> GetRoute(int routeId)
        {
            var userIp = HttpContext.Connection.RemoteIpAddress.ToString();
            var user = new JWTAuthorizeHandler(_config).DecodeToken(HttpContext.Request.Headers["Authorization"]);
            return await _routeRepository.GetRoute(routeId, userIp, user?.Id);
        }

        [Route("user/{userId:int}")]
        [HttpGet]
        public async Task<List<Route>> GetAllRoutes(int userId)
        {
            return await _routeRepository.GetRoutesByUserId(userId);
        }

        [Route("favourite/{userId:int}")]
        [HttpGet]
        public async Task<List<Route>> GetFavouriteRoutes(int userId)
        {
            return await _routeRepository.GetFavouriteRoutes(userId);
        }

        [Route("like/{routeId:int}")]
        [HttpGet]
        [AllowAnonymous]
        public IActionResult LikeRoute(int routeId)
        {
            var user = new JWTAuthorizeHandler(_config).DecodeToken(HttpContext.Request.Headers["Authorization"]);
            if (user == null) return BadRequest();
            _routeRepository.LikeRoute(user.Id, routeId);
            return Ok();
        }
    }
}