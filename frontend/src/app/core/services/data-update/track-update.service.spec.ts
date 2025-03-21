import { TestBed } from '@angular/core/testing';

import { TrackUpdateService } from './track-update.service';

describe('TrackUpdateService', () => {
  let service: TrackUpdateService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [TrackUpdateService]
    });
    service = TestBed.inject(TrackUpdateService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should update track list', () => {
    const track = {
      id: '1',
      trackTitle: 'test',
      length: 'test',
      audioFilePath: 'test',
      userId: 'test',
      recordLabel: 'test',
      trackVisualPath: 'test',
      firstGenreId: 'test',
      secondaryGenreId: 'test',
      albumId: 'test',
      blockchainHashId: 'test',
      creationDate: 'test',
      updateDate: 'test'
    };
    service.trackUpdate$.subscribe((data) => {
      expect(data).toEqual(track);
    });
  });
});
