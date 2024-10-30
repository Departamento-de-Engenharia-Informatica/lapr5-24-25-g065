using System;
using System.Collections.Generic;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.OperationType{
    public class OperationType: Entity<OperationTypeID>{
        public string Name { get; set; }
        public List<string> RequiredStaffBySpecialization { get; set; }
        public TimeSpan EstimatedDuration { get; set; }
        public bool IsActive { get; set; } = true;

        public OperationType(string name, List<string> requiredStaffBySpecialization, TimeSpan estimatedDuration, bool isActive = true){
            Name = name;
            RequiredStaffBySpecialization = requiredStaffBySpecialization;
            EstimatedDuration = estimatedDuration;
            IsActive = isActive;
        }
    }
}