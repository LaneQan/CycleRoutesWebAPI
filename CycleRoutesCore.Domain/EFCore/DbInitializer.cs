﻿using System;
using System.Collections.Generic;
using CycleRoutesCore.Domain.Enums;
using CycleRoutesCore.Domain.Helpers;
using CycleRoutesCore.Domain.Models;
using System.Linq;

namespace CycleRoutesCore.Domain.EFCore
{
    public class DbInitializer
    {
        public static void Initialize(CycleRoutesContext context)
        {
            /*if (context.RouteImages.Any()) { context.RouteImages.RemoveRange(context.RouteImages);}
            context.Routes.RemoveRange(context.Routes);
            var user = context.Users.FirstOrDefault(x => x.Id == 2);
            user.Image =
                "https://x1.xingassets.com/image/1_0_5_49a1e2d5e_19863724_1/alexander-bender-foto.1024x1024.jpg";
            context.Routes.AddRange(new Route
            {
                Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus. Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in, viverra quis, feugiat a, tellus. Phasellus viverra nulla ut metus varius laoreet. Quisque rutrum. Aenean imperdiet. Etiam ultricies nisi vel augue. Curabitur ullamcorper ultricies nisi. Nam eget dui. Etiam rhoncus. Maecenas tempus, tellus eget condimentum rhoncus, sem quam semper libero, sit amet adipiscing sem neque sed ipsum. Nam quam nunc, blandit vel, luctus pulvinar, hendrerit id, lorem. Maecenas nec odio et ante tincidunt tempus. Donec vitae sapien ut libero venenatis faucibus. Nullam quis ante. Etiam sit amet orci eget eros faucibus tincidunt. Duis leo. Sed fringilla mauris sit amet nibh. Donec sodales sagittis magna. Sed consequat, leo eget bibendum sodales, augue velit cursus nunc,",
                IsDeleted = false,
                Name = "Тур по чижовке",
                Image = "https://stat.citydog.by/content/_posts/442X361/53b47f3e27b3f.jpg",
                Landscape = Landscape.Hilly,
                Type = Type.City,
                LineType = LineType.Direct,
                LengthTime = LengthTimes.Brief,
                Length = 12.5,
                Images = new List<RouteImage>
                {
                    new RouteImage
                    {
                        Name = "https://citydog.by/content/editor_images/2015/07_july/19_velo/1.jpg"
                    },
                    new RouteImage
                    {
                        Name = "https://citydog.by/content/editor_images/2015/07_july/19_velo/2.jpg"
                    },
                    new RouteImage
                    {
                        Name = "https://citydog.by/content/editor_images/2015/07_july/19_velo/2-2.jpg"
                    },
                    new RouteImage
                    {
                        Name = "https://citydog.by/content/editor_images/2015/07_july/19_velo/4.jpg"
                    },
                    new RouteImage
                    {
                        Name = "https://citydog.by/content/editor_images/2015/07_july/19_velo/8.jpg"
                    },
                    new RouteImage
                    {
                        Name = "https://citydog.by/content/editor_images/2015/07_july/19_velo/9.jpg"
                    }
                },
                User = user,
                UploadDate = "12.05.2018"
            },
                new Route
                {
                    Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus. Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in, viverra quis, feugiat a, tellus. Phasellus viverra nulla ut metus varius laoreet. Quisque rutrum. Aenean imperdiet. Etiam ultricies nisi vel augue. Curabitur ullamcorper ultricies nisi. Nam eget dui. Etiam rhoncus. Maecenas tempus, tellus eget condimentum rhoncus, sem quam semper libero, sit amet adipiscing sem neque sed ipsum. Nam quam nunc, blandit vel, luctus pulvinar, hendrerit id, lorem. Maecenas nec odio et ante tincidunt tempus. Donec vitae sapien ut libero venenatis faucibus. Nullam quis ante. Etiam sit amet orci eget eros faucibus tincidunt. Duis leo. Sed fringilla mauris sit amet nibh. Donec sodales sagittis magna. Sed consequat, leo eget bibendum sodales, augue velit cursus nunc,",
                    IsDeleted = false,
                    Name = "Едем по пригороду Минска",
                    Image = "https://stat.citydog.by/content/_posts/442X361/5968b27883970.jpg",
                    Landscape = Landscape.Mixed,
                    Type = Type.City,
                    LineType = LineType.Direct,
                    LengthTime = LengthTimes.Daily,
                    Length = 32.1,
                    User = user,
                    UploadDate = "12.05.2018"
                },
                new Route
                {
                    Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus. Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in, viverra quis, feugiat a, tellus. Phasellus viverra nulla ut metus varius laoreet. Quisque rutrum. Aenean imperdiet. Etiam ultricies nisi vel augue. Curabitur ullamcorper ultricies nisi. Nam eget dui. Etiam rhoncus. Maecenas tempus, tellus eget condimentum rhoncus, sem quam semper libero, sit amet adipiscing sem neque sed ipsum. Nam quam nunc, blandit vel, luctus pulvinar, hendrerit id, lorem. Maecenas nec odio et ante tincidunt tempus. Donec vitae sapien ut libero venenatis faucibus. Nullam quis ante. Etiam sit amet orci eget eros faucibus tincidunt. Duis leo. Sed fringilla mauris sit amet nibh. Donec sodales sagittis magna. Sed consequat, leo eget bibendum sodales, augue velit cursus nunc,",
                    IsDeleted = false,
                    Name = "Покатушки по городу",
                    Image = "https://stat.citydog.by/content/_posts/442X361/5345c0666ef60.jpg",
                    Landscape = Landscape.Mountain,
                    Type = Type.City,
                    LineType = LineType.Circular,
                    LengthTime = LengthTimes.Brief,
                    Length = 8.23,
                    User = user,
                    UploadDate = "12.05.2018"
                },
                new Route
                {
                    Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus. Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in, viverra quis, feugiat a, tellus. Phasellus viverra nulla ut metus varius laoreet. Quisque rutrum. Aenean imperdiet. Etiam ultricies nisi vel augue. Curabitur ullamcorper ultricies nisi. Nam eget dui. Etiam rhoncus. Maecenas tempus, tellus eget condimentum rhoncus, sem quam semper libero, sit amet adipiscing sem neque sed ipsum. Nam quam nunc, blandit vel, luctus pulvinar, hendrerit id, lorem. Maecenas nec odio et ante tincidunt tempus. Donec vitae sapien ut libero venenatis faucibus. Nullam quis ante. Etiam sit amet orci eget eros faucibus tincidunt. Duis leo. Sed fringilla mauris sit amet nibh. Donec sodales sagittis magna. Sed consequat, leo eget bibendum sodales, augue velit cursus nunc,",
                    IsDeleted = false,
                    Name = "Минск - Брест за 2 дня",
                    Image = "https://stat.citydog.by/content/_posts/442X361/53e234d496dcc.jpg",
                    Landscape = Landscape.Plain,
                    Type = Type.Mixed,
                    LineType = LineType.Direct,
                    LengthTime = LengthTimes.Weekend,
                    Length = 230.54,
                    User = context.Users.FirstOrDefault(x => x.Id == 3),
                    UploadDate = "12.05.2018"
                },
                new Route
                {
                    Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus. Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in, viverra quis, feugiat a, tellus. Phasellus viverra nulla ut metus varius laoreet. Quisque rutrum. Aenean imperdiet. Etiam ultricies nisi vel augue. Curabitur ullamcorper ultricies nisi. Nam eget dui. Etiam rhoncus. Maecenas tempus, tellus eget condimentum rhoncus, sem quam semper libero, sit amet adipiscing sem neque sed ipsum. Nam quam nunc, blandit vel, luctus pulvinar, hendrerit id, lorem. Maecenas nec odio et ante tincidunt tempus. Donec vitae sapien ut libero venenatis faucibus. Nullam quis ante. Etiam sit amet orci eget eros faucibus tincidunt. Duis leo. Sed fringilla mauris sit amet nibh. Donec sodales sagittis magna. Sed consequat, leo eget bibendum sodales, augue velit cursus nunc,",
                    IsDeleted = false,
                    Name = "Минск - Молодечно - Минск",
                    Image = "https://stat.citydog.by/content/_posts/442X361/55ab73d5bb274.jpg",
                    Landscape = Landscape.Hilly,
                    Type = Type.Suburban,
                    LineType = LineType.Direct,
                    LengthTime = LengthTimes.Multiday,
                    Length = 160,
                    User = context.Users.FirstOrDefault(x => x.Id == 3),
                    UploadDate = "12.05.2018"
                });*/

            /*context.Users.Add(new User
            {
                IsAdministrator = true,
                CurrentCity = "Минск",
                Email = "admin@admin.by",
                Firstname = "Админ",
                Lastname = "Админов",
                Login = "admin",
                Password = PasswordHasher.HashingPassword("admin@admin.by")
            });
            context.Users.Add(new User
            {
                IsAdministrator = false,
                CurrentCity = "Минск",
                Email = "user@user.by",
                Firstname = "Пользователь",
                Lastname = "Пользователев",
                Login = "user",
                Password = PasswordHasher.HashingPassword("user@user.by")
            });
            */
        }
    }
}