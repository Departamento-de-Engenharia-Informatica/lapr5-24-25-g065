import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  // This is just a placeholder. You might want to use tokens or other methods to track user authentication.
  private loggedIn: boolean = false;

  constructor() {}

  login() {
    this.loggedIn = true;
    localStorage.setItem('loggedIn', 'true');
  }

  logout() {
    this.loggedIn = false;
    localStorage.removeItem('loggedIn');
  }

  isAuthenticated(): boolean {
    return localStorage.getItem('loggedIn') === 'true';
  }
}
