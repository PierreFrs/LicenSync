import {environment} from "../../src/environments/environment";
import {Track} from "../../src/app/core/models/track.model";

describe('Backend Connectivity', () => {
  it('should reach the backend API', () => {
    cy.request(`${environment.BASE_URL}/Health`)
      .should((response) => {
        expect(response.status).to.eq(200);
      });
  });
});

describe('Go to the homepage', () => {
  it('should display the homepage', () => {
    cy.visit(`${environment.FRONT_BASE_URL}`);
    cy.contains('Empowering artists, one block at a time');
    cy.get('[data-cy=header]');
    cy.get('[data-cy=footer]');
  });
});

describe('Login to the application', () => {
  beforeEach(() => {
    cy.visit('http://localhost:4200/account/login');
  });
  it('should display the login page', () => {
    cy.contains('Login');
    cy.get('[data-cy=login-form]');
  });
  it('should login with valid credentials', () => {
    cy.get('[data-cy=email-field]').type('p.fraisse@mail.com');
    cy.get('[data-cy=password-field]').type('Pa$$w0rd');
    cy.get('[data-cy=login-button]').click();
    cy.contains('Titres :');
  });
  it('should not login with invalid credentials', () => {
    cy.get('[data-cy=email-field]').type('toto');
    cy.get('[data-cy=login-button]').should('be.disabled');
  });
});

describe('Get tracks by user id', () => {
  let track: Track = {} as Track;
  const baseBackendUrl = `${environment.BASE_URL}`;
  const baseFrontendUrl = `${environment.FRONT_BASE_URL}`;

  beforeEach(() => {
    cy.visit(`${baseFrontendUrl}/account/login`);
    cy.get('[data-cy=email-field]').type('p.fraisse@mail.com');
    cy.get('[data-cy=password-field]').type('Pa$$w0rd');
    cy.get('[data-cy=login-button]').click();
    cy.intercept('GET', `${baseBackendUrl}/Track/user/b0d28ce7-072a-4e2e-b3a7-888b7a88fb45`).as('userData');
    cy.intercept('POST', `${baseBackendUrl}/Track/track-card`).as('uploadTrack');
  });

  it('should display the login page', () => {
    cy.contains('Login');
    cy.get('[data-cy=login-form]');
  });

  it('Visits the user page', () => {
    cy.get('[data-cy=track-list]');
    cy.get('[data-cy=track-card]').its('length').should('be.gt', 0);
    cy.get('[data-cy=paginator]');
  });

  it('should fill up and submit the new track form', () => {
    cy.intercept('GET', `${baseBackendUrl}/Genre`).as('getGenres');
    cy.intercept('GET', `${baseBackendUrl}/Album/user/b0d28ce7-072a-4e2e-b3a7-888b7a88fb45`).as('getAlbums');
    cy.intercept('GET', `${baseBackendUrl}/Contribution`).as('getContributions');

    cy.get('[data-cy=open-upload-dialog]').click();

    cy.wait('@getGenres');
    cy.wait('@getAlbums');
    cy.wait('@getContributions');

    cy.get('[data-cy=track-title-input]').type('New track');
    cy.get('[data-cy=artist-contribution-select]').click();
    cy.get('mat-option').contains('Musique').click();
    cy.get('[data-cy=artist-firstname-input]').type('John');
    cy.get('[data-cy=artist-lastname-input]').type('Doe');
    cy.get('[data-cy=add-artist-button]').click();
    cy.get('[data-cy=remove-artist-button]').eq(1).click();
    cy.get('[data-cy=album-select]').click();
    cy.get('mat-option').contains('The Wall').click();
    cy.get('[data-cy=record-label-input]').type('EMI');
    cy.get('[data-cy=primary-genre-select]').click();
    cy.get('mat-option').contains('Pop').click();
    cy.get('[data-cy=secondary-genre-select]').click();
    cy.get('mat-option').contains('Rock').click();
    cy.get('[data-cy=audio-file-input]').click();
    cy.get('input[type="file"]').eq(0).selectFile('cypress/fixtures/mp3_test_file.mp3', {force: true});
    cy.get('[data-cy=visual-file-input]').click();
    cy.get('input[type="file"]').eq(1).selectFile('cypress/fixtures/jpg_test_file.jpg', {force: true});
    cy.get('[data-cy=submit-upload-button]').click();
    cy.wait('@uploadTrack').then((interception) => {
      expect(interception.response?.statusCode).to.eq(200);
      track = interception.response?.body;
      console.log("track : ", track);
    });
  });

  it('should go to the new track file and verify the data', () => {
    cy.wait(1000);
    cy.visit(`${baseFrontendUrl}/track/${track.id}`);
    cy.contains('New track');
    cy.contains('John Doe');
    cy.contains('The Wall');
    cy.contains('EMI');
    cy.contains('Pop');
    cy.contains('Rock');
    cy.get('[data-cy=protect-track-button]').should('exist');
  });

  it('should protect the track on the blockchain', () => {
    cy.wait(1000);
    cy.visit(`${baseFrontendUrl}/track/${track.id}`);
    cy.intercept('PUT', `${baseBackendUrl}/BlockchainAuthentication/store-hash/${track.id}`).as('storeHash');
    cy.get('[data-cy=protect-track-button]').click();
    cy.wait('@storeHash').then((interception) => {
      expect(interception.response?.statusCode).to.eq(200);
    });
  });

  it('should compare the hash on the database with the one on the blockchain', () => {
    cy.wait(1000);
    cy.visit(`${baseFrontendUrl}/track/${track.id}`);
    cy.get('[data-cy=check-hash-button]').should('exist');
    cy.intercept('GET', `${baseBackendUrl}/BlockchainAuthentication/compareHashes/${track.id}`).as('checkHash');
    cy.get('[data-cy=check-hash-button]').click();
    cy.wait('@checkHash').then((interception) => {
      expect(interception.response?.statusCode).to.eq(200);
    });
    cy.get('[data-cy=database-hash]').invoke('text').then(firstParaText => {
      cy.get('[data-cy=blockchain-hash]').invoke('text').then(secondParaText => {
        expect(firstParaText).to.eq(secondParaText);
      });
    });
  });

  it('should delete the track', () => {
    cy.wait(1000);
    cy.visit(`${baseFrontendUrl}/track/${track.id}`);
    cy.get('[data-cy=delete-track-button]').click();
    cy.get('[data-cy=confirm-delete-track-button]').click();
  });

  it('should not find the track', () => {
    cy.wait(1000);
    cy.visit(`${baseFrontendUrl}/track/${track.id}`);
    cy.contains('404');
  });
});
