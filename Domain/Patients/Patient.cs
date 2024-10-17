using System;
using System.Collections.Generic;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Patients
{
    public class Patient : Entity<PatientId>, IAggregateRoot
    {
        public string Gender { get;  private set; }
        public List<string> Allergies{ get;  private set; }
        public string Firstname { get;  private set; }

        public string LastName { get;  private set; }

        public string EmergencyContact { get;  private set; }
        public string DateOfBirth { get;  private set; }
        /*public string MedicalRecord { get;  private set; }*/

        public Patient(string firstname, string lastName, string gender, Specialization specialization, string type, string licenseNumber ){
            Firstname = firstname;
            LastName = lastName;
            Gender = gender;
            Specialization = specialization;
            Type = type;
            LicenseNumber = licenseNumber;
        }
    }
}