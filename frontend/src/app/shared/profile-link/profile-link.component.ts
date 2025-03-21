import {Component, inject, OnInit} from '@angular/core';
import {MatMenuItem} from "@angular/material/menu";
import {RouterLink} from "@angular/router";
import {MatIcon} from "@angular/material/icon";
import {AccountService} from "../../core/services/account.service";

@Component({
  selector: 'app-profile-link',
  standalone: true,
  imports: [
    MatMenuItem,
    RouterLink,
    MatIcon
  ],
  templateUrl: './profile-link.component.html'
})
export class ProfileLinkComponent implements OnInit {
  accountService = inject(AccountService);
  userId: string | null = null;

  ngOnInit() {
    this.getUserId();
  }

  private getUserId() {
    this.userId = this.accountService.currentUser()?.id ?? null;
  }
}
