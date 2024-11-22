import { Routes } from '@angular/router';
import { LoginComponent } from './Login/login.component';
import { AdminDashboardComponent } from './Dashboards/admin-dashboard/admin-dashboard.component';
import { DoctorDashboardComponent } from './Dashboards/doctor-dashboard/doctor-dashboard.component';
import { NurseDashboardComponent } from './Dashboards/nurse-dashboard/nurse-dashboard.component';
import { TechnicianDashboardComponent } from './Dashboards/technician-dashboard/technician-dashboard.component';
import { PatientDashboardComponent } from './Dashboards/patient-dashboard/patient-dashboard.component';
import { AuthGuard } from './auth.guard';

export const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'admin-dashboard', component: AdminDashboardComponent}, 
  { path: 'doctor-dashboard', component: DoctorDashboardComponent },
  { path: 'nurse-dashboard', component: NurseDashboardComponent },
  { path: 'technician-dashboard', component: TechnicianDashboardComponent },
  { path: 'patient-dashboard', component: PatientDashboardComponent },
  { path: '**', component: LoginComponent }, // Redirect unknown routes to login
];
