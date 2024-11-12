using System;
using DDDSample1.Domain.OperationType;
using DDDSample1.Domain.Patients;
using DDDSample1.Domain.Staffs;

namespace DDDNetCore.DTOs.OperationRequest
{
    public class OperationRequestDTO
    {
        public Guid ID { get; set; }
        public PatientId patientID { get; set; }
        public StaffId doctorID { get; set; }
        public OperationTypeID operationTypeID { get; set; }
        public string operationDateTime { get; set; }
        public string deadline { get; set; }
        public int priority { get; set; }
        public OperationRequestDTO(Guid ID, PatientId patientID, StaffId doctorID, OperationTypeID operationTypeID, string operationDateTime, string deadline, int priority)
        {
            this.ID = ID;
            this.patientID = patientID;
            this.doctorID = doctorID;
            this.operationTypeID = operationTypeID;
            this.operationDateTime = operationDateTime;
            this.deadline = deadline;
            this.priority = priority;
        }
    }
}
