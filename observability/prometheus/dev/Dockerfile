FROM prom/prometheus:latest

RUN mkdir -p /etc/prometheus

COPY dev/prometheus.yml /etc/prometheus/prometheus.yml

EXPOSE 9090

CMD ["--config.file=/etc/prometheus/prometheus.yml"]