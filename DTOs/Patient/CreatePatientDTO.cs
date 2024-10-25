using System.Collections.Generic;

namespace DDDNetCore.DTOs.Patient
{
    public class CreatePatientDTO
    {
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Allergies { get; set; }
        public string EmergencyContact { get; private set; }
        public string DateOfBirth { get; set; }
        public string MedicalRecordNumber { get; set; }
        /*public List<Appointement> AppointmentHistory { get;  private set; }*/

        // public CreatingPatientDto(string firstname, string lastName,string fullName, string gender, string allergies, string emergencyContact, string dateOfBirth, string medicalRecordNumber ){
        //     Firstname = firstname;
        //     LastName = lastName;
        //     FullName = fullName;
        //     Gender = gender;
        //     Allergies = allergies;
        //     EmergencyContact = emergencyContact;
        //     DateOfBirth = dateOfBirth;
        //     MedicalRecordNumber = medicalRecordNumber;
        // }

        // public CreatingPatientDto(string firstname, string lastName,string fullName, string gender, string emergencyContact, string dateOfBirth, string medicalRecordNumber ){
        //     Firstname = firstname;
        //     LastName = lastName;
        //     FullName = fullName;
        //     Gender = gender;
        //     EmergencyContact = emergencyContact;
        //     DateOfBirth = dateOfBirth;
        //     MedicalRecordNumber = medicalRecordNumber;
        // }
    }
}