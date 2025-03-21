import { Component } from '@angular/core';
import { Router} from "@angular/router";
import {MatCard} from "@angular/material/card";

@Component({
  selector: 'app-server-error',
  standalone: true,
  imports: [
    MatCard
  ],
  templateUrl: './server-error.component.html',
})
export class ServerErrorComponent {
  error?: { message: string; details: string };

  constructor(private router: Router) {
    const navigation = this.router.getCurrentNavigation();
    this.error = navigation?.extras.state?.['error'] || { message: '', details: '' };
  }
}
