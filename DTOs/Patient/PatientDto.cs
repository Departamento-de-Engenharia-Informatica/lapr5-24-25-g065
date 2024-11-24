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
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; } // Added Email property

       public PatientDto(Guid id, string firstname, string lastName, string fullName, string gender, List<string>? allergies,
                  string emergencyContact, DateTime? dateOfBirth, string medicalRecordNumber,
                  string phoneNumber, string email)
{
    Id = id;
    Firstname = firstname ?? throw new ArgumentException("Firstname cannot be null");
    LastName = lastName ?? throw new ArgumentException("LastName cannot be null");
    FullName = fullName ?? throw new ArgumentException("FullName cannot be null");
    Gender = gender ?? throw new ArgumentException("Gender cannot be null");
    Allergies = allergies ?? new List<string>();
    EmergencyContact = emergencyContact ?? throw new ArgumentException("EmergencyContact cannot be null");
    DateOfBirth = dateOfBirth;
    MedicalRecordNumber = medicalRecordNumber ?? throw new ArgumentException("MedicalRecordNumber cannot be null");
    PhoneNumber = phoneNumber ?? throw new ArgumentException("PhoneNumber cannot be null");
    Email = email ?? throw new ArgumentException("Email cannot be null");
}

    }
}
