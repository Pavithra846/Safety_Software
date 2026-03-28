namespace NotifyHub.Api.Source.Application.Common.Models
{
    public class NotificationMessage
    {
        public string Type { get; set; }   // "CallCreated", "CallUpdated"
        public object Data { get; set; }   // actual payload
    }
}
