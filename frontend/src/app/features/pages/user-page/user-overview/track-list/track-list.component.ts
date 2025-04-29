import { Component, inject, OnInit, ViewChild } from '@angular/core';
import { TrackCardComponent } from './track-card/track-card.component';
import { CommonModule } from '@angular/common';
import {
  MatPaginator,
  MatPaginatorModule,
  PageEvent,
} from '@angular/material/paginator';
import {Subscription} from 'rxjs';
import { MatGridListModule } from '@angular/material/grid-list';
import {
  BreakpointObserver,
  Breakpoints,
  BreakpointState,
} from '@angular/cdk/layout';
import { TrackService } from '../../../../../core/services/entity-services/tracks/track.service';
import { MatMenu, MatMenuTrigger } from '@angular/material/menu';
import {
  MatListOption,
  MatSelectionList,
  MatSelectionListChange,
} from '@angular/material/list';
import { MatButton, MatIconButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { AppParams } from '../../../../../core/models/app-params';
import { FormsModule } from '@angular/forms';
import {takeUntilDestroyed} from "@angular/core/rxjs-interop";

@Component({
  selector: 'app-track-list',
  standalone: true,
  imports: [
    CommonModule,
    TrackCardComponent,
    MatPaginatorModule,
    MatGridListModule,
    MatMenu,
    MatSelectionList,
    MatListOption,
    MatButton,
    MatMenuTrigger,
    MatIcon,
    FormsModule,
    MatIconButton,
  ],
  templateUrl: './track-list.component.html',
  styleUrls: ['./track-list.component.scss'],
})
export class TrackListComponent implements OnInit {
  private readonly responsive = inject(BreakpointObserver);
  private readonly trackService = inject(TrackService);

  cols = 1;
  rowHeight = '116px';
  gutterSize = '16px';
  isSmallScreen = false;
  sortOptions = [
    { name: 'Dates de sorties ascendantes', value: 'releaseAsc' },
    { name: 'Dates de sorties descendantes', value: 'releaseDesc' },
    { name: 'Titres ascendants', value: 'titleAsc' },
    { name: 'Title descendants', value: 'titleDesc' },
  ];
  appParams = new AppParams();
  pageSizeOptions = [4, 8, 12, 16, 20];

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  private readonly subscriptions = new Subscription();

  constructor() {
    this.subscribeToResponsiveBreakpoints();
  }

  ngOnInit() {
    this.getTrackCardListByUserId();
  }

  get tracks() {
    return this.trackService.trackCardList();
  }

  getTrackCardListByUserId() {
    this.trackService.getTrackCardsByUserId(this.appParams);
  }

  onSearchChange() {
    this.appParams.pageIndex = 1;
    this.getTrackCardListByUserId();
  }

  handlePageEvent(event: PageEvent) {
    this.appParams.pageIndex = event.pageIndex + 1;
    this.appParams.pageSize = event.pageSize;
    this.getTrackCardListByUserId();
  }

  onSortChange(event: MatSelectionListChange) {
    const selectedOption = event.options[0];
    if (selectedOption) {
      this.appParams.sort = selectedOption.value;
      this.appParams.pageIndex = 1;
      this.getTrackCardListByUserId();
    }
  }

  private subscribeToResponsiveBreakpoints() {
      this.responsive
        .observe([
          Breakpoints.HandsetPortrait,
          Breakpoints.HandsetLandscape,
          Breakpoints.TabletPortrait,
          Breakpoints.TabletLandscape,
          Breakpoints.WebLandscape,
        ])
        .pipe(takeUntilDestroyed())
        .subscribe((result) => {
          this.updateLayoutForBreakpoints(result);
          this.updatePaginator(this.tracks?.count ?? 0);
        });
  }

  private updateLayoutForBreakpoints(result: BreakpointState) {
    const breakpoints = result.breakpoints;

    if (breakpoints[Breakpoints.HandsetPortrait]) {
      this.cols = 1;
      this.rowHeight = '116px';
      this.gutterSize = '16px';
      this.appParams.pageSize = 4;
      this.isSmallScreen = true;
    } else if (breakpoints[Breakpoints.HandsetLandscape]) {
      this.cols = 2;
      this.rowHeight = '120px';
      this.gutterSize = '20px';
      this.appParams.pageSize = 8;
      this.isSmallScreen = true;
    } else if (breakpoints[Breakpoints.TabletPortrait]) {
      this.cols = 2;
      this.rowHeight = '120px';
      this.gutterSize = '24px';
      this.appParams.pageSize = 12;
      this.isSmallScreen = false;
    } else if (breakpoints[Breakpoints.TabletLandscape]) {
      this.cols = 3;
      this.rowHeight = '120px';
      this.gutterSize = '28px';
      this.appParams.pageSize = 16;
      this.isSmallScreen = false;
    } else if (breakpoints[Breakpoints.WebLandscape]) {
      this.cols = 4;
      this.rowHeight = '124px';
      this.gutterSize = '34px';
      this.appParams.pageSize = 20;
      this.isSmallScreen = false;
    }
  }

  private updatePaginator(totalTracks: number) {
    if (this.paginator) {
      const { pageIndex, pageSize } = this.appParams;
      this.paginator.pageSize = pageSize;
      const totalPages = Math.ceil(totalTracks / pageSize);

      if (pageIndex > totalPages) {
        this.appParams.pageIndex = totalPages;
      }

      this.paginator.pageIndex = this.appParams.pageIndex - 1;

      this.getTrackCardListByUserId();
    }
  }
}
