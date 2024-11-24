import { Component ,OnInit} from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { HttpClient } from '@angular/common/http';
import { OperationRequestService } from '../../Services/operationRequest.service';
@Component({
  selector: 'app-operation-request-dialog',
  templateUrl: './operation-request-dialog.component.html',
  styleUrls: ['./operation-request-dialog.component.css']
})
export class OperationRequestDialogComponent implements OnInit {
  operationRequest = {
    patientID: '',
    doctorID: '',
    operationTypeID: '',
    operationDateTime: '',
    deadline: '',
    priority: 0,
  };

  patients: any[] = []; 
  operationTypes: any[] = [];

  loading = true;
  error = '';

  constructor(public dialogRef: MatDialogRef<OperationRequestDialogComponent>,private http: HttpClient) {}


  ngOnInit() {
    this.getOperationTypes();
    this.getPatients();
  }

  getPatients() {
    this.http.get<any>('https://localhost:5001/api/Patient')
      .subscribe({
        next: (response) => {
          this.patients = response.data// Assuming the response contains an array of patients
          this.loading = false;
          console.log(response.data);
        },
        error: (err) => {
          this.error = 'Failed to load patients';
          console.error(err);
          this.loading = false;
        },
      });
  }

  getOperationTypes() {
    this.http.get<any>('https://localhost:5001/api/OperationType').subscribe({
      next: (response) => {
        console.log('API Response for operation types:', response);
        this.operationTypes = response;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Failed to load operation types';
        console.error('Error:', err);
        this.loading = false;
      }
    });
  }

  onSave(): void {
    const url = 'https://localhost:5001/api/operationRequest'; // Replace with your API endpoint

   // Construct the JSON body
  const jsonBody = {
    patientID: this.operationRequest.patientID ,
    doctorID: this.operationRequest.doctorID ,
    operationTypeID: this.operationRequest.operationTypeID ,
    operationDateTime: this.operationRequest.operationDateTime,
    deadline: this.operationRequest.deadline,
    priority: this.operationRequest.priority
  };
  console.log(jsonBody);
  const headers = { 'Content-Type': 'application/json' };

  // Make the POST request
  this.http.post(url, jsonBody, { headers }).subscribe(
    (response) => {
      console.log('Operation Request saved successfully:', response);
      // Close the dialog or reset the form after successful save
    },
    (error) => {
      console.error('Error saving Operation Request:', error);
    }
  );
  }
  onShow():void{
    const jsonBody = {
      patientID: this.operationRequest.patientID ,
      doctorID: this.operationRequest.doctorID ,
      operationTypeID: this.operationRequest.operationTypeID ,
      operationDateTime: this.operationRequest.operationDateTime,
      deadline: this.operationRequest.deadline,
      priority: this.operationRequest.priority
    };
    console.log(jsonBody);
  }
  onCancel(): void {
    this.dialogRef.close(); // Close the dialog without saving
  }
}
