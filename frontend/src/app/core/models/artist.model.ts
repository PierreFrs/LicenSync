export type Artist = {
  id: string;
  firstname: string;
  lastname: string;
  trackId: string;
  contributionId: string;
}

export type NewArtist = {
  firstname: string;
  lastname: string;
  trackId: string;
  contributionId: string;
}

export type ArtistGroup = {
  contributionType: string;
  artists: Artist[];
}

export type ArtistWithCOntribuionLabel = {
  firstname: string;
  lastname: string;
  contributionLabel: string;
}
