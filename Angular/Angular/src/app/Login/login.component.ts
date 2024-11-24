import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SocialAuthService, GoogleSigninButtonModule, SocialUser } from '@abacritt/angularx-social-login';
import { NavigationStart, Router, RouterOutlet } from '@angular/router';
import { UserService } from '../Services/user.service';
import { PatientService } from '../Services/patient.service'; // Import PatientService
import { User } from '../Interfaces/user';
import { firstValueFrom } from 'rxjs';
import { PatientDTO } from '../Interfaces/patientId'; // Assuming you have a Patient interface

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
    private userService: UserService,
    private patientService: PatientService // Inject PatientService
  ) {
    // Listen to router navigation events
    this.router.events.subscribe(event => {
      if (event instanceof NavigationStart) {
        console.log('Navigating to:', event.url);  // This prints the URL Angular is trying to navigate to
      }
    });
  }

  ngOnInit(): void {
    this.authService.authState.subscribe(async (user: SocialUser) => {
      if (user) {
        console.log("User logged in successfully:", user);  // Debug log
        const email = user.email;
    
        try {
          const currentUser = await firstValueFrom(this.userService.getUserByEmail(email));
          console.log("User data retrieved:", currentUser);  // Debug log
    
          // Now, pass the email to the Patient Service if the user is a patient
          if (currentUser.role === 4) { // Assuming 4 is the role for "Patient"
            this.patientService.getPatientByEmail(email).subscribe(
              (patient: PatientDTO) => {  // Define patient type
                console.log('Patient profile fetched:', patient);
                this.redirectUser(currentUser.role);
              },
              (error: any) => {  // You can use a more specific type for error if needed
                console.error('Error fetching patient profile:', error);
                // Handle error, perhaps navigate to patient registration page
              }
            );
          } else {
            this.redirectUser(currentUser.role); // For other roles like Admin, Doctor, etc.
          }
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
