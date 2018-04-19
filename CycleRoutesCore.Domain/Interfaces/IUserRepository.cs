using System;
using CycleRoutesCore.Domain.Models;
using System.Threading.Tasks;

namespace CycleRoutesCore.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByCredentials(string login, string email, string password);
        Task<User> Create(User user);
    }
}