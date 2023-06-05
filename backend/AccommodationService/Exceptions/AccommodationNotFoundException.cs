namespace AccommodationService.Exceptions
{
    public class AccommodationNotFoundException : Exception
    {
        public AccommodationNotFoundException(string message = "Accommodation not found") : base(message) { }
    }
}
