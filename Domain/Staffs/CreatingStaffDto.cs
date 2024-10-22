using DDDSample1.Domain.Specializations;

namespace DDDSample1.Domain.Staffs
{
    public class CreatingStaffDto
    {
        public string Gender { get;  private set; }
        public string Type{ get;  private set; }
        public SpecializationId SpecializationId { get;  private set; }
        public string Firstname { get;  private set; }
        public string LastName { get;  private set; }
        public string FullName { get;  private set; }

        public string LicenseNumber { get;  private set; }

        public CreatingStaffDto(string firstname, string lastName,string fullName, string gender, SpecializationId specializationId, string type, string licenseNumber ){
            Firstname = firstname;
            LastName = lastName;
            FullName = fullName;
            Gender = gender;
            SpecializationId = specializationId;
            Type = type;
            LicenseNumber = licenseNumber;
        }
    }
}