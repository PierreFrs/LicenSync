import {ContributionService} from "./contribution.service";
import {TestBed} from "@angular/core/testing";
import {HttpClientTestingModule, HttpTestingController} from "@angular/common/http/testing";
import {Contribution} from "../../../models/entities/contribution.model";
import {environment} from "../../../../../environments/environment";

describe('ContributionService', () => {
  let service: ContributionService;
  let httpTestingController: HttpTestingController;
  let contribution: Contribution = {
    id: '1',
    label: 'test',
  }

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [ContributionService]
    });
    service = TestBed.inject(ContributionService);
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpTestingController.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('getContributionList', () => {
    it('should return contribution list', () => {
      let contributions: Contribution[] | undefined;
      service.getContributionList().subscribe((response) => {
        contributions = response;
      });
      const req = httpTestingController.expectOne(`${environment.BASE_URL}/Contribution`);
      req.flush([contribution]);
      expect(contributions).toEqual([{
        id: '1',
        label: 'test',
      }]);
    });
  });

  describe('getContributionByArtistId', () => {
    it('should return contribution by artist id', () => {
      service.getContributionByArtistId('1').subscribe((response) => {
        contribution = response;
      });
      const req = httpTestingController.expectOne(`${environment.BASE_URL}/Contribution/artist/1`);
      req.flush(contribution);
      expect(contribution).toEqual({
        id: '1',
        label: 'test',
      });
    });
  });
});
