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
        public DateTime? DateOfBirth { get; private set; }
        public string MedicalRecordNumber { get; private set; }
        public string Email { get; private set; } // New Email property
        public List<Appointment> AppointmentHistory { get; private set; } = new List<Appointment>(); // Renamed Navigation property

        // Properties without extra validation
        public string PhoneNumber { get; set; }
        public string EmergencyContact { get; set; }

        public Patient(string firstname, string lastName, string fullName, string gender, List<string>? allergies,
                       string emergencyContact, DateTime? dateOfBirth, string medicalRecordNumber,
                       string phoneNumber, string email)
        {
            Id = new PatientId(Guid.NewGuid());
            Firstname = firstname ?? throw new ArgumentException("Firstname cannot be null");
            LastName = lastName ?? throw new ArgumentException("LastName cannot be null");
            FullName = fullName ?? throw new ArgumentException("FullName cannot be null");
            Gender = gender ?? throw new ArgumentException("Gender cannot be null");
            Allergies = allergies != null ? new List<string>(allergies) : null;

            EmergencyContact = emergencyContact ?? throw new ArgumentException("EmergencyContact cannot be null");
            PhoneNumber = phoneNumber ?? throw new ArgumentException("PhoneNumber cannot be null");
            DateOfBirth = dateOfBirth;
            MedicalRecordNumber = medicalRecordNumber ?? throw new ArgumentException("MedicalRecordNumber cannot be null");
            Email = email ?? throw new ArgumentException("Email cannot be null");
        }

        public void AddAppointment(Appointment appointment)
        {
            if (appointment == null) throw new ArgumentException("Appointment cannot be null");
            AppointmentHistory.Add(appointment);
        }

        public void RemoveAppointment(Appointment appointment)
        {
            if (appointment == null) throw new ArgumentException("Appointment cannot be null");
            AppointmentHistory.Remove(appointment);
        }

        internal void Update(string firstname, string lastName, string fullName, string gender,
                             List<string>? allergies, string emergencyContact, DateTime? dateOfBirth,
                             string medicalRecordNumber, string phoneNumber, string email)
        {
            Firstname = firstname ?? throw new ArgumentException("Firstname cannot be null");
            LastName = lastName ?? throw new ArgumentException("LastName cannot be null");
            FullName = fullName ?? throw new ArgumentException("FullName cannot be null");
            Gender = gender ?? throw new ArgumentException("Gender cannot be null");
            Allergies = allergies != null ? new List<string>(allergies) : null;
            EmergencyContact = emergencyContact ?? throw new ArgumentException("EmergencyContact cannot be null");
            PhoneNumber = phoneNumber ?? throw new ArgumentException("PhoneNumber cannot be null");
            DateOfBirth = dateOfBirth;
            MedicalRecordNumber = medicalRecordNumber ?? throw new ArgumentException("MedicalRecordNumber cannot be null");
            Email = email ?? throw new ArgumentException("Email cannot be null");
        }
    }
}
