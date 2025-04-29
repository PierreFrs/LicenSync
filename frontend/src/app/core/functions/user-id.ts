import { inject } from "@angular/core";
import {AccountService} from "../services/account.service";

export function userId() {
  const accountService = inject(AccountService);
  return accountService.userId$.asObservable();
}
