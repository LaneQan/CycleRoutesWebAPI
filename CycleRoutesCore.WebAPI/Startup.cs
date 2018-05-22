using CycleRoutesCore.Domain.EFCore;
using CycleRoutesCore.Domain.Interfaces;
using CycleRoutesCore.Domain.Repositories;
using CycleRoutesCore.WebAPI.Auth;
using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CycleRoutesCore.WebAPI
{
    public class Startup
    {
        public IHostingEnvironment CurrentEnvironment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            CurrentEnvironment = env;
            var config = new ConfigurationBuilder()
                .SetBasePath(CurrentEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json");
            Configuration = config.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("AllowAny", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            services.AddDbContext<CycleRoutesContext>(options =>
                    options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer();

            services.AddAuthorization(options => {
                options.AddPolicy("JWTAuthorize",
                                policy => policy.Requirements.Add(new AuthorizeRequirement()));
            }
            );

            services.AddSingleton(_ => Configuration);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IAuthorizationHandler, JWTAuthorizeHandler>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRouteRepository, RouteRepository>();

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, CycleRoutesContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc();


            context.Database.Migrate();

            if (context.Database.GetAppliedMigrations().Any())
                DbInitializer.Initialize(context);
        }
    }
}