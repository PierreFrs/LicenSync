import {TransactionReceipt} from "../transaction-receipt.model";

export type Track = {
  id: string;
  trackTitle: string;
  length: string;
  audioFilePath: string;
  userId: string;
  recordLabel: string | null;
  trackVisualPath: string | null;
  firstGenreId: string | null;
  secondaryGenreId: string | null;
  albumId: string | null;
  blockchainHashId: string | null;
  creationDate: string;
  updateDate: string | null;
};

export type TrackWithReceiptDto = {
  trackDto: Track;
  transactionReceipt: TransactionReceipt;
};
