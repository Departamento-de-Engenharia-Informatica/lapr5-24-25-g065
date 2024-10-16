using DDDSample1.Domain.Specializations;

namespace DDDSample1.Domain.BackOfficeUsers
{
    public class CreatingBackOfficeUserDto
    {
        public string Gender { get;  private set; }
        public string Type{ get;  private set; }

        public Specialization Specialization { get;  private set; }
        public string Firstname { get;  private set; }

        public string LastName { get;  private set; }

        public string LicenseNumber { get;  private set; }


        public CreatingBackOfficeUserDto(string firstname, string lastName, string gender, Specialization specialization, string type, string licenseNumber )
        {
            this.Firstname = firstname;
            this.LastName = lastName;
            this.Gender = gender;
            this.Specialization = specialization;
            this.Type = type;
            this.LicenseNumber = licenseNumber;
        }
    }
}