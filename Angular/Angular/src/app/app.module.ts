import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { UpdateStaffDialogComponent } from './Dialogs/update-staff-dialog/update-staff-dialog.component';
import { DeleteStaffDialogComponent } from './Dialogs/delete-staff-dialog/delete-staff-dialog.component';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { ReactiveFormsModule } from '@angular/forms';
import { AdminDashboardComponent } from './Dashboards/admin-dashboard/admin-dashboard.component';
import { DoctorDashboardComponent } from './Dashboards/doctor-dashboard/doctor-dashboard.component';
import { NurseDashboardComponent } from './Dashboards/nurse-dashboard/nurse-dashboard.component';
import { TechnicianDashboardComponent } from './Dashboards/technician-dashboard/technician-dashboard.component';
import { PatientDashboardComponent } from './Dashboards/patient-dashboard/patient-dashboard.component';
import { AppRoutingModule } from './app-routing.module';

@NgModule({
  declarations: [
    UpdateStaffDialogComponent,
    DeleteStaffDialogComponent,
    AdminDashboardComponent,  
  //  DoctorDashboardComponent,
  //  NurseDashboardComponent,
  //  TechnicianDashboardComponent,
  //  PatientDashboardComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MatDialogModule,
    MatFormFieldModule,
    MatSelectModule, 
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppModule]
})
export class AppModule { }
