using MarsWeatherApi.Contexts;
using MarsWeatherApi.Models;
using System.Text.Json.Nodes;

namespace MarsWeatherApi
{
    public class DbUpdateService : BackgroundService
    {
        public ILogger<DbUpdateService> _logger;
        public IServiceProvider _serviceProvider;
        private readonly string[] _assetPaths = 
        {"./assets/week1_sols100-106.json",
        "./assets/week2_sols107-113.json",
        "./assets/week3_sols114-120.json",
        "./assets/week4_sols121-127.json",
        "./assets/week5_sols128-134.json"
        };

        public DbUpdateService() {}

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
                                _logger.LogInformation("Sol " + solKeyInt + " not added (already in the database), {DateTime}", DateTime.Now);
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

        public Sol createSolFromJsonNode(JsonNode node, String solKeyString, int solKeyInt)
        {
            // TODO: Data validity checks and operations

            JsonNode temperature = node![solKeyString]!["AT"]!;
            JsonNode windSpeed = node![solKeyString]!["HWS"]!;
            JsonNode windDirection = node![solKeyString]!["WD"]!["most_common"]!["compass_point"]!;
            JsonNode pressure = node![solKeyString]!["PRE"]!;
            JsonNode solValues = node![solKeyString]!;

            // Validity
            JsonNode validity = node!["validity_checks"]![solKeyString]!;

            // Setting up variables
            float? averageTemperature;
            float? minimumTemperature;
            float? maximumTemperature;

            float? averageSpeed;
            float? minimumSpeed;
            float? maximumSpeed;
            string? mostCommon;

            float? averagePressure;
            float? minimumPressure;
            float? maximumPressure;

            // Temperature value parsing
            bool temperatureValid = validity["AT"]!["valid"]!.GetValue<bool>();

            if (temperatureValid==true)
            {
                averageTemperature = temperature["av"]!.GetValue<float>();
                minimumTemperature = temperature["mn"]!.GetValue<float>();
                maximumTemperature = temperature["mx"]!.GetValue<float>();
            }
            else {
                averageTemperature = null;
                minimumTemperature = null;
                maximumTemperature = null;
            }

            // Wind value parsing
            bool windValid = validity["WD"]!["valid"]!.GetValue<bool>();

            if (windValid==true)
            {
                averageSpeed = windSpeed["av"]!.GetValue<float>();
                minimumSpeed = windSpeed["mn"]!.GetValue<float>();
                maximumSpeed = windSpeed["mx"]!.GetValue<float>();
                mostCommon = windDirection.GetValue<string>();
            }
            else 
            {
                averageSpeed = null;
                minimumSpeed = null;
                maximumSpeed = null;
                mostCommon = null;
            }            

            // Pressure value parsing
            bool pressureValid = validity["PRE"]!["valid"]!.GetValue<bool>();

            if (pressureValid==true)
            {
                averagePressure = pressure["av"]!.GetValue<float>();
                minimumPressure = pressure["mn"]!.GetValue<float>();
                maximumPressure = pressure["av"]!.GetValue<float>();
            }
            else
            {
                averagePressure = null;
                minimumPressure = null;
                maximumPressure = null;
            }            

            // Sol value parsing
            DateTime start = solValues["First_UTC"]!.GetValue<DateTime>();
            DateTime end = solValues["Last_UTC"]!.GetValue<DateTime>();
            string season = solValues["Season"]!.GetValue<string>();

            var createdSol = new Sol
                {
                    Wind = new Wind
                    {
                        Average = (float)averageSpeed,
                        Minimum = (float)minimumSpeed,
                        Maximum = (float)maximumSpeed,
                        MostCommonDirection = mostCommon  
                    },
                    Temperature = new Temperature
                    {
                        Average = (float)averageTemperature,
                        Minimum = (float)minimumTemperature,
                        Maximum = (float)maximumTemperature
                    },
                    Pressure = new Pressure
                    {
                        Average = (float)averagePressure,
                        Minimum = (float)minimumPressure,
                        Maximum = (float)maximumPressure
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