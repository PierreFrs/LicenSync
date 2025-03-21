# Micro-Service

<p align="center">
  <img src="banner.jpg" alt="Microservices">
</p>

## Contexte du projet

Votre application actuelle est un **monolithe** qui gère :

- Une **authentification utilisateur**.
- Une **fonctionnalité principale** (ex. : gestion de tâches, commandes, etc.).

Elle est déjà dockerisée, avec un front-end, un back-end et une base de données. Cependant, pour améliorer la modularité et introduire des pratiques modernes, vous allez intégrer un **micro-service pour gérer les logs d’événements**.

Ce micro-service, déjà disponible dans le dossier `event-logger`, est écrit en **Node.js**, utilise une base de données **MongoDB** et devra être relié au monolithe via une **API Gateway** basée sur **Nginx**.

**Votre objectif**:

1. à chaque action CRUD (authentification + fonctionnalité principale), un événement devra être envoyé au micro-service pour être enregistré.
2. A joutez une route spécifique dans votre API du backend monolithique pour permettre uniquement aux utilisateurs avec le **rôle ADMIN** de lister les logs.

## Modalités pédagogiques

**Activité individuelle en mode collaboratif.**

### Les contraintes

4 jours ouvrés.

### Qu'est-ce qu'un événement ?

Un **événement** représente une action significative dans votre application. Par exemple :

- Création d’un utilisateur : `user_created`.
- Mise à jour d’une tâche : `task_updated`.
- Suppression d’une commande : `order_deleted`.

Ils sont utiles pour garder une trace des actions effectuées, détecter des anomalies, ou pour des besoins d’audit.

### Structure d’un événement

```json
{
  "event_type": "user_created",
  "resource_type": "user", // user, task, order, etc.
  "resource_id": "12345", // user_id, task_id, order_id, etc.
  "user_id": "admin_123", // id de l'utilisateur qui a effectué l'action
  "timestamp": "2024-11-22T14:00:00Z", // date et heure de l'événement
  "metadata": {
    // metadata supplémentaires selon l'événement
    "name": "John Doe",
    "email": "john.doe@example.com"
  }
}
```

### Routes dispos de l'API

- `POST /events` : créer un événement
- `GET /events` : récupérer tous les événements
- `GET /events/:id` : récupérer un événement par son id
- `GET /events/type/:type` : récupérer les événements par type
- `DELETE /events/:id` : supprimer un événement par son id

### Nouveaux chemins attendus pour le backend (via l’API Gateway avec Nginx)

- `/internal/logs` : pour accéder aux fonctionnalités de gestion des logs (restreint au backend du monolithe uniquement).
- `/api` : pour rediriger les requêtes vers votre monolithe.

## Modalités d'évaluation

Analyse du dépôt GitHub et vérification en ligne de l’application déployée par le formateur.

## Livrables

La complétion du dépôt GitHub avec :

- le micro-service intégré dans votre stack existante.
- le README.md documentant en plus les interactions entre le monolithe, le micro-service et l’API Gateway.
- un lien vers votre app en ligne

## Critères de performance

- L'API Gateway redirige correctement les requêtes
- Les routes du micro-service de logs (/internal/logs) doivent être accessibles uniquement depuis le backend monolithique.
- Les actions CRUD génèrent des événements enregistrés dans le micro-service.
- Le déploiement est automatisé et fonctionnel.
- L’application est accessible en production via des sous-domaines sécurisés.

## Ressources

[C'est quoi un micro-service?](https://www.youtube.com/watch?v=rv4LlmLmVWk)
