using Xunit;
using System;
using System.Collections.Generic;
using DDDSample1.Domain.Patients;

public class PatientTest
{
    [Theory]
    [InlineData("Eric", "Cartman", "Eric Cartman", "Male", new string[] { }, "966966966", "2000-01-01", "12345", "b8a64a28-4a77-4d7b-9a77-71f10ebc3303", "eric.cartman@example.com")]
    [InlineData("Stan", "Marsh", "Stan Marsh", "Male", new string[] { "Peanut" }, "966966967", "2000-01-01", "67890", "8c1db048-6342-4ee4-8b77-03835c0ca9f2", "stan.marsh@example.com")]
    public void WhenPassingCorrectData_NewPatientIsCreated(string firstname, string lastName, string fullName, string gender, string[] allergies, string emergencyContact, string dateOfBirth, string medicalRecordNumber, string userId, string email)
    {
        var patient = new Patient(
            firstname,
            lastName,
            fullName,
            gender,
            allergies != null ? new List<string>(allergies) : null,
            emergencyContact,
            DateTime.Parse(dateOfBirth),
            medicalRecordNumber,
            "966966966", // PhoneNumber
            email
        );

        Assert.NotNull(patient);
        Assert.Equal(firstname, patient.Firstname);
        Assert.Equal(lastName, patient.LastName);
        Assert.Equal(fullName, patient.FullName);
        Assert.Equal(gender, patient.Gender);
        Assert.Equal(allergies, patient.Allergies);
        Assert.Equal(emergencyContact, patient.EmergencyContact);
        Assert.Equal(DateTime.Parse(dateOfBirth), patient.DateOfBirth);
        Assert.Equal(medicalRecordNumber, patient.MedicalRecordNumber);
        Assert.Equal("966966966", patient.PhoneNumber);
        Assert.Equal(email, patient.Email);
    }

    [Theory]
    [InlineData("Eric", "Cartman", "Eric Cartman", "Male", null, "966966966", "2000-01-01", null, "b8a64a28-4a77-4d7b-9a77-71f10ebc3303", "eric.cartman@example.com")]
    public void WhenPassingNullMedicalRecordNumber_ExceptionIsThrown(string firstname, string lastName, string fullName, string gender, string[] allergies, string emergencyContact, string dateOfBirth, string medicalRecordNumber, string userId, string email)
    {
        var exception = Assert.Throws<ArgumentException>(() =>
            new Patient(
                firstname,
                lastName,
                fullName,
                gender,
                allergies != null ? new List<string>(allergies) : null,
                emergencyContact,
                DateTime.Parse(dateOfBirth),
                medicalRecordNumber,
                "966966966", // PhoneNumber
                email
            )
        );

        Assert.Equal("MedicalRecordNumber cannot be null", exception.Message); // Adjust exception message based on your implementation
    }

    // Additional tests for other properties can be added here...
}
