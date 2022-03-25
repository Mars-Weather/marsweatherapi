using MarsWeatherApi.Contexts;
using MarsWeatherApi.Models;
using System.Text.Json.Nodes;

namespace MarsWeatherApi
{
    public class DbUpdateService : BackgroundService
    {
        private readonly ILogger<DbUpdateService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly string[] _assetPaths = 
        {"./assets/week1_sols100-106.json",
        "./assets/week2_sols107-113.json",
        "./assets/week3_sols114-120.json",
        "./assets/week4_sols121-127.json",
        "./assets/week5_sols128-134.json"
        };
        public DbUpdateService(IServiceProvider serviceProvider, ILogger<DbUpdateService> logger)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Execute the background task as long as the application is run/is manually stopped
            while (!stoppingToken.IsCancellationRequested) 
            {
                // creates a new scope for each run, so DbContext can be accessed from a singleton
                using (var scope = _serviceProvider.CreateScope()) 
                {
                    var scopedService = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();      
                    int latestSolNumber = 0;
                    var dbSolList = scopedService.Sols.ToList();

                    if (dbSolList.Count > 0)
                    {
                       var latestSol = dbSolList.MaxBy(s => s.SolNumber);
                       latestSolNumber = latestSol.SolNumber;
                    }
                    // Loops through files in assets, saves new Sol objects into DB
                    foreach(var path in _assetPaths)
                    {
                        string jsonString = File.ReadAllText(path);
                        JsonNode marsWeekNode = JsonNode.Parse(jsonString)!;
                        JsonArray marsSolKeys = marsWeekNode!["sol_keys"]!.AsArray()!;

                        foreach(var sol in marsSolKeys)
                        {
                            string solKeyString = sol.ToString();
                            int solKeyInt = int.Parse(solKeyString);
                        
                            if(solKeyInt > latestSolNumber)
                            { 
                                var createdSol = createSolFromJsonNode(marsWeekNode, solKeyString, solKeyInt);
                                scopedService.Sols.Add(createdSol);
                                scopedService.SaveChanges();
                            } else
                            {
                                _logger.LogInformation("Sol " + solKeyInt + " not added, {DateTime}", DateTime.Now);
                            }                        
                        }
                    }                    
                }
                /* Sets the interval of the check with Timespan, can be ms/s/m/h/d
                 in use should be relatively lengthy, at least 1h? */
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
        // Override, logs a graceful CTRL-C shutdown
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("from DbUpdateService: StopAsync, Graceful Shutdown");
            return base.StopAsync(cancellationToken);
        }
        private Sol createSolFromJsonNode(JsonNode node, String solKeyString, int solKeyInt)
        {
            // TODO: Data validity checks and operations

            JsonNode temperature = node![solKeyString]!["AT"]!;
            JsonNode windSpeed = node![solKeyString]!["HWS"]!;
            JsonNode windDirection = node![solKeyString]!["WD"]!["most_common"]!["compass_point"]!;
            JsonNode pressure = node![solKeyString]!["PRE"]!;
            JsonNode solValues = node![solKeyString]!;

            // Temperature value parsing
            float averageTemperature = temperature["av"]!.GetValue<float>();
            float minimumTemperature = temperature["mn"]!.GetValue<float>();
            float maximumTemperature = temperature["mx"]!.GetValue<float>();

            // Wind value parsing
            float averageSpeed = windSpeed["av"]!.GetValue<float>();
            float minimumSpeed = windSpeed["mn"]!.GetValue<float>();
            float maximumSpeed = windSpeed["mx"]!.GetValue<float>();
            string mostCommon = windDirection.GetValue<string>();

            // Pressure value parsing
            float averagePressure = pressure["av"]!.GetValue<float>();
            float minimumPressure = pressure["mn"]!.GetValue<float>();
            float maximumPressure = pressure["av"]!.GetValue<float>();

            // Sol value parsing
            DateTime start = solValues["First_UTC"]!.GetValue<DateTime>();
            DateTime end = solValues["Last_UTC"]!.GetValue<DateTime>();
            string season = solValues["Season"]!.GetValue<string>();

            var createdSol = new Sol
                            {
                                Wind = new Wind
                                {
                                    Average = averageSpeed,
                                    Minimum = minimumSpeed,
                                    Maximum = maximumSpeed,
                                    MostCommonDirection = mostCommon  
                                },
                                Temperature = new Temperature
                                {
                                    Average = averageTemperature,
                                    Minimum = minimumTemperature,
                                    Maximum = maximumTemperature
                                },
                                Pressure = new Pressure
                                {
                                    Average = averagePressure,
                                    Minimum = minimumPressure,
                                    Maximum = maximumPressure
                                },
                                Start = start,
                                End = end,
                                Season = season,
                                SolNumber = solKeyInt
                            };
        return createdSol;                           
        }
    }
}