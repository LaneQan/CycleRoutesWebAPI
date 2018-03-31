using CycleRoutesCore.Domain.Models;
using System.Threading.Tasks;

namespace CycleRoutesCore.Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> Get(int id);

        Task<User> GetUserByCredentials(string email, string password);

        Task<User> GetUserByEmail(string email);
    }
}