import { Router } from "express";
import EventController from "../controllers/EventController";

const router = Router();

router.post("/", EventController.createEvent);
router.get("/", EventController.getAllEvents);
router.get("/type/:eventType", EventController.getEventsByType);
router.get("/:id", EventController.getEventById);
router.patch("/:id", EventController.updateEvent);
router.delete("/:id", EventController.deleteEvent);

export default router;
