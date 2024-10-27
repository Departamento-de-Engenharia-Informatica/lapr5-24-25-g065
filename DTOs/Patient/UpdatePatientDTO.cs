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
        public List<string>? Allergies { get; set; } // Changed to List<string>
        public string EmergencyContact { get; set; }
        public DateTime? DateOfBirth { get; set; } // Changed to nullable DateTime
        public string MedicalRecordNumber { get; set; }
    }
}
