import {Component, inject, OnDestroy, OnInit} from '@angular/core';
import {AccountService} from "../../../core/services/account.service";
import {TrackService} from "../../../core/services/entity-services/tracks/track.service";
import {AsyncPipe, NgClass} from "@angular/common";
import {ResponsiveService} from "../../../core/services/responsive.service";
import {userId} from "../../../core/functions/user-id";
import {user} from "../../../core/functions/user";
import {Subscription} from "rxjs";

@Component({
  selector: 'app-user-profile',
  standalone: true,
  imports: [
    NgClass,
    AsyncPipe
  ],
  templateUrl: './user-profile.component.html',
})
export class UserProfileComponent implements OnInit, OnDestroy{
  accountService = inject(AccountService);
  private trackService = inject(TrackService);
  private responsiveService = inject(ResponsiveService);
  userId$ = userId();
  user$ = user();
  tracksArrayLength = 0;
  padding$ = this.responsiveService.padding$;
  private subscription = new Subscription();

  ngOnInit() {
    this.getTracksArrayLength();
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  private getTracksArrayLength() {
    this.subscription.add(
      this.userId$.subscribe(id => {
        if (id) {
          this.trackService.getTracksByUserId(id).subscribe({
            next: (tracks) => {
              this.tracksArrayLength = tracks.length;
            }
          });
        }
      })
    )
  }
}
