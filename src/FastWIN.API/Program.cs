using fastwin;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using fastwin.Repository.Repositories;
using fastwin.Interfaces;
using fastwin.Models;
using FastWIN.API.Converters;
using fastwin.Entities;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
.AddJsonOptions(options =>
{
  options.JsonSerializerOptions.Converters.Add(new JsonDateTimeConverter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CodeDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
           .LogTo(Console.WriteLine, LogLevel.Information); // Add logging
});



builder.Services.AddScoped(typeof(IRepository<Codes>),typeof(GenericRepository<Codes>));
builder.Services.AddScoped(typeof(IRepository<Product>), typeof(GenericRepository<Product>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder
.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader()
);



app.UseAuthorization();

app.MapControllers();

app.Run();
