using CycleRoutesCore.Domain.Models;

namespace CycleRoutesCore.Domain.EFCore
{
    public class DbInitializer
    {
        public static void Initialize(CycleRoutesContext context)
        {
            context.Routes.RemoveRange(context.Routes);
            context.Routes.AddRange(new Route
                {
                    Description = "Описание",
                    IsDeleted = false,
                    Name = "Тур по чижовке",
                    Complexity = Complexity.Easy,
                    Length = 12.5
                },
                new Route
                {
                    Description = "Описание",
                    IsDeleted = false,
                    Name = "Едем по пригороду Минска",
                    Image = "https://stat.citydog.by/content/_posts/442X361/5968b27883970.jpg",
                    Complexity = Complexity.Medium,
                    Length = 32.1
                },
                new Route
                {
                    Description = "Описание",
                    IsDeleted = false,
                    Name = "Покатушки по городу",
                    Image = "https://stat.citydog.by/content/_posts/442X361/5345c0666ef60.jpg",
                    Complexity = Complexity.Easy,
                    Length = 8.23
                },
                new Route
                {
                    Description = "Описание",
                    IsDeleted = false,
                    Name = "Минск - Брест за 2 дня",
                    Image = "https://stat.citydog.by/content/_posts/442X361/53e234d496dcc.jpg",
                    Complexity = Complexity.Hard,
                    Length = 230.54
                },
                new Route
                {
                    Description = "Описание",
                    IsDeleted = false,
                    Name = "Минск - Молодечно - Минск",
                    Image = "https://stat.citydog.by/content/_posts/442X361/55ab73d5bb274.jpg",
                    Complexity = Complexity.Medium,
                    Length = 160
                });
            context.SaveChanges();
        }
    }
}