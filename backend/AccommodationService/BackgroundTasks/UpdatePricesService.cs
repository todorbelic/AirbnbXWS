namespace AccommodationService.BackgroundTasks
{
    public class UpdatePricesService
    {
        private readonly ILogger<UpdatePricesService> _logger;

        public UpdatePricesService(ILogger<UpdatePricesService> logger)
        {
            _logger = logger;
        }

        public async Task DoSomethingAsync()
        {
            await Task.Delay(100);
            _logger.LogInformation(
                "Sample Service did something.");
        }
    }
}
