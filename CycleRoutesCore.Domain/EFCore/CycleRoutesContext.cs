using CycleRoutesCore.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CycleRoutesCore.Domain.EFCore
{
    public class CycleRoutesContext : DbContext
    {
        public CycleRoutesContext(DbContextOptions<CycleRoutesContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public DbSet<Route> Routes { get; set; }
        public DbSet<User> Users { get; set; }
    }
}