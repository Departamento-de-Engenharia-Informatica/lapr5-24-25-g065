/*using System;
using System.Collections.Generic;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Specializations;

namespace DDDNetCore.DTOs.OperationType
{
    public class CreateOperationTypeDto
    {
        public string Name { get; private set; }

        public List<Specialization> RequiredStaffBySpecialization { get; set; }
        public TimeSpan EstimatedDuration { get; set; }
        public bool IsActive { get; set; } = true;


        public CreateOperationTypeDto(List<Specialization> requiredStaffBySpecialization, TimeSpan estimatedDuration, bool isActive)
        {
            Name = Name;
            RequiredStaffBySpecialization = requiredStaffBySpecialization;
            EstimatedDuration = estimatedDuration;
            IsActive = isActive;
        }

    }
}*/