using System;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Users;
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
            var specialization = "Pediatrics"; // Ensure this is a string
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
            Assert.Equal(specialization, staff.Specialization);
            Assert.Equal(type, staff.Type);
            Assert.Equal(licenseNumber, staff.LicenseNumber);
            Assert.Equal(userId, staff.UserId);
            Assert.Equal(availabilitySlot, staff.AvailabilitySlot);
            Assert.Equal(phoneNumber, staff.PhoneNumber);
            Assert.Equal(email, staff.Email);
        }

        // Other tests remain unchanged...

        private Staff CreateSampleStaff()
        {
            return new Staff(
                "Eric", 
                "Cartman", 
                "Eric Cartman", 
                "Male", 
                "Pediatrics", // Ensure this is a string
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
