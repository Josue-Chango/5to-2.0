# Tabla Comparativa: Grafana + Prometheus vs Datadog

## Métricas del Sistema (durante prueba de carga - 500 VUs)

| Métrica | Grafana + Prometheus | Datadog | Unidad |
|---------|---------------------|---------|--------|
| Uso CPU (pico) | Ver dashboard 1860 | Ver Infrastructure → Hosts | % |
| Memoria RAM | node_memory_MemTotal_bytes | system.mem.usable | bytes |
| Disco I/O | node_disk_read_bytes_total | system.disk.io.read | bytes |
| Red | node_network_receive_bytes_total | system.net.bytes_rcvd | bytes |
| Uptime | node_time_seconds - node_boot_time_seconds | system.uptime | segundos |

## Métricas de la API /api/citas

| Métrica | Grafana + Prometheus | Datadog | Valor (Initial) | Valor (Optimized) |
|---------|---------------------|---------|-----------------|-------------------|
| T95 Latencia | histogram_quantile(0.95, rate(bella_mujer_http_request_duration_seconds_bucket[1m])) | datadog.trace.http.request.duration (percentile:95) | 731.96 ms | 751.77 ms |
| Tasa de requests | rate(bella_mujer_http_requests_total[1m]) | datadog.trace.http.request.count | 741.52 req/s | 852.14 req/s |
| Total peticiones | sum(bella_mujer_http_requests_total) | datadog.trace.http.request.count (sum) | 74,561 | 85,318 |
| Citas disponibles | bella_mujer_citas_disponibles | Custom metric: bella_mujer.citas_disponibles | 95 | 95 |
| Citas reservadas | bella_mujer_citas_reservadas_total | Custom metric: bella_mujer.citas_reservadas | Variable | Variable |
| Tasa de error | 1 - rate(http_req_failed) | datadog.trace.http.request.errors | 0.16% | 0% |
| Tiempo promedio | avg(bella_mujer_http_request_duration_seconds) | datadog.trace.http.request.duration (avg) | 434.03 ms | 365.37 ms |

## Comparativa de Herramientas

| Característica | Grafana + Prometheus | Datadog |
|---------------|---------------------|---------|
| Tipo | Open Source + Open Source | SaaS (proprietario) |
| Costo | Gratuito | Free trial 14 días, depois pago |
| Instalación | Docker en servidor local | Agente instalado en el host |
| Almacenamiento | Local (prometheus data) | Nube (Datadog servers) |
| Latencia de datos | ~15s (scrape interval) | ~15s (agent interval) |
| Personalización | Dashboard personalizado con PromQL | Dashboards predefinidos + custom |
| Métricas personalizadas | Sí (Prometheus metrics) | Sí (DogStatsD / custom metrics) |
| Alertas | Alertmanager (config manual) | Monitors (interfaz web) |
| Retención de datos | Configurable (default 15 días) | Según plan contratado |
| Escalabilidad | Limitada al servidor local | Escalable en la nube |
| API de consultas | PromQL | Datadog Query Language (DQL) |

## Conclusión

Ambas herramientas capturan las mismas métricas fundamentales del sistema y de la API.
La diferencia principal radica en el modelo de despliegue: Grafana+Prometheus es una solución
open source desplegada localmente, mientras que Datadog es un servicio SaaS en la nube.
Para el escenario de la Clínica Bella Mujer, ambas herramientas certifican que bajo carga
de 500 usuarios concurrentes, los tiempos de respuesta superan el SLA de T95 < 200ms,
requiriendo optimización de la capa de base de datos.
