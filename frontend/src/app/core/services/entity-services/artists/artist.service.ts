import {Observable} from "rxjs";
import {inject, Injectable} from "@angular/core";
import { HttpClient } from "@angular/common/http";
import {Artist} from "../../../models/entities/artist.model";
import {environment} from "../../../../../environments/environment";

@Injectable({
  providedIn: 'root',
})
export class ArtistService {
  private httpClient = inject(HttpClient);
  private baseUrl = environment.BASE_URL;

  public getArtistsByTrackId(trackId:string): Observable<Artist[]> {
    return this.httpClient.get<Artist[]>(`${this.baseUrl}/Artist/track/${trackId}`);
  }
  public postArtist(artistFormData: FormData): Observable<Artist> {
    return this.httpClient.post<Artist>(`${this.baseUrl}/Artist/`, artistFormData);
  }
}
