// src/app/interfaces/appointment.ts
import { PatientId } from '../Services/patient.service';
import { AppointmentId } from './appointmentId';
import { PatientDTO } from './patientId';
import { StaffId } from './staffId';

export interface Appointment {
  id: AppointmentId;
  requestId: string;
  roomId: string;
  date: Date; // ISO 8601 date string, e.g., "2023-10-15T09:00:00Z"
  status: string;
  patientId: PatientId;
  staffIds: StaffId[]; // Array of StaffId objects
}
