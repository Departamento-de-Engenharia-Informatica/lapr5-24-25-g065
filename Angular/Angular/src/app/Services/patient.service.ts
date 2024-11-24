import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CreatePatientDTO, PatientDTO } from '../Interfaces/patientId';

// Define the PatientId interface
export interface PatientId {
  value: string;  // GUID as a string
}

@Injectable({
  providedIn: 'root'
})
export class PatientService {
  private baseUrl = 'https://localhost:5001/api/Patient';

  constructor(private http: HttpClient) {}

  // Register a new patient
  registerPatient(patient: CreatePatientDTO): Observable<any> {
    return this.http.post(`${this.baseUrl}/add`, patient);
  }

  // Update patient details
  updatePatient(patient: PatientDTO): Observable<any> {
    return this.http.put(`${this.baseUrl}/update/${patient.id}`, patient);
  }

  // Delete patient profile
  deletePatientProfile(patientIdObj: PatientId): Observable<any> {
    return this.http.delete(`${this.baseUrl}/delete/${patientIdObj.value}`);
  }
  
  // Get patient details
  getPatientProfile(): Observable<any> {
    return this.http.get(`${this.baseUrl}/profile`);
  }

  // Get patient details by ID
  getPatientById(id: string): Observable<PatientDTO> {
    return this.http.get<PatientDTO>(`${this.baseUrl}/${id}`);
  }
}
