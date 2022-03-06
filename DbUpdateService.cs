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
                // TODO: Json deserialization, then refactor to its own method

                List<int> solKeys = new List<int>(); 

                string filePath = "./assets/week1_sols100-106.json";
                string jsonString = File.ReadAllText(filePath);

                JsonNode marsWeekNode = JsonNode.Parse(jsonString)!;

                JsonArray marsSolKeys = marsWeekNode!["sol_keys"]!.AsArray()!;

                /* creates a new scope for each run, so DbContext can be accessed from a singleton, 
                list comparing will be done below
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