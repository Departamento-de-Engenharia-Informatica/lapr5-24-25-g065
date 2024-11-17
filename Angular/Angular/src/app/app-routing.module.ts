// app-routing.module.ts
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

// Update the imports to point to the correct path (Dashboard folder)
import { AdminDashboardComponent } from './Dashboards/admin-dashboard/admin-dashboard.component';
import { DoctorDashboardComponent } from './Dashboards/doctor-dashboard/doctor-dashboard.component';
import { NurseDashboardComponent } from './Dashboards/nurse-dashboard/nurse-dashboard.component';
import { TechnicianDashboardComponent } from './Dashboards/technician-dashboard/technician-dashboard.component';
import { PatientDashboardComponent } from './Dashboards/patient-dashboard/patient-dashboard.component';
import { LoginComponent } from './Login/login.component';  // Your Login Component

const routes: Routes = [
  { path: '', component: LoginComponent }, // Default route (login page)
  { path: 'admin-dashboard', component: AdminDashboardComponent },
  { path: 'doctor-dashboard', component: DoctorDashboardComponent },
  { path: 'nurse-dashboard', component: NurseDashboardComponent },
  { path: 'technician-dashboard', component: TechnicianDashboardComponent },
  { path: 'patient-dashboard', component: PatientDashboardComponent },
  
  // Redirect to login if an unknown route is accessed
  { path: '**', pathMatch: 'full', redirectTo: 'login' }];

@NgModule({
  imports: [RouterModule.forRoot(routes, { enableTracing: true })],
  exports: [RouterModule] // Export RouterModule so it's available in the entire app
})
export class AppRoutingModule { }
