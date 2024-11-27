import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CreatePatientDTO, PatientDTO } from '../Interfaces/patientId';

export interface PatientId {
  value: string;  // GUID as a string
}

@Injectable({
  providedIn: 'root'
})
export class PatientService {
  private baseUrl = 'https://10.9.10.65/api/Patient';  // Directly setting the API base URL

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

  // Get patient details using the email stored in localStorage
  getPatientProfile(): Observable<PatientDTO> {
    const email = localStorage.getItem('userEmail');
    if (email) {
      return this.http.get<PatientDTO>(`${this.baseUrl}/byEmail?email=${email}`);
    } else {
      throw new Error('No email found for the logged-in user');
    }
  }

  // Get patient details by ID
  getPatientById(id: string): Observable<PatientDTO> {
    return this.http.get<PatientDTO>(`${this.baseUrl}/${id}`);
  }

  // Get patient details by email
  getPatientByEmail(email: string): Observable<PatientDTO> {
    return this.http.get<PatientDTO>(`${this.baseUrl}/byEmail?email=${email}`);
  }

  // Verify email for account activation or password reset
  verifyEmail(token: string): Observable<any> {
    return this.http.get(`${this.baseUrl}/verify-email?token=${token}`);
  }
}
