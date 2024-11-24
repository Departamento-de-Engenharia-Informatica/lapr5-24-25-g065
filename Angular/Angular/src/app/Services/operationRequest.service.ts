import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { OperationRequest } from '../Interfaces/operationRequest';

@Injectable({
  providedIn: 'root'
})
export class OperationRequestService {

  constructor(private http: HttpClient) { }

  getAllOperationRequest() {
    return this.http.get<OperationRequest[]>('https://localhost:5001/api/operationRequest');
  }

  /*createStaff(newStaff: Staff) {

    return this.http.post<Staff>('https://10.9.10.65:5001/api/Staff',newStaff);

  }*/

  
}