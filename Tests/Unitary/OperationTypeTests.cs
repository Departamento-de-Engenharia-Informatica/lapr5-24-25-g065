/*using Xunit;
using System;
using System.Collections.Generic;
using DDDSample1.Domain.OperationType;

public class OperationTypeTest
{
    [Theory]
    [InlineData("Appendectomy", new string[] { "Surgeon", "Anesthetist" }, "01:30:00", true)]
    [InlineData("Cataract Surgery", new string[] { "Ophthalmologist", "Nurse" }, "00:45:00", false)]
    public void WhenPassingCorrectData_NewOperationTypeIsCreated(string name, string[] requiredStaffBySpecialization, string estimatedDuration, bool isActive)
    {
        var operationType = new OperationType(
            name,
            new List<string>(requiredStaffBySpecialization),
            TimeSpan.Parse(estimatedDuration),
            isActive
        );

        Assert.NotNull(operationType);
        Assert.Equal(name, operationType.Name);
        Assert.Equal(requiredStaffBySpecialization, operationType.RequiredStaffBySpecialization);
        Assert.Equal(TimeSpan.Parse(estimatedDuration), operationType.EstimatedDuration);
        Assert.Equal(isActive, operationType.IsActive);
    }

    [Theory]
    [InlineData(null, new string[] { "Surgeon", "Anesthetist" }, "01:30:00", true)]
    public void WhenPassingNullName_ExceptionIsThrown(string name, string[] requiredStaffBySpecialization, string estimatedDuration, bool isActive)
    {
        var exception = Assert.Throws<ArgumentException>(() =>
            new OperationType(
                name,
                new List<string>(requiredStaffBySpecialization),
                TimeSpan.Parse(estimatedDuration),
                isActive
            )
        );

        Assert.Equal("Name cannot be null", exception.Message); // Adjust exception message as per your implementation
    }

    // Additional tests for other properties and edge cases can be added here...
}*/
