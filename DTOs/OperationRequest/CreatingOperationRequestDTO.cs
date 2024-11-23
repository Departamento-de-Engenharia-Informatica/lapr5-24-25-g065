using System;
using DDDSample1.Domain.OperationType;
using DDDSample1.Domain.Patients;
using DDDSample1.Domain.Staffs;

namespace DDDNetCore.DTOs.OperationRequest
{
    public class CreatingOperationRequestDTO
    {
        public Guid  patientID { get; set; }
        public Guid  doctorID { get; set; }
        public Guid  operationTypeID { get; set; }
        public string operationDateTime { get; set; }
        public string deadline { get; set; }
        public int priority { get; set; }
        public CreatingOperationRequestDTO(Guid patientID, Guid doctorID, Guid operationTypeID, string operationDateTime, string deadline, int priority)
        {
            this.patientID = patientID;
            this.doctorID = doctorID;
            this.operationTypeID = operationTypeID;
            this.operationDateTime = operationDateTime;
            this.deadline = deadline;
            this.priority = priority;
        }
    }
}
