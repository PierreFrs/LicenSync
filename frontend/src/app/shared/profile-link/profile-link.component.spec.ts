import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileLinkComponent } from './profile-link.component';
import {HttpClientTestingModule} from "@angular/common/http/testing";
import {ActivatedRoute, Router} from "@angular/router";
import {of} from "rxjs";

describe('ProfileLinkComponent', () => {
  let component: ProfileLinkComponent;
  let fixture: ComponentFixture<ProfileLinkComponent>;

  const activatedRouteStub = {
    paramMap: of({ get: () => 'test-user-id' }) // Simulating paramMap
  };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        ProfileLinkComponent,
        HttpClientTestingModule,
      ],
      providers: [
        { provide: ActivatedRoute, useValue: activatedRouteStub },
        { provide: Router, useValue: { navigate: jest.fn() } }, // Mock Router if used in the component
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProfileLinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
