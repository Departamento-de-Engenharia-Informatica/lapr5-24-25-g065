import { UserId } from './userId'; 
import { PatientId } from './patientId';     
import { StaffId } from './staffId'; 
import { OperationRequestId } from './operationRequestId';


export interface OperationRequest {
    id: { value: string };
    patientID: { value: string };
    doctorID: { value: string };
    operationTypeID: { value: string };
    operationDateTime: string;
    deadline:string;
    priority: number ;
}
