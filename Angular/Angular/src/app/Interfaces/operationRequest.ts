import { UserId } from './userId'; 
import { PatientId } from './patientId';     
import { StaffId } from './staffId'; 
import { OperationRequestId } from './operationRequestId';


export interface OperationRequest {
    Id: OperationRequestId;
    patientId: PatientId;
    doctorId:StaffId;
    operationType:OperationRequestId;
    operationDateTime: string;
    deadline:string;
    priority: number ;
}
