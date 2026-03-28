using NotifyHub.Api.Source.Domain.Enums;

namespace NotifyHub.Api.Source.Domain.Entities
{
    public class Stack
    {
        public Guid StackID { get; set; }

        public Guid CallID { get; set; }

        public long StkNbr { get; set; }
        public String Location { get; set; }
        public StackStatus Status { get; set; }
        public DateTime CreatedDttm { get; set; }
        public DateTime UpdatedDttm { get; set; }
        //public Call Call { get; set; }
    }
}
