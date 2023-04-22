using DecoratorPatternCachingExample.Interfaces;
using DecoratorPatternCachingExample.Persistence.Caching;
using DecoratorPatternCachingExample.Persistence.Repositories;
using DecoratorPatternCachingExample.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add controllers to the container.
builder.Services.AddControllers();

// Add memory cache
builder.Services.AddMemoryCache();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add AutoMapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// Add Repositories
builder.Services.AddScoped<IStudentRepository, StudentRepository>();

// Add Services
builder.Services.AddScoped<IStudentService, StudentService>();

// Add Decorator
builder.Services.AddScoped<StudentRepository>();
builder.Services.AddScoped<IStudentRepository, StudentCachingDecorator<StudentRepository>>();

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
