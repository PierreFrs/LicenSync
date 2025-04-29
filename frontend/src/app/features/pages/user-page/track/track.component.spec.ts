import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { TrackComponent } from './track.component';
import { ActivatedRoute, convertToParamMap, Router } from '@angular/router';
import {Observable, of} from 'rxjs';
import { TrackService } from '../../../../core/services/entity-services/tracks/track.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import {TrackCard} from "../../../../core/models/entities/track-card.model";

describe('TrackComponent', () => {
  let component: TrackComponent;
  let fixture: ComponentFixture<TrackComponent>;
  let mockRouter: Partial<Router>;
  let trackServiceMock: Partial<TrackService>;

  const createMockTrackCard = (found: boolean): TrackCard | undefined => {
    if (!found) return undefined;

    return {
      id: '123',
      trackTitle: 'Test Track',
      length: '3:30',
      recordLabel: 'Test Label',
      firstGenre: 'Pop',
      secondaryGenre: 'Rock',
      albumTitle: 'Test Album',
      blockchainHash: 'hash123',
      artistsLyrics: ['Lyricist 1', 'Lyricist 2'],
      artistsMusic: ['Musician 1'],
      artistsMusicAndLyrics: ['Artist 1'],
      creationDate: new Date().toISOString(),
      trackAudioFilePath: '/audio/test-track.mp3',
      trackVisualFilePath: '/images/test-visual.jpg',
      userId: 'user123'
    };
  };



  beforeEach(async () => {
    mockRouter = {
      navigate: jest.fn(() => Promise.resolve(true))
    };

    trackServiceMock = {
      getTrackCardByTrackId: jest.fn((): Observable<TrackCard | undefined> => {
        return of(createMockTrackCard(false));
      }),
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
