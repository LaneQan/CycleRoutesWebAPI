using System.IO;
using CycleRoutesCore.Domain.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CycleRoutesCore.WebAPI
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CycleRoutesContext>
    {
        public static IConfiguration Configuration { get; set; }

        public CycleRoutesContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            Configuration = configuration.Build();

            var builder = new DbContextOptionsBuilder<CycleRoutesContext>();

            builder.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]);

            return new CycleRoutesContext(builder.Options);
        }
    }
}