import {TrackCard} from "./track-card.model";

export type AlbumCard = {
  id: string;
  albumTitle: string;
  albumVisualPath: string | null;
  trackCards: TrackCard[];
};
