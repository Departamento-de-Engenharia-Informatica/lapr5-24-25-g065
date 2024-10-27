/*using System;
using System.Collections.Generic;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Specializations;

namespace DDDSample1.Domain.OperationTypes
{
public class OperationType: Entity<OperationTypeID> {
    public string Name { get; set; }
    public List<string> RequiredStaffBySpecialization { get; set; }
    public TimeSpan EstimatedDuration { get; set; }
    public bool IsActive { get; set; } = true;

    public OperationType()
    {
        RequiredStaffBySpecialization = new List<string>();
    }

    public OperationType(OperationTypeID id, string name, List<string> requiredStaffBySpecialization, TimeSpan estimatedDuration, bool isActive = true)
    {
    Id = id;
    Name = name;
    RequiredStaffBySpecialization = requiredStaffBySpecialization;
    EstimatedDuration = estimatedDuration;
    IsActive = isActive;
    }
    public void ChangeName(string name)
        {
            this.Name = name;
        }

        public void ChangeRequiredStaffBySpecialization(List<string> requiredStaffBySpecialization)
        {
            this.RequiredStaffBySpecialization = requiredStaffBySpecialization;
        }

        public void ChangeEstimatedDuration(TimeSpan estimatedDuration)
        {
            this.EstimatedDuration = estimatedDuration;
        }

        public void ChangeIsActive(bool isActive)
        {
            this.IsActive = isActive;
        }
    }
}*/