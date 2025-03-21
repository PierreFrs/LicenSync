import {Component, inject, OnInit} from '@angular/core';
import {AccountService} from "../../../core/services/account.service";
import {TrackService} from "../../../core/services/entity-services/tracks/track.service";
import {AsyncPipe, NgClass} from "@angular/common";
import {ResponsiveService} from "../../../core/services/responsive.service";

@Component({
  selector: 'app-user-profile',
  standalone: true,
  imports: [
    NgClass,
    AsyncPipe
  ],
  templateUrl: './user-profile.component.html',
})
export class UserProfileComponent implements OnInit{
  accountService = inject(AccountService);
  private trackService = inject(TrackService);
  private responsiveService = inject(ResponsiveService);

  tracksArrayLength = 0;
  padding$ = this.responsiveService.padding$;

  ngOnInit() {
    this.getTracksArrayLength();
  }

  private getTracksArrayLength() {
    this.trackService.getTracksByUserId(this.accountService.currentUser()?.id ?? '').subscribe({
      next: (tracks) => {
        this.tracksArrayLength = tracks.length;
      }
    });
  }
}
