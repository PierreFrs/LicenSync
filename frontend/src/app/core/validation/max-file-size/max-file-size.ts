import {AbstractControl, ValidationErrors, ValidatorFn} from "@angular/forms";

export function maxFileSize(maxSize: number) : ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const file = control.value;
    if (file && file.size > maxSize) {
      return {
        maxFileSize: {
          maxSize: maxSize,
          actualSize: file.size
        }
      };
    }
    return null;
  };
}
