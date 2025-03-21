import {inject, Injectable} from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import {Observable, of} from "rxjs";
import {catchError, map} from "rxjs/operators";
import {HashesComparisonModel} from "../../../models/hashes-comparison.model";
import {TransactionReceipt} from "../../../models/transaction-receipt.model";
import {TrackWithReceiptDto} from "../../../models/track.model";
import {environment} from "../../../../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class BlockchainService {
  private httpClient = inject(HttpClient);
  private baseUrl = environment.BASE_URL;

  public storeHash(trackId: string): Observable<TransactionReceipt> {
    const url = `${this.baseUrl}/BlockchainAuthentication/store-hash/${trackId}`;
    const options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };

    return this.httpClient.put<TrackWithReceiptDto>(url, {}, options).pipe(
      map(response => {
        return response.transactionReceipt;
      }),
      catchError(() => {
        return of({} as TransactionReceipt);
      })
    );
  }

  compareHashes(trackId: string): Observable<HashesComparisonModel> {
    return this.httpClient.get<HashesComparisonModel>(`${this.baseUrl}/BlockchainAuthentication/compareHashes/${trackId}`)
      .pipe(
        catchError(() => {
          return of({} as HashesComparisonModel);
        })
      );
  }



}
