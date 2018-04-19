using CycleRoutesCore.Domain.EFCore;
using CycleRoutesCore.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public List<Models.Route> GetAllRoutes()
        {
            return _db.Routes.ToList();
        }

        public Models.Route GetRoute(int id)
        {
            return _db.Routes
                .Where(r => r.Id == id)
                .FirstOrDefault();
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