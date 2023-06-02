using UserService.Attributes;

namespace UserService.Model
{
    [BsonCollection("users")]
    public class AppUser : Document
    {
        public AppUser()
        {
        }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Address? Address { get; set; }
        public string Role { get; set; }

        
    }
}