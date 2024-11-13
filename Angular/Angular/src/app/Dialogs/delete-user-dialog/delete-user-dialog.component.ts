import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { User } from '../../Interfaces/user'; // Make sure to adjust the path
import { UserService } from '../../Services/user.service'; // Make sure to adjust the path

@Component({
  selector: 'app-delete-user-dialog',
  templateUrl: './delete-user-dialog.component.html',
  styleUrls: ['./delete-user-dialog.component.scss']
})
export class DeleteUserDialogComponent implements OnInit {

  userToDelete!: User;

  deleteForm = new FormGroup({
    userName: new FormControl({ value: '', disabled: true }),
    email: new FormControl({ value: '', disabled: true }),
    role: new FormControl({ value: '', disabled: true }),
  });

  constructor(
    public dialogRef: MatDialogRef<DeleteUserDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: User,
    private userService: UserService
  ) {
    this.userToDelete = data;
  }

  ngOnInit() {
    // Populate form with user details
    this.deleteForm.controls['userName'].setValue(this.userToDelete.userName);
    this.deleteForm.controls['email'].setValue(this.userToDelete.email);
    this.deleteForm.controls['role'].setValue(this.userToDelete.role);
  }

  onSubmit() {
    const userId = this.userToDelete.Id.value;
    this.userService.deleteUser(userId).subscribe(() => {
      console.log("User deleted:", userId);
      this.dialogRef.close(true); // Pass `true` to indicate deletion success
    });
  }

  onCancel() {
    this.dialogRef.close(false); // Pass `false` if deletion is canceled
  }
}
