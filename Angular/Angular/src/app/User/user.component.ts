import { Component, OnInit } from '@angular/core';
import { UserService } from '../Services/user.service';
import { User } from '../Interfaces/user';
import { MatDialog } from '@angular/material/dialog';
import { UpdateUserDialogComponent } from '../Dialogs/update-user-dialog/update-user-dialog.component';
import { DeleteUserDialogComponent } from '../Dialogs/delete-user-dialog/delete-user-dialog.component';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-users',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {

  usersDataArray: any = [];
  dataSource = new MatTableDataSource<User>();
  columnsToDisplay = ['FirstName', 'LastName', 'FullName', 'Role', 'Email', 'PhoneNumber', 'Update', 'Delete'];

  constructor(private userService: UserService, private dialog: MatDialog) {}

  ngOnInit() {
    this.userService.getUsers().subscribe({
      next: (data) => {
        this.usersDataArray = data;
        this.dataSource = new MatTableDataSource<User>(this.usersDataArray);
      },
      error: (err) => {
        console.error('Error fetching users:', err);
      }
    });
  }

  onUpdate(user: User) {
    const dialogRef = this.dialog.open(UpdateUserDialogComponent, {
      height: '500px',
      width: '500px',
      data: user,
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.refreshUserData();
      }
    });
  }

  onDelete(user: User) {
    const dialogRef = this.dialog.open(DeleteUserDialogComponent, {
      height: '500px',
      width: '500px',
      data: user,
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.refreshUserData();
      }
    });
  }

  refreshUserData() {
    this.userService.getUsers().subscribe({
      next: (data) => {
        this.usersDataArray = data;
        this.dataSource.data = this.usersDataArray;
      },
      error: (err) => {
        console.error('Error refreshing user data:', err);
      }
    });
  }
}

