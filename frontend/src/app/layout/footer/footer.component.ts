import {Component, inject, OnInit} from '@angular/core';
import {NavigationEnd, Router, RouterLink} from '@angular/router';
import {ProfileLinkComponent} from "../../shared/profile-link/profile-link.component";
import {UploadLinkComponent} from "../../shared/upload-link/upload-link.component";
import {CommonModule, Location} from '@angular/common';
import {filter} from "rxjs";
import {UserService} from "../../core/services/entity-services/users/user.service";
import {LogoutBtnComponent} from "../../shared/logout-btn/logout-btn.component";
import {UserMenuComponent} from "../../shared/user-menu/user-menu.component";
import {AccountService} from "../../core/services/account.service";

@Component({
  selector: 'app-footer',
  standalone: true,
  imports: [
    ProfileLinkComponent,
    UploadLinkComponent,
    CommonModule,
    LogoutBtnComponent,
    UserMenuComponent,
    RouterLink
  ],
  templateUrl: './footer.component.html'
})
export class FooterComponent implements OnInit{
  private router = inject(Router);
  private location = inject(Location);
  private userService = inject(UserService);

  isUserPage: boolean = false;
  isTrackPage: boolean = false;
  isHomePage: boolean = false;
  userId: string | null = null;
  isAuthenticated = false;

  accountService = inject(AccountService);


  ngOnInit() {
    this.fetchRouteInfo();
    this.isAuthenticatedCheck();
  }
  private fetchRouteInfo() {
    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd)
    ).subscribe(() => {
      this.isUserPage = this.userService.isUserRoute(this.router.url);
      this.isTrackPage = this.userService.isTrackRoute(this.router.url);
      this.isHomePage = this.userService.isHomeRoute(this.router.url);
      this.userId = this.userService.getUserIdFromRoute();
    });
  }

  private isAuthenticatedCheck() {
    this.accountService.getAuthState().subscribe({
      next: (auth) => {
        this.isAuthenticated = auth.isAuthenticated;
        console.log(auth);
      }
    });
  }

  goBack() {
    this.location.back();
  }
}
