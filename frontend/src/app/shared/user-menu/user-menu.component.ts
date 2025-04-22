import {Component, inject, OnInit} from '@angular/core';
import {ProfileLinkComponent} from "../profile-link/profile-link.component";
import {LogoutBtnComponent} from "../logout-btn/logout-btn.component";
import {MatButton} from "@angular/material/button";
import {MatMenu, MatMenuTrigger} from "@angular/material/menu";
import {MatIcon} from "@angular/material/icon";
import {MatDivider} from "@angular/material/divider";
import {UserPageLinkComponent} from "../user-page-link/user-page-link.component";
import {filter} from "rxjs";
import {NavigationEnd, Router} from "@angular/router";
import {AccountService} from "../../core/services/account.service";
import {UserService} from "../../core/services/entity-services/users/user.service";
import {user} from "../../core/functions/user";
import {AsyncPipe} from "@angular/common";

@Component({
  selector: 'app-user-menu',
  standalone: true,
  imports: [
    ProfileLinkComponent,
    LogoutBtnComponent,
    MatButton,
    MatMenuTrigger,
    MatMenu,
    MatIcon,
    MatDivider,
    UserPageLinkComponent,
    AsyncPipe
  ],
  templateUrl: './user-menu.component.html',
})
export class UserMenuComponent implements OnInit {
  accountService = inject(AccountService);
  isXlScreen: boolean = false;
  isUserPage: boolean = false;
  isProfilePage: boolean = false;
  isTrackPage: boolean = false;
  router = inject(Router);
  userService = inject(UserService);
  user$ = user();

  constructor() {
    this.checkScreenSize();
  }

  ngOnInit() {
    this.fetchRouteInfo();
  }

  private checkScreenSize() {
    this.isXlScreen = window.matchMedia('(min-width: 1280px)').matches;
  }

  private fetchRouteInfo() {
    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd)
    ).subscribe(() => {
      this.isUserPage = this.userService.isUserRoute(this.router.url);
      this.isProfilePage = this.userService.isProfileRoute(this.router.url);
      this.isTrackPage = this.userService.isTrackRoute(this.router.url);
    });
  }
}
