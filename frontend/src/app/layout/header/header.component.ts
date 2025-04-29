import {Component, DestroyRef, inject, OnInit} from '@angular/core';
import {UploadLinkComponent} from "../../shared/upload-link/upload-link.component";
import {filter} from "rxjs";
import {NavigationEnd, Router, RouterLink} from "@angular/router";
import {UserService} from "../../core/services/entity-services/users/user.service";
import {UserMenuComponent} from "../../shared/user-menu/user-menu.component";
import {AccountService} from "../../core/services/account.service";
import {BusyService} from "../../core/services/busy.service";
import {MatProgressBar} from "@angular/material/progress-bar";
import {userId} from "../../core/functions/user-id";
import {AsyncPipe, NgOptimizedImage} from "@angular/common";
import {user} from "../../core/functions/user";
import {takeUntilDestroyed} from "@angular/core/rxjs-interop";

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    UploadLinkComponent,
    UserMenuComponent,
    RouterLink,
    MatProgressBar,
    AsyncPipe,
    NgOptimizedImage
  ],
  templateUrl: './header.component.html'
})
export class HeaderComponent implements OnInit {
  private router = inject(Router);
  private userService = inject(UserService);
  accountService = inject(AccountService);
  busyService = inject(BusyService);
  private destroyRef = inject(DestroyRef);

  isUserPage: boolean = false;
  isTrackPage: boolean = false;
  userId$ = userId();
  user$ = user();

  ngOnInit() {
    this.fetchRouteInfo();
  }

  private fetchRouteInfo() {
    this.router.events.pipe(
      takeUntilDestroyed(this.destroyRef),
      filter(event => event instanceof NavigationEnd)
    ).subscribe(() => {
      this.isUserPage = this.userService.isUserRoute(this.router.url);
      this.isTrackPage = this.userService.isTrackRoute(this.router.url);
    });
  }
}
