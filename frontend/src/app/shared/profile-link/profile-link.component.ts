import {Component, inject} from '@angular/core';
import {MatMenuItem} from "@angular/material/menu";
import {RouterLink} from "@angular/router";
import {MatIcon} from "@angular/material/icon";
import {AccountService} from "../../core/services/account.service";
import {userId} from "../../core/functions/user-id";
import {AsyncPipe} from "@angular/common";

@Component({
  selector: 'app-profile-link',
  standalone: true,
  imports: [
    MatMenuItem,
    RouterLink,
    MatIcon,
    AsyncPipe
  ],
  templateUrl: './profile-link.component.html'
})
export class ProfileLinkComponent {
  accountService = inject(AccountService);
  userId$ = userId();
}
