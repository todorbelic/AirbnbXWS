using AccommodationService.Enums;
using AccommodationService.Model;
using AccommodationService.Repository;

namespace AccommodationService.BackgroundTasks
{
    public class UpdatePricesService: BackgroundService
    {
        private readonly ILogger<UpdatePricesService> _logger;
        private readonly IServiceProvider serviceProvider;


        public UpdatePricesService(ILogger<UpdatePricesService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            this.serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Background service is running.");
                
                using(var scope = serviceProvider.CreateScope())
                {
                    var repository = scope.ServiceProvider.GetRequiredService<IRepository<AppAccommodation>>();
                    foreach (AppAccommodation accommodation in repository.GetAll())
                    {
                        if (accommodation.Occasions != null)
                        {
                            DateTime now = DateTime.Now;
                            List<int> months = accommodation.Occasions[0];
                            List<int> days = accommodation.Occasions[1];
                            List<int> holidays = accommodation.Occasions[2];

                            foreach (int month in months)
                            {
                                if (now.Month == month) accommodation.CurrentPrice = accommodation.SpecialPrice;
                                else accommodation.CurrentPrice = accommodation.BasePrice;
                            }

                            foreach (int day in days)
                            {
                                if ((int)now.DayOfWeek == day) accommodation.CurrentPrice = accommodation.SpecialPrice;
                                else accommodation.CurrentPrice = accommodation.BasePrice;
                            }
                            foreach (int holiday in holidays)
                            {
                                HolidaysEnum holidayEnum = HolidaysEnum.FindHoliday(holiday);
                                DateTime holidayStart = new DateTime(now.Year, holidayEnum.StartDate.Month, holidayEnum.StartDate.Day);
                                DateTime holidayEnd = new DateTime(now.Year, holidayEnum.EndDate.Month, holidayEnum.EndDate.Day);

                                if (DateTime.Compare(now, holidayStart) >= 0 && DateTime.Compare(now, holidayEnd) <= 0)
                                {
                                    accommodation.CurrentPrice = accommodation.SpecialPrice;
                                }
                                else accommodation.CurrentPrice = accommodation.BasePrice;

                            }
                        }

                    }
                
                }


                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken); // Delay for 5 seconds
            }
        }
    }
}
