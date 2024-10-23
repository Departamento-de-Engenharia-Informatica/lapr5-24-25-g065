using System;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Specializations;
using DDDSample1.Domain.Users;

namespace DDDSample1.Domain.Staffs
{
    public class Staff : Entity<StaffId>, IAggregateRoot
    {
        public string Gender { get;  private set; }
        public string Type{ get;  private set; }

        public SpecializationId SpecializationId { get;  private set; }

        public Specialization Specialization { get; private set; }

        public string Firstname { get;  private set; }

        public string LastName { get;  private set; }
        public string FullName { get;  private set; }

        public string LicenseNumber { get;  private set; }

         public UserId UserId { get;  private set; }

        public User User { get; private set; }

        public Staff(string firstname, string lastName,string fullName, string gender, SpecializationId specializationId, string type, string licenseNumber, UserId userId ){
            this.Id = new StaffId(Guid.NewGuid());
            this.Firstname = firstname;
            this.LastName = lastName;
            this.FullName = fullName;
            this.Gender = gender;
            this.SpecializationId = specializationId;
            this.Type = type;
            this.LicenseNumber = licenseNumber;
            this.UserId = userId;
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
        public void ChangeSpecialization(SpecializationId specializationId){
            this.SpecializationId = specializationId;
        }
        public void ChangeType(string type){
            this.Type = type;
        }
        public void ChangeLicenseNumber(string licenseNumber){
            this.LicenseNumber = licenseNumber;
        }

    }
}