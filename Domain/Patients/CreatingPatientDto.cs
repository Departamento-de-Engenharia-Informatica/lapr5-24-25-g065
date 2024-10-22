using System.Collections.Generic;

namespace DDDSample1.Domain.Patients
{
    public class CreatingPatientDto
    {
       public string Firstname { get;  private set; }
        public string LastName { get;  private set; }
        public string FullName { get;  private set; }
        public string Gender { get;  private set; }
        public string Allergies{ get;  private set; }
        public string EmergencyContact { get;  private set; }
        public string DateOfBirth { get;  private set; }
        public string MedicalRecordNumber {get; private set;}
        /*public List<Appointement> AppointmentHistory { get;  private set; }*/

        public CreatingPatientDto(string firstname, string lastName,string fullName, string gender, string allergies, string emergencyContact, string dateOfBirth, string medicalRecordNumber ){
            Firstname = firstname;
            LastName = lastName;
            FullName = fullName;
            Gender = gender;
            Allergies = allergies;
            EmergencyContact = emergencyContact;
            DateOfBirth = dateOfBirth;
            MedicalRecordNumber = medicalRecordNumber;
        }

        public CreatingPatientDto(string firstname, string lastName,string fullName, string gender, string emergencyContact, string dateOfBirth, string medicalRecordNumber ){
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