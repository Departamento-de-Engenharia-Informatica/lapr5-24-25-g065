using System;
using System.Collections.Generic;

namespace DDDNetCore.DTOs.OperationType
{
    public class CreateOperationTypeDTO
    {
        public string Name { get; private set; }
        public List<string> RequiredStaffBySpecialization { get; set; }
        public string EstimatedDuration { get; set; }
        public bool IsActive { get; set; }

        public CreateOperationTypeDTO(string Name, List<string> RequiredStaffBySpecialization, string EstimatedDuration, bool IsActive)
        {
            this.Name = Name;
            this.RequiredStaffBySpecialization = RequiredStaffBySpecialization;
            this.EstimatedDuration = EstimatedDuration;
            this.IsActive = IsActive;
        }
    }
}