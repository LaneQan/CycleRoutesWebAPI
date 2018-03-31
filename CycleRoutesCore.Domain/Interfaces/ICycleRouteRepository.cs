using System;
using System.Linq;
using System.Threading.Tasks;

namespace CycleRoutesCore.Domain.Interfaces
{
    public interface ICycleRouteRepository : IDisposable
    {
        Task CreateRoute(Models.Route route);

        Task UpdateRoute(Models.Route route);

        Task DeleteRoute(Models.Route route);

        IQueryable<Models.Route> GetAllRoutes();

        Models.Route GetRoute(int id);
    }
}