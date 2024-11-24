import { Component } from '@angular/core';
import { OnInit } from '@angular/core';
import { OperationRequestService } from '../../Services/operationRequest.service';


@Component({
  selector: 'app-operation-dashboard',
  templateUrl: './operation-dashboard.component.html',
  styleUrls: ['./operation-dashboard.component.css'],
  
})
export class OperationDashboardComponent implements OnInit {
  operations: any[] = [];
  loading: boolean = true;
  error: string | null = null;

  constructor(private operationService: OperationRequestService) {}

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

  
