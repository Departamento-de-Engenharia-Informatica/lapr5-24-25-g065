import { UserId } from './userId'; 
import { PatientDTO } from './patientId';     
import { StaffId } from './staffId'; 
import { OperationRequestId } from './operationRequestId';
import { PatientId } from '../Services/patient.service';


export interface OperationRequest {
    id: { value: string };
    patientID: { value: string };
    doctorID: { value: string };
    operationTypeID: { value: string };
    operationDateTime: string;
    deadline:string;
    priority: number ;
}
