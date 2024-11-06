import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Staff } from '../../Interfaces/staff';
import { StaffService } from '../../Services/staff.service';

@Component({
  selector: 'app-update-staff-dialog',
  templateUrl: './update-staff-dialog.component.html',
  styleUrls: ['./update-staff-dialog.component.scss']
})
export class UpdateStaffDialogComponent implements OnInit {

  updateStaff!: Staff;

  updateForm = new FormGroup({
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
  });

  staffToUpdate!: Staff;

  constructor(
    public dialogRef: MatDialogRef<UpdateStaffDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Staff,
    private staffsService: StaffService
  ) {
    this.staffToUpdate = data;
  }

  ngOnInit() {
    // Pre-fill form with existing staff data
    this.updateForm.controls['firstName'].setValue(this.staffToUpdate.FirstName);
    this.updateForm.controls['lastName'].setValue(this.staffToUpdate.LastName);
    this.updateForm.controls['fullName'].setValue(this.staffToUpdate.FullName);
    this.updateForm.controls['gender'].setValue(this.staffToUpdate.Gender);
    this.updateForm.controls['type'].setValue(this.staffToUpdate.Type);
    this.updateForm.controls['specialization'].setValue(this.staffToUpdate.Specialization);
    this.updateForm.controls['licenseNumber'].setValue(this.staffToUpdate.LicenseNumber);
    this.updateForm.controls['availabilitySlot'].setValue(this.staffToUpdate.AvailabilitySlot);
    this.updateForm.controls['phoneNumber'].setValue(this.staffToUpdate.PhoneNumber);
    this.updateForm.controls['email'].setValue(this.staffToUpdate.Email);
    console.log("Loaded staff data for update:", this.staffToUpdate);
  }

  onSubmit() {
    // Create updated Staff object
    this.updateStaff = {
      Id: this.staffToUpdate.Id,
      FirstName: this.updateForm.controls['firstName'].value as string,
      LastName: this.updateForm.controls['lastName'].value as string,
      FullName: this.updateForm.controls['fullName'].value as string,
      Gender: this.updateForm.controls['gender'].value as string,
      Type: this.updateForm.controls['type'].value as string,
      Specialization: this.updateForm.controls['specialization'].value as string,
      LicenseNumber: this.updateForm.controls['licenseNumber'].value as string,
      UserId: this.staffToUpdate.UserId, // Preserve existing UserId
      AvailabilitySlot: this.updateForm.controls['availabilitySlot'].value as string,
      PhoneNumber: this.updateForm.controls['phoneNumber'].value as string,
      Email: this.updateForm.controls['email'].value as string,
      Appointments: this.staffToUpdate.Appointments, // Preserve existing appointments
    };

    // Call service to update the staff member
    this.staffsService.updateStaff(this.updateStaff).subscribe(() => {
    });
    console.log("Updated staff data:", this.updateStaff);
    this.dialogRef.close();
  }

  onCancel() {
    this.dialogRef.close();
  }
}
