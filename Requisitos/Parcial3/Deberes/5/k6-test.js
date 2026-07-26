import http from 'k6/http';
import { check, sleep } from 'k6';
import { Trend, Rate } from 'k6/metrics';

const t95 = new Trend('t95_response_time');
const failRate = new Rate('failed_requests');

export const options = {
  stages: [
    { duration: '10s', target: 500 },
    { duration: '30s', target: 500 },
    { duration: '10s', target: 0 },
  ],
  thresholds: {
    http_req_duration: ['p(95)<200'],
    failed_requests: ['rate<0.01'],
  },
};

export default function () {
  const payload = JSON.stringify({
    paciente: `Paciente-${__VU}`,
    fecha: '2026-07-13',
    hora: '11:00',
  });

  const params = {
    headers: { 'Content-Type': 'application/json' },
  };

  const res = http.post('http://localhost:3001/api/citas', payload, params);

  check(res, {
    'status es 201': (r) => r.status === 201,
    'respuesta < 200ms': (r) => r.timings.duration < 200,
  });

  t95.add(res.timings.duration);
  failRate.add(res.status !== 201);

  sleep(0.5);
}
