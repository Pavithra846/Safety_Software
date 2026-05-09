namespace NotifyHub.Api.Source.Domain.Entities
{
    public class Notification
    {
        public Guid NotifyId { get; set; }
        public string Message { get; set; }
        public Guid UserId { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
