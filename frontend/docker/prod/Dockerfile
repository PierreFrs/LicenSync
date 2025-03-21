# Stage 1: Build Angular 18 App
FROM node:22-alpine AS builder

# Install Angular CLI globally
RUN npm install -g @angular/cli@18.0.0

WORKDIR /app

# Copy package files to install dependencies
COPY package*.json ./

# Install dependencies
RUN npm install

# Copy the rest of the application files
COPY . .

# Build the Angular app for production
RUN ng build --configuration=preprod

# Stage 3: Copy Angular to NGINX
FROM nginx:alpine

# Copy the NGINX config
COPY ./docker/prod/nginx.conf /etc/nginx/conf.d/default.conf

# Copy the Angular build from the builder stage to NGINX
COPY --from=builder /app/dist/frontend /usr/share/nginx/html

# Start NGINX
CMD ["nginx", "-g", "daemon off;"]
