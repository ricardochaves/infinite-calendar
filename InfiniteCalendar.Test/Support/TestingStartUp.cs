using System;
using System.Linq;

using AutoMapper;

using InfiniteCalendar.Models;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace InfiniteCalendar.Test.Support
{
    /**
     * Testing start up classes that are used to injected mocked services and migrate
     * the database for the integration tests.
     */
    public class TestingStartUp
    {
        public TestingStartUp(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // this is required to add the controllers of the main Hangman project
            var startupAssembly = typeof(Startup).Assembly;

            services.AddHttpContextAccessor()
                .AddDbContext<InfiniteCalendarContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionTest"))
                        .EnableSensitiveDataLogging(), ServiceLifetime.Singleton)
                .AddAutoMapper(startupAssembly)
                .AddControllers().AddNewtonsoftJson()
                .AddApplicationPart(startupAssembly) // adds controllers from main project
                .AddControllersAsServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsEnvironment("Local"))
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            try
            {
                Migrate();
            }
            catch (Microsoft.Data.SqlClient.SqlException e)
            {
                Console.WriteLine(e);
            }
            
        }

        /**
         * Applies possible missing migrations from the database.
         */
        private void Migrate()
        {

            // testing migrations
            var dbConnectionString = Configuration.GetConnectionString("DefaultConnectionTest");
            var options = new DbContextOptionsBuilder<InfiniteCalendarContext>()
                .UseSqlServer(dbConnectionString)
                .Options;

            var context = new InfiniteCalendarContext(options);

            // always execute possible missing migrations
            if (!context.Database.GetPendingMigrations().ToList().Any()) return;
            // context.Database.EnsureCreated();

            context.Database.Migrate();
        }
    }
}
