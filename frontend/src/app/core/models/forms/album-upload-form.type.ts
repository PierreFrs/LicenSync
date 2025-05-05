import {FormControl} from "@angular/forms";

export type AlbumUploadForm = {
  albumTitle: FormControl<string>
  userId: FormControl<string>
  recordLabel: FormControl<string | null>
  firstGenre: FormControl<string | null>
  secondaryGenre: FormControl<string | null>
  releaseDate: FormControl<string | null>
  visualFile: FormControl<File | null>
}
