FROM node:lts

RUN npm install -g @angular/cli

WORKDIR /app

COPY . /app
COPY ./entrypoint.sh /app/entrypoint.sh

RUN npm install -legacy-peer-deps --verbose

CMD ["ng", "serve", "--host", "0.0.0.0", "--poll", "1000"]
