import {AbstractControl, ValidationErrors, ValidatorFn} from "@angular/forms";

export function requiredFileType(types: string | string[]): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const file = control.value;
    if (file) {
      const extension = file.name.split('.').pop()?.toLowerCase();
      const allowedTypes = Array.isArray(types) ? types : [types];

      if (!extension || !allowedTypes.map(type => type.toLowerCase()).includes(extension)) {
        return {
          requiredFileType: true,
          requiredTypes: allowedTypes
        };
      }

      return null;
    }

    return null;
  };
}
