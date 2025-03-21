import { ComponentFixture, TestBed } from '@angular/core/testing';
import { TrackComponent } from './track.component';
import { ActivatedRoute, convertToParamMap, Router } from '@angular/router';
import { of, BehaviorSubject } from 'rxjs';
import { TrackService } from '../../../../core/services/entity-services/tracks/track.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('TrackComponent', () => {
  let component: TrackComponent;
  let fixture: ComponentFixture<TrackComponent>;

  const paramMapSubject = new BehaviorSubject(convertToParamMap({ id: '123' }));
  const activatedRouteStub = {
    snapshot: { paramMap: convertToParamMap({ id: '123' }) },
    paramMap: paramMapSubject.asObservable(),
  };

  const mockRouter = {
    navigate: jest.fn(() => Promise.resolve(true)) // Ensure `navigate` returns a promise
  };

  const trackServiceMock = {
    getTrackCardByTrackId: jest.fn(() => of(null)), // Simulate no track found, triggers navigation
  };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TrackComponent, HttpClientTestingModule],
      providers: [
        { provide: ActivatedRoute, useValue: activatedRouteStub },
        { provide: Router, useValue: mockRouter },  // Provide mocked Router
        { provide: TrackService, useValue: trackServiceMock },  // Mock TrackService
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(TrackComponent);
    component = fixture.componentInstance;
    fixture.detectChanges(); // Trigger ngOnInit
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should fetch the correct trackId from route', () => {
    expect(component.trackId).toBe('123');
  });

  it('should navigate to not-found if trackId is missing', async () => {
    paramMapSubject.next(convertToParamMap({})); // Simulate missing param
    component.ngOnInit();
    await fixture.whenStable(); // Ensure async operations complete
    expect(mockRouter.navigate).toHaveBeenCalledWith(['/not-found']);
  });
});
