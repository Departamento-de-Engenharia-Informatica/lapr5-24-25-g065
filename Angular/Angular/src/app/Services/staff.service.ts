import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Staff } from '../Interfaces/staff';

@Injectable({
  providedIn: 'root'
})
export class StaffService {

  constructor(private http: HttpClient) { }

  getStaffs() {
    return this.http.get<Staff[]>('https://localhost:5001/api/Staff');
  }

  createStaff(newStaff: Staff) {

    return this.http.post<Staff>('https://10.9.10.65:5001/api/Staff',newStaff);

  }

  updateStaff(updateStaff: Staff) {
    const link = `https://localhost:5001/api/Staff/${updateStaff.Id.value}`;
    return this.http.put<Staff>(link, updateStaff);
  }

  deleteStaff(id: string) {
    let link : string ='https://localhost:5001/api/Staff/'+id;
    return this.http.delete<Staff>(link);
  }
}