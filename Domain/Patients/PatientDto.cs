using System;
using System.Collections.Generic;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Patients
{
    public class PatientDto
    {
       public string Firstname { get;  private set; }
        public string LastName { get;  private set; }
        public string FullName { get;  private set; }
        public string Gender { get;  private set; }
        public List<string> Allergies{ get;  private set; }
        public string EmergencyContact { get;  private set; }
        public string DateOfBirth { get;  private set; }
        public string MedicalRecordNumber {get; private set;}
        /*public List<Appointement> AppointmentHistory { get;  private set; }*/

        public PatientDto(string firstname, string lastName,string fullName, string gender, List<String> allergies, string emergencyContact, string dateOfBirth, string medicalRecordNumber ){
            Firstname = firstname;
            LastName = lastName;
            FullName = fullName;
            Gender = gender;
            Allergies = allergies;
            EmergencyContact = emergencyContact;
            DateOfBirth = dateOfBirth;
            MedicalRecordNumber = medicalRecordNumber;
        }

        public PatientDto(string firstname, string lastName,string fullName, string gender, string emergencyContact, string dateOfBirth, string medicalRecordNumber ){
            Firstname = firstname;
            LastName = lastName;
            FullName = fullName;
            Gender = gender;
            EmergencyContact = emergencyContact;
            DateOfBirth = dateOfBirth;
            MedicalRecordNumber = medicalRecordNumber;
        }
    }
}