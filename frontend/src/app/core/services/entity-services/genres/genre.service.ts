import {inject, Injectable} from '@angular/core';
import { HttpClient } from "@angular/common/http";
import {Observable} from "rxjs";
import {Genre} from "../../../models/genre.model";
import {environment} from "../../../../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class GenreService {
  private httpClient = inject(HttpClient);
  private baseUrl = environment.BASE_URL;

  getGenreList(): Observable<Genre[]> {
    return this.httpClient.get<Genre[]>(`${this.baseUrl}/Genre`);
  }
}
