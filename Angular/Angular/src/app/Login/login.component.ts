import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SocialAuthService, GoogleSigninButtonModule, SocialUser } from '@abacritt/angularx-social-login';
import { NavigationStart, Router, RouterOutlet } from '@angular/router';
import { UserService } from '../Services/user.service';
import { User } from '../Interfaces/user';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    CommonModule,
    GoogleSigninButtonModule,
    RouterOutlet
  ],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  constructor(
    private authService: SocialAuthService,
    private router: Router,
    private userService: UserService
  ) {
    // Listen to router navigation events
    this.router.events.subscribe(event => {
      if (event instanceof NavigationStart) {
        console.log('Navigating to:', event.url);  // This prints the URL Angular is trying to navigate to
      }
    })}

  ngOnInit(): void {
    this.authService.authState.subscribe(async (user: SocialUser) => {
      if (user) {
        console.log("User logged in successfully:", user);  // Debug log
        const email = user.email;

        try {
          const currentUser = await firstValueFrom(this.userService.getUserByEmail(email));
          console.log("User data retrieved:", currentUser);  // Debug log
          this.redirectUser(currentUser.role);
        } catch (error) {
          console.error('Error retrieving user by email:', error);
        }
      } else {
        console.log("No user logged in.");  // Debug log
      }
    });
  }

  private redirectUser(role: number): void {
    console.log("Redirecting user with role:", role);  // Debug log

    switch (role) {
      case 0: // Admin
        this.router.navigate(['/admin-dashboard']).then((navigationSuccess) => {
          console.log('Navigation to /admin-dashboard was successful:', navigationSuccess);
        }).catch((err) => {
          console.error('Navigation error:', err);
        });
        break;
      case 1: // Doctor
        this.router.navigate(['/doctor-dashboard']);
        break;
      case 2: // Nurse
        this.router.navigate(['/nurse-dashboard']);
        break;
      case 3: // Technician
        this.router.navigate(['/technician-dashboard']);
        break;
      case 4: // Patient
        this.router.navigate(['/patient-dashboard']);
        break;
      default:
        console.warn("Unknown role:", role);  // Debug log
        this.router.navigate(['/']); // Redirect to home or an error page if role is unknown
    }
  }
}