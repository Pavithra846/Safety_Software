using NotifyHub.Api.Source.Domain.Enums;

namespace NotifyHub.Api.Source.Domain.Entities
{
    public class Users
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
    }
}
