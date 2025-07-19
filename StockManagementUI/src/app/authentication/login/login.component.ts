import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { LoginDto } from '../../core/dataContracts/loginDto';
import { AuthService } from '../../core/services/auth.service';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';

@Component({
  selector: 'app-login',
  imports: [CommonModule, ReactiveFormsModule, FormsModule, 
    RouterModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatSnackBarModule, MatProgressSpinnerModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  loginForm: FormGroup;
  isLoading = false;


  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private snackBar: MatSnackBar
  )
   {
    this.loginForm = this.fb.group({
      email: ['', [Validators.email]],
      password: ['', [Validators.required]]
    });
  }

  login() {
    if (this.loginForm.valid) {
      this.isLoading = true; // Set loading state
      const loginDto: LoginDto = this.loginForm.value;

      this.authService.login(loginDto).subscribe({
        next: (result: any) => {
          this.isLoading = false; // Reset loading state
          localStorage.setItem('token', result.token);
          this.snackBar.open('Login successful!', 'Close', {
            duration: 2000,
          });
          this.router.navigate(['/platform/products']);
        },
        error: (err: string) => {
          this.isLoading = false; // Reset loading state
          this.snackBar.open('Login failed. Please check your credentials.', 'Close', { duration: 3000 });
          console.log('Login failed:', err);
        }
      });
    }
  }
}
