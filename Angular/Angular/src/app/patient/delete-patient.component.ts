import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PatientId, PatientService } from '../../app/Services/patient.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-delete-patient',
  standalone: true, // Indicate this is a standalone component
  imports: [CommonModule, FormsModule], // Include FormsModule here
  templateUrl: './delete-patient.component.html',
  styleUrls: ['./delete-patient.component.css']
})
export class DeletePatientComponent implements OnInit {
  patientId: string | null = null;
  errorMessage: string | null = null;

  constructor(
    private patientService: PatientService,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.patientId = this.activatedRoute.snapshot.paramMap.get('id');
  }

  onDelete(): void {
    if (this.patientId) {
      // Wrap the patientId in an object of type PatientId
      const patientIdObj: PatientId = { value: this.patientId };
  
      this.patientService.deletePatientProfile(patientIdObj).subscribe(
        () => {
          this.router.navigate(['/patient-dashboard']);
        },
        error => {
          this.errorMessage = error.error.Message || 'An error occurred during deletion';
        }
      );
    }
  }
  
    }
  
