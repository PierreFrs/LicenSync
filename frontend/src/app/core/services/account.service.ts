import {inject, Injectable} from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import {User, UserInfo} from "../models/entities/user";
import {LoginValues} from "../models/login.model";
import {RegisterValues} from "../models/register.model";
import {environment} from "../../../environments/environment";
import {BehaviorSubject, switchMap, tap} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private readonly _httpClient = inject(HttpClient);
  private readonly baseUrl = environment.BASE_URL;
  isAuth$ = new BehaviorSubject<boolean>(false);
  user$ = new BehaviorSubject<User | null>(null);
  userId$ = new BehaviorSubject<string | null>(null);

  login(values: LoginValues) {
    let params = new HttpParams();
    params = params.append('useCookies', 'true');
    const headers = { 'Content-Type': 'application/json' };

    return this._httpClient.post(`${this.baseUrl}/Account/login`, values, {
      params,
      headers
    })
      .pipe(
        tap(() => {
          this.isAuth$.next(true);
        }),
        switchMap(() => this.getUser())
      );
  }

  refreshAccessToken() {
    const headers = { 'Content-Type': 'application/json' };
    return this._httpClient.post(`${this.baseUrl}/refresh`, {}, { headers });
  }

  register(values: RegisterValues) {
    let params = new HttpParams();
    params = params.append('useCookies', 'true');
    const headers = { 'Content-Type': 'application/json' };

    return this._httpClient.post(`${this.baseUrl}/Account/register`, values, {
      params,
      headers
    });
  }

  getUser() {
    return this._httpClient.get<User>(`${this.baseUrl}/Account/user-info`).pipe(
      tap(user => {
        if (user) {
          this.user$.next(user);
          this.userId$.next(user.id);
        }
      })
    )
  }

  logout() {
    return this._httpClient.post(`${this.baseUrl}/Account/logout`, {})
      .pipe(
        tap(() => {
          this.isAuth$.next(false);
          this.user$.next(null);
          this.userId$.next(null);
        })
      );
  }

  updateUserInfos(userInfo: UserInfo) {
    return this._httpClient.post(`${this.baseUrl}/Account/user-details`, userInfo);
  }
}
