using System;
using System.Collections.Generic;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Appointments;
using DDDNetCore.IRepos;

namespace DDDSample1.Domain.Patients
{
    public class Patient : Entity<PatientId>, IAggregateRoot
    {
        public string Firstname { get; private set; }
        public string LastName { get; private set; }
        public string FullName { get; private set; }
        public string Gender { get; private set; }
        public List<string>? Allergies { get; private set; }
        public string EmergencyContact { get; private set; }
        public DateTime? DateOfBirth { get; private set; }
        public string MedicalRecordNumber { get; private set; }
        public Guid UserId { get; private set; } // Added UserId
        public List<Appointment> AppointmentHistory { get; private set; }

        public Patient(string firstname, string lastName, string fullName, string gender, List<string>? allergies, 
                       string emergencyContact, DateTime? dateOfBirth, string medicalRecordNumber, Guid userId)
        {
            Id = new PatientId(Guid.NewGuid()); // Use the entity's Id
            Firstname = firstname ?? throw new ArgumentException("Firstname cannot be null");
            LastName = lastName ?? throw new ArgumentException("LastName cannot be null");
            FullName = fullName ?? throw new ArgumentException("FullName cannot be null");
            Gender = gender ?? throw new ArgumentException("Gender cannot be null");
            Allergies = allergies;
            EmergencyContact = emergencyContact ?? throw new ArgumentException("EmergencyContact cannot be null");
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

        // Method to change UserId if necessary
        public void ChangeUserId(Guid userId)
        {
            UserId = userId; 
        }

        // Update patient details
        internal void Update(string firstname, string lastName, string fullName, string gender, 
                             List<string>? allergies, string emergencyContact, DateTime? dateOfBirth, 
                             string medicalRecordNumber, Guid userId)
        {
            Firstname = firstname ?? throw new ArgumentException("Firstname cannot be null");
            LastName = lastName ?? throw new ArgumentException("LastName cannot be null");
            FullName = fullName ?? throw new ArgumentException("FullName cannot be null");
            Gender = gender ?? throw new ArgumentException("Gender cannot be null");
            Allergies = allergies; // Assuming allergies can be null
            EmergencyContact = emergencyContact ?? throw new ArgumentException("EmergencyContact cannot be null");
            DateOfBirth = dateOfBirth;
            MedicalRecordNumber = medicalRecordNumber ?? throw new ArgumentException("MedicalRecordNumber cannot be null");
            UserId = userId; // Update UserId
        }
    }
}
