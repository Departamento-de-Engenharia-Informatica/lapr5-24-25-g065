using System;
using System.Collections.Generic;

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
        public Guid UserId { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; } // Added Email property

        public PatientDto(Guid id, string firstname, string lastName, string fullName, string gender, List<string>? allergies,
                          string emergencyContact, DateTime? dateOfBirth, string medicalRecordNumber,
                          Guid userId, string phoneNumber, string email)
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
            UserId = userId;
            PhoneNumber = phoneNumber;
            Email = email ?? throw new ArgumentException("Email cannot be null"); // Initialize Email
        }
    }
}
