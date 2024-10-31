using System;
using System.Collections.Generic;
using DDDNetCore.IRepos;
using DDDSample1.Domain.Appointments;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Users;

namespace DDDSample1.Domain.Staffs
{
    public class Staff : Entity<StaffId>, IAggregateRoot
    {
        // Properties
        public string Gender { get; private set; }
        public string Type { get; private set; }
        public string Specialization { get; private set; }
        public string Firstname { get; private set; }
        public string LastName { get; private set; }
        public string FullName { get; private set; }
        public string LicenseNumber { get; private set; }
        public UserId UserId { get; private set; }
        public string AvailabilitySlot { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }
        public List<Appointment> Appointments { get; private set; } = new List<Appointment>(); // Navigation property

        // Parameterless constructor for EF Core
        private Staff() { }

        // Constructor with required fields
        public Staff(string firstname, string lastName, string fullName, string gender, string specialization,
                     string type, string licenseNumber, UserId userId, string availabilitySlot, string phoneNumber, string email)
            : this() // Call parameterless constructor
        {
            Id = new StaffId(Guid.NewGuid());
            Firstname = firstname ?? throw new ArgumentNullException(nameof(firstname));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
            Gender = gender ?? throw new ArgumentNullException(nameof(gender));
            Specialization = specialization ?? throw new ArgumentNullException(nameof(specialization));
            Type = type ?? throw new ArgumentNullException(nameof(type));
            LicenseNumber = licenseNumber ?? throw new ArgumentNullException(nameof(licenseNumber));
            UserId = userId ?? throw new ArgumentNullException(nameof(userId));
            AvailabilitySlot = availabilitySlot ?? throw new ArgumentNullException(nameof(availabilitySlot));
            PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
            Email = email ?? throw new ArgumentNullException(nameof(email));
        }

        // Change methods for updating staff properties
        public void ChangeFirstName(string firstname) => 
            Firstname = string.IsNullOrWhiteSpace(firstname) ? throw new ArgumentException("Firstname cannot be empty.", nameof(firstname)) : firstname;

        public void ChangeLastName(string lastName) => 
            LastName = string.IsNullOrWhiteSpace(lastName) ? throw new ArgumentException("LastName cannot be empty.", nameof(lastName)) : lastName;

        public void ChangeFullName(string fullName) => 
            FullName = string.IsNullOrWhiteSpace(fullName) ? throw new ArgumentException("FullName cannot be empty.", nameof(fullName)) : fullName;

        public void ChangeGender(string gender) => 
            Gender = string.IsNullOrWhiteSpace(gender) ? throw new ArgumentException("Gender cannot be empty.", nameof(gender)) : gender;

        public void ChangeType(string type) => 
            Type = string.IsNullOrWhiteSpace(type) ? throw new ArgumentException("Type cannot be empty.", nameof(type)) : type;

        public void ChangeLicenseNumber(string licenseNumber) => 
            LicenseNumber = string.IsNullOrWhiteSpace(licenseNumber) ? throw new ArgumentException("LicenseNumber cannot be empty.", nameof(licenseNumber)) : licenseNumber;

        public void ChangeSpecialization(string specialization) => 
            Specialization = string.IsNullOrWhiteSpace(specialization) ? throw new ArgumentException("Specialization cannot be empty.", nameof(specialization)) : specialization;

        public void ChangeAvailabilitySlot(string availabilitySlot) => 
            AvailabilitySlot = string.IsNullOrWhiteSpace(availabilitySlot) ? throw new ArgumentException("Availability slot cannot be empty.", nameof(availabilitySlot)) : availabilitySlot;

        public void ChangePhoneNumber(string phoneNumber) => 
            PhoneNumber = string.IsNullOrWhiteSpace(phoneNumber) ? throw new ArgumentException("Phone number cannot be empty.", nameof(phoneNumber)) : phoneNumber;

        public void ChangeEmail(string email) => 
            Email = string.IsNullOrWhiteSpace(email) ? throw new ArgumentException("Email cannot be empty.", nameof(email)) : email;
    }
}
