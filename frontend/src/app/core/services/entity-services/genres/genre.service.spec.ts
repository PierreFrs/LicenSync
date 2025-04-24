import {GenreService} from "./genre.service";
import {TestBed} from "@angular/core/testing";
import {HttpClientTestingModule, HttpTestingController} from "@angular/common/http/testing";
import {Genre} from "../../../models/entities/genre.model";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {environment} from "../../../../../environments/environment";

describe('GenreService', () => {
  let service: GenreService;
  let httpTestingController: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        GenreService,
        BrowserAnimationsModule
      ]
    });
    service = TestBed.inject(GenreService);
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpTestingController.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('getGenreList', () => {
    it('should return a list of genres', () => {
      let genres: Genre[] | undefined;
      service.getGenreList().subscribe(response => {
        genres = response;
      });
      const req = httpTestingController.expectOne(`${environment.BASE_URL}/Genre`);
      req.flush([{id: '1', genreLabel: 'foo'}]);
      expect(genres).toEqual([{id: '1', genreLabel: 'foo'}]);
    });
  });
});
