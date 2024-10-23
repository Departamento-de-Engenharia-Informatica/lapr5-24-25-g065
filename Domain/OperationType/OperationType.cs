using System;
using System.Collections.Generic;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Specializations;

namespace DDDSample1.Domain.OperationType
{
public class OperationType: Entity<OperationTypeID> {
    public OperationTypeID operationTypeID;
    public string Name { get; set; }
    public List<Specialization> RequiredStaffBySpecialization { get; set; }
    public TimeSpan EstimatedDuration { get; set; }
    public bool IsActive { get; set; } = true;

    public OperationType()
    {
        RequiredStaffBySpecialization = new List<Specialization>();
    }
}
}