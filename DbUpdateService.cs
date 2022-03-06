using MarsWeatherApi.Contexts;

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

                // creates a new scope for each run, so DbContext can be accessed from a singleton
                using (var scope = _serviceProvider.CreateScope()) {

                    var scopedService = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    
                    var dbSolList = scopedService.Sols.ToList();

                    foreach (var sol in dbSolList)
                    {
                    _logger.LogInformation("Solnumber: {solNumber}", sol.SolNumber);
                    _logger.LogInformation("It works! {dateTime}", DateTime.Now);    
                    }
                }

                // Sets the interval of the check with Timespan, can be ms/s/m/h
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
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