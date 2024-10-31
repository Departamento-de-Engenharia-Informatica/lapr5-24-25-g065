using System;

namespace DDDNetCore.DTOs.Staff
{
    public class CreatingStaffDto
    {
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Specialization { get; set; }
        public string Type { get; set; }
        public string LicenseNumber { get; set; }
        public Guid UserId { get; set; }
        public string AvailabilitySlot { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        // Constructor with parameters
        public CreatingStaffDto(string firstname, string lastName, string fullName, string gender, string specialization, string type, string licenseNumber, Guid userId, string availabilitySlot, string phoneNumber, string email)
        {
            Firstname = firstname;
            LastName = lastName;
            FullName = fullName;
            Gender = gender;
            Specialization = specialization; // Ensure this remains a string
            Type = type;
            LicenseNumber = licenseNumber;
            UserId = userId; // Guid is appropriate
            AvailabilitySlot = availabilitySlot;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        // Parameterless constructor for deserialization (optional)
        public CreatingStaffDto() { }
    }
}
