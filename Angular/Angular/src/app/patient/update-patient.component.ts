import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PatientService } from '../../app/Services/patient.service';
import { PatientDTO } from '../Interfaces/patientId'; // Adjust the import if needed
import { FormsModule } from '@angular/forms'; 
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-update-patient',
  standalone: true, // Indicate this is a standalone component
  imports: [CommonModule, FormsModule], // Include FormsModule here
  templateUrl: './update-patient.component.html',
  styleUrls: ['./update-patient.component.css']
})
export class UpdatePatientComponent implements OnInit {
  patient: PatientDTO = { // Initialize patient as an object conforming to the interface
    id: '',
    firstname: '',
    lastname: '',
    email: '',
    phone: ''
    // Initialize other properties as needed
  };
  errorMessage: string | null = null;

  constructor(
    private patientService: PatientService,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    const patientId = this.activatedRoute.snapshot.paramMap.get('id');
    if (patientId) {
      this.patientService.getPatientById(patientId).subscribe(
        data => {
          this.patient = data;
        },
        error => {
          this.errorMessage = error.error.Message || 'Failed to load patient data';
        }
      );
    }
  }

  onSubmit(): void {
    this.patientService.updatePatient(this.patient).subscribe(
      response => {
        this.router.navigate(['/patient-dashboard']);
      },
      error => {
        this.errorMessage = error.error.Message || 'An error occurred during update';
      }
    );
  }
}
