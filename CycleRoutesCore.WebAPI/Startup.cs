using CycleRoutesCore.Domain.EFCore;
using CycleRoutesCore.Domain.Interfaces;
using CycleRoutesCore.Domain.Repositories;
using CycleRoutesCore.WebAPI.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Microsoft.AspNetCore.HttpOverrides;

namespace CycleRoutesCore.WebAPI
{
    public class Startup
    {
        public IHostingEnvironment CurrentEnvironment { get; }
        public IConfiguration Configuration { get; private set; }

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
            services.AddSingleton(_ => Configuration);

            services.AddCors(o => o.AddPolicy("AllowAny", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IAuthorizationHandler, JWTAuthorizeHandler>();
            services.AddScoped<IRouteRepository, RouteRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddDbContext<CycleRoutesContext>(options =>
                options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("JWTAuthorize",
                    policy => policy.Requirements.Add(new AuthorizeRequirement()));
            }
            );

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, CycleRoutesContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseMvc();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                                   ForwardedHeaders.XForwardedProto
            });

            context.Database.Migrate();
            if (context.Database.GetAppliedMigrations().Any())
                DbInitializer.Initialize(context);
        }
    }
}