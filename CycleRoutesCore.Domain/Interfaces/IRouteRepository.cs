using System.Collections.Generic;
using System.Threading.Tasks;
using CycleRoutesCore.Domain.Models;

namespace CycleRoutesCore.Domain.Interfaces
{
    public interface IRouteRepository 
    {
        Task DeleteRoute(int routeId);

        Task<List<Route>> GetAllRoutes(int? userId);

        Task<List<Route>> GetRoutesByUserId(int userId);

        Task<Route> GetRoute(int id, string userIP, int? userId);

        Task<List<Route>> GetFavouriteRoutes(int userId);

        Task<int> Create(Route route, int userId);

        void LikeRoute(int userId, int routeId);
    }
}