using System;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Specializations
{
    public class SpecializationDto
    {
        public string Type{ get;  private set; }

        public string Description { get;  private set; }

        public SpecializationDto(string type, string description )
        {
            this.Type = type;
            this.Description = description;
        }
    }
}