import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UploadLinkComponent } from './upload-link.component';
import {BehaviorSubject} from "rxjs";
import {ActivatedRoute} from "@angular/router";

describe('UploadLinkComponent', () => {
  let component: UploadLinkComponent;
  let fixture: ComponentFixture<UploadLinkComponent>;

  const paramMapSubject = new BehaviorSubject(new Map<string, string>());
  const activatedRouteStub = {
    paramMap: paramMapSubject.asObservable()
  };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UploadLinkComponent],
      providers: [
        { provide: ActivatedRoute, useValue: activatedRouteStub }
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UploadLinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
