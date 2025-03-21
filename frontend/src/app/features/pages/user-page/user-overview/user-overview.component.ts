import {Component, inject} from '@angular/core';
import {TrackListComponent} from "./track-list/track-list.component";
import {MatListModule} from "@angular/material/list";
import {AlbumListComponent} from "./album-list/album-list.component";
import {RouterOutlet} from "@angular/router";
import {AsyncPipe, NgClass} from "@angular/common";
import {ResponsiveService} from "../../../../core/services/responsive.service";

@Component({
  selector: 'app-user-overview',
  standalone: true,
  imports: [
    TrackListComponent,
    MatListModule,
    AlbumListComponent,
    RouterOutlet,
    NgClass,
    AsyncPipe,
  ],
  templateUrl: './user-overview.component.html'
})
export class UserOverviewComponent  {
  private responsiveService = inject(ResponsiveService);

  padding$ = this.responsiveService.padding$;
}
