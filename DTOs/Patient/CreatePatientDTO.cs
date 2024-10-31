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
        public Guid UserId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; } // Added Email property

        public CreatePatientDTO(string firstname, string lastName, string fullName, string gender, List<string>? allergies,
                                string emergencyContact, DateTime? dateOfBirth, string medicalRecordNumber,
                                Guid userId, string phoneNumber, string email)
        {
            Firstname = firstname;
            LastName = lastName;
            FullName = fullName;
            Gender = gender;
            Allergies = allergies;
            EmergencyContact = emergencyContact;
            DateOfBirth = dateOfBirth;
            MedicalRecordNumber = medicalRecordNumber;
            UserId = userId;
            PhoneNumber = phoneNumber;
            Email = email ?? throw new ArgumentException("Email cannot be null"); // Initialize Email
        }

        public CreatePatientDTO() { }
    }
}
