import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../Interfaces/user'; 

@Injectable({
  providedIn: 'root',
})
export class UserService {
    constructor(private http: HttpClient) { }

    getUsers() {
      return this.http.get<User>('https://localhost:5001/api/User');
    }

    getUserByEmail(email: string)  {
      const link: string = `https://localhost:5001/api/Users/by-email/${encodeURIComponent(email)}`;
      return this.http.get<User>(link);
    }
  
    createUser(newUser: User) {

      return this.http.post<User>('https://localhost:5001/api/User',newUser);
  
    }
  
    updateUser(updateUser: User) {
      const link = `https://localhost:5001/api/User/${updateUser.Id.value}`;
      return this.http.put<User>(link, updateUser);
    }
  
    deleteUser(id: string) {
      let link : string ='https://localhost:5001/api/User/'+id;
      return this.http.delete<User>(link);
    }
}
