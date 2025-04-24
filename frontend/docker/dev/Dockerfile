#app-pf/frontend/docker/dev/Dockerfile

FROM node:22-alpine

WORKDIR /app

COPY package.json package-lock.json ./

RUN npm install

COPY . .

EXPOSE 4200

ENTRYPOINT ["npm", "start"]
