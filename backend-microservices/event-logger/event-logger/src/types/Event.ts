// Type de base pour un événement
export interface IEventBase {
  event_type: string;
  resource_id: string;
  resource_type: string;
  metadata: Record<string, any>;
}

// Type pour un événement en base de données
export interface IEvent extends IEventBase {
  _id?: string;
  timestamp: Date;
}
