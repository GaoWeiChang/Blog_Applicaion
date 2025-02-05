using backend.Data;
using backend.Repositories.Implementation;
using backend.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BlogAppConnectionString"));
});

// need to write this after created implementation and interface
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>(); // add dependency injection and create new instance in every HTTP request
builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options =>
{
    options.AllowAnyHeader()
           .WithOrigins("http://localhost:4200")  // Only allow requests from port 4200
           .AllowAnyMethod();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
