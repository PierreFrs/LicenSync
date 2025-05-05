import {Component, DestroyRef, OnInit, inject} from '@angular/core';
import { GenreService } from 'src/app/core/services/entity-services/genres/genre.service';
import {TrackService, TrackService } from 'src/app/core/services/entity-services/tracks/track.service';
import {AlbumService} from "../../../core/services/entity-services/albums/album.service";
import {ContributionService} from "../../../core/services/entity-services/contributions/contribution.service";
import {FormGroup} from "@angular/forms";
import {takeUntilDestroyed} from "@angular/core/rxjs-interop";
import {Genre} from "../../../core/models/entities/genre.model";
import {Album} from "../../../core/models/entities/album.model";
import {Contribution} from "../../../core/models/entities/contribution.model";

@Component({
  selector: 'app-album-upload',
  standalone: true,
  imports: [],
  templateUrl: './album-upload.component.html',
  styleUrl: './album-upload.component.scss'
})
export class AlbumUploadComponent implements OnInit {
  private readonly trackService = inject(TrackService);
  private readonly albumService = inject(AlbumService);
  private readonly genreService = inject(GenreService);
  private readonly contributionService = inject(ContributionService);

  private destroyRef = inject(DestroyRef);

  genresList: string[] = [];
  contributionsList: string[] = [];
  albumUploadForm!: FormGroup;
  errorMessage: string | null = null;
  isCreatingNewTrack: boolean = false;

  ngOnInit() {
    this.initializeForm();
    this.fetchData();
  }

  private initializeForm() {
    this.albumUploadForm = new FormGroup<AlbumUploadForm>({

    });
  }

  private fetchData() {
    this.fetchGenres();
    this.fetchContributions();
  }

  private fetchGenres() {
    this.genreService.getGenreList()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((genres: Genre[]) => {
        this.genresList = genres.map((genre: Genre) => genre.label);
      });
  }

  private fetchContributions() {
    this.contributionService.getContributionList()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((contributions: Contribution[]) => {
        this.contributionsList = contributions.map((contribution: Contribution) => contribution.label);
      });
  }

  toggleNewTrack(): void {
    this.isCreatingNewTrack = !this.isCreatingNewTrack;


  }
}
