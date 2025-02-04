  import { CommonModule } from '@angular/common';
  import { Component, OnInit } from '@angular/core';
  import { FormsModule, ReactiveFormsModule, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { NavbarComponent } from '../navbar/navbar.component';
import { AuthService } from '../../services/auth.service';
import { UserRegister } from '../../interfaces/auth';

  @Component({
    selector: 'app-signup',
    standalone: true,
    imports: [CommonModule, FormsModule, ReactiveFormsModule, RouterLink, NavbarComponent],
    templateUrl: './signup.component.html',
    styleUrl: './signup.component.css'
  })
  export class SignupComponent implements OnInit {
    signupForm: FormGroup;

    constructor(private fb: FormBuilder, private authService: AuthService) {
      this.signupForm = this.fb.group({});
    }

    ngOnInit() {
      this.signupForm = this.fb.group({
        name: ['', [Validators.required, Validators.minLength(2)]],
        email: ['', [Validators.required, Validators.email]],
        phoneNumber: ['', [Validators.required, Validators.pattern(/^[0-9]{10}$/)]],
        password: ['', [Validators.required, Validators.minLength(8)]],
        confirmPassword: ['', Validators.required]
      }, {
        validator: this.mustMatch('password', 'confirmPassword')
      });
    }

    mustMatch(controlName: string, matchingControlName: string) {
      return (formGroup: FormGroup) => {
        const control = formGroup.controls[controlName];
        const matchingControl = formGroup.controls[matchingControlName];

        if (matchingControl.errors && !matchingControl.errors['mustMatch']) {
          return;
        }

        if (control.value !== matchingControl.value) {
          matchingControl.setErrors({ mustMatch: true });
        } else {
          matchingControl.setErrors(null);
        }
      };
    }

    onSubmit() {
      if (this.signupForm.invalid) {
        return;
      }
      if (!this.signupForm.value.name ||!this.signupForm.value.email ||!this.signupForm.value.phoneNumber ||!this.signupForm.value.password ||!this.signupForm.value.confirmPassword) {
        return;
      }
      if (this.signupForm.value.password!== this.signupForm.value.confirmPassword) {
        return;
      }

      const user_register: UserRegister = {
        name: this.signupForm.value.name,
        email: this.signupForm.value.email,
        phoneNumber: this.signupForm.value.phoneNumber,
        country: "Kenya",
        password: this.signupForm.value.password
      }

      this.authService.register(user_register).subscribe(response => {
        console.log(response);
      });
    }
  }
