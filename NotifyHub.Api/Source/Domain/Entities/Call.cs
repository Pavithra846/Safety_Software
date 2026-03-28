using NotifyHub.Api.Source.Domain.Enums;

namespace NotifyHub.Api.Source.Domain.Entities
{
    public class Call
    {
        public Guid CallID { get; set; }
        public string Location { get; set; }
        public string LandMark { get; set; }
        public CallType Type { get; set; }              // ✅ Instead of IsPolice/IsFire/IsEMS
        public string Name { get; set; }
        public string Comments { get; set; }
        public DateTime CreatedDttm { get; set; }
        public DateTime UpdatedDttm { get; set; }
        public CallStatus Status { get; set; }         // ✅ Instead of IsFinished
    }
}
