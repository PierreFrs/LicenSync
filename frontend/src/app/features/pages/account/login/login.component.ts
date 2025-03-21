import {Component, inject} from '@angular/core';
import {FormBuilder, ReactiveFormsModule, Validators} from "@angular/forms";
import {Router, RouterLink} from "@angular/router";
import {MatFormField, MatLabel} from "@angular/material/form-field";
import {MatCard} from "@angular/material/card";
import {MatInput} from "@angular/material/input";
import {MatButton} from "@angular/material/button";
import {AccountService} from "../../../../core/services/account.service";
import {AsyncPipe, NgClass} from "@angular/common";
import {ResponsiveService} from "../../../../core/services/responsive.service";
import {LoginValues} from "../../../../core/models/login.model";
import {TextInputComponent} from "../../../../shared/form-components/text-input/text-input.component";
import {SnackbarService} from "../../../../core/services/snackbar.service";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  standalone: true,
  imports: [
    MatLabel,
    MatCard,
    ReactiveFormsModule,
    MatFormField,
    MatInput,
    MatButton,
    NgClass,
    AsyncPipe,
    TextInputComponent,
    RouterLink
  ]
})
export class LoginComponent {
  private fb = inject(FormBuilder)
  private accountService = inject(AccountService)
  private router = inject(Router)
  private snack = inject(SnackbarService);
  private responsiveService = inject(ResponsiveService)

  padding$ = this.responsiveService.padding$;
  validationErrors?: string[];

  loginForm = this.fb.group({
    email: ['', Validators.required],
    password: ['', Validators.required]
  });

  onSubmit() {
    if (this.loginForm.valid) {
      const loginValues: LoginValues = {
        email: this.loginForm.value.email ?? '',
        password: this.loginForm.value.password ?? ''
      };

      this.accountService.login(loginValues).subscribe({
        next: () => {
          this.accountService.getUserInfos().subscribe(() => {
            const user = this.accountService.currentUser();
            if (user) {
              const id = user.id;
              this.router.navigateByUrl(`/user/${id}`);
            } else {
              this.snack.error('Failed to retrieve user info:');
            }
          });
        },
        error: () => {
          this.snack.error('Login failed:');
        }
      });
    } else {
      this.snack.error('Form is invalid');
    }
  }
}
