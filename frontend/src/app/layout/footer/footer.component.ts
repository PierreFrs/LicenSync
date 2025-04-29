import {Component, DestroyRef, inject, OnInit} from '@angular/core';
import {NavigationEnd, Router, RouterLink} from '@angular/router';
import {UploadLinkComponent} from "../../shared/upload-link/upload-link.component";
import {CommonModule, Location, NgOptimizedImage} from '@angular/common';
import {filter} from "rxjs";
import {UserService} from "../../core/services/entity-services/users/user.service";
import {UserMenuComponent} from "../../shared/user-menu/user-menu.component";
import {AccountService} from "../../core/services/account.service";
import {userId} from "../../core/functions/user-id";
import {user} from "../../core/functions/user";
import {takeUntilDestroyed} from "@angular/core/rxjs-interop";

@Component({
  selector: 'app-footer',
  standalone: true,
  imports: [
    UploadLinkComponent,
    CommonModule,
    UserMenuComponent,
    RouterLink,
    NgOptimizedImage
  ],
  templateUrl: './footer.component.html'
})
export class FooterComponent implements OnInit{
  private router = inject(Router);
  private location = inject(Location);
  private userService = inject(UserService);
  private destroyRef = inject(DestroyRef);

  isUserPage: boolean = false;
  isTrackPage: boolean = false;
  isHomePage: boolean = false;
  userId$ = userId();
  user$ = user();

  accountService = inject(AccountService);


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
      this.isHomePage = this.userService.isHomeRoute(this.router.url);
    });
  }

  goBack() {
    this.location.back();
  }
}
