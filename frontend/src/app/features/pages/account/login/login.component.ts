import {Component, inject} from '@angular/core';
import {FormBuilder, ReactiveFormsModule, Validators} from "@angular/forms";
import {Router, RouterLink} from "@angular/router";
import {MatCard} from "@angular/material/card";
import {MatButton} from "@angular/material/button";
import {AccountService} from "../../../../core/services/account.service";
import {AsyncPipe, NgClass} from "@angular/common";
import {ResponsiveService} from "../../../../core/services/responsive.service";
import {LoginValues} from "../../../../core/models/login.model";
import {TextInputComponent} from "../../../../shared/form-components/text-input/text-input.component";
import {SnackbarService} from "../../../../core/services/snackbar.service";
import {userId} from "../../../../core/functions/user-id";
import {filter} from "rxjs/operators";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  standalone: true,
  imports: [
    MatCard,
    ReactiveFormsModule,
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

  userId$ = userId();
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
            if (this.userId$) {
              this.userId$.pipe(
                filter(userId => !!userId),
              ).subscribe(userId => {
                this.router.navigateByUrl(`/user/${userId}`)
                }
              )
            } else {
              this.snack.error('Failed to retrieve user info:');
            }
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
