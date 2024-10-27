

namespace DDDSample1.Domain.Specializations
{
    public class CreateSpecializationDto
    {

        public string Type{ get;  private set; }

        public string Description { get;  private set; }

        public CreateSpecializationDto(string type, string description )
        {
            this.Type = type;
            this.Description = description;
        }
    }
}