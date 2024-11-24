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
import { OperationDashboardComponent } from './Dashboards/operation-dashboard/operation-dashboard.component';
import { FormsModule } from '@angular/forms';
import { OperationRequestDialogComponent } from './Dashboards/operation-request-dialog/operation-request-dialog.component';
import { MatButtonModule } from '@angular/material/button';

@NgModule({
  declarations: [
    AppComponent,
    UpdateStaffDialogComponent,
    DeleteStaffDialogComponent,
    AdminDashboardComponent,
    OperationDashboardComponent,
    OperationRequestDialogComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MatDialogModule,
    MatFormFieldModule,
    MatSelectModule, 
    ReactiveFormsModule,
    FormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatButtonModule,

    
  ],
  providers: [],
  bootstrap: [AppComponent]
})

export class AppModule { }
