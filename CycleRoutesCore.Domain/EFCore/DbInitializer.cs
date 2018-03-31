using CycleRoutesCore.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CycleRoutesCore.Domain.EFCore
{
    public class DbInitializer
    {
        public static void Initialize(CycleRoutesContext context)
        {
            if (!context.Routes.AnyAsync().Result)
            {
                context.Routes.AddRange(new Route
                    {
                        Description = "Test",
                        IsDeleted = false,
                        Name = "Test"
                    },
                    new Route
                    {
                        Description = "Test2",
                        IsDeleted = false,
                        Name = "Test2"
                    });
                context.SaveChanges();
            }
        }
    }
}