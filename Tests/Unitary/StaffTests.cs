using System;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Users;
using DDDSample1.Domain.Appointments; // Added for Appointment
using Xunit;
using DDDSample1.Domain.Staffs;

namespace DDDSample1.Tests.Unitary
{
    public class StaffTests
    {
        [Fact]
        public void Staff_Creation_ValidParameters_StaffCreated()
        {
            // Arrange
            var firstname = "Eric";
            var lastName = "Cartman";
            var fullName = "Eric Cartman";
            var gender = "Male";
            var specialization = "Pediatrics"; // Updated to string
            var type = "Doctor";
            var licenseNumber = "LN12345";
            var userId = new UserId(Guid.NewGuid());
            var availabilitySlot = "9 AM - 5 PM";
            var phoneNumber = "123-456-7890";
            var email = "ericcartman@gmail.com";

            // Act
            var staff = new Staff(firstname, lastName, fullName, gender, specialization, type, licenseNumber, userId, availabilitySlot, phoneNumber, email);

            // Assert
            Assert.Equal(firstname, staff.Firstname);
            Assert.Equal(lastName, staff.LastName);
            Assert.Equal(fullName, staff.FullName);
            Assert.Equal(gender, staff.Gender);
            Assert.Equal(specialization, staff.Specialization); // Updated to string
            Assert.Equal(type, staff.Type);
            Assert.Equal(licenseNumber, staff.LicenseNumber);
            Assert.Equal(userId, staff.UserId);
            Assert.Equal(availabilitySlot, staff.AvailabilitySlot);
            Assert.Equal(phoneNumber, staff.PhoneNumber);
            Assert.Equal(email, staff.Email);
        }

        [Fact]
        public void ChangeFirstName_ValidName_FirstNameChanged()
        {
            // Arrange
            var staff = CreateSampleStaff();

            // Act
            staff.ChangeFirstName("Stan");

            // Assert
            Assert.Equal("Stan", staff.Firstname);
        }

        [Fact]
        public void ChangeLastName_ValidName_LastNameChanged()
        {
            // Arrange
            var staff = CreateSampleStaff();

            // Act
            staff.ChangeLastName("Marsh");

            // Assert
            Assert.Equal("Marsh", staff.LastName);
        }

        [Fact]
        public void ChangeFullName_ValidName_FullNameChanged()
        {
            // Arrange
            var staff = CreateSampleStaff();

            // Act
            staff.ChangeFullName("Stan Marsh");

            // Assert
            Assert.Equal("Stan Marsh", staff.FullName);
        }

        [Fact]
        public void ChangeGender_ValidGender_GenderChanged()
        {
            // Arrange
            var staff = CreateSampleStaff();

            // Act
            staff.ChangeGender("Female");

            // Assert
            Assert.Equal("Female", staff.Gender);
        }

        [Fact]
        public void ChangeSpecialization_ValidSpecialization_SpecializationChanged()
        {
            // Arrange
            var staff = CreateSampleStaff();
            var newSpecialization = "Cardiology"; // Updated to string

            // Act
            staff.ChangeSpecialization(newSpecialization);

            // Assert
            Assert.Equal(newSpecialization, staff.Specialization); // Updated to string
        }

        [Fact]
        public void ChangeType_ValidType_TypeChanged()
        {
            // Arrange
            var staff = CreateSampleStaff();

            // Act
            staff.ChangeType("Nurse");

            // Assert
            Assert.Equal("Nurse", staff.Type);
        }

        [Fact]
        public void ChangeLicenseNumber_ValidLicenseNumber_LicenseNumberChanged()
        {
            // Arrange
            var staff = CreateSampleStaff();

            // Act
            staff.ChangeLicenseNumber("LN67890");

            // Assert
            Assert.Equal("LN67890", staff.LicenseNumber);
        }

        [Fact]
        public void ChangeAvailabilitySlot_ValidSlot_AvailabilitySlotChanged()
        {
            // Arrange
            var staff = CreateSampleStaff();

            // Act
            staff.ChangeAvailabilitySlot("10 AM - 6 PM");

            // Assert
            Assert.Equal("10 AM - 6 PM", staff.AvailabilitySlot);
        }

        [Fact]
        public void ChangePhoneNumber_ValidPhoneNumber_PhoneNumberChanged()
        {
            // Arrange
            var staff = CreateSampleStaff();

            // Act
            staff.ChangePhoneNumber("987-654-3210");

            // Assert
            Assert.Equal("987-654-3210", staff.PhoneNumber);
        }

        [Fact]
        public void ChangeEmail_ValidEmail_EmailChanged()
        {
            // Arrange
            var staff = CreateSampleStaff();

            // Act
            staff.ChangeEmail("stanmarsh@gmail.com");

            // Assert
            Assert.Equal("stanmarsh@gmail.com", staff.Email);
        }

        private Staff CreateSampleStaff()
        {
            return new Staff(
                "Eric", 
                "Cartman", 
                "Eric Cartman", 
                "Male", 
                "Pediatrics", // Updated to string
                "Doctor", 
                "LN12345", 
                new UserId(Guid.NewGuid()), 
                "9 AM - 5 PM", 
                "123-456-7890", 
                "ericcartman@gmail.com"
            );
        }
    }
}
