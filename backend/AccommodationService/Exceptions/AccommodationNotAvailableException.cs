namespace AccommodationService.Exceptions
{
    public class AccommodationNotAvailableException : Exception
    {
        public AccommodationNotAvailableException(string message = "Accommodation is not available.") : base(message) { }

    }
}
