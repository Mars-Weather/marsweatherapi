using Microsoft.EntityFrameworkCore;
using MarsWeatherApi.Contexts;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Add background services
builder.Services.AddHostedService<MarsWeatherApi.DbUpdateService>(); // Background service for timed DB updates
builder.Services.AddHostedService<MarsWeatherApi.DbNasaUpdateService>(); // / Background service for timed requests to Nasa's API

// Add a reference handler
builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

// Add database Context
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    //UseInMemoryDatabase("MarsWeatherApiDB"));

// Add CORS policy
var AllowGETFromAllOrigins = "_AllowGETFromAllOrigins";
builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: AllowGETFromAllOrigins, builder =>
            {
                builder.AllowAnyOrigin().WithMethods("GET");
            });
    }
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(AllowGETFromAllOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
