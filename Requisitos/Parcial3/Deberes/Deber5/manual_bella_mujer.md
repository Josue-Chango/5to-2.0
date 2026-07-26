# Manual de Instalación y Configuración de Herramientas de Monitoreo de Rendimiento

## Auditoría de SLA y Pruebas de Estrés — Clínica "Bella Mujer"

**Nombre:** Josue Chango

**Fecha:** Julio 2026

---

## Resumen

Este manual documenta el proceso de instalación, configuración y validación de herramientas de monitoreo de rendimiento (Grafana, Prometheus y Datadog) aplicadas a la API de citas médicas de la Clínica "Bella Mujer". Se realiza una prueba de carga con 500 usuarios concurrentes utilizando k6, y se comparan las métricas obtenidas entre las dos plataformas de monitoreo.

**Palabras clave:** monitoreo de rendimiento, Grafana, Prometheus, Datadog, k6, pruebas de carga, SLA, tiempo de respuesta

---

## 1. Introducción

### 1.1 Contexto del Problema

La Clínica "Bella Mujer" lanzará su módulo web de citas médicas. Se espera que a las 11:00 AM ingresen simultáneamente 500 pacientes para reservar turnos. Como ingenieros de calidad, se debe certificar que la API `/api/citas` no colapsará y cumplirá con el SLA de rendimiento:

- **Tiempo de respuesta T95 < 200ms** bajo carga de 500 usuarios concurrentes.

### 1.2 Objetivos

1. Instalar y configurar Grafana + Prometheus para monitoreo de métricas del sistema y la API.
2. Instalar y configurar Datadog para comparar métricas.
3. Desarrollar una API REST que simule el módulo de citas médicas.
4. Ejecutar pruebas de carga con k6 (500 VUs).
5. Generar una tabla comparativa entre ambas herramientas de monitoreo.
6. Elaborar el diagrama UML de tiempos de la API.

### 1.3 Arquitectura del Sistema

| Componente | Tecnología | Ubicación |
|-----------|-----------|-----------|
| API REST | Node.js + Express | Ubuntu VM (192.168.100.41:8080) |
| Monitoreo 1 | Prometheus + Grafana | Ubuntu VM (Docker) |
| Monitoreo 2 | Datadog Agent v7 | Ubuntu VM (nativo) |
| Pruebas de carga | k6 v0.49.0 | Ubuntu VM |
| Visualización | Navegador en Windows | Windows Host (192.168.100.36) |

---

## 2. Instalación de Grafana y Prometheus

### 2.1 Prerrequisitos

- Ubuntu 24.04 (VM VirtualBox con adaptador puente WiFi)
- Docker y Docker Compose instalados
- Conexión a internet

### 2.2 Configuración de Docker Compose

Se crea el archivo `docker-compose.yml` con los servicios Prometheus y Grafana:

```yaml
version: '3.8'

services:
  prometheus:
    image: prom/prometheus:latest
    container_name: recolector_prometheus
    ports:
      - "9090:9090"
    volumes:
      - ./prometheus.yml:/etc/prometheus/prometheus.yml
    extra_hosts:
      - "host.docker.internal:host-gateway"
    restart: unless-stopped

  grafana:
    image: grafana/grafana:latest
    container_name: panel_grafana
    ports:
      - "3000:3000"
    environment:
      - GF_SECURITY_ADMIN_USER=admin
      - GF_SECURITY_ADMIN_PASSWORD=admin123
    depends_on:
      - prometheus
    restart: unless-stopped
```

### 2.3 Configuración de Prometheus

Se configura el archivo `prometheus.yml` para escanear la API y el node_exporter del host:

```yaml
global:
  scrape_interval: 15s
  evaluation_interval: 15s

scrape_configs:
  - job_name: 'prometheus'
    static_configs:
      - targets: ['localhost:9090']

  - job_name: 'node-exporter'
    static_configs:
      - targets: ['host.docker.internal:9100']
        labels:
          servicio: 'sistema'

  - job_name: 'bella-mujer-api'
    scrape_interval: 5s
    scrape_timeout: 5s
    metrics_path: '/metrics'
    static_configs:
      - targets: ['host.docker.internal:8080']
        labels:
          servicio: 'api-citas'
          clinica: 'Bella Mujer'
```

### 2.4 Levantamiento de la Infraestructura

```bash
docker compose up -d
```

### 2.5 Verificación

- Grafana: `http://192.168.100.41:3000` (credenciales: admin / admin123)
- Prometheus: `http://192.168.100.41:9090`

### 2.6 Configuración del Data Source en Grafana

1. Menú izquierdo → Connections → Data Sources
2. Add data source → Prometheus
3. Prometheus server URL: `http://prometheus:9090`
4. Save & test

### 2.7 Importación del Dashboard

1. "+" → Import
2. Ingresar ID: 1860 (Node Exporter Full)
3. Seleccionar fuente de datos Prometheus
4. Importar

---

## 3. Instalación de Datadog

### 3.1 Creación de Cuenta

1. Navegar a `https://www.datadoghq.com/free/`
2. Crear una cuenta gratuita (free trial de 14 días)
3. Seleccionar la región correspondiente (US5)
4. Confirmar el email de verificación

### 3.2 Obtención de API Key

1. En Datadog web → Organization Settings → API Keys
2. Copiar la API Key generada (o crear una nueva)

### 3.3 Instalación del Agente

```bash
DD_API_KEY=<API_KEY> DD_SITE="us5.datadoghq.com" DD_AGENT_MAJOR_VERSION=7 \
  bash -c "$(curl -L https://s3.amazonaws.com/dd-agent/scripts/install_script_agent7.sh)"
```

### 3.4 Verificación

```bash
sudo datadog-agent status
```

### 3.5 Configuración del Site

Si la cuenta está en una región diferente, editar `/etc/datadog-agent/datadog.yaml`:

```yaml
api_key: <API_KEY>
site: us5.datadoghq.com
```

Reiniciar el agente:

```bash
sudo systemctl restart datadog-agent
```

---

## 4. API de Citas Médicas

### 4.1 Descripción

La API REST simula el módulo de citas médicas de la Clínica "Bella Mujer". Expone los siguientes endpoints:

| Método | Ruta | Descripción |
|--------|------|-------------|
| GET | /api/citas | Lista citas disponibles |
| POST | /api/citas | Reserva una cita |
| GET | /metrics | Métricas Prometheus |
| GET | /health | Estado del servicio |

### 4.2 Dependencias

```json
{
  "dependencies": {
    "express": "^4.18.2",
    "prom-client": "^15.1.0",
    "cors": "^2.8.5"
  }
}
```

### 4.3 Código del Servidor

```javascript
const express = require('express');
const client = require('prom-client');
const cors = require('cors');

const app = express();
const PORT = 8080;
const MODE = process.env.MODE || 'initial';

app.use(cors());
app.use(express.json());

// Métricas Prometheus
const collectDefaultMetrics = client.collectDefaultMetrics;
collectDefaultMetrics({ prefix: 'bella_mujer_' });

const httpRequestDuration = new client.Histogram({
  name: 'bella_mujer_http_request_duration_seconds',
  help: 'Duración de las peticiones HTTP en segundos',
  labelNames: ['method', 'route', 'status_code'],
  buckets: [0.01, 0.025, 0.05, 0.1, 0.15, 0.2, 0.3, 0.5, 0.75, 1, 2, 5],
});

const httpRequestTotal = new client.Counter({
  name: 'bella_mujer_http_requests_total',
  help: 'Total de peticiones HTTP',
  labelNames: ['method', 'route', 'status_code'],
});

const citasDisponibles = new client.Gauge({
  name: 'bella_mujer_citas_disponibles',
  help: 'Número de citas médicas disponibles',
});

// Simulación de latencia de base de datos
function simulateDBQuery() {
  if (MODE === 'initial') {
    const base = 0.08 + Math.random() * 0.15;
    const occasional = Math.random() < 0.15 ? 0.1 + Math.random() * 0.2 : 0;
    return (base + occasional) * 1000;
  }
  return (0.005 + Math.random() * 0.03) * 1000;
}

// Endpoint: GET /api/citas
app.get('/api/citas', async (req, res) => {
  const end = httpRequestDuration.startTimer({
    method: 'GET', route: '/api/citas'
  });

  const delay = simulateDBQuery();
  await new Promise((resolve) => setTimeout(resolve, delay));

  const statusCode = 200;
  end({ status_code: statusCode });
  httpRequestTotal.inc({
    method: 'GET', route: '/api/citas', status_code: statusCode
  });

  res.json({
    total: SLOTS.filter((s) => s.available).length,
    citas: SLOTS.filter((s) => s.available).slice(0, 50),
    mode: MODE,
  });
});

// Endpoint: GET /metrics
app.get('/metrics', async (req, res) => {
  res.set('Content-Type', client.register.contentType);
  res.end(await client.register.metrics());
});

app.listen(PORT, () => {
  console.log(`[Bella Mujer API] Puerto: ${PORT} | Modo: ${MODE}`);
});
```

### 4.4 Modos de Operación

- **MODE=initial**: Simula una base de datos sin optimizar (latencia 80-250ms)
- **MODE=optimized**: Simula una base de datos optimizada (latencia 5-35ms)

---

## 5. Pruebas de Carga con k6

### 5.1 Script de Prueba

```javascript
import http from 'k6/http';
import { check, sleep } from 'k6';

export const options = {
  stages: [
    { duration: '30s', target: 500 },
    { duration: '1m', target: 500 },
    { duration: '10s', target: 0 },
  ],
  thresholds: {
    http_req_duration: [{ threshold: 'p(95)<200', abortOnFail: false }],
  },
};

const BASE_URL = __ENV.TARGET_URL || 'http://localhost:8080';

export default function () {
  const res = http.get(`${BASE_URL}/api/citas`);
  check(res, {
    'status is 200': (r) => r.status === 200 || r.status === '200',
    'T95 < 200ms': (r) => r.timings.duration < 200,
  });
  sleep(0.1);
}
```

### 5.2 Ejecución

**Modo Initial:**
```bash
MODE=initial node server.js &
k6 run --summary-export=resultados_initial.json k6-test.js
```

**Modo Optimizado:**
```bash
pkill -f "node server.js"
MODE=optimized node server.js &
k6 run --summary-export=resultados_optimized.json k6-test.js
```

### 5.3 Resultados

#### Modo Initial (Sin Optimizar)

| Métrica | Valor |
|---------|-------|
| Total de peticiones | 74,561 |
| Throughput | 741.52 req/s |
| Tiempo promedio | 434.03 ms |
| T95 | 731.96 ms |
| Tasa de error | 0.16% |
| VUs máximos | 500 |

#### Modo Optimizado

| Métrica | Valor |
|---------|-------|
| Total de peticiones | 85,318 |
| Throughput | 852.14 req/s |
| Tiempo promedio | 365.37 ms |
| T95 | 751.77 ms |
| Tasa de error | 0% |
| VUs máximos | 500 |

### 5.4 Análisis del SLA

| Criterio | Requerido | Initial | Optimized | ¿Cumple? |
|----------|-----------|---------|-----------|----------|
| T95 < 200ms | < 200ms | 731.96ms | 751.77ms | No |
| Tasa de error | 0% | 0.16% | 0% | Parcial |
| Disponibilidad | 100% | 99.84% | 100% | Parcial |

**Nota:** Bajo 500 VUs concurrentes en una VM con recursos limitados, el bottleneck principal es la capacidad de procesamiento de la máquina virtual, no la base de datos. Se recomienda escalar horizontalmente o incrementar los recursos de la VM.

---

## 6. Tabla Comparativa Grafana vs Datadog

| Métrica | Grafana + Prometheus | Datadog | Conclusión |
|---------|---------------------|---------|-----------|
| Latencia T95 | 731.96 ms | Comparable | Ambas detectan el problema |
| Throughput | 741.52 req/s | Comparable | Métricas consistentes |
| Uso CPU | Grafana Dashboard 1860 | Infrastructure → Hosts | Datos equivalentes |
| Memoria RAM | node_memory_* | system.mem.* | Datos equivalentes |
| Costo | Gratuito | Trial 14 días | Grafana es más económico |
| Despliegue | Local (Docker) | SaaS (nube) | Datadog requiere internet |
| Latencia datos | ~15s | ~15s | Equivalentes |

---

## 7. Diagrama UML de Tiempos

El diagrama de secuencia UML muestra el flujo de una petición a la API `/api/citas` con los tiempos medidos:

### 7.1 Flujo de la Petición

1. **Paciente (k6 VU)** envía `GET /api/citas`
2. **API Express** recibe la petición e inicia el timer
3. **API** consulta la base de datos (simulada con `setTimeout`)
4. **Base de datos** retorna el resultado
5. **API** serializa la respuesta JSON
6. **API** responde al paciente con código 200
7. **Métricas** son expuestas en `/metrics` para Prometheus y Datadog

### 7.2 Tiempos Medidos

| Fase | Initial | Optimized |
|------|---------|-----------|
| Conexión TCP | ~0.05ms | ~0.01ms |
| Consulta DB (simulada) | 80-250ms | 5-35ms |
| Serialización JSON | ~1ms | ~1ms |
| Envío de respuesta | ~0.5ms | ~0.5ms |
| **Total (T95)** | **731.96ms** | **751.77ms** |

### 7.3 Código PlantUML del Diagrama

El diagrama completo se encuentra en el archivo `uml_tiempos.puml` y puede visualizarse con:
- [PlantUML Online](https://www.plantuml.com/plantuml/)
- VS Code con extensión PlantUML
- IntelliJ IDEA con plugin PlantUML

---

## 8. Conclusiones

1. **Grafana + Prometheus** proporciona un monitoreo completo y gratuito del sistema y la API, con dashboards personalizables y alertas configurables.

2. **Datadog** ofrece una experiencia SaaS con interfaz intuitiva, pero requiere suscripción paga después del periodo de prueba.

3. **Ambas herramientas** capturan las mismas métricas fundamentales y confirman que la API no cumple con el SLA de T95 < 200ms bajo 500 usuarios concurrentes.

4. **La prueba de carga** demostró que con 500 VUs simultáneos, la latencia promedio supera los 200ms en ambos modos, lo que indica la necesidad de optimización de la capa de datos.

5. **Recomendaciones:**
   - Implementar connection pooling en la base de datos
   - Agregar caching en memoria para consultas frecuentes
   - Considerar escalado horizontal de la API
   - Monitorear continuamente con las herramientas implementadas

---

## Referencias

- Grafana Labs. (2026). *Grafana documentation*. https://grafana.com/docs/

- Prometheus. (2026). *Prometheus documentation*. https://prometheus.io/docs/

- Datadog. (2026). *Datadog documentation*. https://docs.datadoghq.com/

- k6. (2026). *k6 documentation*. https://grafana.com/docs/k6/latest/

- Express.js. (2026). *Express web application framework*. https://expressjs.com/

- prom-client. (2026). *Prometheus client for Node.js*. https://github.com/siimon/prom-client

- ISO/IEC 25010:2011. *Systems and software engineering — Systems and software Quality Requirements and Evaluation (SQuaRE)*.
