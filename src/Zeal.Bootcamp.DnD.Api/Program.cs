using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Zeal.Bootcamp.DnD.Api.Configuration;
using Zeal.Bootcamp.DnD.Application;
using Zeal.Bootcamp.DnD.Data;
using Zeal.Bootcamp.DnD.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddControllers()

    // week 4: Add OData
    .AddOData(opt =>
     {
         opt
             .AddRouteComponents("odata", ODataConfiguration.GetEdmModel())
             .EnableQueryFeatures(100);
         opt.TimeZone = TimeZoneInfo.Utc;
     });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddMediatrBehaviors();

// Week 3: AddDataServices to add support for the data layer
builder.Services.AddDataServices(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DnDDatabase")));

var app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<IDatabase>().Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
