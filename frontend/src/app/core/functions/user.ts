import { inject } from "@angular/core";
import {AccountService} from "../services/account.service";

export function user() {
  const accountService = inject(AccountService);
  return accountService.user$.asObservable();
}
