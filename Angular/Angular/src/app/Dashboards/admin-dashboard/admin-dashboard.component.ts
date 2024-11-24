import { Component, OnInit } from '@angular/core';
import { UserService } from '../../Services/user.service';
import { Router } from '@angular/router';
import { User } from '../../Interfaces/user';
import { StaffService } from '../../Services/staff.service';
import { Staff } from '../../Interfaces/staff';

@Component({
  selector: 'admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent implements OnInit {

  staffs: Staff[] = []; 
  loading: boolean = true;
  error: string | null = null;

  constructor(private staffService: StaffService) {}

  ngOnInit(): void {
   
  }

  loadStaffs(): void{
    this.staffService.getStaffs().subscribe({
      next: (data) => {
        this.staffs = data;
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
