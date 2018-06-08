using System.Collections.Generic;
using System.Threading.Tasks;
using CycleRoutesCore.Domain.Models;

namespace CycleRoutesCore.Domain.Interfaces
{
    public interface IRouteRepository : IRepository<Models.Route>
    {
        Task <Route> UpdateRoute(Route route);

        Task <Route> DeleteRoute(Route route);

        Task<List<Route>> GetAllRoutes(int? userId);

        Task<List<Route>> GetRoutesByUserId(int userId);

        Task<Route> GetRoute(int id, string userIP, int? userId);

        Task<List<Route>> GetFavouriteRoutes(int userId);

        void LikeRoute(int userId, int routeId);
    }
}