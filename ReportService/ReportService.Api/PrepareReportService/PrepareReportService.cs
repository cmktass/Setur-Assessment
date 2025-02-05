
namespace ReportService.Api.PrepareReportBackgroundService
{
    public class PrepareReportService : BackgroundService
    {
        private readonly ILogger<PrepareReportService> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        public PrepareReportService(ILogger<PrepareReportService> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("MyBackgroundService başlatıldı.");

            // Servis iptal edilene kadar sürekli çalışacak döngü
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var _reportDbContext = scope.ServiceProvider.GetRequiredService<ReportDbContext>();
                    var result = await _reportDbContext.Reports.Where(x => !x.IsDeleted && x.ReportState == "Preparing").ToListAsync();
                    if (result.Any())
                    {
                        result.ForEach(report =>
                        {
                            report.ReportState = "Completed";
                        });
                        await _reportDbContext.SaveChangesAsync();
                    }
                }
                _logger.LogInformation("Arka plan işlemi çalışıyor: {Time}", DateTimeOffset.Now);
                await Task.Delay(TimeSpan.FromSeconds(100), stoppingToken);
            }
            _logger.LogInformation("MyBackgroundService durduruluyor.");
        }
    }
}
