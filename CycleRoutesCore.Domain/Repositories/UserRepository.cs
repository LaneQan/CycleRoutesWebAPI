using System;
using CycleRoutesCore.Domain.EFCore;
using CycleRoutesCore.Domain.Helpers;
using CycleRoutesCore.Domain.Interfaces;
using CycleRoutesCore.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CycleRoutesCore.Domain.Repositories
{
    public class UserRepository : IUserRepository
    {
        private CycleRoutesContext _db;

        public UserRepository(CycleRoutesContext db)
        {
            _db = db;
        }

        public async Task<User> Create(User user)
        {
            var existingUser = await _db.Users
                .FirstOrDefaultAsync(u => (u.Login.Equals(user.Login) || u.Email.Equals(user.Email)) && u.Email == user.Email);

            if (existingUser != null)
                return null;

            user.IsAdministrator = false;
            user.Password = user.Password.HashingPassword();
            user.RegisteredAt = DateTime.Now;

            _db.Users.Add(user);

            await _db.SaveChangesAsync();

            return user;
        }

        public async Task Update(User user)
        {
            User existingUser = await _db.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
            _db.Entry(existingUser).CurrentValues.SetValues(user);
            await _db.SaveChangesAsync();
        }

        public async Task<User> GetUserByCredentials(string login, string email, string password)
        {
            return await _db.Users
                  .FirstOrDefaultAsync(u => (u.Login.Equals(login) || u.Email.Equals(email)) && u.Password.Equals(password.HashingPassword()));
        }

        public async Task<User> GetUserById(int userId)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == userId);
            user.LikesCount = await _db.Likes.Where(x => x.UserId == user.Id).CountAsync();
            user.RoutesCount = await _db.Routes.Where(x => x.User.Id == user.Id).CountAsync();
            return user;
        }
    }
}