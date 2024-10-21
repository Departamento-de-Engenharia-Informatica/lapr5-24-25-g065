using System;
using System.Collections.Generic;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Patients
{
    public class PatientDto
    {
        public Guid Id { get; set; }
       public string Firstname { get;  private set; }
        public string LastName { get;  private set; }
        public string FullName { get;  private set; }
        public string Gender { get;  private set; }
        public List<string> Allergies{ get;  private set; }
        public string EmergencyContact { get;  private set; }
        public string DateOfBirth { get;  private set; }
        public string MedicalRecordNumber {get; private set;}
        /*public List<Appointement> AppointmentHistory { get;  private set; }*/

        public PatientDto(Guid id,string firstname, string lastName,string fullName, string gender, List<string> allergies, string emergencyContact, string dateOfBirth, string medicalRecordNumber ){
            this.Id=id;
            this.Firstname = firstname;
            this.LastName = lastName;
            this.FullName = fullName;
            this.Gender = gender;
            this.Allergies = allergies;
            this.EmergencyContact = emergencyContact;
            this.DateOfBirth = dateOfBirth;
            this.MedicalRecordNumber = medicalRecordNumber;
        }

        public PatientDto(Guid id,string firstname, string lastName,string fullName, string gender, string emergencyContact, string dateOfBirth, string medicalRecordNumber ){
            this.Id = id;
            this.Firstname = firstname;
            this.LastName = lastName;
            this.FullName = fullName;
            this.Gender = gender;
            this.EmergencyContact = emergencyContact;
            this.DateOfBirth = dateOfBirth;
            this.MedicalRecordNumber = medicalRecordNumber;
        }
    }
}