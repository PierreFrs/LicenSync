import { TestBed } from '@angular/core/testing';

import { UserService } from './user.service';
import {BehaviorSubject} from "rxjs";
import {ActivatedRoute} from "@angular/router";
import {TrackService} from "../tracks/track.service";
import {HttpClientTestingModule} from "@angular/common/http/testing";

describe('UserService', () => {
  let service: UserService;

  const paramMapSubject = new BehaviorSubject(new Map<string, string>());
  const activatedRouteStub = {
    root: {
      firstChild: {
        snapshot: {
          paramMap: {
            get: (key: string) => paramMapSubject.value.get(key),
          },
        },
        firstChild: null, // Add more nesting if needed
      },
    },
  };


  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        { provide: ActivatedRoute, useValue: activatedRouteStub },
        TrackService
      ]
    });
    service = TestBed.inject(UserService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('isUserRoute', () => {
    it('should return true for user route', () => {
      expect(service.isUserRoute('/user/1')).toEqual(true);
    });

    it('should return false for non-user route', () => {
      expect(service.isUserRoute('/track/1')).toEqual(false);
    });
  });
});
