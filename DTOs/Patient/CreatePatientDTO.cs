using System;
using System.Collections.Generic;

namespace DDDNetCore.DTOs.Patient
{
    public class CreatePatientDTO
    {
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public List<string>? Allergies { get; set; }
        public string EmergencyContact { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string MedicalRecordNumber { get; set; }
        public Guid UserId { get; set; } // Add UserId

        public CreatePatientDTO(string firstname, string lastName, string fullName, string gender, List<string>? allergies, string emergencyContact, DateTime? dateOfBirth, string medicalRecordNumber, Guid userId)
        {
            Firstname = firstname;
            LastName = lastName;
            FullName = fullName;
            Gender = gender;
            Allergies = allergies;
            EmergencyContact = emergencyContact;
            DateOfBirth = dateOfBirth;
            MedicalRecordNumber = medicalRecordNumber;
            UserId = userId; // Initialize UserId
        }

        public CreatePatientDTO() { }
    }
}
