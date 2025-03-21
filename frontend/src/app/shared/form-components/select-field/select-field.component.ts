import {Component, forwardRef, Input, OnInit} from '@angular/core';
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatOptionModule} from "@angular/material/core";
import {MatSelectModule} from "@angular/material/select";
import {NgForOf} from "@angular/common";
import {
  ControlValueAccessor,
  FormBuilder,
  FormControl,
  NG_VALUE_ACCESSOR,
  ReactiveFormsModule,
  Validators
} from "@angular/forms";

@Component({
  selector: 'app-select-field',
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatOptionModule,
    MatSelectModule,
    NgForOf,
    ReactiveFormsModule
  ],
  templateUrl: './select-field.component.html',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => SelectFieldComponent),
      multi: true
    }
  ]
})
export class SelectFieldComponent implements ControlValueAccessor, OnInit {
  @Input() label: string = '';
  @Input() placeholder: string = '';
  @Input() options: string[] = [];
  @Input() displayWith!: string;
  @Input() valueField!: string;
  @Input() errorMessage?: string = '';
  @Input() required: boolean = false;

  control: FormControl;

  constructor(private fb: FormBuilder) {
    this.control = this.fb.control('');
  }

  ngOnInit(): void {
    if (this.required) {
      this.control.setValidators(Validators.required);
    }
    this.control.updateValueAndValidity();
  }

  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  onChange: any = () => {};
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  onTouched: any = () => {};

  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  writeValue(obj: any): void {
    this.control.setValue(obj, {emitEvent: false});
  }

  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  registerOnChange(fn: any): void {
    this.control.valueChanges.subscribe(fn);
  }

  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  setDisabledState?(isDisabled: boolean): void {
    isDisabled ? this.control.disable() : this.control.enable();
  }
}
