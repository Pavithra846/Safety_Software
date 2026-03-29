using NotifyHub.Api.Source.Domain.Enums;

namespace NotifyHub.Api.Source.Application.DTOs
{
    public class CreateCallDTO
    {
        public string Location { get; set; }
        public string LandMark { get; set; }
        public CallType Type { get; set; }              // ✅ Instead of IsPolice/IsFire/IsEMS
        public string Name { get; set; }
        public string Comments { get; set; }
        public DateTime CreatedDttm { get; set; }
        public DateTime UpdatedDttm { get; set; }
        public CallStatus Status { get; set; }
    }

    public class UpdateCallDTO
    {
        public DateTime UpdatedDttm { get; set; }
        public CallStatus Status { get; set; }
        public string Comments { get; set; }
    }
    public class CallResponseDTO
    {
        public Guid CallID { get; set; }
        public string Location { get; set; }
        public string LandMark { get; set; }
        public CallType Type { get; set; }              // ✅ Instead of IsPolice/IsFire/IsEMS
        public string Name { get; set; }
        public string Comments { get; set; }
        public DateTime CreatedDttm { get; set; }
        public DateTime UpdatedDttm { get; set; }
        public CallStatus Status { get; set; }
    }

}

