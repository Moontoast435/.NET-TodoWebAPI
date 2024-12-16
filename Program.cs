using Microsoft.EntityFrameworkCore;
using TodoWebAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

// Register services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        builder => builder.WithOrigins("https://localhost:5173")
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

builder.Services.AddDbContext<LHQ_SeanContext>(options =>
    options.UseSqlServer("Data Source=SEANLAPTOP;Initial Catalog=LHQ_Sean;Integrated Security=True"));

var app = builder.Build();

// Enable CORS globally
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:5173"));

app.UsePathBase("/api/GetTodos");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
