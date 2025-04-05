using Company.Api.Database;
using Company.Api.Repositories;
using Company.Api.Services;
using Company.Api.Utils.Extensions;
using Company.Api.Validation;
using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Authentication;

// Make the implicit Program class public (for in-memory integration tests)
namespace Company.Api;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        ConfigureServices(builder.Services, builder.Configuration);
        var app = builder.Build();

        //cross-cutting concerns
        app
            .UseMiddleware<ValidationExceptionMiddleware>()
            .UseAuthentication()
            .UseAuthorization();

        // endpoints configuration
        app.SetupFastEndpoints();

        await InitializeDatabase(app);

        app.Run();
    }

    private static async Task InitializeDatabase(WebApplication app)
    {
        var databaseInitializer = app.Services.GetRequiredService<DatabaseInitializer>();
        await databaseInitializer.InitializeAsync();
    }

    public static void ConfigureServices(IServiceCollection services, IConfiguration config)
    {
        services  //setup FastEndpoints services
            .AddAuthorization()
            .AddFastEndpoints()
            .SwaggerDocument(opts =>
            {
                opts.ShortSchemaNames = true;
                opts.MaxEndpointVersion = 1;
            });

        //temp workaround for issue w/ integration tests:
        //when used from the WebApplicationFactory, the app.Run() above throws "JwtBearerSchemeProvider already added"
        //try: use FastEndpoints test extensions for integration tests, instead of WebApplicationFactory 
        if (services.All(s => s.ServiceType != typeof(IAuthenticationSchemeProvider)))
        {
            //todo: find the root cause & remove the workaround
            services.AddAuthenticationJwtBearer(opts =>
            {
                opts.SigningKey = config.GetValue<string>("Auth:TokenSecret");
                opts.SigningStyle = TokenSigningStyle.Symmetric;
            });
        }

        services //setup application services
            .AddSingleton<IDbConnectionFactory>(_ => new SqliteConnectionFactory(config.GetValue<string>("Database:ConnectionString")))
            .AddSingleton<DatabaseInitializer>()
            .AddSingleton<ICompanyRepository, CompanyRepository>()
            .AddSingleton<ICompanyAuthenticationService, CompanyAuthenticationService>()
            .AddSingleton<ICompanyService, CompanyService>();
    }

}