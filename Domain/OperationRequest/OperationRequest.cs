using System;

namespace DDDNetCore.Domain.OperationRequest
{
    public class OperationRequest{
        public string patientID { get; set; }
        public string doctorID { get; set; }
        public string operationTypeID { get; set; }
        public DateTime operationDateTime { get; set; }
        public DateTime deadline { get; set; }
        public int priority { get; set; }
        public OperationRequest(string patientID, string doctorID, string operationTypeID, DateTime operationDateTime, DateTime  deadline, int priority){
            this.patientID = patientID;
            this.doctorID = doctorID;
            this.operationTypeID = operationTypeID;
            this.operationDateTime = operationDateTime;
            this.deadline = deadline;
            this.priority = priority;
        }

        internal void Update(string patientID, string doctorID, string operationTypeID, string operationDateTime, string deadline, int priority)
        {
            throw new NotImplementedException();
        }
    }
}
