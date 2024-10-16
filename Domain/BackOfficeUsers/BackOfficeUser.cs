using System;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Specializations;

namespace DDDSample1.Domain.BackOfficeUsers
{
    public class BackOfficeUser : Entity<BackOfficeUserId>, IAggregateRoot
    {
        public string Gender { get;  private set; }
        public string Type{ get;  private set; }

        public Specialization Specialization { get;  private set; }
        public string Firstname { get;  private set; }

        public string LastName { get;  private set; }

        public string LicenseNumber { get;  private set; }

        public BackOfficeUser(string firstname, string lastName, string gender, Specialization specialization, string type, string licenseNumber ){
            Firstname = firstname;
            LastName = lastName;
            Gender = gender;
            Specialization = specialization;
            Type = type;
            LicenseNumber = licenseNumber;
        }
    }
}