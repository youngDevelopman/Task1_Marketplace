import { Component, OnInit } from "@angular/core";
import { AuthService } from "../../services/AuthService";
import { ReactiveFormsModule } from '@angular/forms';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { Validators } from '@angular/forms';
import { CommonModule } from "@angular/common";


@Component({
  standalone: true,
  selector: 'app-sign-in-component',
  templateUrl: './sign-in.component.html',
  imports: [ReactiveFormsModule, CommonModule]
})
export class SignInComponent implements OnInit {
  loginForm!: FormGroup;
  authFailed: boolean = false;
  signedIn: boolean = false;

  constructor(private authService: AuthService,
    private formBuilder: FormBuilder,
    private router: Router) {
    this.authService.isSignedIn().subscribe(
      isSignedIn => {
        this.signedIn = isSignedIn;
      });
  }

  ngOnInit(): void {
    this.authFailed = false;
    this.loginForm = this.formBuilder.group(
      {
        email: ['', [Validators.required, Validators.email]],
        password: ['', [Validators.required]]
      });
  }

  public signIn(event: any) {
    if (!this.loginForm.valid) {
      return;
    }
    const userName = this.loginForm.get('email')?.value;
    const password = this.loginForm.get('password')?.value;
    this.authService.signIn(userName, password).subscribe(
      response => {
        if (response.isSuccess) {
          this.router.navigateByUrl('product/create')
        }
      },
      error => {
        if (!error?.error?.isSuccess) {
          this.authFailed = true;
        }
      });
  }
}
