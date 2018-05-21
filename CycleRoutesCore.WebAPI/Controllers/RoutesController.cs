using System;
using CycleRoutesCore.Domain.Interfaces;
using CycleRoutesCore.Domain.Models;
using CycleRoutesCore.WebAPI.Auth;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace CycleRoutesCore.WebAPI.Controllers
{
    [Authorize(Policy = "JWTAuthorize")]
    [EnableCors("AllowAny")]
    [Route("api/[controller]")]
    public class RoutesController : Controller
    {
        private readonly IRouteRepository _routeRepository;
        private readonly IHttpContextAccessor _context;

        public RoutesController(IRouteRepository routeRepository, IHttpContextAccessor context)
        {
            _routeRepository = routeRepository;
            _context = context;
        }

        [AllowAnonymous]
        [Route("")]
        [HttpGet]
        public List<Route> GetAllRoutes()
        {
            AuthUser user = _context.HttpContext.User as AuthUser;
            return _routeRepository.GetAllRoutes(3);
        }

        [AllowAnonymous]
        [Route("{routeId:int}")]
        [HttpGet]
        public Route GetRoute(int routeId)
        {
            var userIp = HttpContext.Connection.RemoteIpAddress.ToString();
            return _routeRepository.GetRoute(routeId, userIp);
        }

        [Route("user/{userId:int}")]
        [HttpGet]
        public List<Route> GetAllRoutes(int userId)
        {
            return _routeRepository.GetRoutesByUserId(userId);
        }

        [Route("like/{routeId:int}")]
        [HttpGet]
        [AllowAnonymous]
        public IActionResult LikeRoute(int routeId)
        {
            _routeRepository.LikeRoute(3, routeId);
            return Ok();
        }
    }
}