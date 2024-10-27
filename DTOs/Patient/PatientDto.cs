using System;
using System.Collections.Generic;
using DDDSample1.Domain.Shared;

namespace DDDNetCore.DTOs.Patient
{
    public class PatientDto
    {
        public Guid Id { get; set; }
        public string Firstname { get; private set; }
        public string LastName { get; private set; }
        public string FullName { get; private set; }
        public string Gender { get; private set; }
        public List<string>? Allergies { get; private set; }
        public string EmergencyContact { get; private set; }
        public DateTime? DateOfBirth { get; private set; }
        public string MedicalRecordNumber { get; private set; }
        public List<Appointement> AppointmentHistory { get;  private set; }*/

        public PatientDto(Guid id, string firstname, string lastName, string fullName, string gender, List<string>? allergies, string emergencyContact, DateTime? dateOfBirth, string medicalRecordNumber)
        {
            Id = id;
            Firstname = firstname;
            LastName = lastName;
            FullName = fullName;
            Gender = gender;
            Allergies = allergies;
            EmergencyContact = emergencyContact;
            DateOfBirth = dateOfBirth;
            MedicalRecordNumber = medicalRecordNumber;
        }

        public PatientDto(Guid id, string firstname, string lastName, string fullName, string gender, string emergencyContact, DateTime? dateOfBirth, string medicalRecordNumber)
        {
            Id = id;
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