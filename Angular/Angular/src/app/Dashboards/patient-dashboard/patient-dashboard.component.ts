import { Component, OnInit } from '@angular/core';
import { PatientService } from '../../Services/patient.service';
import { Router } from '@angular/router';
import { SocialAuthService, SocialUser } from '@abacritt/angularx-social-login';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-patient-dashboard',
  templateUrl: './patient-dashboard.component.html',
  styleUrls: ['./patient-dashboard.component.css']
})
export class PatientDashboardComponent implements OnInit {
  patient: any;

  constructor(
    private patientService: PatientService,
    private router: Router,
    private authService: SocialAuthService // Injecting authService to get the logged-in user
  ) {}

  ngOnInit(): void {
    this.authService.authState.subscribe(async (user: SocialUser) => {
      if (user) {
        // Now use the email from the logged-in user
        await this.loadPatientData(user.email);
      } else {
        console.log('No user logged in.');
      }
    });
  }

  // Load patient data by email
  loadPatientData(email: string): void {
    this.patientService.getPatientByEmail(email).subscribe(
      (data: any) => {  // You can replace 'any' with a proper type like 'Patient'
        this.patient = data;
      },
      (error: any) => {  // You can replace 'any' with a more specific error type
        console.error('Error loading patient data', error);
      }
    );
  }

  updateProfile(): void {
    // Navigate to update profile and pass the patient ID as a route parameter
    this.router.navigate(['/patient/update', this.patient.id]);
  }
  

  deleteProfile(): void {
    if (confirm('Are you sure you want to delete your profile?')) {
      const patientIdObj = { value: this.patient.id }; // Create the PatientId object
      this.patientService.deletePatientProfile(patientIdObj).subscribe(
        () => {
          alert('Profile deleted');
          this.router.navigate(['/login']);
        },
        (error: any) => {  // Explicitly type 'error' as 'any'
          console.error('Error deleting profile', error);
        }
      );
    }
  }
}
