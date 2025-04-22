import {
  Component,
  ElementRef,
  forwardRef,
  HostListener, inject,
  Input,
  OnChanges,
  SimpleChanges,
  ViewChild
} from '@angular/core';
import {
  ControlValueAccessor, FormControl,
  NG_VALUE_ACCESSOR,
} from "@angular/forms";
import {MatInputModule} from "@angular/material/input";
import {CommonModule} from "@angular/common";
import {MatIconButton} from "@angular/material/button";
import {FileSizePipe} from "../../../core/pipes/file-size.pipe";

@Component({
  selector: 'app-file-input',
  standalone: true,
  imports: [
    MatInputModule,
    CommonModule,
    FileSizePipe,
    MatIconButton,
    FileSizePipe
  ],
  templateUrl: './file-input.component.html',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => FileInputComponent),
      multi: true
    }
  ]
})
export class FileInputComponent implements ControlValueAccessor, OnChanges {
  private host = inject(ElementRef<HTMLInputElement>)
  @Input() label!: string;
  @Input() acceptedFormats!: string;
  @Input() id!: string;
  @Input() control!: FormControl;
  @Input() formSubmitted: boolean = false;

  onChange: (file: File | null) => void = () => {};
  fileName: string = '';

  private file: File | null = null;

  @ViewChild('fileInput') fileInput!: ElementRef<HTMLInputElement>;

  @HostListener('change', ['$event.target.files']) emitFiles( event: FileList ) {
    const file = event?.item(0);
    this.onChange(file);
    this.file = file;
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['control']) {
      this.control.statusChanges.subscribe(() => {
      });
    }
  }

  writeValue() {
    this.host.nativeElement.value = '';
    this.file = null;
  }

  registerOnChange(fn: (file: File | null) => void): void {
    this.onChange = fn;
  }

  registerOnTouched(): void {}

  triggerFileInput() {
    this.fileInput.nativeElement.click();
  }

  onFileSelected(event: Event) {
    const element = event.target as HTMLInputElement;
    const file = element.files ? element.files[0] : null;
    if (file) {
      this.fileName = file.name;
      this.onChange(file);
    } else {
      this.onChange(null);
    }
    this.control?.markAsTouched();
  }

  clearFile() {
    this.host.nativeElement.value = '';
    this.fileName = '';
    this.onChange(null);
    if (this.control) {
      this.control.setValue(null);
      this.control.markAsPristine();
      this.control.markAsUntouched();
    }
  }
}
