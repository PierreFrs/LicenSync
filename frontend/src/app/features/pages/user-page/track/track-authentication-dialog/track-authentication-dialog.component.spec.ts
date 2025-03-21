import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrackAuthenticationDialogComponent } from './track-authentication-dialog.component';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";

describe('TrackAuthenticationDialogComponent', () => {
  let component: TrackAuthenticationDialogComponent;
  let fixture: ComponentFixture<TrackAuthenticationDialogComponent>;
  const mockDialogData = { trackId: 'someTrackId' };
  const mockDialogRef = { close: jest.fn() };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TrackAuthenticationDialogComponent],
      providers: [
        { provide: MAT_DIALOG_DATA, useValue: {useValue: mockDialogData} },
        { provide: MatDialogRef, useValue: {useValue: mockDialogRef} },
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TrackAuthenticationDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
