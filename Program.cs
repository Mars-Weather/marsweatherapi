using Microsoft.EntityFrameworkCore;
using MarsWeatherApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// This is for the Sol class & Sol Context, add ones for your own classes below!
builder.Services.AddDbContext<SolContext>(opt =>
    opt.UseInMemoryDatabase("SolList"));

// This is for the Pressure class & Pressure Context
builder.Services.AddDbContext<PressureContext>(opt =>
    opt.UseInMemoryDatabase("PressureList"));

// This is for the Wind class & Wind Context
builder.Services.AddDbContext<WindContext>(opt =>
    opt.UseInMemoryDatabase("WindList"));

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
