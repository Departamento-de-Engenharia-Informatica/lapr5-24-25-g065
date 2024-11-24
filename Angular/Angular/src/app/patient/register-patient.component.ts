import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PatientService } from '../../app/Services/patient.service';
import { CreatePatientDTO } from '../Interfaces/patientId'; // Make sure to import the correct path
import { FormsModule } from '@angular/forms'; 
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-register-patient',
  standalone: true, // Indicate this is a standalone component
  imports: [CommonModule, FormsModule], // Include FormsModule here
  templateUrl: './register-patient.component.html',
  styleUrls: ['./register-patient.component.css']
})
export class RegisterPatientComponent implements OnInit {
  patient: CreatePatientDTO = { // Initialize patient as an object matching the interface
    firstname: '',
    lastname: '',
    email: '',
    phone: ''
    // Initialize other properties as needed
  };
  errorMessage: string | null = null;

  constructor(private patientService: PatientService, private router: Router) {}

  ngOnInit(): void {}

  onSubmit(): void {
    this.patientService.registerPatient(this.patient).subscribe(
      response => {
        this.router.navigate(['/login']);
      },
      error => {
        this.errorMessage = error.error.Message || 'An error occurred during registration';
      }
    );
  }
}
