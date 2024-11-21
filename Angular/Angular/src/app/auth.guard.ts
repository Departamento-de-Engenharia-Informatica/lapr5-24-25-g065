import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service'; // You may need to adjust this based on your app structure

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> | Promise<boolean> | boolean {
    // Check if user is authenticated (you can check for token, user object, etc.)
    const isAuthenticated = this.authService.isAuthenticated();

    if (!isAuthenticated) {
      // Redirect to login if not authenticated
      this.router.navigate(['/login']);
      return false;
    }

    return true; // Allow route activation if authenticated
  }
}
