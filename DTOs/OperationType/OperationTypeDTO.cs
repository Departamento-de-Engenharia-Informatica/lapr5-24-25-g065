/*sing System;
using System.Collections.Generic;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Specializations;

namespace DDDNetCore.DTOs.OperationType
{
    public class OperationTypeDto
    {
        public Guid Id { get; set; }
        public string Name { get; private set; }

        public List<Specialization> RequiredStaffBySpecialization { get; set; }
        public TimeSpan EstimatedDuration { get; set; }
        public bool IsActive { get; set; } = true;


        public OperationTypeDto(Guid id,String name, List<Specialization> requiredStaffBySpecialization, TimeSpan estimatedDuration, bool isActive)
        {
            Id = id;
            Name = name;
            RequiredStaffBySpecialization = requiredStaffBySpecialization;
            EstimatedDuration = estimatedDuration;
            IsActive = isActive;
        }

    }
}*/