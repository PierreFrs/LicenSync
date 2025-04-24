import {TrackService} from "./track.service";
import {TestBed} from "@angular/core/testing";
import {HttpClientTestingModule, HttpTestingController} from "@angular/common/http/testing";
import {Track} from "../../../models/entities/track.model";
import {HttpErrorResponse} from "@angular/common/http";
import {environment} from "../../../../../environments/environment";
import {TrackCard} from "../../../models/entities/track-card.model";
import {AccountService} from "../../account.service";
import {of} from "rxjs";

describe('TrackService', () => {
  let service: TrackService;
  let httpTestingController: HttpTestingController;
  let accountServiceMock: Partial<AccountService>;

  const track: TrackCard = {
    id: '1',
    trackTitle: 'test',
    length: 'test',
    trackAudioFilePath: 'test',
    userId: 'test',
    recordLabel: 'test',
    trackVisualFilePath: 'test',
    firstGenre: 'test',
    secondaryGenre: 'test',
    albumTitle: 'test',
    blockchainHash: 'test',
    creationDate: 'test',
    artistsLyrics: ['Artist A', 'Artist B'],
    artistsMusic: ['Artist C'],
    artistsMusicAndLyrics: ['Artist D']
  }

  beforeEach(() => {
    const mockUserInfo = { id: 'testUserId' };

    accountServiceMock = {
      getUserInfos: jest.fn().mockReturnValue(of(mockUserInfo))
    };

    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        TrackService,
        { provide: AccountService, useValue: accountServiceMock }

      ]
    });
    service = TestBed.inject(TrackService);
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpTestingController.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('getTracksByUserId', () => {
    it('should return tracks by user id', () => {
      let tracks: Track[] | undefined;
      service.getTracksByUserId('1').subscribe((response) => {
        tracks = response;
      });
      const req = httpTestingController.expectOne(`${environment.BASE_URL}/Track/user/1`);
      req.flush([track]);
      expect(tracks).toEqual([{
        id: '1',
        trackTitle: 'test',
        length: 'test',
        trackAudioFilePath: 'test',
        userId: 'test',
        recordLabel: 'test',
        trackVisualFilePath: 'test',
        firstGenre: 'test',
        secondaryGenre: 'test',
        albumTitle: 'test',
        blockchainHash: 'test',
        creationDate: 'test',
        artistsLyrics: ['Artist A', 'Artist B'],
        artistsMusic: ['Artist C'],
        artistsMusicAndLyrics: ['Artist D']
      }]);
    });
  });

  describe('getTrackPictureByTrackId', () => {
    it('should return null if no picture', () => {
      let emptyResponse: string | null | undefined;
      service.getTrackPictureByTrackId('1').subscribe((response) => {
        emptyResponse = response;
      });
      const req = httpTestingController.expectOne(`${environment.BASE_URL}/Track/picture/1`);
      req.flush(null);

      expect(emptyResponse).toEqual(null);
    });
  });

  describe('getTrackById', () => {
    it('should return track by id', () => {
      service.getTrackById('1').subscribe((response) => {
        expect(response).toEqual(track);
      });
      const req = httpTestingController.expectOne(`${environment.BASE_URL}/Track/1`);
      req.flush(track);
      expect(track).toEqual({
        id: '1',
        trackTitle: 'test',
        length: 'test',
        trackAudioFilePath: 'test',
        userId: 'test',
        recordLabel: 'test',
        trackVisualFilePath: 'test',
        firstGenre: 'test',
        secondaryGenre: 'test',
        albumTitle: 'test',
        blockchainHash: 'test',
        creationDate: 'test',
        artistsLyrics: ['Artist A', 'Artist B'],
        artistsMusic: ['Artist C'],
        artistsMusicAndLyrics: ['Artist D']
      });
    });
  });

  describe('uploadTrack', () => {
    const formData = new FormData();
    let newTrack: TrackCard | undefined = track;

    Object.entries(newTrack).forEach(([key, value]) => {
      if (value !== null && value !== undefined) {
        if (Array.isArray(value)) {
          value.forEach((v) => {
            formData.append(key, v);
          });
        } else {
          formData.append(key, value);
        }
      }
    });


    it('should upload track', async () => {
      service.uploadTrack(formData).subscribe((response) => {
        newTrack = response;
      });

      // Now handle the expected request to /Track/track-card
      const req = httpTestingController.expectOne(`${environment.BASE_URL}/Track/track-card`);
      req.flush(track);

      // Handle the GET request to /Track/track-card-list/{user.id}
      const trackListReq = httpTestingController.expectOne(`${environment.BASE_URL}/Track/track-card-list/testUserId`);
      trackListReq.flush({ items: [track], totalCount: 1 });

      expect(newTrack).toEqual({
        id: '1',
        trackTitle: 'test',
        length: 'test',
        trackAudioFilePath: 'test',
        userId: 'test',
        recordLabel: 'test',
        trackVisualFilePath: 'test',
        firstGenre: 'test',
        secondaryGenre: 'test',
        albumTitle: 'test',
        blockchainHash: 'test',
        creationDate: 'test',
        artistsLyrics: ['Artist A', 'Artist B'],
        artistsMusic: ['Artist C'],
        artistsMusicAndLyrics: ['Artist D']
      });
    });

    it('passes the correct body', () => {
      service.uploadTrack(formData).subscribe((response) => {
        newTrack = response;
      });

      // Handle the POST request to /Track/track-card
      const trackReq = httpTestingController.expectOne(`${environment.BASE_URL}/Track/track-card`);
      trackReq.flush(track);

      // Handle the GET request to /Track/track-card-list/{user.id}
      const trackListReq = httpTestingController.expectOne(`${environment.BASE_URL}/Track/track-card-list/testUserId`);
      trackListReq.flush({ items: [track], totalCount: 1 });

      expect(trackReq.request.method).toEqual('POST');
      expect(trackReq.request.body).toEqual(formData);
    });

    it('should handle error', () => {
      const formData = new FormData();
      let actualError: HttpErrorResponse | undefined;
      service.uploadTrack(formData).subscribe({
        next: () => {
          fail('should have failed');
        },
        error: (error: HttpErrorResponse) => {
          actualError = error;
        }
      });

      const req = httpTestingController.expectOne(`${environment.BASE_URL}/Track/track-card`);
      req.flush('Error', { status: 404, statusText: 'Not Found' });

      if (!actualError) {
        throw new Error('Expected an error');
      }
      expect(actualError.status).toEqual(404);
      expect(actualError.statusText).toEqual('Not Found');
    });
  });
});
