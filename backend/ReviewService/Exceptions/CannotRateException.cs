namespace ReviewService.Exceptions
{
    public class CannotRateException : Exception
    {
        public CannotRateException(string message = "Cannot rate this!") : base(message) { }
    }
}
