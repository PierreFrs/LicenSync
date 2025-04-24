import {Component, inject} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {Router, RouterLink} from "@angular/router";
import {MatCard} from "@angular/material/card";
import {MatButton} from "@angular/material/button";
import {AccountService} from "../../../../core/services/account.service";
import {SnackbarService} from "../../../../core/services/snackbar.service";
import {AsyncPipe, NgClass} from "@angular/common";
import {ResponsiveService} from "../../../../core/services/responsive.service";
import {RegisterValues} from "../../../../core/models/register.model";
import {TextInputComponent} from "../../../../shared/form-components/text-input/text-input.component";
import {RegisterFormModel} from "../../../../core/models/forms/register-form.type";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatCard,
    MatButton,
    TextInputComponent,
    AsyncPipe,
    NgClass,
    RouterLink
  ]
})
export class RegisterComponent {
  private accountService = inject(AccountService);
  private router = inject(Router);
  private snack = inject(SnackbarService);
  private responsiveService = inject(ResponsiveService);

  padding$ = this.responsiveService.padding$;
  validationErrors?: string[];

  registerForm = new FormGroup<RegisterFormModel>({
    firstName: new FormControl('', {nonNullable: true, validators : [Validators.required]}),
    lastName: new FormControl('', {nonNullable: true, validators : [Validators.required]}),
    email: new FormControl('', {nonNullable: true, validators : [Validators.required, Validators.email]}),
    password: new FormControl('', {nonNullable: true, validators : [Validators.required, Validators.pattern('^(?=.*)(?=.*[a-z])(?=.*[A-Z]).{8,}$')]})
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
