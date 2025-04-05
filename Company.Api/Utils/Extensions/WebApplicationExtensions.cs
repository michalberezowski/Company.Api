using FastEndpoints;
using FastEndpoints.Swagger;

namespace Company.Api.Utils.Extensions;

public static class WebApplicationExtensions
{
    public static IApplicationBuilder SetupFastEndpoints(this WebApplication app)
    {
        app //FastEndpoints setup
            .UseFastEndpoints(c =>
            {
                c.Versioning.Prefix = "v";
                c.Versioning.DefaultVersion = 1;
                c.Versioning.PrependToRoute = true;
            });

        //setup FastEndpoints Swagger/Swagger Ui for testing the apis
        if (!app.Environment.IsDevelopment()) return app;

        return app
            .UseSwaggerGen()
            .UseSwaggerUi();
    }
}

