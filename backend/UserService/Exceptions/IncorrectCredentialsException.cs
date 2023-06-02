namespace UserService.Exceptions
{
    public class IncorrectCredentialsException : Exception
    {
        public IncorrectCredentialsException(string message = "Incorrect credentials") : base(message) { }
    }
}
