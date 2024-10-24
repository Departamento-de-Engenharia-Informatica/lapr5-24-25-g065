using System;
using System.Collections.Generic;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Specializations;

namespace DDDSample1.Domain.OperationTypes
{
    public class CreatingOperationTypeDto
    {
       public string Name { get;  private set; }
    
        public List<Specialization> RequiredStaffBySpecialization { get; set; }
        public TimeSpan EstimatedDuration { get; set; }
        public bool IsActive { get; set; } = true;


        public CreatingOperationTypeDto(List<Specialization> requiredStaffBySpecialization,TimeSpan estimatedDuration,bool isActive){
            this.Name = Name;
            this.RequiredStaffBySpecialization= requiredStaffBySpecialization;
            this.EstimatedDuration=estimatedDuration;
            this.IsActive=isActive;
        }

    }
}