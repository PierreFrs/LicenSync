#app-pf/frontend/docker/dev/Dockerfile

FROM node:22

RUN npm install -g @angular/cli

WORKDIR /app

COPY . /app
COPY ./entrypoint.sh /app/entrypoint.sh

RUN chmod +x /app/entrypoint.sh

ENTRYPOINT ["/app/entrypoint.sh"]
