import { Component } from '@angular/core';
import { OnInit } from '@angular/core';
import { OperationRequestService } from '../../Services/operationRequest.service';
import { MatDialog } from '@angular/material/dialog';
import { OperationRequestDialogComponent } from '../operation-request-dialog/operation-request-dialog.component';
import { OperationDetailDialogComponent } from '../operation-detail-dialog/operation-detail-dialog.component';


@Component({
  selector: 'app-operation-dashboard',
  templateUrl: './operation-dashboard.component.html',
  styleUrls: ['./operation-dashboard.component.css'],
  
})
export class OperationDashboardComponent implements OnInit {
  operations: any[] = [];
  loading: boolean = true;
  error: string | null = null;

  constructor(private operationService: OperationRequestService,private dialog: MatDialog) {}

  openOperationDetailsDialog(operation: any) {
    const dialogRef = this.dialog.open(OperationDetailDialogComponent, {
      width: '500px',
      data: operation // Pass the selected operation data to the dialog
    });
  
    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      // You can handle the result here if needed
    });
  }
  
  openAddOperationDialog(): void {
    const dialogRef = this.dialog.open(OperationRequestDialogComponent, {
      width: '400px'
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.operations.push(result); // Add the new operation to the list
        // Optionally, call an API to save the operation request
      }
    });
  }
  ngOnInit(): void {
    this.operationService.getAllOperationRequest().subscribe({
      next: (data) => {
        this.operations = data;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Failed to load operations.';
        console.error(err);
        this.loading = false;
      }
    });
  }
}

  
