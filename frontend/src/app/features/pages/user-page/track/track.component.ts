import {Component, inject, Input, OnDestroy, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import { Subscription } from 'rxjs';
import {MatCardModule} from "@angular/material/card";
import {CommonModule, Location} from "@angular/common";
import {MatButtonModule} from "@angular/material/button";
import {MatIconModule} from "@angular/material/icon";
import {TrackAuthenticationDialogComponent} from "./track-authentication-dialog/track-authentication-dialog.component";
import {MatDialog} from "@angular/material/dialog";
import {MatProgressSpinnerModule} from "@angular/material/progress-spinner";
import {BreakpointObserver, Breakpoints, BreakpointState} from "@angular/cdk/layout";
import {TrackService} from "../../../../core/services/entity-services/tracks/track.service";
import {BlockchainService} from "../../../../core/services/entity-services/blockchain/blockchain.service";
import {ImagesModule} from "../../../../core/modules/images.module";
import {TransactionReceipt} from "../../../../core/models/transaction-receipt.model";
import {HashesComparisonModel} from "../../../../core/models/hashes-comparison.model";
import {DialogDataModel} from "../../../../core/models/authentication-dialog-data-model.model";
import {TrackCard} from "../../../../core/models/track-card.model";

@Component({
  selector: 'app-track',
  standalone: true,
  imports: [MatCardModule, CommonModule, MatButtonModule, MatIconModule, ImagesModule, MatProgressSpinnerModule],
  templateUrl: './track.component.html',
  styleUrls: ['./track.component.scss']
})
export class TrackComponent implements OnInit, OnDestroy {
  @Input() id!: string;
  private activatedRoute = inject(ActivatedRoute);
  private router = inject(Router);
  private location = inject(Location);
  private dialog = inject(MatDialog);
  private responsive = inject(BreakpointObserver);
  private trackService = inject(TrackService);
  private blockchainService = inject(BlockchainService);

  track?: TrackCard;
  imageURL: string | null = null;
  operationSuccess: string | null = "neutral";
  isLoading: boolean = false;
  message: string = '';
  cardLargeLayout: boolean = false;
  showConfirm: boolean = false;

  private subscriptions = new Subscription();

  ngOnInit() {
    if (this.id) {
      this.fetchSelectedTrack(this.id);
    }
    this.subscribeToResponsiveBreakpoints();
  }

  private fetchSelectedTrack(trackId: string): void {
    this.trackService.getTrackCardByTrackId(trackId).subscribe({
      next: (track) => {
        if (track) {
          this.track = track;
          this.fetchImageURL();
        } else {
          this.router.navigate(['/not-found']).then((navigated) => {
            if (!navigated) {
              console.warn('Navigation to 404 failed');
            }
          });
        }
      },
      error: (error) => {
        console.error('Error fetching track:', error);
        this.router.navigate(['/not-found']).then((navigated) => {
          if (!navigated) {
            console.warn('Navigation to 404 failed');
          }
        });
      }
    });
  }

  private fetchImageURL(): void {
    if (this.track?.trackVisualFilePath) {
      this.trackService.getTrackPictureByTrackId(this.track.id).subscribe({
        next: (url) => {
          this.imageURL = url;
        },
        error: (error) => {
          console.error('Error fetching track picture:', error);
        }
      });
    }
  }

  private subscribeToResponsiveBreakpoints() {
    this.subscriptions.add(
      this.responsive.observe([
        Breakpoints.HandsetPortrait,
        Breakpoints.HandsetLandscape,
        Breakpoints.TabletPortrait,
        Breakpoints.TabletLandscape,
        Breakpoints.WebLandscape
      ])
      .subscribe(result => {
        this.updateLayoutForBreakpoints(result);
      }));
  }

  private updateLayoutForBreakpoints(result: BreakpointState) {
    const breakpoints = result.breakpoints;
    if (breakpoints[Breakpoints.HandsetPortrait]) {
      this.cardLargeLayout = false;
    } else if (breakpoints[Breakpoints.HandsetLandscape]) {
      this.cardLargeLayout = false;
    } else if (breakpoints[Breakpoints.TabletPortrait]) {
      this.cardLargeLayout = true;
    } else if (breakpoints[Breakpoints.TabletLandscape]) {
      this.cardLargeLayout = false;
    } else if (breakpoints[Breakpoints.WebLandscape]) {
      this.cardLargeLayout = true;
    }
  }

  authenticateTrack(): void {
    if (this.id === null) {
      console.error('trackId is null');
      return;
    }

    this.operationSuccess = "neutral";
    this.isLoading = true;

    this.blockchainService.storeHash(this.id).subscribe({
      next: (response: TransactionReceipt) => {
        this.isLoading = false;
        this.operationSuccess = response ? 'success' : 'failure';

        if (this.operationSuccess === 'success') {
          this.message = "Authentication réussie !!";
          this.fetchSelectedTrack(this.id!);
          } else {
          this.message = "Echec de l'authentification...";
        }
      },
      error: (error) => {
        console.error('Error during authentication:', error);
        this.isLoading = false;
        this.operationSuccess = "failure";
        this.message = "Echec de l'authentification...";
      }
    });
  }

  fetchHashesAndOpenDialog(): void {
    if (!this.id) {
      console.error('Track ID is null or undefined');
      return;
    }
    this.isLoading = true;
    this.blockchainService.compareHashes(this.id).subscribe({
      next: (hashes) => {
        this.isLoading = false;
        this.openDialogWithHashes(hashes);
      },
      error: (error) => {
        this.isLoading = false;
        console.error('Error fetching hashes:', error);
      }
    });
  }

  private openDialogWithHashes(hashes: HashesComparisonModel): void {
    const dialogData: DialogDataModel = {
      ...hashes,
      trackTitle: this.track?.trackTitle ?? 'Unknown Title'
    };

    this.dialog.open(TrackAuthenticationDialogComponent, {
      panelClass: 'track-authentication-dialog',
      data: dialogData
    });
  }

  deleteTrack(): void {
    if (!this.id) {
      console.error('trackId is null');
      return;
    }

    this.trackService.deleteTrack(this.id);

    if (this.trackService.trackCardList()) {
      this.operationSuccess = "success";
      this.message = 'Track supprimé avec succès';
      this.location.back();
    } else {
      this.operationSuccess = "failure";
      this.message = 'Echec de la suppression du track';
    }
  }


  toggleShowConfirm(): void {
    this.showConfirm = true;
  }

  ngOnDestroy() {
    this.subscriptions.unsubscribe();
    }
}
