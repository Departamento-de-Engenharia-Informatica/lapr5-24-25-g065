using System;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Specializations;

namespace DDDSample1.Domain.Staffs
{
    public class Staff : Entity<StaffId>, IAggregateRoot
    {
        public string Gender { get;  private set; }
        public string Type{ get;  private set; }

        public Specialization Specialization { get;  private set; }
        public string Firstname { get;  private set; }

        public string LastName { get;  private set; }
        public string FullName { get;  private set; }

        public string LicenseNumber { get;  private set; }

        public Staff(string firstname, string lastName,string fullName, string gender, Specialization specialization, string type, string licenseNumber ){
            this.Id = new StaffId(Guid.NewGuid());
            this.Firstname = firstname;
            this.LastName = lastName;
            this.FullName = fullName;
            this.Gender = gender;
            this.Specialization = specialization;
            this.Type = type;
            this.LicenseNumber = licenseNumber;
        }

        public void ChangeFirstName(string firstname){
            this.Firstname = firstname;
        }
        public void ChangeLastName(string lastName){
            this.LastName = lastName;
        }
        public void ChangeFullName(string fullName){
            this.FullName = fullName;
        }
        public void ChangeGender(string gender){
            this.Gender = gender;
        }
        public void ChangeSpecialization(Specialization specialization){
            this.Specialization = specialization;
        }
        public void ChangeType(string type){
            this.Type = type;
        }
        public void ChangeLicenseNumber(string licenseNumber){
            this.LicenseNumber = licenseNumber;
        }

    }
}