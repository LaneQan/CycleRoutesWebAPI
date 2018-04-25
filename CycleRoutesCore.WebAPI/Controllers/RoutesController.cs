﻿using System;
using CycleRoutesCore.Domain.Interfaces;
using CycleRoutesCore.Domain.Models;
using CycleRoutesCore.WebAPI.Auth;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CycleRoutesCore.WebAPI.Controllers
{
    //[Authorize(Policy = "JWTAuthorize")]
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
        
        [Route("")]
        [HttpGet]
        public List<Route> GetAllRoutes()
        {
            AuthIdentity user = _context.HttpContext.User.Identity as AuthIdentity;

            return _routeRepository.GetAllRoutes();
        }

        [Route("{routeId:int}")]
        [HttpGet]
        public Route GetRoute(int routeId)
        {
            return _routeRepository.GetRoute(routeId);
        }

        [Route("user/{userId:int}")]
        [HttpGet]
        public List<Route> GetAllRoutes(int userId)
        {
            return _routeRepository.GetRoutesByUserId(userId);
        }
    }
}