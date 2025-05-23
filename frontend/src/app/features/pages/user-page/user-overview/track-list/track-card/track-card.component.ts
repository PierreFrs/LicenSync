import { Component, inject, Input, OnInit } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { RouterLink } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { ImagesModule } from '../../../../../../core/modules/images.module';
import { TrackService } from '../../../../../../core/services/entity-services/tracks/track.service';
import { TrackCard } from '../../../../../../core/models/track-card.model';

@Component({
  selector: 'app-track-card',
  standalone: true,
  imports: [
    MatCardModule,
    MatIconModule,
    MatButtonModule,
    CommonModule,
    ImagesModule,
    RouterLink,
  ],
  templateUrl: './track-card.component.html',
  styleUrls: ['./track-card.component.scss'],
})
export class TrackCardComponent implements OnInit {
  private readonly trackService = inject(TrackService);

  imageURL: string | null = null;
  cardWidth = 'w-80';
  @Input() track?: TrackCard;

  ngOnInit() {
    if (this.track) {
      this.fetchTrackPicture(this.track.id);
    }
  }

  private fetchTrackPicture(trackId: string) {
    this.trackService.getTrackPictureByTrackId(trackId).subscribe((url) => {
      this.imageURL = url;
    });
  }
}
