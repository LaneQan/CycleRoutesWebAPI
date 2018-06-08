using CycleRoutesCore.Domain.EFCore;
using CycleRoutesCore.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CycleRoutesCore.Domain.Models;

namespace CycleRoutesCore.Domain.Repositories
{
    public class RouteRepository : IRouteRepository
    {
        private CycleRoutesContext _db;

        public RouteRepository(CycleRoutesContext db)
        {
            _db = db;
        }

        public async Task Create(Route route)
        {
            _db.Routes.Add(route);
            await _db.SaveChangesAsync();
        }

        public async Task<Route> UpdateRoute(Route route)
        {
            Route existingRoute = await _db.Routes.FirstOrDefaultAsync(x => x.Id == route.Id);
            _db.Entry(existingRoute).CurrentValues.SetValues(route);
            await _db.SaveChangesAsync();
            return existingRoute;
        }

        public async Task<Route> DeleteRoute(Route deletedRoute)
        {
            deletedRoute.IsDeleted = true;
            _db.Entry(deletedRoute).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return deletedRoute;
        }

        public async Task<List<Route>> GetAllRoutes(int? userId)
        {
            var routes = await _db.Routes.Include(x => x.Images).ToListAsync();
            if (userId == null)
            {
                return routes;
            }
            else
            {
                routes.ForEach(route =>
                {
                    if (_db.Likes.FirstOrDefault(like => like.RouteId == route.Id && like.UserId == userId) != null)
                        route.IsLiked = true;
                });
                return routes;
            }
        }

        public async Task<Route> GetRoute(int id, string userIp, int? userId)
        {
            var route = await _db.Routes.Include(x => x.Images)
                .Include(x => x.User)
                .Where(r => r.Id == id)
                .FirstOrDefaultAsync();

            if (route == null)
                return null;

            if (await _db.Views.FirstOrDefaultAsync(x => (x.RouteId == id && x.UserIP == userIp)) == null)
            {
                await _db.Views.AddAsync(new View
                {
                    RouteId = id,
                    UserIP = userIp
                });
                route.ViewsCount++;
            }

            await _db.SaveChangesAsync();

            if (userId == null)
            {
                return route;
            }
            else
            {
                if (await _db.Likes.FirstOrDefaultAsync(like => like.RouteId == route.Id && like.UserId == userId) != null)
                {
                    route.IsLiked = true;

                }

                return route;
            }
        }

        public async Task<List<Route>> GetRoutesByUserId(int userId)
        {
            return await _db.Routes.Where(x => x.User.Id == userId).ToListAsync();
        }

        public async Task<List<Route>> GetFavouriteRoutes(int userId)
        {
            var routesIds = await _db.Likes.Where(x => x.UserId == userId).Select(y => y.RouteId).ToListAsync();
            return await _db.Routes.Where(x => routesIds.Contains(x.Id)).ToListAsync();
        }

        public void LikeRoute(int userId, int routeId)
        {
                var route = _db.Routes
                    .Where(r => r.Id == routeId)
                    .FirstOrDefault();

                var like = _db.Likes.FirstOrDefault(x => x.RouteId == routeId && x.UserId == userId);
                if (like == null)
                {
                    _db.Likes.Add(new Like
                    {
                        RouteId = routeId,
                        UserId = userId
                    });
                    route.LikesCount++;
                }
                else
                {
                    _db.Likes.Remove(like);
                    route.LikesCount--;
                }

                _db.SaveChanges();
        }
    }
}