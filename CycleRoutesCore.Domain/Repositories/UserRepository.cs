using CycleRoutesCore.Domain.EFCore;
using CycleRoutesCore.Domain.Helpers;
using CycleRoutesCore.Domain.Interfaces;
using CycleRoutesCore.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

            _db.Users.Add(user);

            await _db.SaveChangesAsync();

            return user;

        }
        public async Task Update(User user)
        {
            User existingUser = _db.Users.FirstOrDefault(x => x.Id == user.Id);
            _db.Entry(existingUser).CurrentValues.SetValues(user);
            await _db.SaveChangesAsync();
        }


        public async Task<User> GetUserByCredentials(string login, string email, string password)
        {
            var test = _db.Users.LastOrDefault();
            return await _db.Users
                  .FirstOrDefaultAsync(u => ((u.Login.Equals(login) || u.Email.Equals(email)) && u.Password.Equals(password.HashingPassword())));
        }
    }
}