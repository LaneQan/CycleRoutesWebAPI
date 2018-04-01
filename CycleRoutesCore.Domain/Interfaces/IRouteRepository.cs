using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CycleRoutesCore.Domain.Interfaces
{
    public interface IRouteRepository : IDisposable
    {
        Task CreateRoute(Models.Route route);

        Task UpdateRoute(Models.Route route);

        Task DeleteRoute(Models.Route route);

        List<Models.Route> GetAllRoutes();

        Models.Route GetRoute(int id);
    }
}