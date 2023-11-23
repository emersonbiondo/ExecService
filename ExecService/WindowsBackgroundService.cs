namespace ExecService
{
    public class WindowsBackgroundService : BackgroundService
    {
        private readonly ILogger<WindowsBackgroundService> logger;
        private ExecCommand? execCommand;
        private Service service;

        public WindowsBackgroundService(Service service, ILogger<WindowsBackgroundService> logger, IConfiguration configuration)
        {
            execCommand = configuration.GetSection("ExecCommand").Get<ExecCommand>();
            this.service = service;
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    logger.LogInformation("Running...");
                    await service.Execute(execCommand != null ? execCommand : new ExecCommand());
                }
            }
            catch (TaskCanceledException)
            {
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Error Execute");
                Environment.Exit(1);
            }
        }
    }
}
