using System;
using System.Collections.Generic;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Appointments;
using DDDNetCore.IRepos;

namespace DDDSample1.Domain.Patients
{
    public class Patient : Entity<PatientId>, IAggregateRoot
    {
        private string _phoneNumber; // Private field for PhoneNumber
        private string _emergencyContact; // Private field for EmergencyContact

        public string Firstname { get; private set; }
        public string LastName { get; private set; }
        public string FullName { get; private set; }
        public string Gender { get; private set; }
        public List<string>? Allergies { get; private set; }
        
        // PhoneNumber property with validation
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                if (!IsValidPhoneNumber(value))
                    throw new ArgumentException("Phone must be a 9-digit number.");
                _phoneNumber = value;
            }
        }

        // EmergencyContact property with validation
        public string EmergencyContact
        {
            get => _emergencyContact;
            set
            {
                if (!IsValidEmergencyContact(value))
                    throw new ArgumentException("Emergency contact must be a valid phone number.");
                _emergencyContact = value;
            }
        }

        public DateTime? DateOfBirth { get; private set; }
        public string MedicalRecordNumber { get; private set; }
        public Guid UserId { get; private set; } // Added UserId
        public List<Appointment> AppointmentHistory { get; private set; }

        public Patient(string firstname, string lastName, string fullName, string gender, List<string>? allergies, 
                       string emergencyContact, DateTime? dateOfBirth, string medicalRecordNumber, Guid userId, string phoneNumber)
        {
            Id = new PatientId(Guid.NewGuid()); // Use the entity's Id
            Firstname = firstname ?? throw new ArgumentException("Firstname cannot be null");
            LastName = lastName ?? throw new ArgumentException("LastName cannot be null");
            FullName = fullName ?? throw new ArgumentException("FullName cannot be null");
            Gender = gender ?? throw new ArgumentException("Gender cannot be null");
            Allergies = allergies != null ? new List<string>(allergies) : null; // Ensure read-only collection
            
            EmergencyContact = emergencyContact ?? throw new ArgumentException("EmergencyContact cannot be null");
            PhoneNumber = phoneNumber ?? throw new ArgumentException("PhoneNumber cannot be null"); // Initialize PhoneNumber
            DateOfBirth = dateOfBirth;
            MedicalRecordNumber = medicalRecordNumber ?? throw new ArgumentException("MedicalRecordNumber cannot be null");
            UserId = userId; // Initialize UserId
            AppointmentHistory = new List<Appointment>();
        }

        // Method to add an appointment to the history
        public void AddAppointment(Appointment appointment)
        {
            if (appointment == null) throw new ArgumentException("Appointment cannot be null");
            AppointmentHistory.Add(appointment);
        }

        // Method to remove an appointment from the history
        public void RemoveAppointment(Appointment appointment)
        {
            if (appointment == null) throw new ArgumentException("Appointment cannot be null");
            AppointmentHistory.Remove(appointment);
        }

        // Update patient details
        internal void Update(string firstname, string lastName, string fullName, string gender, 
                             List<string>? allergies, string emergencyContact, DateTime? dateOfBirth, 
                             string medicalRecordNumber, Guid userId, string phoneNumber)
        {
            Firstname = firstname ?? throw new ArgumentException("Firstname cannot be null");
            LastName = lastName ?? throw new ArgumentException("LastName cannot be null");
            FullName = fullName ?? throw new ArgumentException("FullName cannot be null");
            Gender = gender ?? throw new ArgumentException("Gender cannot be null");
            Allergies = allergies != null ? new List<string>(allergies) : null; // Update to read-only
            
            EmergencyContact = emergencyContact ?? throw new ArgumentException("EmergencyContact cannot be null");
            PhoneNumber = phoneNumber ?? throw new ArgumentException("PhoneNumber cannot be null"); // Update PhoneNumber
            DateOfBirth = dateOfBirth;
            MedicalRecordNumber = medicalRecordNumber ?? throw new ArgumentException("MedicalRecordNumber cannot be null");
            UserId = userId; // Update UserId directly
        }

        // Validation methods
        private bool IsValidPhoneNumber(string phone)
        {
            return phone.Length == 9 && long.TryParse(phone, out _); // Ensure it's a 9-digit number
        }

        private bool IsValidEmergencyContact(string contact)
        {
            return IsValidPhoneNumber(contact); // Assuming emergency contact is also a phone number
        }
    }
}
