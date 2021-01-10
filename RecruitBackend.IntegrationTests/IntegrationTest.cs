using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RecruitBackend.Repositories;

namespace RecruitBackend.IntegrationTests
{
    public class IntegrationTest
    {
        protected readonly HttpClient TestClient;

        protected IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(DatabaseContext));
                        services.AddDbContext<DatabaseContext>(
                            options => { options.UseInMemoryDatabase("TestDb"); });

                        var serviceProvider = services.BuildServiceProvider();

                        using var scope = serviceProvider.CreateScope();
                        var scopedServices = scope.ServiceProvider;
                        var db = scopedServices.GetRequiredService<DatabaseContext>();
                        db.Database.EnsureCreated();
                    });
                });

            TestClient = appFactory.CreateClient();
        }
    }
}