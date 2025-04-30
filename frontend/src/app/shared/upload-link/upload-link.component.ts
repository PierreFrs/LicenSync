import {Component} from '@angular/core';
import {RouterLink} from "@angular/router";

@Component({
  selector: 'app-upload-link',
  standalone: true,
  imports: [
    RouterLink
  ],
  templateUrl: './upload-link.component.html'
})
export class UploadLinkComponent {}
