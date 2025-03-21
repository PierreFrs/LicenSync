import {inject, Injectable} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {Observable, of} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {catchError, map} from "rxjs/operators";
import {environment} from "../../../../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private httpClient = inject(HttpClient);
  private activatedRoute = inject(ActivatedRoute)
  private baseUrl = environment.BASE_URL;

  getUserIdFromRoute(): string | null {
    let child = this.activatedRoute.root;
    while (child.firstChild) {
      child = child.firstChild;
    }

    return child.snapshot.paramMap.get('id');
  }

  isUserRoute(url: string): boolean {
    return url.includes('/user/');
  }

  isProfileRoute(url: string): boolean {
    return url.includes('/profile/');
  }

  isTrackRoute(url: string): boolean {
    return url.includes('/track/');
  }

  isHomeRoute(url: string): boolean {
    return url === '/';
  }

  getUserPicture(userId: string): Observable<string | null> {
    return this.httpClient.get(`${this.baseUrl}/User/picture/${userId}`, { responseType: 'blob' }).pipe(
      map(blob => {
        if (blob) {
          return URL.createObjectURL(blob);
        }
        return null;
      }),
      catchError(error => {
        console.error('Error fetching user picture:', error);
        return of(null);
      })
    );
  }
}
