import { UserId } from './userId'; 
import { PatientDTO } from './patientId';     
import { StaffId } from './staffId'; 
import { OperationRequestId } from './operationRequestId';
import { PatientId } from '../Services/patient.service';


export interface OperationRequest {
    Id: OperationRequestId;
    patientId: PatientId;
    doctorId:StaffId;
    operationType:OperationRequestId;
    operationDateTime: string;
    deadline:string;
    priority: number ;
}
