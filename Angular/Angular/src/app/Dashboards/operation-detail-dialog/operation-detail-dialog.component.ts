import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-operation-detail-dialog',
  templateUrl: './operation-detail-dialog.component.html',
  styleUrls: ['./operation-detail-dialog.component.css']
})
export class OperationDetailDialogComponent {
  constructor(@Inject(MAT_DIALOG_DATA) public data: any,private http: HttpClient) {}

  onDelete(): void {
    this.deleteOperation();
  }
  onClose(): void {
  }

  deleteOperation() {
    const deleteUrl = `https://10.9.10.65/api/operationRequest/${this.data.id}/hard`; // Replace with your actual delete endpoint
  
    this.http.delete(deleteUrl).subscribe({
      next: () => {
        // Successfully deleted, remove the operation from the list
        console.log('Operation deleted successfully');
      },
      error: (err) => {
        console.error('Error deleting operation:', err);
      }
    });
  }
}
