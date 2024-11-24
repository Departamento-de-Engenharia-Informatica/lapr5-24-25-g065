import { Component, OnInit } from '@angular/core';
import { PatientService } from '../../Services/patient.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-patient-dashboard',
  templateUrl: './patient-dashboard.component.html',
  styleUrls: ['./patient-dashboard.component.css']
})
export class PatientDashboardComponent implements OnInit {
  patient: any;

  constructor(private patientService: PatientService, private router: Router) {}

  ngOnInit(): void {
    this.loadPatientData();
  }

  loadPatientData(): void {
    this.patientService.getPatientProfile().subscribe(
      (data: any) => { // Explicitly type 'data' as 'any'
        this.patient = data;
      },
      (error: any) => { // Explicitly type 'error' as 'any'
        console.error('Error loading patient data', error);
      }
    );
  }

  updateProfile(): void {
    this.router.navigate(['/patient/update']);
  }

  deleteProfile(): void {
    if (confirm('Are you sure you want to delete your profile?')) {
      const patientIdObj = { value: this.patient.id }; // Create the PatientId object
      this.patientService.deletePatientProfile(patientIdObj).subscribe(
        () => {
          alert('Profile deleted');
          this.router.navigate(['/login']);
        },
        (error: any) => { // Explicitly type 'error' as 'any'
          console.error('Error deleting profile', error);
        }
      );
    }
  }
}
