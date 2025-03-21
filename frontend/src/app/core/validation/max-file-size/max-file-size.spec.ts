import {maxFileSize} from "./max-file-size";
import {FormControl} from "@angular/forms";

describe('maxFileSize', () => {
  let control: FormControl;

  beforeEach(() => {
    control = new FormControl();
  });

  it('returns null if file size is less than max size', () => {
    control.setValue({size: 100});
    expect(maxFileSize(200)(control)).toBeNull();
  });

  it('returns error if file size is greater than max size', () => {
    control.setValue({size: 300});
    expect(maxFileSize(200)(control)).toEqual({
      maxFileSize: {
        maxSize: 200,
        actualSize: 300
      }
    });
  });

  it('returns null if file is not set', () => {
    control.setValue(null);
    expect(maxFileSize(200)(control)).toBeNull();
  });
});
