using System;
using System.Net.Http;

using InfiniteCalendar.Models;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace InfiniteCalendar.Test.Support
{
    /**
     * Testing Case class which opens a DB transaction and rolls it back on the end of every
     * test in order have a clean database for every executed test.
     */
    public class TestingCaseFixture<TStartup> : IDisposable where TStartup : class
    {
        // private testing properties
        private readonly IDbContextTransaction _transaction;

        // properties used by testing classes
        protected readonly HttpClient _client;
        protected InfiniteCalendarContext _context { get; }

        protected TestingCaseFixture()
        {
            var builder = WebHost.CreateDefaultBuilder()
                .UseStartup<TStartup>();

            // constructs the testing server with the WebHostBuilder configuration
            // Startup class configures injected mocked services, and middleware (ConfigureServices, etc.)  
            var server = new TestServer(builder);
            var services = server.Host.Services;

            // resolve a DbContext instance from the container and begin a transaction on the context.
            _client = server.CreateClient();
            _context = services.GetRequiredService<InfiniteCalendarContext>();
            _transaction = _context.Database.BeginTransaction();
        }

        public void Dispose()
        {
            if (_transaction == null) return;

            _transaction.Rollback();
            _transaction.Dispose();
        }
    }
}
