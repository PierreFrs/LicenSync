import {AlbumService} from "./album.service";
import {TestBed} from "@angular/core/testing";
import {HttpClientTestingModule, HttpTestingController} from "@angular/common/http/testing";
import {Album} from "../../../models/entities/album.model";
import {HttpErrorResponse} from "@angular/common/http";
import {environment} from "../../../../../environments/environment";

describe('AlbumService', () => {
  let service: AlbumService;
  let httpTestingController: HttpTestingController;
  let album: Album = {
    id: '1',
    albumTitle: 'test',
    userId: 'test'
  }

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [AlbumService]
    });
    service = TestBed.inject(AlbumService);
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpTestingController.verify();
  });

  it('creates a service', () => {
    expect(service).toBeTruthy();
  });

  describe('getAlbumById', () => {
    it('should return album by id', () => {
      service.getAlbumById('1').subscribe((response: Album) => {
        album = response;
      });
      const req = httpTestingController.expectOne(`${environment.BASE_URL}/Album/1`);
      req.flush(album);

      expect(album).toEqual({
        id: '1',
        albumTitle: 'test',
        userId: 'test'
      });
    });
  });

  describe('getAlbumsByUserId', () => {
    it('should return albums by user id', () => {
      let albums: Album[] | undefined;
      service.getAlbumsByUserId('1').subscribe((response) => {
        albums = response;
      });
      const req = httpTestingController.expectOne(`${environment.BASE_URL}/Album/user/1`);
      req.flush([album]);
      expect(albums).toEqual([{
        id: '1',
        albumTitle: 'test',
        userId: 'test'
      }]);
    });
  });


  describe('postAlbum', () => {
    const formData = new FormData();
    const albumVisual = new File([''], 'test.jpg', { type: 'image/jpg' });
    const albumTupple: (Album | File)[] = [album, albumVisual];
    Object.entries(album).forEach(([key, value]) => {
      if (value !== null) {
        formData.append(key, value);
      }
    });
    it('should create an album', () => {
      Object.entries(album).forEach(([key, value]) => {
        if (value !== null) {
          formData.append(key, value);
        }
      });
      service.postAlbum(album).subscribe((response: Album) => {
        album = response;
      });
      const req = httpTestingController.expectOne(`${environment.BASE_URL}/Album/`);
      req.flush(album);

      expect(album).toEqual({
        id: '1',
        albumTitle: 'test',
        userId: 'test'
      });
    });

    it('should create an album with albumVisual', () => {
      formData.append('albumVisual', albumVisual);
      service.postAlbum(album, albumVisual).subscribe((response: Album) => {
        album = response;
      });
      const req = httpTestingController.expectOne(`${environment.BASE_URL}/Album/`);
      req.flush(albumTupple);
      expect(albumTupple).toEqual([{
        id: '1',
        albumTitle: 'test',
        userId: 'test'
      }, albumVisual]);
    });

    it('passes the correct body', () => {
      service.postAlbum(album).subscribe((response: Album) => {
        album = response;
      });
      const req = httpTestingController.expectOne(`${environment.BASE_URL}/Album/`);
      req.flush(album);
      expect(req.request.method).toEqual('POST');
    });

    it('passes the correct body with albumVisual', () => {
      formData.append('albumVisual', albumVisual);
      service.postAlbum(album, albumVisual).subscribe((response: Album) => {
        album = response;
      });
      const req = httpTestingController.expectOne(`${environment.BASE_URL}/Album/`);
      req.flush(albumTupple);
      expect(req.request.method).toEqual('POST');
      expect(albumTupple).toEqual([{
        id: '1',
        albumTitle: 'test',
        userId: 'test'
      }, albumVisual]);
    });

    it ('should return an error', () => {
      let actualError: HttpErrorResponse | undefined;
      service.postAlbum(album).subscribe({
        next: () => {
          fail('should have failed');
        },
        error: (error: HttpErrorResponse) => {
          actualError = error;
        }
      });
      const req = httpTestingController.expectOne(`${environment.BASE_URL}/Album/`);
      req.flush(null, { status: 400, statusText: 'Bad Request' });

      if (!actualError) {
        fail('should have failed');
      }

      expect(actualError.status).toEqual(400);
      expect(actualError.statusText).toEqual('Bad Request');
    })
  });
});
