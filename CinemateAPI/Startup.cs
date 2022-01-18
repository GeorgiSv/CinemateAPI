namespace CinemateAPI
{
    using System;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    using CinemateAPI.Data;
    using CinemateAPI.Data.Models;
    using CinemateAPI.Infrastructure.Extensions;
    using CinemateAPI.Features.Identity;
    using CinemateAPI.Infrastructure.MovieDb;
    using CinemateAPI.Features.Reviews;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CinemateDbContext>(options => options
                    .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddCors();
            services
                .AddIdentity<User, IdentityRole>(options =>
                {
                    options.Password.RequiredLength = 6;
                    options.Password.RequireDigit = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                })
                .AddEntityFrameworkStores<CinemateDbContext>();

            var appSettingsSection = Configuration.GetSection("AppSettings");
            var movieDbConfigSection = Configuration.GetSection("MoviesDbConfig");

            services.Configure<AppSettings>(appSettingsSection);
            services.Configure<MovieDbConfig>(movieDbConfigSection);

            var appConfig = appSettingsSection.Get<AppSettings>();
            var movieDbConfig = movieDbConfigSection.Get<MovieDbConfig>();

            services.AddJwtAuthentication(appConfig);

            services.AddHttpClient(movieDbConfig.ClientName, httpClient =>
            {
                httpClient.BaseAddress = new Uri(movieDbConfig.BaseUrl);
            });

            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IReviewService, ReviewService>();
            services.AddTransient<MovieDbService>();

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.ApplyMigrations();
        }
    }
}
