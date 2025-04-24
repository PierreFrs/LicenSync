import {FormControl} from "@angular/forms";

export type RegisterFormModel = {
  firstName: FormControl<string>;
  lastName: FormControl<string>;
  email: FormControl<string>;
  password: FormControl<string>;
}
