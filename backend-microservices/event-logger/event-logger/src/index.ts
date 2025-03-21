import express, { Application } from "express";
import mongoose from "mongoose";
import cors from "cors";
import eventRoutes from "./routes/events";

const app: Application = express();
const PORT: number = Number(process.env.PORT) || 3001;

// Middleware
app.use(express.json());
app.use(cors());

// Health check
app.get("/health", (_, res) => {
  res.status(200).json({ status: "OK", service: "event-logger" });
});

// MongoDB connection
mongoose
  .connect(process.env.MONGODB_URI || "mongodb://mongodb:27017/eventlogger")
  .then(() => console.log("Connecté à MongoDB"))
  .catch((err) => console.error("Erreur de connexion MongoDB:", err));

// Routes
app.use("/events", eventRoutes);

app.listen(PORT, () => {
  console.log(`Service event-logger démarré sur le port ${PORT}`);
});
