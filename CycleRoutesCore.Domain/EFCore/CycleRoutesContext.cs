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
            modelBuilder.Entity<Route>().HasMany(x => x.Images).WithOne();
        }

        public DbSet<Route> Routes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RouteImage> RouteImages { get; set; }
    }
}