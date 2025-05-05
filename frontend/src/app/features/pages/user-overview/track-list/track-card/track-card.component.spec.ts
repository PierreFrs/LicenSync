import { ComponentFixture, TestBed } from '@angular/core/testing';
import { TrackCardComponent } from './track-card.component';
import {HttpClientTestingModule} from "@angular/common/http/testing";
import {RouterTestingModule} from "@angular/router/testing";

describe('TrackCardComponent', () => {
  let component: TrackCardComponent;
  let fixture: ComponentFixture<TrackCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        TrackCardComponent,
        RouterTestingModule,
        HttpClientTestingModule
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TrackCardComponent);
    component = fixture.componentInstance;
    component.track = {
      id: 'mock-track-id',
      trackTitle: 'mock-track-title',
      length: '',
      recordLabel: 'mock-track-label',
      firstGenre: 'mock-first-genre',
      secondaryGenre: 'mock-secondary-genre',
      albumTitle: 'mock-album-title',
      blockchainHash: 'mock-blockchain-hash',
      creationDate: 'mock-creation-date',
      trackAudioFilePath: 'mock-track-audio-file-path',
      trackVisualFilePath: 'mock-track-visual-file-path',
      userId: 'mock-user-id',
    }
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
