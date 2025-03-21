import { Request, Response } from "express";
import Event from "../models/Event";
import {
  ApiResponse,
  EventCreateRequest,
  EventUpdateRequest,
  EventResponse,
} from "../types/api";

export default class EventController {
  public static async getAllEvents(
    _req: Request,
    res: Response<ApiResponse<EventResponse[]>>
  ): Promise<void> {
    try {
      const events = await Event.find();
      res.status(200).json({ data: events });
    } catch (error) {
      res.status(500).json({
        error:
          error instanceof Error ? error.message : "Une erreur est survenue",
      });
    }
  }

  public static async getEventById(
    req: Request<{ id: string }>,
    res: Response<ApiResponse<EventResponse>>
  ): Promise<void> {
    try {
      const event = await Event.findById(req.params.id);

      if (!event) {
        res.status(404).json({ error: "Événement non trouvé" });
        return;
      }

      res.status(200).json({ data: event });
    } catch (error) {
      res.status(500).json({
        error:
          error instanceof Error ? error.message : "Une erreur est survenue",
      });
    }
  }

  public static async getEventsByType(
    req: Request<{ eventType: string }>,
    res: Response<ApiResponse<EventResponse[]>>
  ): Promise<void> {
    try {
      const events = await Event.find({ event_type: req.params.eventType });
      res.status(200).json({ data: events });
    } catch (error) {
      res.status(500).json({
        error:
          error instanceof Error ? error.message : "Une erreur est survenue",
      });
    }
  }

  public static async createEvent(
    req: Request<{}, {}, EventCreateRequest>,
    res: Response<ApiResponse<EventResponse>>
  ): Promise<void> {
    try {
      const event = new Event(req.body);
      const savedEvent = await event.save();
      res.status(201).json({ data: savedEvent });
    } catch (error) {
      res.status(400).json({
        error:
          error instanceof Error ? error.message : "Une erreur est survenue",
      });
    }
  }

  public static async updateEvent(
    req: Request<{ id: string }, {}, EventUpdateRequest>,
    res: Response<ApiResponse<EventResponse>>
  ): Promise<void> {
    try {
      console.log("Body reçu:", req.body);

      const updatedEvent = await Event.findByIdAndUpdate(
        req.params.id,
        req.body,
        {
          new: true,
          runValidators: true,
          strict: true,
        }
      );

      if (!updatedEvent) {
        res.status(404).json({ error: "Événement non trouvé" });
        return;
      }

      res.status(200).json({ data: updatedEvent });
    } catch (error) {
      console.error("Erreur complète:", error);
      const statusCode =
        error instanceof Error && error.name === "ValidationError" ? 400 : 500;
      res.status(statusCode).json({
        error:
          error instanceof Error ? error.message : "Une erreur est survenue",
      });
    }
  }

  public static async deleteEvent(
    req: Request<{ id: string }>,
    res: Response<ApiResponse<void>>
  ): Promise<void> {
    try {
      const deletedEvent = await Event.findByIdAndDelete(req.params.id);

      if (!deletedEvent) {
        res.status(404).json({ error: "Événement non trouvé" });
        return;
      }

      res.status(200).json({ message: "Événement supprimé avec succès" });
    } catch (error) {
      res.status(500).json({
        error:
          error instanceof Error ? error.message : "Une erreur est survenue",
      });
    }
  }
}
