using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.NewtonsoftJson;
using Zeal.Bootcamp.DnD.Api.COnfiguration;
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
     })
    .AddODataNewtonsoftJson();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Week 3: AddDataServices to add support for the data layer
builder.Services.AddDataServices();

var app = builder.Build();

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
