/*import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Staff } from '../../Interfaces/staff';
import { StaffService } from '../../Services/staff.service';

@Component({
  selector: 'app-delete-staff-dialog',
  templateUrl: './delete-staff-dialog.component.html',
  styleUrls: ['./delete-staff-dialog.component.scss']
})
export class DeleteStaffDialogComponent implements OnInit {

  staffToDelete!: Staff;

  deleteForm = new FormGroup({
    firstName: new FormControl({ value: '', disabled: true }),
    lastName: new FormControl({ value: '', disabled: true }),
    fullName: new FormControl({ value: '', disabled: true }),
    gender: new FormControl({ value: '', disabled: true }),
    type: new FormControl({ value: '', disabled: true }),
    specialization: new FormControl({ value: '', disabled: true }),
    licenseNumber: new FormControl({ value: '', disabled: true }),
    availabilitySlot: new FormControl({ value: '', disabled: true }),
    phoneNumber: new FormControl({ value: '', disabled: true }),
    email: new FormControl({ value: '', disabled: true }),
  });

  constructor(
    public dialogRef: MatDialogRef<DeleteStaffDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Staff,
    private staffsService: StaffService
  ) {
    this.staffToDelete = data;
  }

  ngOnInit() {
    // Populate form with staff details
    this.deleteForm.controls['firstName'].setValue(this.staffToDelete.FirstName);
    this.deleteForm.controls['lastName'].setValue(this.staffToDelete.LastName);
    this.deleteForm.controls['fullName'].setValue(this.staffToDelete.FullName);
    this.deleteForm.controls['gender'].setValue(this.staffToDelete.Gender);
    this.deleteForm.controls['type'].setValue(this.staffToDelete.Type);
    this.deleteForm.controls['specialization'].setValue(this.staffToDelete.Specialization);
    this.deleteForm.controls['licenseNumber'].setValue(this.staffToDelete.LicenseNumber);
    this.deleteForm.controls['availabilitySlot'].setValue(this.staffToDelete.AvailabilitySlot);
    this.deleteForm.controls['phoneNumber'].setValue(this.staffToDelete.PhoneNumber);
    this.deleteForm.controls['email'].setValue(this.staffToDelete.Email);
  }

  onSubmit() {
    const staffId = this.staffToDelete.Id.value;
    this.staffsService.deleteStaff(staffId).subscribe(() => {
    });
    console.log("Staff deleted:", staffId);
      this.dialogRef.close(true); // Pass `true` to indicate deletion success
  }

  onCancel() {
    this.dialogRef.close(false); // Pass `false` if deletion is canceled
  }
}*/
