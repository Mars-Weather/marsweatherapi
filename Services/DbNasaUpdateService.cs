using MarsWeatherApi.Contexts;
using System.Text.Json.Nodes;

namespace MarsWeatherApi
{
    public class DbNasaUpdateService : DbUpdateService
    {
        private readonly IConfiguration _config;

        public DbNasaUpdateService(IServiceProvider serviceProvider, ILogger<DbNasaUpdateService> logger, IConfiguration config)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _config = config;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Get the API key from secrets storage
            string nasaApiKey = _config["NasaApi:ApiKey"];

            // Set up the HttpClient             
            HttpClient client = new HttpClient();
            
            // Execute the background task as long as the application is run/is manually stopped
            while (!stoppingToken.IsCancellationRequested) 
            {
                // Send the request
                HttpResponseMessage response = await client.GetAsync("https://api.nasa.gov/insight_weather/?api_key=" + nasaApiKey + "&feedtype=json&ver=1.0");

                // Continue only if the request was successful
                if (response.IsSuccessStatusCode) 
                {
                    _logger.LogInformation("Response successful, {DateTime}", DateTime.Now);

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

                        // Loops through the response, saves new Sol objects into DB    
                        string jsonString = await response.Content.ReadAsStringAsync();
                        JsonNode marsWeekNode = JsonNode.Parse(jsonString)!;
                        JsonArray marsSolKeys = marsWeekNode!["sol_keys"]!.AsArray()!;
                        _logger.LogInformation("Length of Sol keys array: " + marsSolKeys.Count);

                        foreach(var sol in marsSolKeys)
                        {                            
                            string solKeyString = sol.ToString();
                            int solKeyInt = int.Parse(solKeyString);
                        
                            if(solKeyInt > latestSolNumber)
                            { 
                                var createdSol = base.createSolFromJsonNode(marsWeekNode, solKeyString, solKeyInt);
                                scopedService.Sols.Add(createdSol);
                                scopedService.SaveChanges();
                            } else
                            {
                                _logger.LogInformation("Sol " + solKeyInt + " not added (already in the database), {DateTime}", DateTime.Now);
                            }                        
                        }                                        
                    }
    
                }
                else
                {
                    _logger.LogError("Request not successful. Status code: " + response.StatusCode + ", {DateTime}", DateTime.Now);
                }

                /* Sets the interval of the check with Timespan, can be ms/s/m/h/d
                The NASA data should contain the last 7 sols, so the check should be done at least every 7 days */                
                await Task.Delay(TimeSpan.FromDays(6), stoppingToken);
            }
        }
        
        // Override, logs a graceful CTRL-C shutdown
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("from DbNasaUpdateService: StopAsync, Graceful Shutdown");
            return base.StopAsync(cancellationToken);
        }

    }
}