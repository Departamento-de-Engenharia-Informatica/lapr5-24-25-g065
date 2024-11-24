import { Routes } from '@angular/router';
import { LoginComponent } from './Login/login.component';
import { AdminDashboardComponent } from './Dashboards/admin-dashboard/admin-dashboard.component';
import { DoctorDashboardComponent } from './Dashboards/doctor-dashboard/doctor-dashboard.component';
import { NurseDashboardComponent } from './Dashboards/nurse-dashboard/nurse-dashboard.component';
import { TechnicianDashboardComponent } from './Dashboards/technician-dashboard/technician-dashboard.component';
import { PatientDashboardComponent } from './Dashboards/patient-dashboard/patient-dashboard.component';
import { OperationDashboardComponent } from './Dashboards/operation-dashboard/operation-dashboard.component';

import { AuthGuard } from './auth.guard';
import { RegisterPatientComponent } from './patient/register-patient.component';
import { UpdatePatientComponent } from './patient/update-patient.component';
import { DeletePatientComponent } from './patient/delete-patient.component';

export const routes: Routes = [
  { path: '', component: LoginComponent }, // Default route to login
  { path: 'admin-dashboard', component: AdminDashboardComponent, canActivate: [AuthGuard] }, 
  { path: 'doctor-dashboard', component: DoctorDashboardComponent, canActivate: [AuthGuard] },
  { path: 'nurse-dashboard', component: NurseDashboardComponent, canActivate: [AuthGuard] },
  { path: 'technician-dashboard', component: TechnicianDashboardComponent, canActivate: [AuthGuard] },
  { path: 'patient-dashboard', component: PatientDashboardComponent, canActivate: [AuthGuard] },
  { path: 'operation-dashboard', component: OperationDashboardComponent, canActivate: [AuthGuard] },
  { path: '**', component: LoginComponent }, // Redirect unknown routes to login
  { path: 'register-patient', component: RegisterPatientComponent }, // Register a new patient
  { path: 'update-patient/:id', component: UpdatePatientComponent }, // Update an existing patient's profile
  { path: 'delete-patient/:id', component: DeletePatientComponent }, // Delete an existing patient
];
