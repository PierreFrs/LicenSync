import { Injectable } from '@angular/core';
import {Subject} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class TrackUpdateService {
  _trackUpdate$ = new Subject<void>;

  get trackUpdate$() {
    return this._trackUpdate$.asObservable();
  }
}
