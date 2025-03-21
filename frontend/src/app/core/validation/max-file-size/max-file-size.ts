import { FormControl } from "@angular/forms";

export function maxFileSize(maxSize: number) {
  return function (control: FormControl) {
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
