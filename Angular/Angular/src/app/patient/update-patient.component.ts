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
            firstname: this.updatePatientForm.value.firstName,
            lastname: this.updatePatientForm.value.lastName,
            fullName: `${this.updatePatientForm.value.firstName} ${this.updatePatientForm.value.lastName}`, // Assuming fullName is the combination of first and last name
            gender: this.updatePatientForm.value.gender, // Ensure gender is in your form
            allergies: this.updatePatientForm.value.allergies || [], // Handle allergies (array of strings), default to empty array if null or undefined
            emergencyContact: this.updatePatientForm.value.emergencyContact, // Make sure emergencyContact is present in the form
            dateOfBirth: this.updatePatientForm.value.dateOfBirth, // Ensure dateOfBirth is in your form
            medicalRecordNumber: this.updatePatientForm.value.medicalRecordNumber, // Ensure medicalRecordNumber is in your form
            phoneNumber: this.updatePatientForm.value.phone,
            email: this.updatePatientForm.value.email,
            userName: ''
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
      id: this.patientId!, // Patient ID (Ensure patientId is properly assigned)
      firstname: this.updatePatientForm.value.firstName,
      lastname: this.updatePatientForm.value.lastName,
      fullName: `${this.updatePatientForm.value.firstName} ${this.updatePatientForm.value.lastName}`, // Assuming fullName is the combination of first and last name
      gender: this.updatePatientForm.value.gender, // Ensure gender is in your form
      allergies: this.updatePatientForm.value.allergies || [], // Handle allergies (array of strings), default to empty array if null or undefined
      emergencyContact: this.updatePatientForm.value.emergencyContact, // Make sure emergencyContact is present in the form
      dateOfBirth: this.updatePatientForm.value.dateOfBirth, // Ensure dateOfBirth is in your form
      medicalRecordNumber: this.updatePatientForm.value.medicalRecordNumber, // Ensure medicalRecordNumber is in your form
      phoneNumber: this.updatePatientForm.value.phone,
      email: this.updatePatientForm.value.email,
      userName: ''
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
