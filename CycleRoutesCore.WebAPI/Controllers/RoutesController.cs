using System;
using CycleRoutesCore.Domain.Interfaces;
using CycleRoutesCore.Domain.Models;
using CycleRoutesCore.WebAPI.Auth;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Threading.Tasks;
using CycleRoutesCore.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;

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

        [Route("delete/{routeId:int}")]
        [HttpGet]
        public async Task<IActionResult> DeleteRoute(int routeId)
        {
            await _routeRepository.DeleteRoute(routeId);
            return Ok();
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

        [Route("add")]
        [HttpPost, DisableRequestSizeLimit]
        public async Task<ActionResult> AddRoute(IFormCollection routeInfo)
        {
            var user = new JWTAuthorizeHandler(_config).DecodeToken(HttpContext.Request.Headers["Authorization"]);
            if (user == null) return BadRequest();

            List<RouteImage> images = new List<RouteImage>();

            var route = new Route
            {
                Name = routeInfo["RouteName"][0],
                Description = routeInfo["RouteDescription"][0],
                IsDeleted = false,
                Length = double.Parse(routeInfo["Length"][0], CultureInfo.InvariantCulture),
                Landscape = (Landscape) Convert.ToInt32(routeInfo["Landscape"][0]),
                Type = (Domain.Enums.Type) Convert.ToInt32(routeInfo["Type"][0]),
                LineType = (LineType) Convert.ToInt32(routeInfo["LineType"][0]),
                LengthTime = (LengthTimes) Convert.ToInt32(routeInfo["LengthTime"][0]),
                MapLink = routeInfo["MapLink"][0],
                UploadDate = DateTime.Now,
            };

            if (routeInfo.Files.Count > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img");

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                foreach (IFormFile file in routeInfo.Files)
                {
                    if (file.Length > 0)
                    {
                        string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.ToString();
                        var extension = Path.GetExtension(file.FileName);
                        string uniqFilename = Path.GetFileNameWithoutExtension(fileName) +
                            "_" +
                            Guid.NewGuid().ToString().Substring(0, 4) +
                            Path.GetExtension(fileName);
                        if (System.IO.File.Exists(Path.Combine(path, fileName)))
                            return Ok(fileName);

                        using (var fileStream = new FileStream(Path.Combine(path, uniqFilename), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }

                        images.Add(new RouteImage {Name = uniqFilename});
                    }
                }
            }

            if (images.Count != 0)
                route.Images = images;
            await _routeRepository.Create(route, user.Id);
            return this.Content("OK");
        }
    }
}