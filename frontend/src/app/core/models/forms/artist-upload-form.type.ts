import {FormControl} from "@angular/forms";

export type ArtistUploadForm = {
  firstname: FormControl<string>;
  lastname: FormControl<string>;
  contributionLabel: FormControl<string>;
}
