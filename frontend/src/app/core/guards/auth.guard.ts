import {CanActivateFn, Router} from '@angular/router';
import {AccountService} from "../services/account.service";
import {inject} from "@angular/core";
import {map} from "rxjs/operators";

export const authGuard: CanActivateFn = () => {
  const accountService = inject(AccountService);
  const router = inject(Router);

  return accountService.isAuth$.pipe(
    map(isAuth => {
      if (!isAuth) {
        router.navigateByUrl('/account/login');
      }
      return isAuth;
    })
  )
};
