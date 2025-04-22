import {Component, inject} from '@angular/core';
import {Router} from "@angular/router";
import {MatButton} from "@angular/material/button";
import {MatMenuItem} from "@angular/material/menu";
import {MatIcon} from "@angular/material/icon";
import {AccountService} from "../../core/services/account.service";

@Component({
  selector: 'app-logout-btn',
  templateUrl: './logout-btn.component.html',
  imports: [
    MatButton,
    MatMenuItem,
    MatIcon
  ],
  standalone: true
})
export class LogoutBtnComponent {
  accountService = inject(AccountService);
  private router = inject(Router);

  logout() {
    this.accountService.logout().subscribe( {
      next: () => {
        this.accountService.user$.next(null);
        this.accountService.userId$.next(null);
        this.router.navigateByUrl('/account/login');
      }
    });
  }
}
