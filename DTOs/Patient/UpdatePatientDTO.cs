using System;
using System.Collections.Generic;

namespace DDDNetCore.DTOs.Patient
{
    public class UpdatePatientDTO
    {
        public Guid Id { get; set; } // Patient ID
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public List<string>? Allergies { get; set; } // Nullable List for flexibility
        public string EmergencyContact { get; set; }
        public DateTime? DateOfBirth { get; set; } // Nullable DateTime for flexibility
        public string MedicalRecordNumber { get; set; }

        // Fields from User class
        public string UserName { get; set; }
        public string Email { get; set; }
        public Guid UserId { get; set; } // Add UserId property

        // Optional: Add properties for tracking additional verification if needed
        public bool EmailVerificationRequired { get; set; }
    }
}
