import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  standalone: false
})
export class RegisterComponent {
  error = '';
  loading = false;

  readonly form = this.fb.group({
    name: ['', [Validators.required, Validators.minLength(2)]],
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(8)]],
    role: ['STUDENT', [Validators.required]]
  });

  constructor(private readonly fb: FormBuilder, private readonly auth: AuthService, private readonly router: Router) {}

  submit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.error = '';
    this.loading = true;

    this.auth.register(this.form.getRawValue() as { name: string; email: string; password: string; role: string }).subscribe({
      next: (response) => {
        this.router.navigate([`/${response.role.toLowerCase()}`]);
      },
      error: () => {
        this.error = 'Unable to register';
        this.loading = false;
      },
      complete: () => {
        this.loading = false;
      }
    });
  }
}
