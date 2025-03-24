using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Api.Tests.Integration;

public class TestableCompanyApiFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    internal DbSeeder DbSeeder { get; } = new();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((_, config) =>
        {
            // Optionally add configuration settings for the test environment
            config.AddJsonFile("appsettings.Test.Integration.json", optional: true);
        });

        builder.ConfigureServices((context, services) =>
        {
            Program.ConfigureServices(services, context.Configuration);

            services.AddSingleton(context.Configuration);

            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var scopedServices = scope.ServiceProvider;

            DbSeeder.InitAsync(scopedServices).GetAwaiter().GetResult();
        });
    }

    public TService Get<TService>() where TService : notnull 
        => Services.GetRequiredService<TService>();
}


