using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using RecruitBackend.Repositories;

namespace RecruitBackend.IntegrationTests
{
    public class IntegrationTest : TestWebApplicationFactory
    {
        private readonly WebApplicationFactory<Startup> _appFactory;
        protected readonly HttpClient TestClient;

        protected IntegrationTest(WebApplicationFactory<Startup> appFactory)
        {
            _appFactory = appFactory;
            TestClient = _appFactory.CreateClient();
        }

        private protected DatabaseContext GetDatabaseContext()
        {
            var scopeFactory = _appFactory.Services.GetRequiredService<IServiceScopeFactory>();
            return scopeFactory.CreateScope().ServiceProvider.GetRequiredService<DatabaseContext>();
        }
    }
}