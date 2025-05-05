import {Component, inject, Inject} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {CommonModule} from "@angular/common";
import {MatButtonModule} from "@angular/material/button";
import {DialogDataModel} from "../../../../core/models/authentication-dialog-data-model.model";

@Component({
  selector: 'app-track-authentication-dialog',
  standalone: true,
  imports: [CommonModule, MatButtonModule],
  templateUrl: './track-authentication-dialog.component.html',
  styleUrls: ['./track-authentication-dialog.component.scss']
})
export class TrackAuthenticationDialogComponent {
  dialogRef = inject(MatDialogRef<TrackAuthenticationDialogComponent>);
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: DialogDataModel
  ) {}
}
