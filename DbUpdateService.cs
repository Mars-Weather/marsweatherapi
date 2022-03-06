using MarsWeatherApi.Contexts;
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
                // TODO: Json deserialization, once working refactor to its own method 

                string filePath = "./assets/week1_sols100-106.json";
                string jsonString = File.ReadAllText(filePath);

                JsonNode marsWeekNode = JsonNode.Parse(jsonString)!;

                JsonArray marsSolKeys = marsWeekNode!["sol_keys"]!.AsArray()!;

                foreach(var sol in marsSolKeys)
                {
                    // parsing commands and temporary console checks
                    // TODO: Sol object creation & collection, db comparing, db saving, iteration over all asset files
                    string solKeyString = sol.ToString();

                    // Temperature value parsing checks
                    float averageTemperature = marsWeekNode![solKeyString]!["AT"]!["av"]!.GetValue<float>();
                    float minimumTemperature = marsWeekNode![solKeyString]!["AT"]!["mn"]!.GetValue<float>();
                    float maximumTemperature = marsWeekNode![solKeyString]!["AT"]!["mx"]!.GetValue<float>();
                    Console.WriteLine("Sol " + solKeyString);
                    Console.WriteLine("average temp: " + averageTemperature);
                    Console.WriteLine("minimum temp: " + minimumTemperature);
                    Console.WriteLine("maximum temp: " + maximumTemperature);
                    Console.WriteLine("");

                    // Wind value parsing checks
                    float averageSpeed = marsWeekNode![solKeyString]!["HWS"]!["av"]!.GetValue<float>();
                    float minimumSpeed = marsWeekNode![solKeyString]!["HWS"]!["mn"]!.GetValue<float>();
                    float maximumSpeed = marsWeekNode![solKeyString]!["HWS"]!["mx"]!.GetValue<float>();
                    string mostCommon = marsWeekNode![solKeyString]!["WD"]!["most_common"]!["compass_point"]!.GetValue<string>();
                    Console.WriteLine("average ws: " + averageSpeed);
                    Console.WriteLine("minimum ws: " + minimumSpeed);
                    Console.WriteLine("maximum ws: " + maximumSpeed);
                    Console.WriteLine("most common direction: " + mostCommon);
                    Console.WriteLine("");

                    // Pressure value parsing checks
                    float averagePressure = marsWeekNode![solKeyString]!["PRE"]!["av"]!.GetValue<float>();
                    float minimumPressure = marsWeekNode![solKeyString]!["PRE"]!["mn"]!.GetValue<float>();
                    float maximumPressure = marsWeekNode![solKeyString]!["PRE"]!["av"]!.GetValue<float>();
                    Console.WriteLine("average pre: " + averagePressure);
                    Console.WriteLine("minimum pre: " + minimumPressure);
                    Console.WriteLine("maximum pre: " + maximumPressure);
                    Console.WriteLine("");

                    // Sol value parsing checks
                    DateTime start = marsWeekNode![solKeyString]!["First_UTC"]!.GetValue<DateTime>();
                    DateTime end = marsWeekNode![solKeyString]!["Last_UTC"]!.GetValue<DateTime>();
                    string season = marsWeekNode![solKeyString]!["Season"]!.GetValue<string>();
                    Console.WriteLine("Start: " + start);
                    Console.WriteLine("End: " + end);
                    Console.WriteLine("Season: " + season);
                    Console.WriteLine("");
                }

                /* creates a new scope for each run, so DbContext can be accessed from a singleton, 
                list comparing will be done here
                */
                using (var scope = _serviceProvider.CreateScope()) {

                    var scopedService = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    
                    var dbSolList = scopedService.Sols.ToList();

                    foreach (var sol in dbSolList)
                    {
                    _logger.LogInformation("Solnumber: {solNumber}", sol.SolNumber);
                    _logger.LogInformation("It works! {dateTime}", DateTime.Now);    
                    }
                }

                // Sets the interval of the check with Timespan, can be ms/s/m/h/d
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