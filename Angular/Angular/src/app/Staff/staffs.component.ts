import { Component, OnInit } from '@angular/core';
import { StaffService } from '../Services/staff.service';
import { Staff } from '../Interfaces/staff';
import { MatDialog } from '@angular/material/dialog';
import { UpdateStaffDialogComponent } from './Dialogs/update-staff-dialog/update-staff-dialog.component';
import { DeleteStaffDialogComponent } from './Dialogs/delete-staff-dialog/delete-staff-dialog.component';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-staffs',
  templateUrl: './staffs.component.html',
  styleUrls: ['./staffs.component.scss']
})
export class staffsComponent implements OnInit {

  staffsDataArray: any = [];

  dataSource = new MatTableDataSource<Staff>();

  columnsToDisplay = ['FirstName', 'LastName', 'FullName', 'Gender', 'Specialization', 'PhoneNumber', 'Email', 'Update', 'Delete'];

  constructor(private staffsService: StaffService, private dialog: MatDialog) {

  }

  ngOnInit() {
    this.staffsService.getStaffs().subscribe({
      next:(data) => {
        this.staffsDataArray = data;
        this.dataSource = new MatTableDataSource<Staff>(this.staffsDataArray);
      },
      error: (err) => {
        console.log(err);
      }
    });
    
  }

  onUpdate(Staff: Staff) {
    let dialogRef = this.dialog.open(UpdateStaffDialogComponent, {
      height: '500px',
      width: '500px',
      data: Staff,
    });
  }

  onDelete(Staff: Staff) {
    let dialogRef = this.dialog.open(DeleteStaffDialogComponent, {
      height: '500px',
      width: '500px',
      data: Staff,
    });

    dialogRef.afterClosed().subscribe(result => {
      this.updateDataSource(this.staffsDataArray);
    });
  }

  updateDataSource(dataArray: Staff[]) {
    this.dataSource.connect().next(dataArray);
  }

}
