import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PatientService } from '../../app/Services/patient.service';
import { PatientDTO } from '../Interfaces/patientId'; // Adjust the import if needed
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-update-patient',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule], // Use ReactiveFormsModule here
  templateUrl: './update-patient.component.html',
  styleUrls: ['./update-patient.component.css']
})
export class UpdatePatientComponent implements OnInit {
  updatePatientForm: FormGroup;
  patientId: string | null = null;
  errorMessage: string | null = null;

  constructor(
    private patientService: PatientService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private fb: FormBuilder
  ) {
    this.updatePatientForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', [Validators.required, Validators.pattern('^[0-9]{9}$')]], // Assuming phone needs to be a 9-digit number
    });
  }

  ngOnInit(): void {
    this.patientId = this.activatedRoute.snapshot.paramMap.get('id');
    
    if (this.patientId) {
      this.patientService.getPatientById(this.patientId).subscribe(
        (data: PatientDTO) => {
          this.updatePatientForm.patchValue({
            firstName: data.firstname,
            lastName: data.lastname,
            email: data.email,
            phone: data.phone,
          });
        },
        (error) => {
          this.errorMessage = error.error.Message || 'Failed to load patient data';
        }
      );
    }
  }

  onSubmit(): void {
    if (this.updatePatientForm.invalid) {
      return; // Prevent submission if the form is invalid
    }

    const updatedPatient: PatientDTO = {
      id: this.patientId!,
      firstname: this.updatePatientForm.value.firstName,
      lastname: this.updatePatientForm.value.lastName,
      email: this.updatePatientForm.value.email,
      phone: this.updatePatientForm.value.phone,
    };

    this.patientService.updatePatient(updatedPatient).subscribe(
      (response) => {
        console.log('Patient updated successfully');
        alert('Patient profile updated!');
        this.router.navigate(['/patient-dashboard']);
      },
      (error) => {
        this.errorMessage = error.error.Message || 'An error occurred during update';
        console.error('Error updating patient:', error);
      }
    );
  }
}
