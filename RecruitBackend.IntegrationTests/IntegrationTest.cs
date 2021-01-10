using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using RecruitBackend.Repositories;

namespace RecruitBackend.IntegrationTests
{
    public class IntegrationTest
    {
        private readonly WebApplicationFactory<Startup> _appFactory;
        protected readonly HttpClient TestClient;

        protected IntegrationTest()
        {
            _appFactory = new TestWebApplicationFactory(Guid.NewGuid().ToString());
            TestClient = _appFactory.CreateClient();
        }

        private protected DatabaseContext GetDatabaseContext()
        {
            var scope = _appFactory.Services.CreateScope();
            return scope.ServiceProvider.GetService<DatabaseContext>();
        }
    }
}