using CycleRoutesCore.Domain.Interfaces;
using CycleRoutesCore.Domain.Models;
using CycleRoutesCore.WebAPI.Auth;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CycleRoutesCore.WebAPI.Controllers
{
    //[Authorize(Policy = "JWTAuthorize")]
    [EnableCors("AllowAny")]
    [Route("api/[controller]")]
    public class RoutesController : Controller
    {
        private readonly ICycleRouteRepository _routeRepository;
        private readonly IHttpContextAccessor _context;

        public RoutesController(ICycleRouteRepository routeRepository, IHttpContextAccessor context)
        {
            _routeRepository = routeRepository;
            _context = context;
        }

        [HttpGet]
        public IQueryable<Route> GetAll()
        {
            AuthIdentity user = _context.HttpContext.User.Identity as AuthIdentity;

            return _routeRepository.GetAllRoutes();
        }
    }
}