using NotifyHub.Api.Source.Domain.Enums;

namespace NotifyHub.Api.Source.Application.DTOs
{
    public class CreateUnitDTO
    {
        public string UnitName { get; set; }

        public UnitType UnitType { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public bool IsActive { get; set; }

        public bool IsAvail { get; set; }
    }
    public class UpdateUnitDTO
    {
        public long? StkNbr { get; set; }

        public DateTime UpdatedOn { get; set; }

        public bool IsActive { get; set; }

        public bool IsAvail { get; set; }
        public bool IsFinished { get; set; }
    }
    public class UnitResponseDTO
    {
        public Guid UnitID { get; set; }

        public string UnitName { get; set; }

        public long? StkNbr { get; set; }

        public UnitType UnitType { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public bool IsActive { get; set; }

        public bool IsAvail { get; set; }
        public bool IsFinished { get; set; }
    }
}
