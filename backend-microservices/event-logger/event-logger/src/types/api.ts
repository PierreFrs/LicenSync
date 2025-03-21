import { IEventBase, IEvent } from './Event';

export interface ApiResponse<T = any> {
  message?: string;
  data?: T;
  error?: string;
}

export interface EventCreateRequest extends IEventBase {
  timestamp?: Date;
}

export type EventUpdateRequest = Partial<IEventBase> & {
  timestamp?: Date;
};

export interface EventResponse extends Omit<IEvent, '_id'> {
  _id: string;
} 