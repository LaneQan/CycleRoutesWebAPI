using System.Collections.Generic;
using System.Threading.Tasks;
using CycleRoutesCore.Domain.Models;

namespace CycleRoutesCore.Domain.Interfaces
{
    public interface IRouteRepository : IRepository<Models.Route>
    {
        Task UpdateRoute(Models.Route route);

        Task DeleteRoute(Route route);

        List<Models.Route> GetAllRoutes();

        List<Route> GetRoutesByUserId(int userId);

        Models.Route GetRoute(int id);
    }
}