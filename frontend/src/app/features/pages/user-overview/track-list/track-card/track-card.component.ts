import {Component, DestroyRef, inject, Input, OnInit} from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { RouterLink } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import {CommonModule, NgOptimizedImage} from '@angular/common';
import { ImagesModule } from '../../../../../core/modules/images.module';
import { TrackService } from '../../../../../core/services/entity-services/tracks/track.service';
import { TrackCard } from '../../../../../core/models/entities/track-card.model';
import {takeUntilDestroyed} from "@angular/core/rxjs-interop";

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
    NgOptimizedImage,
  ],
  templateUrl: './track-card.component.html',
  styleUrls: ['./track-card.component.scss'],
})
export class TrackCardComponent implements OnInit {
  private readonly trackService = inject(TrackService);
  private destroyRef = inject(DestroyRef);

  imageURL: string | null = null;
  cardWidth = 'w-80';
  @Input({required: true}) track!: TrackCard;

  ngOnInit() {
      this.fetchTrackPicture(this.track.id);
  }

  private fetchTrackPicture(trackId: string) {
    this.trackService.getTrackPictureByTrackId(trackId).pipe(
      takeUntilDestroyed(this.destroyRef),
    ).subscribe((url) => {
      this.imageURL = url;
    });
  }
}
