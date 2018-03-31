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

        public async Task Update(User user)
        {
            User existingUser = _db.Users.FirstOrDefault(x => x.Id == user.Id);
            _db.Entry(existingUser).CurrentValues.SetValues(user);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _db.Users
                     .ToListAsync();
        }

        public async Task<User> Get(int id)
        {
            return await _db.Users
                .FirstOrDefaultAsync(s => s.Id.Equals(id));
        }

        public async Task<User> GetUserByCredentials(string email, string password)
        {
            return await _db.Users
                  .FirstOrDefaultAsync(u => u.Email.Equals(email) && u.Password.Equals(password.HashingPassword()));
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _db.Users
                  .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<User>> Find(Expression<Func<User, bool>> predicate)
        {
            return await _db.Users
                .Where(predicate)
                .ToListAsync();
        }
    }
}