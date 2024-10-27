using System;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Specializations
{
    public class SpecializationDto
    {
        public Guid Id { get; set; }
        public string Type{ get;  private set; }

        public string Description { get;  private set; }

        public SpecializationDto(Guid id,string type, string description )
        {
            this.Id=id;
            this.Type = type;
            this.Description = description;
        }
    }
}