const express = require('express');
const client = require('prom-client');
const cors = require('cors');

const app = express();
const PORT = 8080;
const MODE = process.env.MODE || 'initial';

app.use(cors());
app.use(express.json());

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

const citasReservadas = new client.Counter({
  name: 'bella_mujer_citas_reservadas_total',
  help: 'Total de citas reservadas',
});

const SLOTS = [];
const DOCTORS = [
  'Dra. María López',
  'Dr. Carlos Pérez',
  'Dra. Ana García',
  'Dr. Luis Martínez',
  'Dra. Sofía Hernández',
];
const SPECIALTIES = ['Ginecología', 'Dermatología', 'Medicina General', 'Nutrición', 'Estética'];

for (let h = 8; h <= 17; h++) {
  for (let m = 0; m < 60; m += 30) {
    if (h === 17 && m > 0) break;
    for (const doctor of DOCTORS) {
      SLOTS.push({
        id: `${h}${m === 0 ? '00' : '30'}_${doctor.replace(/\s/g, '_')}`,
        doctor,
        specialty: SPECIALTIES[DOCTORS.indexOf(doctor)],
        time: `${h}:${m === 0 ? '00' : '30'}`,
        available: true,
      });
    }
  }
}

citasDisponibles.set(SLOTS.filter((s) => s.available).length);

function simulateDBQuery() {
  if (MODE === 'initial') {
    const base = 0.08 + Math.random() * 0.15;
    const occasional = Math.random() < 0.15 ? 0.1 + Math.random() * 0.2 : 0;
    return (base + occasional) * 1000;
  }
  return (0.005 + Math.random() * 0.03) * 1000;
}

app.get('/api/citas', async (req, res) => {
  const end = httpRequestDuration.startTimer({ method: 'GET', route: '/api/citas' });
  const start = Date.now();

  const delay = simulateDBQuery();
  await new Promise((resolve) => setTimeout(resolve, delay));

  const available = SLOTS.filter((s) => s.available);
  const statusCode = 200;

  end({ status_code: statusCode });
  httpRequestTotal.inc({ method: 'GET', route: '/api/citas', status_code: statusCode });

  res.json({
    total: available.length,
    citas: available.slice(0, 50),
    server_ms: Math.round(Date.now() - start),
    mode: MODE,
  });
});

app.post('/api/citas', async (req, res) => {
  const end = httpRequestDuration.startTimer({ method: 'POST', route: '/api/citas' });
  const start = Date.now();

  const { slotId } = req.body;

  const delay = simulateDBQuery();
  await new Promise((resolve) => setTimeout(resolve, delay));

  const slot = SLOTS.find((s) => s.id === slotId && s.available);
  if (!slot) {
    const statusCode = 404;
    end({ status_code: statusCode });
    httpRequestTotal.inc({ method: 'POST', route: '/api/citas', status_code: statusCode });
    return res.status(404).json({ error: 'Cita no disponible' });
  }

  slot.available = false;
  citasDisponibles.set(SLOTS.filter((s) => s.available).length);
  citasReservadas.inc();

  const statusCode = 200;
  end({ status_code: statusCode });
  httpRequestTotal.inc({ method: 'POST', route: '/api/citas', status_code: statusCode });

  res.json({
    message: 'Cita reservada exitosamente',
    cita: slot,
    server_ms: Math.round(Date.now() - start),
    mode: MODE,
  });
});

app.get('/metrics', async (req, res) => {
  res.set('Content-Type', client.register.contentType);
  res.end(await client.register.metrics());
});

app.get('/health', (req, res) => {
  res.json({ status: 'ok', mode: MODE, uptime: process.uptime() });
});

app.listen(PORT, () => {
  console.log(`[Bella Mujer API] Escuchando en http://localhost:${PORT}`);
  console.log(`[Bella Mujer API] Modo: ${MODE}`);
  console.log(`[Bella Mujer API] Métricas: http://localhost:${PORT}/metrics`);
  console.log(`[Bella Mujer API] Citas disponibles: ${SLOTS.filter((s) => s.available).length}`);
});
