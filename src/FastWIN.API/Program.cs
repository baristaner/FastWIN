using fastwin;
using Microsoft.EntityFrameworkCore;
using fastwin.Repository.Repositories;
using fastwin.Interfaces;
using FastWIN.API.Converters;
using FluentValidation.AspNetCore;
using FluentValidation;
using MediatR;
using fastwin.Features.Products.Commands.AddProduct;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
.AddJsonOptions(options =>
{
  options.JsonSerializerOptions.Converters.Add(new JsonDateTimeConverter());
});

builder.Services.AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters()
    .AddValidatorsFromAssemblyContaining<Program>();


builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddDbContext<CodeDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
           .LogTo(Console.WriteLine, LogLevel.Information); 
});


builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

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
