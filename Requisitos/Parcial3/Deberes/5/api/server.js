const express = require('express');
const promClient = require('prom-client');
const app = express();
const PORT = 3001;

const isSlow = process.argv.includes('--slow');

const register = new promClient.Registry();
promClient.collectDefaultMetrics({ register });

const httpRequestDuration = new promClient.Histogram({
  name: 'http_request_duration_ms',
  help: 'Duración de peticiones HTTP en ms',
  labelNames: ['method', 'route', 'status'],
  buckets: [10, 25, 50, 75, 100, 150, 200, 300, 500, 1000, 2000],
  registers: [register],
});

const httpRequestsTotal = new promClient.Counter({
  name: 'http_requests_total',
  help: 'Total de peticiones HTTP',
  labelNames: ['method', 'route', 'status'],
  registers: [register],
});

app.use(express.json());

app.use((req, res, next) => {
  const end = httpRequestDuration.startTimer();
  res.on('finish', () => {
    const route = req.route ? req.route.path : req.path;
    end({ method: req.method, route, status: res.statusCode });
    httpRequestsTotal.inc({ method: req.method, route, status: res.statusCode });
  });
  next();
});

app.get('/metrics', async (req, res) => {
  res.set('Content-Type', register.contentType);
  res.end(await register.metrics());
});

const sleep = (ms) => new Promise(resolve => setTimeout(resolve, ms));

app.post('/api/citas', async (req, res) => {
  const { paciente, fecha, hora } = req.body || {};

  if (!paciente || !fecha || !hora) {
    return res.status(400).json({ error: 'Faltan campos: paciente, fecha, hora' });
  }

  if (isSlow) {
    await sleep(100 + Math.random() * 400);
  } else {
    await sleep(Math.random() * 50);
  }

  const cita = {
    id: Math.floor(Math.random() * 1000000),
    paciente,
    fecha,
    hora,
    estado: 'confirmada',
    timestamp: new Date().toISOString(),
  };

  res.status(201).json(cita);
});

app.get('/api/health', (req, res) => {
  res.json({ status: 'ok', uptime: process.uptime() });
});

app.listen(PORT, () => {
  console.log(`API Citas Bella Mujer corriendo en http://localhost:${PORT}`);
  console.log(`Modo: ${isSlow ? 'LENTO (simula sobrecarga)' : 'NORMAL'}`);
  console.log(`Métricas: http://localhost:${PORT}/metrics`);
});
