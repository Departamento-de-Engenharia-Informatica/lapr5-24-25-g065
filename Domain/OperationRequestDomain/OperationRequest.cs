using DDDNetCore.IRepos;
using DDDSample1.Domain.Shared;
using System;

namespace DDDNetCore.Domain.OperationRequestDomain{
    public class OperationRequest : Entity<OperationRequestID>, IAggregateRoot{
        public string patientID { get; set; }
        public string doctorID { get; set; }
        public string operationTypeID { get; set; }
        public DateTime operationDateTime { get; set; }
        public DateTime deadline { get; set; }
        public int priority { get; set; }

        public OperationRequest(string patientID, string doctorID, string operationTypeID, DateTime operationDateTime, DateTime  deadline, int priority){
            Id = new OperationRequestID(Guid.NewGuid());
            this.patientID = patientID;
            this.doctorID = doctorID;
            this.operationTypeID = operationTypeID;
            this.operationDateTime = operationDateTime;
            this.deadline = deadline;
            this.priority = priority;
        }

        internal void Update(string patientID, string doctorID, string operationTypeID, string operationDateTime, string deadline, int priority){
            this.operationTypeID = operationTypeID;
            this.patientID = patientID;
            this.doctorID = doctorID;
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
