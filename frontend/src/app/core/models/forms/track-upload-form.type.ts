import {FormArray, FormControl, FormGroup} from "@angular/forms";
import {ArtistUploadForm} from "./artist-upload-form.type";

export type TrackUploadForm = {
  artists: FormArray<FormGroup<ArtistUploadForm>>;
  trackTitle: FormControl<string>
  userId: FormControl<string>
  recordLabel: FormControl<string | null>
  firstGenre: FormControl<string | null>
  secondaryGenre: FormControl<string | null>
  albumTitle: FormControl<string | null>
  releaseDate: FormControl<string>
  audioFile: FormControl<File | null>
  visualFile: FormControl<File | null>
  newAlbumTitle: FormControl<string | null>
  newAlbumVisual: FormControl<File | null>
};
