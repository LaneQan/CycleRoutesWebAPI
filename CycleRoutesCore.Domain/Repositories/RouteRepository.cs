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
        private bool disposed = false;
        private CycleRoutesContext _db;

        public RouteRepository(CycleRoutesContext db)
        {
            _db = db;
        }

        public async Task Create(Models.Route route)
        {
            _db.Routes.Add(route);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateRoute(Models.Route route)
        {
            Models.Route existingRoute = _db.Routes.FirstOrDefault(x => x.Id == route.Id);
            _db.Entry(existingRoute).CurrentValues.SetValues(route);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteRoute(Models.Route deletedRoute)
        {
            deletedRoute.IsDeleted = true;
            _db.Entry(deletedRoute).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public List<Route> GetAllRoutes(int? userId)
        {
            var routes = _db.Routes.Include(x => x.Images).ToList();
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

        public Route GetRoute(int id, string userIp)
        {
            var route = _db.Routes.Include(x => x.Images)
                .Include(x => x.User)
                .Where(r => r.Id == id)
                .FirstOrDefault();

            if (route == null)
                return null;

            if (_db.Views.FirstOrDefault(x => (x.RouteId == id && x.UserIP == userIp)) == null)
            {
                _db.Views.Add(new View
                {
                    RouteId = id,
                    UserIP = userIp
                });
                route.ViewsCount++;
            }

            _db.SaveChangesAsync();

            return route;
        }

        public List<Route> GetRoutesByUserId(int userId)
        {
            return _db.Routes.Where(x => x.User.Id == userId).ToList();
        }

        public void LikeRoute(int userId, int routeId)
        {
            var route = _db.Routes
                .Where(r => r.Id == routeId)
                .FirstOrDefault();

            if (route == null)
                return;

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
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}