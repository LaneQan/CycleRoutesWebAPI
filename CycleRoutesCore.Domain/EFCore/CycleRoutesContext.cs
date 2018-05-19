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

            modelBuilder.Entity<Route>()
                .Property(b => b.ViewsCount)
                .HasDefaultValue(0);
        }

        public DbSet<Route> Routes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RouteImage> RouteImages { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<View> Views { get; set; }
    }
}