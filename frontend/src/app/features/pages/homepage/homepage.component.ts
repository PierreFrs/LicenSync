import {Component, inject} from '@angular/core';
import {RouterLink} from "@angular/router";
import {AsyncPipe, NgClass} from "@angular/common";
import {ResponsiveService} from "../../../core/services/responsive.service";
import {MatButton} from "@angular/material/button";

@Component({
  selector: 'app-homepage',
  templateUrl: './homepage.component.html',
  standalone: true,
  imports: [
    RouterLink,
    AsyncPipe,
    NgClass,
    MatButton
  ],
  styleUrl: './homepage.component.scss'
})
export class HomepageComponent {
  responsiveService = inject(ResponsiveService);

  padding$ = this.responsiveService.padding$;
}
