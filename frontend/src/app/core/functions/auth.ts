import {AccountService} from "../services/account.service";
import {inject} from "@angular/core";

export function isAuth() {
  const accountService = inject(AccountService);
  return accountService.isAuth$.asObservable();
}
