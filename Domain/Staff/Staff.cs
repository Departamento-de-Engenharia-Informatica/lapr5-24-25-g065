using System;
using System.Collections.Generic; // Added for List
using DDDNetCore.IRepos;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Specializations;
using DDDSample1.Domain.Users;
using DDDSample1.Domain.Appointments; // Added for Appointment

namespace DDDSample1.Domain.Staffs
{
    public class Staff : Entity<StaffId>, IAggregateRoot
    {
        public string Gender { get; private set; }
        public string Type { get; private set; }
        public string Specialization { get; private set; }
        public string Firstname { get; private set; }
        public string LastName { get; private set; }
        public string FullName { get; private set; }
        public string LicenseNumber { get; private set; }
        public UserId UserId { get; private set; }
        public User User { get; private set; }
        public string AvailabilitySlot { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }

        // Added collection of Appointments
        public List<Appointment> Appointments { get; private set; }

        public Staff(string firstname, string lastName, string fullName, string gender, string specialization,
                     string type, string licenseNumber, UserId userId, string availabilitySlot, string phoneNumber, string email)
        {
            this.Id = new StaffId(Guid.NewGuid());
            this.Firstname = firstname;
            this.LastName = lastName;
            this.FullName = fullName;
            this.Gender = gender;
            this.Specialization = specialization;
            this.Type = type;
            this.LicenseNumber = licenseNumber;
            this.UserId = userId;
            this.AvailabilitySlot = availabilitySlot;
            this.PhoneNumber = phoneNumber;
            this.Email = email;
            this.Appointments = new List<Appointment>(); // Initialize list
        }

        public void ChangeFirstName(string firstname)
        {
            this.Firstname = firstname;
        }

        public void ChangeLastName(string lastName)
        {
            this.LastName = lastName;
        }

        public void ChangeFullName(string fullName)
        {
            this.FullName = fullName;
        }

        public void ChangeGender(string gender)
        {
            this.Gender = gender;
        }

        public void ChangeSpecialization(string specialization)
        {
            this.Specialization = specialization;
        }

        public void ChangeType(string type)
        {
            this.Type = type;
        }

        public void ChangeLicenseNumber(string licenseNumber)
        {
            this.LicenseNumber = licenseNumber;
        }

        public void ChangeAvailabilitySlot(string availabilitySlot)
        {
            this.AvailabilitySlot = availabilitySlot;
        }

        public void ChangePhoneNumber(string phoneNumber)
        {
            this.PhoneNumber = phoneNumber;
        }

        public void ChangeEmail(string email)
        {
            this.Email = email;
        }
    }
}
