import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SocialAuthService, GoogleSigninButtonModule, SocialUser } from '@abacritt/angularx-social-login';
import { Router } from '@angular/router';
import { UserService } from '../Services/user.service';
import { User } from '../Interfaces/user';
import { firstValueFrom } from 'rxjs';
import { RouterModule } from '@angular/router';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule,
    GoogleSigninButtonModule
  ],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  constructor(private authService: SocialAuthService, private router: Router, private userService: UserService,) {}

  ngOnInit(): void {
    this.authService.authState.subscribe(async (user: SocialUser) => {
      if (user) {
        const email = user.email;
  
        try {
          // Wait for the observable to emit its first value and resolve
          const currentUser = await firstValueFrom(this.userService.getUserByEmail(email));
          this.redirectUser(currentUser.role);
        } catch (error) {
          console.error('Error retrieving user by email:', error);
        }
      }
    });
  }
  

  private redirectUser(role: string): void {
    switch (role) {
      case 'Admin':
        this.router.navigate(['/admin-dashboard']);
        break;
      case 'Doctor':
        this.router.navigate(['/doctor-dashboard']);
        break;
      case 'Nurse':
        this.router.navigate(['/nurse-dashboard']);
        break;
      case 'Technician':
        this.router.navigate(['/technician-dashboard']);
        break;
      case 'Patient':
        this.router.navigate(['/patient-dashboard']);
        break;
      default:
        this.router.navigate(['/']); // Redirect to home or an error page if role is unknown
    }
  }
}
