import {Component, inject} from '@angular/core';
import {FormBuilder, ReactiveFormsModule, Validators} from "@angular/forms";
import {Router, RouterLink} from "@angular/router";
import {MatCard} from "@angular/material/card";
import {MatError, MatFormField, MatLabel} from "@angular/material/form-field";
import {MatInput} from "@angular/material/input";
import {MatButton} from "@angular/material/button";
import {AccountService} from "../../../../core/services/account.service";
import {SnackbarService} from "../../../../core/services/snackbar.service";
import {AsyncPipe, JsonPipe, NgClass} from "@angular/common";
import {ResponsiveService} from "../../../../core/services/responsive.service";
import {RegisterValues} from "../../../../core/models/register.model";
import {TextInputComponent} from "../../../../shared/form-components/text-input/text-input.component";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatCard,
    MatFormField,
    MatLabel,
    MatInput,
    MatButton,
    JsonPipe,
    MatError,
    TextInputComponent,
    AsyncPipe,
    NgClass,
    RouterLink
  ]
})
export class RegisterComponent {
  private fb = inject(FormBuilder);
  private accountService = inject(AccountService);
  private router = inject(Router);
  private snack = inject(SnackbarService);
  private responsiveService = inject(ResponsiveService);

  padding$ = this.responsiveService.padding$;
  validationErrors?: string[];

  registerForm = this.fb.group({
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.pattern('^(?=.*)(?=.*[a-z])(?=.*[A-Z]).{8,}$')]]
  });

  onSubmit() {
    if (this.registerForm.valid) {

      const registerValues: RegisterValues = {
        firstName: this.registerForm.value.firstName ?? '',
        lastName: this.registerForm.value.lastName ?? '',
        email: this.registerForm.value.email ?? '',
        password: this.registerForm.value.password ?? ''
      };

      this.accountService.register(registerValues).subscribe({
        next: () => {
          this.snack.success('Enregistrement réussi, vous pouvez maintenant vous identifier.');
          this.router.navigateByUrl('/account/login');
        },
        error: errors => this.validationErrors = errors
      });
    } else {
      this.snack.error('Veuillez remplir tous les champs.');
    }
  }
}
