export type TrackCard = {
  id: string;
  trackTitle: string;
  length: string;
  recordLabel: string | null;
  firstGenre: string | null;
  secondaryGenre: string | null;
  albumTitle: string | null;
  blockchainHash: string | null;
  artistsLyrics?: string[];
  artistsMusic?: string[];
  artistsMusicAndLyrics?: string[];
  creationDate: string;
  trackAudioFilePath: string;
  trackVisualFilePath: string | null;
  userId: string;
};
