import {Observable, of, switchMap, tap} from "rxjs";
import {Track} from "../../../models/track.model";
import {inject, Injectable, signal} from "@angular/core";
import {HttpClient, HttpParams} from "@angular/common/http";
import {catchError, map} from "rxjs/operators";
import {TrackCard} from "../../../models/track-card.model";
import {Pagination} from "../../../models/pagination.model";
import {AppParams} from "../../../models/app-params";
import {environment} from "../../../../../environments/environment";
import {AccountService} from "../../account.service";

@Injectable({
  providedIn: 'root',
})
export class TrackService {
  private httpClient = inject(HttpClient);
  private baseUrl = environment.BASE_URL;
  private accountService = inject(AccountService);
  trackCardList = signal<Pagination<TrackCard> | null>(null);

  titles: string[] = [];
  albums: string[] = [];
  genres: string[] = [];
  releaseDates: string[] = [];

  getTracksByUserId(userId:string): Observable<Track[]> {
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
    // Use the accountService to get the user ID dynamically
    this.accountService.getUserInfos().subscribe({
      next: (user) => {
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
        this.httpClient.get<Pagination<TrackCard>>(`${this.baseUrl}/Track/track-card-list/${user.id}`, { params }).subscribe({
          next: trackCardList => this.trackCardList.set(trackCardList),
        });
      },
      error: (err) => console.error('Error fetching user info:', err),
    });
  }

  uploadTrack(formData: FormData): Observable<TrackCard> {
    return this.accountService.getUserInfos().pipe(
      switchMap(() => this.httpClient.post<TrackCard>(`${this.baseUrl}/Track/track-card`, formData)),
      tap(() => {
        // Refresh the track list after uploading the track
        this.accountService.getUserInfos().subscribe(user => {
          this.httpClient.get<Pagination<TrackCard>>(`${this.baseUrl}/Track/track-card-list/${user.id}`).subscribe({
            next: trackCardList => this.trackCardList.set(trackCardList),
            error: err => console.error('Error fetching updated track list:', err)
          });
        });
      })
    );
  }

  deleteTrack(trackId: string) {
    // Use the accountService to get the user ID dynamically
    this.accountService.getUserInfos().subscribe({
      next: (user) => {
        // Delete the track
        this.httpClient.delete<boolean>(`${this.baseUrl}/Track/${trackId}`).subscribe({
          next: () => {
            // After deleting the track, refresh the track list for the user
            this.httpClient.get<Pagination<TrackCard>>(`${this.baseUrl}/Track/track-card-list/${user.id}`).subscribe({
              next: trackCardList => this.trackCardList.set(trackCardList),
              error: err => console.error('Error fetching updated track list:', err)
            });
          },
          error: err => console.error('Error deleting track:', err)
        });
      },
      error: (err) => console.error('Error fetching user info:', err),
    });
  }

  getTrackCardByTrackId(trackId: string): Observable<TrackCard> {
    return this.httpClient.get<TrackCard>(`${this.baseUrl}/Track/track-card/${trackId}`);
  }
}
