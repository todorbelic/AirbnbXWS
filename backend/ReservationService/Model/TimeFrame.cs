namespace ReservationService.Model
{
    public class TimeFrame
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TimeFrame(DateTime startDate, DateTime endDate) {
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
