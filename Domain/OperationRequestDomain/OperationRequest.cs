using DDDNetCore.IRepos;
using DDDSample1.Domain.OperationType;
using DDDSample1.Domain.Patients;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Staffs;
using System;

namespace DDDNetCore.Domain.OperationRequestDomain{
    public class OperationRequest : Entity<OperationRequestID>, IAggregateRoot{
        public PatientId patientID { get; set; }
        public StaffId doctorID { get; set; }
        public OperationTypeID operationTypeID { get; set; }
        public DateTime operationDateTime { get; set; }
        public DateTime deadline { get; set; }
        public int priority { get; set; }

        public OperationRequest(PatientId patientID, StaffId doctorID, OperationTypeID operationTypeID, DateTime operationDateTime, DateTime  deadline, int priority){
            Id = new OperationRequestID(Guid.NewGuid());
            this.patientID = patientID;
            this.doctorID = doctorID;
            this.operationTypeID = operationTypeID;
            this.operationDateTime = operationDateTime;
            this.deadline = deadline;
            this.priority = priority;
        }

        

        internal void Update(PatientId patientID, StaffId doctorID, OperationTypeID operationTypeID, string operationDateTime, string deadline, int priority){
            this.patientID = patientID;
            this.doctorID = doctorID;
            this.operationTypeID = operationTypeID;
            if (DateTime.TryParse(operationDateTime, out DateTime parsedOperationDateTime)){
                this.operationDateTime = parsedOperationDateTime;
            }
            if (DateTime.TryParse(deadline, out DateTime parsedDeadline)){
                this.deadline = parsedDeadline;
            }
            this.priority = priority;
        }
    }
}
