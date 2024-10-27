using System;
using DDDSample1.Domain.Specializations;
using DDDSample1.Domain.Users;

namespace DDDNetCore.DTOs.Staff
{
    public class CreatingStaffDto
    {
        public string Gender { get; set; }  // Changed to public set
        public string Type { get; set; }    // Changed to public set
        public Guid SpecializationId { get; set; }  // Changed to public set
        public string Firstname { get; set; }  // Changed to public set
        public string LastName { get; set; }  // Changed to public set
        public string FullName { get; set; }  // Changed to public set

        public string LicenseNumber { get; set; }  // Changed to public set
        public Guid UserId { get; set; }  // Changed to public set

        public string AvailabilitySlot { get; set; }  // Changed to public set

        public string PhoneNumber { get; set; }  // Changed to public set

        public string Email { get; set; }  // Changed to public set

        // Constructor with parameters
        public CreatingStaffDto(string firstname, string lastName, string fullName, string gender, Guid specializationId, string type, string licenseNumber, Guid userId, string availabilitySlot, string phoneNumber, string email)
        {
            Firstname = firstname;
            LastName = lastName;
            FullName = fullName;
            Gender = gender;
            SpecializationId = specializationId;  // Changed parameter type to Guid
            Type = type;
            LicenseNumber = licenseNumber;
            UserId = userId;  // Changed parameter type to Guid
            AvailabilitySlot = availabilitySlot;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        // Parameterless constructor for deserialization (optional)
        public CreatingStaffDto() { }
    }
}
