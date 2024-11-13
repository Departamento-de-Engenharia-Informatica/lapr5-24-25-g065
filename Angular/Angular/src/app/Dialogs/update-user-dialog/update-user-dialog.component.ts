import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { User } from '../../Interfaces/user';
import { UserService } from '../../Services/user.service';

@Component({
  selector: 'app-update-user-dialog',
  templateUrl: './update-user-dialog.component.html',
  styleUrls: ['./update-user-dialog.component.scss']
})
export class UpdateUserDialogComponent implements OnInit {

  updateUser!: User;

  updateForm = new FormGroup({
    userName: new FormControl('', [Validators.required, Validators.maxLength(50)]),
    email: new FormControl('', [Validators.required, Validators.email]),
    role: new FormControl('', Validators.required),
  });

  userToUpdate!: User;

  constructor(
    public dialogRef: MatDialogRef<UpdateUserDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: User,
    private userService: UserService
  ) {
    this.userToUpdate = data;
  }

  ngOnInit() {
    // Pre-fill form with existing user data
    this.updateForm.controls['userName'].setValue(this.userToUpdate.userName);
    this.updateForm.controls['email'].setValue(this.userToUpdate.email);
    this.updateForm.controls['role'].setValue(this.userToUpdate.role);
    console.log("Loaded user data for update:", this.userToUpdate);
  }

  onSubmit() {
    // Create updated User object
    this.updateUser = {
      ...this.userToUpdate,
      userName: this.updateForm.controls['userName'].value as string,
      email: this.updateForm.controls['email'].value as string,
      role: this.updateForm.controls['role'].value as string,
    };

    // Call service to update the user
    this.userService.updateUser(this.updateUser).subscribe(() => {
      console.log("Updated user data:", this.updateUser);
      this.dialogRef.close();
    });
  }

  onCancel() {
    this.dialogRef.close();
  }
}
