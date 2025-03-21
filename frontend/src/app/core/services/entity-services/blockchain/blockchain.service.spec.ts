import {BlockchainService} from "./blockchain.service";
import {HttpClientTestingModule, HttpTestingController} from "@angular/common/http/testing";
import {TestBed} from "@angular/core/testing";
import {HashesComparisonModel} from "../../../models/hashes-comparison.model";
import {TransactionReceipt} from "../../../models/transaction-receipt.model";
import {environment} from "../../../../../environments/environment";

describe('BlockchainService', () => {
  let service: BlockchainService;
  let httpServiceController: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [BlockchainService]
    });
    service = TestBed.inject(BlockchainService);
    httpServiceController = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpServiceController.verify();
  });

  it('creates a service', () => {
    expect(service).toBeTruthy();
  });

  describe('storeHash', () => {
    let result: TransactionReceipt | undefined;
    it('should store a hash', () => {
      service.storeHash('123').subscribe((response: TransactionReceipt) => {
        result = response;
      });

      const req = httpServiceController.expectOne(`${environment.BASE_URL}/BlockchainAuthentication/store-hash/123`);
      req.flush({transactionReceipt: {}}, {status: 200, statusText: 'OK'});
      expect(result).toBeTruthy();
    });

    it('should return false if request fails', () => {
      service.storeHash('123').subscribe((response: TransactionReceipt) => {
        result = response;
      });

      const req = httpServiceController.expectOne(`${environment.BASE_URL}/BlockchainAuthentication/store-hash/123`);
      req.flush({}, {status: 500, statusText: 'Internal server error'});
      expect(result).toEqual({} as TransactionReceipt);
    });
  });

  describe('compareHashes', () => {
    it('should return a HashComparisonModel', () => {
      let result: HashesComparisonModel | undefined;

      service.compareHashes('123').subscribe(response => {
        result = response;
      });

      const req = httpServiceController.expectOne(`${environment.BASE_URL}/BlockchainAuthentication/compareHashes/123`);
      req.flush({databaseHash: '123', blockchainHash: '123'});
      expect(result).toEqual({
        databaseHash: '123',
        blockchainHash: '123'
      });
    });
  });

  it('should return an empty object if request fails', () => {
    let result: HashesComparisonModel | undefined;

    service.compareHashes('123').subscribe(response => {
      result = response;
    });

    const req = httpServiceController.expectOne(`${environment.BASE_URL}/BlockchainAuthentication/compareHashes/123`);
    req.flush({}, {status: 500, statusText: 'Internal server error'});
    expect(result).toEqual({} as HashesComparisonModel)
  });
});
