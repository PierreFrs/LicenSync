import {ArtistService} from "./artist.service";
import {TestBed} from "@angular/core/testing";
import {HttpClientTestingModule, HttpTestingController} from "@angular/common/http/testing";
import {Artist} from "../../../models/artist.model";
import {HttpErrorResponse} from "@angular/common/http";
import {environment} from "../../../../../environments/environment";

describe('ArtistService', () => {
  let service: ArtistService;
  let httpTestingController: HttpTestingController;
  let artist: Artist = {
    id: '1',
    firstname: 'test',
    lastname: 'test',
    trackId: 'test',
    contributionId: 'test',
  };

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [ArtistService]
    });
    service = TestBed.inject(ArtistService);
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpTestingController.verify();
  });

  it('creates a service', () => {
    expect(service).toBeTruthy();
  });

  describe('getArtistsByTrackId', () => {
    it('should return artists by track id', () => {
      let artists: Artist[] | undefined;
      service.getArtistsByTrackId('1').subscribe((response) => {
        artists = response;
      });
      const req = httpTestingController.expectOne(`${environment.BASE_URL}/Artist/track/1`);
      req.flush([artist]);
      expect(artists).toEqual([artist]);
    });

    it('throws an error if request fails', () => {
      let actualError: HttpErrorResponse | undefined;

      service.getArtistsByTrackId('1').subscribe({
        next: () => {
          fail('should have failed');
        },
        error: (error: HttpErrorResponse) => {
          actualError = error;
        }
      });

      const req = httpTestingController.expectOne(`${environment.BASE_URL}/Artist/track/1`);
      req.flush('Error', {status: 404, statusText: 'Not Found'});

      expect(actualError?.status).toEqual(404);
      expect(actualError?.statusText).toEqual('Not Found');
    });
  });

  describe('postArtist', () => {
    it('should create an artist', () => {
      const formData = new FormData();

      Object.keys(artist).forEach((key) => {
        const value = artist[key as keyof typeof artist];
        if (value !== null) {
          formData.append(key, value);
        }
      });

      service.postArtist(formData).subscribe((response) => {
        artist = response;
      });

      const req = httpTestingController.expectOne(`${environment.BASE_URL}/Artist/`);
      req.flush(artist);
      expect(artist).toEqual({
        id: '1',
        firstname: 'test',
        lastname: 'test',
        trackId: 'test',
        contributionId: 'test',
      });
    });

    it('passes the correct body', () => {
      const formData = new FormData();

      Object.keys(artist).forEach((key) => {
        const value = artist[key as keyof typeof artist];
        if (value !== null) {
          formData.append(key, value);
        }
      });

      service.postArtist(formData).subscribe((response) => {
        artist = response;
      });

      const req = httpTestingController.expectOne(`${environment.BASE_URL}/Artist/`);
      req.flush(artist);
      expect(req.request.method).toEqual('POST');
      expect(req.request.body).toEqual(formData);
    });

    it('throws an error if request fails', () => {
      const formData = new FormData();
      let actualError: HttpErrorResponse | undefined;

      service.postArtist(formData).subscribe({
        next: () => {
          fail('should have failed');
        },
        error: (error: HttpErrorResponse) => {
          actualError = error;
        }
      });

      const req = httpTestingController.expectOne(`${environment.BASE_URL}/Artist/`);
      req.flush('Error', {status: 404, statusText: 'Not Found'});

      expect(actualError?.status).toEqual(404);
      expect(actualError?.statusText).toEqual('Not Found');
    });
  });
});
