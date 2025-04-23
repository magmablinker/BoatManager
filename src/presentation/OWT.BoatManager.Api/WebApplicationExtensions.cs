using OWT.BoatManager.Api.UseCases.Boats;

namespace OWT.BoatManager.Api;

internal static class WebApplicationExtensions
{
    public static void UseBoatManager(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors(builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapGet("/", () => "BoatManager is up and running");
        app.MapHealthChecks("/health");

        var apiVersionSet = app.NewApiVersionSet()
            .HasApiVersion(new (1))
            .Build();
        var versionGroup = app.MapGroup("/api/v{apiVersion:apiVersion}")
            .WithApiVersionSet(apiVersionSet);
        versionGroup.MapBoatEndpoints();
    }
}
