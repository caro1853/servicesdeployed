using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Hosting;
using Scheduling.API;
using Scheduling.API.Extensions;
using Scheduling.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthorization();
app.MapControllers();

app.MigrateDatabase<ApplicationDbContext>((context, services) =>
{
    var logger = services.GetService<ILogger<ApplicationDBContextSeed>>();

    if (logger == null)
        throw new Exception("logger can't be null");

    ApplicationDBContextSeed
        .SeedAsync(context, logger)
        .Wait();
});

app.Run();