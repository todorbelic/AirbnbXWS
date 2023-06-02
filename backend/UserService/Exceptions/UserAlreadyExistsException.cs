namespace UserService.Exceptions
{
    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException(string message="User with that username already exists") : base(message) { }
    }
}
