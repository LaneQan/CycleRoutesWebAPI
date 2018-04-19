using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CycleRoutesCore.Domain.Interfaces
{
    public interface IRouteRepository : IRepository<Models.Route>
    {
        Task UpdateRoute(Models.Route route);

        Task DeleteRoute(Models.Route route);

        List<Models.Route> GetAllRoutes();

        Models.Route GetRoute(int id);
    }
}