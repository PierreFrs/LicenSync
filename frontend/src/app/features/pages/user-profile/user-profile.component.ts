import {Component, DestroyRef, inject, Input, OnInit} from '@angular/core';
import {AccountService} from "../../../core/services/account.service";
import {TrackService} from "../../../core/services/entity-services/tracks/track.service";
import {AsyncPipe, NgClass} from "@angular/common";
import {ResponsiveService} from "../../../core/services/responsive.service";
import {user} from "../../../core/functions/user";
import {takeUntilDestroyed} from "@angular/core/rxjs-interop";

@Component({
  selector: 'app-user-profile',
  standalone: true,
  imports: [
    NgClass,
    AsyncPipe
  ],
  templateUrl: './user-profile.component.html',
})
export class UserProfileComponent implements OnInit {
  @Input() id!: string;
  accountService = inject(AccountService);
  private trackService = inject(TrackService);
  private responsiveService = inject(ResponsiveService);
  private destroyRef = inject(DestroyRef);
  user$ = user();
  tracksArrayLength = 0;
  padding$ = this.responsiveService.padding$;
  ngOnInit() {
    this.getTracksArrayLength();
  }

  private getTracksArrayLength() {
      this.trackService.getTracksByUserId(this.id)
        .pipe(takeUntilDestroyed(this.destroyRef))
        .subscribe({
        next: (tracks) => {
          this.tracksArrayLength = tracks.length;
        }
      }
    )
  }
}
