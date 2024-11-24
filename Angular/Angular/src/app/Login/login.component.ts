import { Component, OnInit, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { Router } from '@angular/router';
import { SocialAuthService, SocialUser } from '@abacritt/angularx-social-login';
import { UserService } from '../Services/user.service';
import { firstValueFrom } from 'rxjs';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { GoogleLoginProvider } from '@abacritt/angularx-social-login';


@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  constructor(
    private authService: SocialAuthService,
    private router: Router,
    private userService: UserService
  ) {}

  ngOnInit(): void {
    this.authService.authState.subscribe(async (user: SocialUser) => {
      if (user) {
        console.log("User logged in successfully:", user);
        const email = user.email;

        try {
          const currentUser = await firstValueFrom(
            this.userService.getUserByEmail(email)
          );
          console.log("User data retrieved:", currentUser);
          this.redirectUser(currentUser.role);
        } catch (error) {
          console.error("Error retrieving user by email:", error);
        }
      } else {
        console.log("No user logged in.");
      }
    });
  }

  onRegister(): void {
    this.router.navigate(['/register']);
  }

  private redirectUser(role: number): void {
    switch (role) {
      case 0: this.router.navigate(['/admin-dashboard']); break;
      case 1: this.router.navigate(['/doctor-dashboard']); break;
      case 2: this.router.navigate(['/nurse-dashboard']); break;
      case 3: this.router.navigate(['/technician-dashboard']); break;
      case 4: this.router.navigate(['/patient-dashboard']); break;
      default: this.router.navigate(['/']);
    }
  }
}
