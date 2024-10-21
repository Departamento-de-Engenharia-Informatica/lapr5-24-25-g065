using System;
using System.Collections.Generic;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Patients
{
    public class Patient : Entity<PatientId>, IAggregateRoot
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

        public Patient(string firstname, string lastName,string fullName, string gender, List<String> allergies, string emergencyContact, string dateOfBirth, string medicalRecordNumber ){
            Firstname = firstname;
            LastName = lastName;
            FullName = fullName;
            Gender = gender;
            Allergies = allergies;
            EmergencyContact = emergencyContact;
            DateOfBirth = dateOfBirth;
            MedicalRecordNumber = medicalRecordNumber;
        }

        public Patient(string firstname, string lastName,string fullName, string gender, string emergencyContact, string dateOfBirth, string medicalRecordNumber ){
            Firstname = firstname;
            LastName = lastName;
            FullName = fullName;
            Gender = gender;
            EmergencyContact = emergencyContact;
            DateOfBirth = dateOfBirth;
            MedicalRecordNumber = medicalRecordNumber;
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
        public void ChangeEmergencyContact(string emergencyContact){
            this.EmergencyContact = emergencyContact;
        }
        public void ChangeAllergies(List<string> allergies){
            this.Allergies = allergies;
        }
        public void ChangeDateOfBirth(string dateOfBirth){
            this.DateOfBirth = dateOfBirth;
        }

        public void ChangeMedicalRecordNumber(string medicalRecordNumber){
            this.MedicalRecordNumber=medicalRecordNumber;
        }
    }
}