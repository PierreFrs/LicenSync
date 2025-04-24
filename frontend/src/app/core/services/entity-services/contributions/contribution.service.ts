import {inject, Injectable} from '@angular/core';
import { HttpClient } from "@angular/common/http";
import {Observable} from "rxjs";
import {Contribution} from "../../../models/entities/contribution.model";
import {environment} from "../../../../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class ContributionService {
  private httpClient = inject(HttpClient);
  private baseUrl = environment.BASE_URL;

  public getContributionList(): Observable<Contribution[]> {
    return this.httpClient.get<Contribution[]>(`${this.baseUrl}/Contribution`);

  }
  public getContributionByArtistId(artistId:string): Observable<Contribution> {
    return this.httpClient.get<Contribution>(`${this.baseUrl}/Contribution/artist/${artistId}`);
  }
}
