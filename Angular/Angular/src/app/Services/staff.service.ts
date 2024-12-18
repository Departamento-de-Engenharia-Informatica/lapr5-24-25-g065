import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Staff } from '../Interfaces/staff';
import { StaffId } from '../Interfaces/staffId';

@Injectable({
  providedIn: 'root'
})
export class StaffService {

  constructor(private http: HttpClient) { }

  getStaffs() {
    console.log("getting staffs");
    return this.http.get<Staff[]>('https://localhost:5001/api/Staff');
  }

  createStaff(newStaff: Staff) {

    return this.http.post<Staff>('https://localhost:5001/api/Staff',newStaff);

  }

  updateStaff(updateStaff: any) {
    const link = `https://localhost:5001/api/Staff/${updateStaff.Id}`;
    return this.http.put<Staff>(link, updateStaff);
  }

  deleteStaff(id: StaffId) {
    const link = `https://localhost:5001/api/Staff/${id}/hard`;
    return this.http.delete<Staff>(link);
  }
}