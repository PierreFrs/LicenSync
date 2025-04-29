import {Component, inject} from '@angular/core';
import {MatIcon} from "@angular/material/icon";
import {MatMenuItem} from "@angular/material/menu";
import {RouterLink} from "@angular/router";
import {AccountService} from "../../core/services/account.service";
import {userId} from "../../core/functions/user-id";
import {AsyncPipe} from "@angular/common";

@Component({
  selector: 'app-user-page-link',
  standalone: true,
  imports: [
    MatIcon,
    MatMenuItem,
    RouterLink,
    AsyncPipe
  ],
  templateUrl: './user-page-link.component.html',
})
export class UserPageLinkComponent {
  accountService = inject(AccountService);
  userId$= userId();
}
