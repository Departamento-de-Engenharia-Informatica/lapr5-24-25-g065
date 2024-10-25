

namespace DDDSample1.Domain.Specializations
{
    public class CreatingSpecializationDto
    {

        public string Type{ get;  private set; }

        public string Description { get;  private set; }

        public CreatingSpecializationDto(string type, string description )
        {
            this.Type = type;
            this.Description = description;
        }
    }
}