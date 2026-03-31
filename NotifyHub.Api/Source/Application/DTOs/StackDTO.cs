using NotifyHub.Api.Source.Domain.Enums;

namespace NotifyHub.Api.Source.Application.DTOs
{
    public class CreateStackDto
    {
        public Guid CallID { get; set; }

        public long StkNbr { get; set; }
        public String Location { get; set; }
        public StackStatus Status { get; set; }
        public DateTime CreatedDttm { get; set; }
        public DateTime UpdatedDttm { get; set; }
    }
    public class UpdateStackDto
    {
        public String Location { get; set; }
        public StackStatus Status { get; set; }
        public DateTime CreatedDttm { get; set; }
        public DateTime UpdatedDttm { get; set; }
    }
    public class StackResponseDto
    {
        public Guid StackID { get; set; }
        public Guid CallID { get; set; }

        public long StkNbr { get; set; }
        public String Location { get; set; }
        public StackStatus Status { get; set; }
        public DateTime CreatedDttm { get; set; }
        public DateTime UpdatedDttm { get; set; }
    }
}
