using System;
using System.Collections.Generic;
using DDDNetCore.IRepos;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Appointments; // Import Appointment domain

namespace DDDSample1.Domain.Patients
{
    public class Patient : Entity<PatientId>, IAggregateRoot
    {
        public PatientId Id { get; private set; }
        public string Firstname { get; private set; }
        public string LastName { get; private set; }
        public string FullName { get; private set; }
        public string Gender { get; private set; }
        public List<string>? Allergies { get; private set; }
        public string EmergencyContact { get; private set; }
        public DateTime? DateOfBirth { get; private set; }
        public string MedicalRecordNumber { get; private set; }
        public List<Appointment> AppointmentHistory { get; private set; } // Added

        public Patient(string firstname, string lastName, string fullName, string gender, List<string> allergies, string emergencyContact, DateTime? dateOfBirth, string medicalRecordNumber)
        {
            Id = new PatientId(Guid.NewGuid());
            Firstname = firstname;
            LastName = lastName;
            FullName = fullName;
            Gender = gender;
            Allergies = allergies;
            EmergencyContact = emergencyContact;
            DateOfBirth = dateOfBirth;
            MedicalRecordNumber = medicalRecordNumber;
            AppointmentHistory = new List<Appointment>(); // Initialize the list
        }

        public void AddAppointment(Appointment appointment)
        {
            AppointmentHistory.Add(appointment);
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

        public void ChangeEmergencyContact(string emergencyContact)
        {
            this.EmergencyContact = emergencyContact;
        }

        public void ChangeAllergies(List<string>? allergies)
        {
            this.Allergies = allergies;
        }

        public void ChangeDateOfBirth(DateTime dateOfBirth)
        {
            this.DateOfBirth = dateOfBirth;
        }

        public void ChangeMedicalRecordNumber(string medicalRecordNumber)
        {
            this.MedicalRecordNumber = medicalRecordNumber;
        }
    }
}
