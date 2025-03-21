import {Component, inject, Inject, OnDestroy, OnInit, ViewEncapsulation} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {
  AbstractControl,
  FormArray,
  FormBuilder, FormControl,
  FormGroup,
  ReactiveFormsModule, ValidationErrors, ValidatorFn,
  Validators
} from "@angular/forms";
import {Subject, takeUntil} from "rxjs";
import {CommonModule} from "@angular/common";
import {MatSelectModule} from "@angular/material/select";
import {MatInputModule} from "@angular/material/input";
import {MatButtonModule} from "@angular/material/button";
import {Album} from "../../../../../core/models/album.model";
import {Genre} from "../../../../../core/models/genre.model";
import {Contribution} from "../../../../../core/models/contribution.model";
import {TrackService} from "../../../../../core/services/entity-services/tracks/track.service";
import {AlbumService} from "../../../../../core/services/entity-services/albums/album.service";
import {GenreService} from "../../../../../core/services/entity-services/genres/genre.service";
import {ContributionService} from "../../../../../core/services/entity-services/contributions/contribution.service";
import {requiredFileType} from "../../../../../core/validation/required-file-type/required-file-type";
import {maxFileSize} from "../../../../../core/validation/max-file-size/max-file-size";
import {InputFieldComponent} from "../../../../../shared/form-components/input-field/input-field.component";
import {SelectFieldComponent} from "../../../../../shared/form-components/select-field/select-field.component";
import {FileInputComponent} from "../../../../../shared/form-components/file-input/file-input.component";
import {TrackCard} from "../../../../../core/models/track-card.model";
import {ArtistWithCOntribuionLabel} from "../../../../../core/models/artist.model";

@Component({
  selector: 'app-track-upload-dialog',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    CommonModule,
    MatSelectModule,
    MatInputModule,
    MatButtonModule,
    InputFieldComponent,
    SelectFieldComponent,
    FileInputComponent,
  ],
  templateUrl: './track-upload-dialog.component.html',
  styleUrls: ['./track-upload-dialog.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class TrackUploadDialogComponent implements OnInit, OnDestroy {
  private readonly formBuilder = inject(FormBuilder);
  private readonly dialogRef = inject(MatDialogRef<TrackUploadDialogComponent>);
  private readonly trackService = inject(TrackService);
  private readonly albumService = inject(AlbumService);
  private readonly genreService = inject(GenreService);
  private readonly contributionService = inject(ContributionService);

  private artistsLyrics: string[] = [];
  private artistsMusic: string[] = [];
  private artistsMusicAndLyrics: string[] = [];


  genreList: string[] = [];
  userAlbumList: string[] = [];
  contributionList: string[] = [];
  trackUploadForm!: FormGroup;
  errorMessage: string | null = null;
  formSubmitted: boolean = false;
  isCreatingNewAlbum: boolean = false;
  newAlbumTitleControl: AbstractControl | null = null;

  private readonly _unsubscribeAll: Subject<void> = new Subject<void>();

  constructor(@Inject(MAT_DIALOG_DATA) public data: { userId: string },) {}

  ngOnInit() {
    this.initializeForm();
    this.fetchData();
  }

  private initializeForm() {
    this.trackUploadForm = this.formBuilder.group({
      artists: this.formBuilder.array([]),
      trackTitle: ['', [Validators.required]],
      userId: [this.data.userId],
      recordLabel: [''],
      firstGenre: [''],
      secondaryGenre: [''],
      albumTitle: [''],
      audioFile: [null, [Validators.required, requiredFileType('mp3'), maxFileSize(20971520)]],
      visualFile: [null, [requiredFileType(['jpg', 'png']), maxFileSize(5242880)]],
      newAlbumTitle: [''],
      newAlbumVisual: [null, [requiredFileType(['jpg', 'png']), maxFileSize(5242880)]],
    });
    this.addArtistContribution();
  }

  private fetchData() {
    this.fetchGenres();
    this.fetchAlbums();
    this.fetchContributions();
  }

  private fetchGenres() {
    this.genreService.getGenreList()
      .pipe(takeUntil(this._unsubscribeAll))
      .subscribe((genres: Genre[]) => {
      this.genreList = genres.map((genre: Genre) => genre.label);
    });
  }

  private fetchAlbums() {
    this.albumService.getAlbumsByUserId(this.data.userId)
      .pipe(takeUntil(this._unsubscribeAll))
      .subscribe((albums: Album[]) => {
      this.userAlbumList = albums.map((album: Album) => album.albumTitle);
    });
  }

  private fetchContributions() {
    this.contributionService.getContributionList()
      .pipe(takeUntil(this._unsubscribeAll))
      .subscribe((contributions: Contribution[]) => {
      this.contributionList = contributions.map((contribution: Contribution) => contribution.label);
    });
  }

  toggleNewAlbum(): void {
    this.isCreatingNewAlbum = !this.isCreatingNewAlbum;

    this.newAlbumTitleControl = this.trackUploadForm.get('newAlbumTitle');

    if (this.isCreatingNewAlbum) {
      // Add Validators
      this.newAlbumTitleControl?.setValidators([
        Validators.required,
        this.albumTitleExistsValidator()
      ]);
      this.newAlbumTitleControl?.updateValueAndValidity();

      // Clear the selected album if creating a new one
      this.trackUploadForm.get('albumTitle')?.setValue('');
    } else {
      // Remove Validators
      this.newAlbumTitleControl?.clearValidators();
      this.newAlbumTitleControl?.updateValueAndValidity();

      // Clear new album fields if not creating a new one
      this.trackUploadForm.get('newAlbumTitle')?.setValue('');
      this.trackUploadForm.get('newAlbumVisual')?.setValue(null);
    }
  }

  ngOnDestroy() {
    this._unsubscribeAll.next();
    this._unsubscribeAll.complete();
  }

  get artistsFormArray(): FormArray {
    return this.trackUploadForm.get('artists') as FormArray;
  }

  addArtistContribution(): void {
    this.artists.push(this.createArtistGroup());
  }
  removeArtistContribution(index: number): void {
    this.artists.removeAt(index);
  }
  private createArtistGroup(): FormGroup {
    return this.formBuilder.group({
      firstname: ['', [Validators.required, Validators.minLength(2)]],
      lastname: ['', [Validators.required, Validators.minLength(2)]],
      contributionLabel: ['', [Validators.required]]
    });
  }

  get artists(): FormArray {
    return this.trackUploadForm.get('artists') as FormArray;
  }

  submit(): void {
    this.formSubmitted = true;
    if (this.trackUploadForm.invalid) {
      console.log("Form is invalid");
      return;
    }

    this.processArtists();

    if (this.isCreatingNewAlbum) {
      this.createAlbumAndUploadTrack();
    } else {
      const formData = this.prepareFormData();
      this.uploadTrack(formData);
    }
  }

  private processArtists(): void {
    const formValue = this.trackUploadForm.value;

    this.artistsLyrics = [];
    this.artistsMusic = [];
    this.artistsMusicAndLyrics = [];

    formValue.artists.forEach((artist: ArtistWithCOntribuionLabel) => {
      const artistName = `${artist.firstname} ${artist.lastname}`;
      const contribution = artist.contributionLabel;

      if (contribution === 'Paroles') {
        this.artistsLyrics.push(artistName);
      } else if (contribution === 'Musique') {
        this.artistsMusic.push(artistName);
      } else if (contribution === 'Musique et paroles') {
        this.artistsMusicAndLyrics.push(artistName);
      }
    });
  }

  private createAlbumAndUploadTrack(): void {
    const newAlbumTitle = this.trackUploadForm.get('newAlbumTitle')?.value;
    const albumVisual = this.trackUploadForm.get('newAlbumVisual')?.value;

    const albumDto: Album = {
      albumTitle: newAlbumTitle,
      userId: this.data.userId,
    };

    this.albumService.postAlbum(albumDto, albumVisual).subscribe({
      next: (album: Album) => {
        console.log('Album created successfully', album);
        // Set the albumTitle in the form to the new album's title
        this.trackUploadForm.get('albumTitle')?.setValue(album.albumTitle);
        const formData = this.prepareFormData();
        this.uploadTrack(formData);
      },
      error: (err) => {
        console.error('Error creating album:', err);
        this.errorMessage = 'An error occurred while creating the album.';
      },
    });
  }

  albumTitleExistsValidator(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      if (!control.value) {
        return null;
      }

      const albumTitle = control.value.trim().toLowerCase();

      const albumExists = this.userAlbumList.some(title => title.trim().toLowerCase() === albumTitle);

      return albumExists ? { albumTitleExists: true } : null;
    };
  }

  private prepareFormData(): FormData {
    const formData = new FormData();
    const formValue = this.trackUploadForm.value;

    // Append files to formData
    if (formValue.audioFile) formData.append('audioFile', formValue.audioFile);
    if (formValue.visualFile) formData.append('visualFile', formValue.visualFile);

    // Exclude 'audioFile', 'visualFile', and 'artists' from formValue while appending the rest to formData
    Object.keys(formValue).forEach(key => {
      if (!['audioFile', 'visualFile', 'artists'].includes(key)) {
        formData.append(key, formValue[key]);
      }
    });

    // Append artist arrays to formData
    this.artistsLyrics?.forEach((artistName: string) => {
      formData.append('artistsLyrics', artistName);
    });
    this.artistsMusic?.forEach((artistName: string) => {
      formData.append('artistsMusic', artistName);
    });
    this.artistsMusicAndLyrics?.forEach((artistName: string) => {
      formData.append('artistsMusicAndLyrics', artistName);
    });

    return formData;
  }

  private uploadTrack(formData: FormData): void {
    this.trackService.uploadTrack(formData).subscribe({
      next: (response: TrackCard) => {
        console.log('Track uploaded successfully', response);
        this.handleTrackUploadSuccess(response);
      },
      error: (err) => {
        console.error('Error uploading track:', err);
        this.errorMessage = 'An error occurred while uploading the track.';
      }
    });
  }

  private handleTrackUploadSuccess(response: TrackCard): void {
    console.log('Track uploaded successfully', response);
    this.dialogRef.close(response); // Pass the response if needed
  }

  get audioFileControl(): FormControl {
    return this.trackUploadForm.get('audioFile') as FormControl;
  }

  get visualFileControl(): FormControl {
    return this.trackUploadForm.get('visualFile') as FormControl;
  }

  get newAlbumVisualControl(): FormControl {
    return this.trackUploadForm.get('newAlbumVisual') as FormControl;
  }

  closeDialog(): void {
    this.dialogRef.close();
  }
}
