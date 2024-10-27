using System;
using System.Collections.Generic;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Specializations;
using DDDSample1.Domain.Staffs;
using DDDSample1.Domain.Users;
using Xunit;

namespace DDDSample1.Tests.Unitary
{
    public class SpecializationTests
    {
        [Fact]
        public void Specialization_Creation_ValidParameters_SpecializationCreated()
        {
            // Arrange
            var type = "Pediatrics";
            var description = "Specializes in children's health.";

            // Act
            var specialization = new Specialization(type, description);

            // Assert
            Assert.Equal(type, specialization.Type);
            Assert.Equal(description, specialization.Description);
            Assert.NotNull(specialization.StaffMembers);
            Assert.Empty(specialization.StaffMembers); // Check that the collection is initialized and empty
        }

        [Fact]
        public void Specialization_Initialize_WithStaffMembers_StaffMembersCollectionInitialized()
        {
            // Arrange
            var specialization = new Specialization("Cardiology", "Specializes in heart conditions.");
            var staffMember = CreateSampleStaff();

            // Act
            specialization.StaffMembers.Add(staffMember);

            // Assert
            Assert.Contains(staffMember, specialization.StaffMembers);
        }

        private Staff CreateSampleStaff()
        {
            // Assuming the UserId and SpecializationId classes have a parameterless constructor
            return new Staff(
                "Dr. Smith", 
                "Johnson", 
                "Dr. Smith Johnson", 
                "Male", 
                new SpecializationId(), 
                "Doctor", 
                "LN54321", 
                new UserId(Guid.NewGuid()), 
                "8 AM - 4 PM", 
                "555-123-4567", 
                "drsmithjohnson@gmail.com"
            );
        }
    }
}
