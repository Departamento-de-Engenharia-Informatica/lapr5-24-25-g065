import { Component, OnInit } from '@angular/core';
import { UserService } from '../../Services/user.service';
import { Router } from '@angular/router';
import { User } from '../../Interfaces/user';
import { StaffService } from '../../Services/staff.service';
import { Staff } from '../../Interfaces/staff';
import { S } from '@angular/cdk/keycodes';
import { StaffId } from '../../Interfaces/staffId';

@Component({
  selector: 'admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent implements OnInit {

  staffs: Staff[] = []; 
  loading: boolean = true;
  error: string | null = null;
  showStaffList = false; // Initialize with `false`

  constructor(private staffService: StaffService) {}

  ngOnInit(): void {
   
  }

  loadStaffs(): void {
    this.showStaffList = true; // Show the staff list
    this.loading = true; // Show loading state
    this.error = null; // Clear any previous errors
  
    this.staffService.getStaffs().subscribe({
      next: (staffs) => {
        // Map the API response to match the expected structure
        this.staffs = staffs.map((staff: any) => ({
          Id: staff.id,
          FirstName: staff.firstname,
          LastName: staff.lastname,
          FullName: staff.fullName,
          Gender: staff.gender,
          Specialization: staff.specialization,
          PhoneNumber: staff.phoneNumber,
          Email: staff.email,
          LicenseNumber: staff.licenseNumber,
          Type: staff.type,
          AvailabilitySlot: staff.availabilitySlot,
          UserId: staff.userId, // Assuming this is an object or null
          Appointments: staff.appointments || [] // Default to empty array if null
        }));
        console.log(this.staffs);
        this.loading = false; // Hide loading state after data is loaded
      },
      error: (err) => {
        this.error = 'Failed to load staff list.';
        console.error(err);
        this.loading = false; // Hide loading state in case of error
      },
    });
  }
  
  onCreate(): void {
    console.log('Create staff');
  }

  onUpdate(staff: any): void {
    console.log('Update staff:', staff);
  }

  onDelete(index : number): void {
    console.log("Deleting staff: ",this.staffs[index].FullName);
    console.log(this.staffs[index].Id.value)
    this.staffService.deleteStaff(this.staffs[index].Id).subscribe({
      next: (response) => {
        console.log("Delete successful:", response);
        this.loadStaffs(); // Reload staff list after successful deletion
      },
      error: (err) => {
        console.error("Error deleting staff:", err);
        this.error = 'Failed to delete staff member.'; // Handle the error case
      }
    });
  }
}
