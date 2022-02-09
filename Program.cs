using Microsoft.EntityFrameworkCore;
using MarsWeatherApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add SolContext to database
builder.Services.AddDbContext<SolContext>(opt =>
    opt.UseInMemoryDatabase("SolList"));

// Add TemperatureContext to database
builder.Services.AddDbContext<TemperatureContext>(opt =>
    opt.UseInMemoryDatabase("TemperatureList"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
