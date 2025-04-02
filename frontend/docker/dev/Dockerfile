#app-pf/frontend/docker/dev/Dockerfile

FROM node:22

RUN npm install -g @angular/cli

WORKDIR /app

COPY . /app

RUN npm install

ENTRYPOINT ["ng", "serve", "--host", "0.0.0.0"]
