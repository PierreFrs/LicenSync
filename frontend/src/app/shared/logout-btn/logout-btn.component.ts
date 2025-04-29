import {Component, DestroyRef, inject} from '@angular/core';
import {Router} from "@angular/router";
import {MatMenuItem} from "@angular/material/menu";
import {MatIcon} from "@angular/material/icon";
import {AccountService} from "../../core/services/account.service";
import {takeUntilDestroyed} from "@angular/core/rxjs-interop";

@Component({
  selector: 'app-logout-btn',
  templateUrl: './logout-btn.component.html',
  imports: [
    MatMenuItem,
    MatIcon
  ],
  standalone: true
})
export class LogoutBtnComponent {
  accountService = inject(AccountService);
  private router = inject(Router);
  private destroyRef = inject(DestroyRef);

  logout() {
    this.accountService.logout()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe( {
      next: () => {
        this.accountService.user$.next(null);
        this.accountService.userId$.next(null);
        this.router.navigateByUrl('/account/login');
      }
    });
  }
}
