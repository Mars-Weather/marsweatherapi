using MarsWeatherApi.Contexts;
using MarsWeatherApi.Models;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace MarsWeatherApi
{
    public class DbUpdateService : BackgroundService
    {
        private readonly ILogger<DbUpdateService> _logger;
        private readonly IServiceProvider _serviceProvider;

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
                // TODO: Possible refactoring for readability 

                // creates a new scope for each run, so DbContext can be accessed from a singleton
                using (var scope = _serviceProvider.CreateScope()) {

                    var scopedService = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    
                    // gets all existing sol numbers in db and adds them to a list
                    var dbSolList = scopedService.Sols.ToList();
                    List<int> dbSolNumbers = new List<int>();
                    foreach(var dbsol in dbSolList)
                    {
                        dbSolNumbers.Add(dbsol.SolNumber);
                    }

                    // Loop through files in assets
                    string[] assetPaths = 
                    {"./assets/week1_sols100-106.json",
                    "./assets/week2_sols107-113.json",
                    "./assets/week3_sols114-120.json",
                    "./assets/week4_sols121-127.json",
                    "./assets/week5_sols128-134.json"
                    };

                    foreach(var path in assetPaths)
                    {
                        string jsonString = File.ReadAllText(path);

                        JsonNode marsWeekNode = JsonNode.Parse(jsonString)!;

                        JsonArray marsSolKeys = marsWeekNode!["sol_keys"]!.AsArray()!;

                        foreach(var sol in marsSolKeys)
                        {
                            string solKeyString = sol.ToString();
                            int solKeyInt = int.Parse(solKeyString);

                            /* if an asset sol key is not found in db keys, parses values, creates a sol object
                            and saves it into db. If found, logs a message of not adding sol to db
                        
                            TODO:
                            Add measurement data validity check, if not enough data, either flag as insufficient
                            data for the user, or do not add to db?
                             */
                            if(!dbSolNumbers.Contains(solKeyInt))
                            {
                            // Temperature value parsing
                            float averageTemperature = marsWeekNode![solKeyString]!["AT"]!["av"]!.GetValue<float>();
                            float minimumTemperature = marsWeekNode![solKeyString]!["AT"]!["mn"]!.GetValue<float>();
                            float maximumTemperature = marsWeekNode![solKeyString]!["AT"]!["mx"]!.GetValue<float>();

                            // Wind value parsing
                            float averageSpeed = marsWeekNode![solKeyString]!["HWS"]!["av"]!.GetValue<float>();
                            float minimumSpeed = marsWeekNode![solKeyString]!["HWS"]!["mn"]!.GetValue<float>();
                            float maximumSpeed = marsWeekNode![solKeyString]!["HWS"]!["mx"]!.GetValue<float>();
                            string mostCommon = marsWeekNode![solKeyString]!["WD"]!["most_common"]!["compass_point"]!.GetValue<string>();

                            // Pressure value parsing
                            float averagePressure = marsWeekNode![solKeyString]!["PRE"]!["av"]!.GetValue<float>();
                            float minimumPressure = marsWeekNode![solKeyString]!["PRE"]!["mn"]!.GetValue<float>();
                            float maximumPressure = marsWeekNode![solKeyString]!["PRE"]!["av"]!.GetValue<float>();

                            // Sol value parsing
                            DateTime start = marsWeekNode![solKeyString]!["First_UTC"]!.GetValue<DateTime>();
                            DateTime end = marsWeekNode![solKeyString]!["Last_UTC"]!.GetValue<DateTime>();
                            string season = marsWeekNode![solKeyString]!["Season"]!.GetValue<string>();

                            // scopedService is the database context

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
    }
}