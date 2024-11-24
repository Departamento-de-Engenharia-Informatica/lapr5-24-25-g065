import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { OperationRequest } from '../Interfaces/operationRequest';

@Injectable({
  providedIn: 'root'
})
export class OperationRequestService {

  constructor(private http: HttpClient) { }

  getAllOperationRequest() {
    return this.http.get<OperationRequest[]>('https://10.9.10.65/api/operationRequest');
  }



  
}