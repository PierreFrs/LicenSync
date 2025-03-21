import {Component, inject, OnInit} from '@angular/core';
import {MatIcon} from "@angular/material/icon";
import {MatMenuItem} from "@angular/material/menu";
import {RouterLink} from "@angular/router";
import {AccountService} from "../../core/services/account.service";

@Component({
  selector: 'app-user-page-link',
  standalone: true,
  imports: [
    MatIcon,
    MatMenuItem,
    RouterLink
  ],
  templateUrl: './user-page-link.component.html',
})
export class UserPageLinkComponent implements OnInit {
  accountService = inject(AccountService);
  userId: string | null = null;

  ngOnInit() {
    this.getUserId();
  }

  private getUserId() {
    this.userId = this.accountService.currentUser()?.id ?? null;
  }
}
