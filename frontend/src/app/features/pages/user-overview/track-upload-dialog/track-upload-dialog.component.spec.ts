import { ComponentFixture, TestBed } from '@angular/core/testing';
import { TrackUploadDialogComponent } from './track-upload-dialog.component';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {ReactiveFormsModule} from "@angular/forms";
import {HttpClientTestingModule} from "@angular/common/http/testing";
import {NoopAnimationsModule} from "@angular/platform-browser/animations";

describe('TrackUploadDialogComponent', () => {
  let component: TrackUploadDialogComponent;
  let fixture: ComponentFixture<TrackUploadDialogComponent>;
  const mockDialogData = { userId: 'someUserId' };
  const mockDialogRef = { close: jest.fn() };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        TrackUploadDialogComponent,
        ReactiveFormsModule,
        HttpClientTestingModule,
        NoopAnimationsModule
      ],
      providers: [
        { provide: MAT_DIALOG_DATA, useValue: {useValue: mockDialogData} },
        { provide: MatDialogRef, useValue: {useValue: mockDialogRef} },
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TrackUploadDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
