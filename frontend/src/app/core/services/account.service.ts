import {inject, Injectable, signal} from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import {User, UserInfo} from "../models/user";
import {map} from "rxjs/operators";
import {LoginValues} from "../models/login.model";
import {RegisterValues} from "../models/register.model";
import {environment} from "../../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private readonly _httpClient = inject(HttpClient);
  private readonly baseUrl = environment.BASE_URL;

  currentUser = signal<User | null>(null);

  login(values: LoginValues) {
    let params = new HttpParams();
    params = params.append('useCookies', 'true');
    const headers = { 'Content-Type': 'application/json' };

    return this._httpClient.post(`${this.baseUrl}/Account/login`, values, {
      params,
      headers
    });
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

  getUserInfos() {
    return this._httpClient.get<User>(`${this.baseUrl}/Account/user-info`).pipe(
      map(user => {
        this.currentUser.set(user);
        return user;
      })
    )
  }

  logout() {
    return this._httpClient.post(`${this.baseUrl}/Account/logout`, {});
  }

  updateUserInfos(userInfo: UserInfo) {
    return this._httpClient.post(`${this.baseUrl}/Account/user-details`, userInfo);
  }

  getAuthState() {
    return this._httpClient.get<{isAuthenticated: boolean}>(`${this.baseUrl}/Account/auth-status`);
  }
}
