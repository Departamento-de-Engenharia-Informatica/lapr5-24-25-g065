import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { PatientService } from '../Services/patient.service';  // Assuming you have a service to interact with the backend

@Component({
  selector: 'app-register-patient',
  templateUrl: './register-patient.component.html',
  styleUrls: ['./register-patient.component.css']
})
export class RegisterPatientComponent implements OnInit {
  registerPatientForm: FormGroup;
  errorMessage: string = '';
  successMessage: string = '';
  
  constructor(
    private fb: FormBuilder,
    private patientService: PatientService,
    private router: Router
  ) { 
    this.registerPatientForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', [Validators.required, Validators.pattern('^[0-9]{9}$')]],  // Assuming phone should be 9 digits
    });
  }

  ngOnInit(): void { }

  onSubmit(): void {
    if (this.registerPatientForm.invalid) {
      return;
    }

    const patientData = this.registerPatientForm.value;

    this.patientService.registerPatient(patientData).subscribe(
      response => {
        this.successMessage = response.message;
        setTimeout(() => {
          this.router.navigate(['/login']);
        }, 2000);
      },
      error => {
        this.errorMessage = error.error.Message || 'An error occurred during registration.';
      }
    );
  }
}
