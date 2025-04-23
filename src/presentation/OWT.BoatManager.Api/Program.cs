using OWT.BoatManager.Api;
using OWT.BoatManager.Infrastructure.Persistence.EfCore.Common;

var builder = WebApplication.CreateBuilder(args);
builder.Configure();

var app = builder.Build();
app.UseBoatManager();

if (app.Environment.IsDevelopment())
{
    await using var scope = app.Services.CreateAsyncScope();
    await scope.MigrateAsync();
}

app.Run();
