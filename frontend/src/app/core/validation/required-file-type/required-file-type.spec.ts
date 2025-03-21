import {FormControl} from "@angular/forms";
import {requiredFileType} from "./required-file-type";

describe('RequiredFileType', () => {
  let control: FormControl;
  const mockFile = new File([""], "test.jpg", { type: 'image/jpeg' });

  beforeEach(() => {
    control = new FormControl();
  });

  it('returns null if file type is allowed in array', () => {
    control.setValue(mockFile);
    expect(requiredFileType(['jpg', 'png'])(control)).toBeNull();
  });

  it('returns error if file type is not allowed in array', () => {
    control.setValue(mockFile);
    expect(requiredFileType(['svg', 'png'])(control)).toEqual({
      requiredFileType: true,
      requiredTypes: ['svg', 'png']
    });
  });

  it('returns null if file is not set', () => {
    control.setValue(null);
    expect(requiredFileType('image/jpeg')(control)).toBeNull();
  });
});
