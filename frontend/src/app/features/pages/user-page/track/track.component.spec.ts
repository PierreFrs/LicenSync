import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { TrackComponent } from './track.component';
import { ActivatedRoute, convertToParamMap, Router } from '@angular/router';
import { of } from 'rxjs';
import { TrackService } from '../../../../core/services/entity-services/tracks/track.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

describe('TrackComponent', () => {
  let component: TrackComponent;
  let fixture: ComponentFixture<TrackComponent>;
  let mockRouter: any;
  let trackServiceMock: any;

  beforeEach(async () => {
    // Create mocks
    mockRouter = {
      navigate: jest.fn(() => Promise.resolve(true))
    };

    trackServiceMock = {
      getTrackCardByTrackId: jest.fn(() => of(null)), // Simulate no track found
      getTrackPictureByTrackId: jest.fn(() => of('test-url'))
    };

    await TestBed.configureTestingModule({
      imports: [TrackComponent, HttpClientTestingModule],
      providers: [
        { provide: ActivatedRoute, useValue: {
            snapshot: { paramMap: convertToParamMap({ id: '123' }) }
          }
        },
        { provide: Router, useValue: mockRouter },
        { provide: TrackService, useValue: trackServiceMock },
        { provide: MatDialog, useValue: { open: jest.fn() } },
      ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA]
    }).compileComponents();

    fixture = TestBed.createComponent(TrackComponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should navigate to not-found if track is missing', fakeAsync(() => {
    // Set the ID directly (the way the component actually uses it)
    component.id = '123';

    // Call ngOnInit to trigger the fetch
    component.ngOnInit();

    // Use fakeAsync's tick to resolve all pending promises/observables
    tick();

    // Verify router was called with not-found route
    expect(mockRouter.navigate).toHaveBeenCalledWith(['/not-found']);
  }));
});
