import {Observable, of, tap} from "rxjs";
import {Track} from "../../../models/entities/track.model";
import {inject, Injectable, signal} from "@angular/core";
import {HttpClient, HttpParams} from "@angular/common/http";
import {catchError, filter, map} from "rxjs/operators";
import {TrackCard} from "../../../models/entities/track-card.model";
import {Pagination} from "../../../models/pagination.model";
import {AppParams} from "../../../models/app-params";
import {environment} from "../../../../../environments/environment";
import {user} from "../../../functions/user";
import { userId } from "../../../functions/user-id";

@Injectable({
  providedIn: 'root',
})
export class TrackService {
  private httpClient = inject(HttpClient);
  private baseUrl = environment.BASE_URL;
  trackCardList = signal<Pagination<TrackCard> | null>(null);
  user$ = user();
  userId$ = userId()

  titles: string[] = [];
  albums: string[] = [];
  genres: string[] = [];
  releaseDates: string[] = [];

  getTracksByUserId(userId: string): Observable<Track[]> {
    return this.httpClient.get<Track[]>(`${this.baseUrl}/Track/user/${userId}`);
  }

  getTrackPictureByTrackId(trackId: string): Observable<string | null> {
    return this.httpClient.get(`${this.baseUrl}/Track/picture/${trackId}`, { responseType: 'blob' }).pipe(
      map(blob => {
        if (blob) {
          return URL.createObjectURL(blob);
        }
        return null;
      }),
      catchError(error => {
        console.error('Error fetching track picture:', error);
        return of(null);
      })
    );
  }

  getTrackById(trackId: string | undefined): Observable<Track> {
    return this.httpClient.get<Track>(`${this.baseUrl}/Track/${trackId}`);
  }

  getTrackCardsByUserId(appParams: AppParams) {
    let params = new HttpParams();
    if (appParams.sort) {
      params = params.append('sort', appParams.sort);
    }
    if (appParams.search) {
      params = params.append('search', appParams.search);
    }

    params = params.append('pageSize', appParams.pageSize.toString());
    params = params.append('pageIndex', appParams.pageIndex.toString());

    // Call the track service with the user ID
    this.userId$.pipe(
      filter(userId => !!userId), // Only proceed if userId is not null
    ).subscribe(userId => {
      this.httpClient.get<Pagination<TrackCard>>(
        `${this.baseUrl}/Track/track-card-list/${userId}`,
        { params }
      ).subscribe({
        next: trackCardList => this.trackCardList.set(trackCardList),
        error: err => console.error('Error fetching track cards:', err)
      });
    });
  }

  uploadTrack(formData: FormData): Observable<TrackCard> {
    return this.httpClient.post<TrackCard>(`${this.baseUrl}/Track/track-card`, formData)
    .pipe(
      tap(() => {
        if (this.userId$) {
          // Refresh the track list after uploading the track
          this.userId$.pipe(
            filter(userId => !!userId), // Only proceed if userId is not null
          ).subscribe(userId => {
            this.httpClient.get<Pagination<TrackCard>>(
              `${this.baseUrl}/Track/track-card-list/${userId}`
            ).subscribe({
              next: trackCardList => this.trackCardList.set(trackCardList),
              error: err => console.error('Error fetching track cards:', err)
            });
          });
        }
      })
    );
  }

  deleteTrack(trackId: string) {
        // Delete the track
        this.httpClient.delete<boolean>(`${this.baseUrl}/Track/${trackId}`).subscribe({
          next: () => {
            // After deleting the track, refresh the track list for the user
            if (this.userId$) {
              this.userId$.pipe(
                filter(userId => !!userId), // Only proceed if userId is not null
              ).subscribe(userId => {
                this.httpClient.get<Pagination<TrackCard>>(
                  `${this.baseUrl}/Track/track-card-list/${userId}`
                ).subscribe({
                  next: trackCardList => this.trackCardList.set(trackCardList),
                  error: err => console.error('Error fetching track cards:', err)
                });
              });
            }
          },
          error: err => console.error('Error deleting track:', err)
        });
  }

  getTrackCardByTrackId(trackId: string): Observable<TrackCard> {
    return this.httpClient.get<TrackCard>(`${this.baseUrl}/Track/track-card/${trackId}`);
  }
}
