import { Component } from '@angular/core';

@Component({
  selector: 'app-patient-dashboard',
  standalone: true,
  imports: [],
  templateUrl: './patient-dashboard.component.html',
  styleUrls: ['./patient-dashboard.component.css']
})
export class PatientDashboardComponent {

  patient: any = {
    FullName: 'John Doe',
    Email: 'john.doe@example.com',
    PhoneNumber: '1234567890',
    MedicalRecordNumber: '12345',
  };

  isUpdating = false;
  isConfirmingDelete = false;

  onUpdateClick() {
    this.isUpdating = true;
  }

  onDeleteClick() {
    this.isConfirmingDelete = true;
  }

  onSubmitUpdate() {
    // Simulate the update logic (you can implement your API call here)
    console.log('Updated Patient Info:', this.patient);
    this.isUpdating = false;
  }

  onConfirmDelete() {
    // Simulate account deletion logic (e.g., calling a delete API)
    console.log('Account deleted:', this.patient.FullName);
    this.isConfirmingDelete = false;
  }

  onCancelDelete() {
    this.isConfirmingDelete = false;
  }

}
