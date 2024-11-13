import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { StaffService } from '../Services/staff.service';
import { Staff } from '../Interfaces/staff';

@Component({
  selector: 'app-newStaff',
  templateUrl: './newStaff.component.html',
  styleUrls: ['./newStaff.component.scss']
})
export class NewStaffComponent {

  newStaff!: Staff;

  // Form setup for full Staff entity
  StaffForm = new FormGroup({
    firstName: new FormControl('', [Validators.required, Validators.maxLength(50)]),
    lastName: new FormControl('', [Validators.required, Validators.maxLength(50)]),
    fullName: new FormControl('', [Validators.required, Validators.maxLength(100)]),
    gender: new FormControl('', Validators.required),
    type: new FormControl('', Validators.required),
    specialization: new FormControl('', [Validators.required, Validators.maxLength(100)]),
    licenseNumber: new FormControl('', [Validators.required, Validators.maxLength(20)]),
    availabilitySlot: new FormControl('', Validators.required),
    phoneNumber: new FormControl('', [Validators.required, Validators.pattern(/^[0-9]+$/), Validators.maxLength(20)]),
    email: new FormControl('', [Validators.required, Validators.email]),
    address: new FormControl('', [Validators.required, Validators.maxLength(200)]),
  });

  constructor(private router: Router, private staffsService: StaffService) { }

  onSubmit() {
    // Create a new Staff object with all fields
    this.newStaff = {
      Id: 0,  // Assuming 'Id' is generated on the server
      FirstName: this.StaffForm.controls['firstName'].value as string,
      LastName: this.StaffForm.controls['lastName'].value as string,
      FullName: this.StaffForm.controls['fullName'].value as string,
      Gender: this.StaffForm.controls['gender'].value as string,
      Type: this.StaffForm.controls['type'].value as string,
      Specialization: this.StaffForm.controls['specialization'].value as string,
      LicenseNumber: this.StaffForm.controls['licenseNumber'].value as string,
      UserId: { id: 'user-id-placeholder' },  // Replace with actual UserId
      AvailabilitySlot: this.StaffForm.controls['availabilitySlot'].value as string,
      PhoneNumber: this.StaffForm.controls['phoneNumber'].value as string,
      Email: this.StaffForm.controls['email'].value as string,
      Address: this.StaffForm.controls['address'].value as string,
      Appointments: []  // Start with an empty array, or populate if needed
    };

    // Save the new staff record
    this.staffsService.createStaff(this.newStaff).subscribe(() => {
      
    });
    console.log("Staff created:", this.newStaff);
    this.router.navigate(['/Staffs']);
  }

  onCancel() {
    this.router.navigate(['/Staffs']);
  }
}
