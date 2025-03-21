import mongoose, { Schema } from "mongoose";
import { IEvent } from "../types/Event";

const eventSchema = new Schema<IEvent>({
  event_type: {
    type: String,
    required: true,
  },
  resource_id: {
    type: String,
    required: true,
  },
  resource_type: {
    type: String,
    required: true,
  },
  timestamp: {
    type: Date,
    default: Date.now,
  },
  metadata: {
    type: Schema.Types.Mixed,
    required: true,
  },
});

export default mongoose.model<IEvent>("Event", eventSchema);
