import {Component, inject, Input} from '@angular/core';
import {MatDialog} from "@angular/material/dialog";
import {
  TrackUploadDialogComponent
} from "../../features/pages/user-page/user-overview/track-upload-dialog/track-upload-dialog.component";

@Component({
  selector: 'app-upload-link',
  standalone: true,
  imports: [],
  templateUrl: './upload-link.component.html'
})
export class UploadLinkComponent {
  private dialog = inject(MatDialog);

  @Input() userId: string | null = null;

  openUploadDialog() {
    if (this.userId) {
      this.dialog.open(TrackUploadDialogComponent, {
        panelClass: "modal-panel",
        data: {userId: this.userId}
      });
    }
    else {
      console.error(
        "UploadLinkComponent: userId is null"
      );
    }
  }
}
