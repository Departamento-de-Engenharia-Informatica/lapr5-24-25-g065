import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = 'https://localhost:5001/api/Patient'; // Replace with your API URL

  constructor(private http: HttpClient) {}

  login(email: string, password: string): Observable<any> {
    // Sending login request to the backend (assuming you have an authenticate endpoint)
    return this.http.post<any>(`${this.apiUrl}authenticate`, { email, password }).pipe(
      tap(response => {
        // If authentication is successful, store the email in localStorage
        localStorage.setItem('userEmail', email);
      })
    );
  }

  logout(): void {
    // Remove email from localStorage when user logs out
    localStorage.removeItem('userEmail');
  }
}
