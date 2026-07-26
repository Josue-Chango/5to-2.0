import http from 'k6/http';
import { check, sleep } from 'k6';

export const options = {
  /*stages: [
    { duration: '30s', target: 500 },
    { duration: '1m', target: 500 },
    { duration: '10s', target: 0 },
  ],
  thresholds: {
    http_req_duration: [{ threshold: 'p(95)<200', abortOnFail: false }],
  },*/
  vus: 50,
  iterations: 500,
};

const BASE_URL = __ENV.TARGET_URL || 'http://localhost:8080';

export default function () {
  const res = http.get(`${BASE_URL}/api/citas`);

  check(res, {
    'status is 200': (r) => r.status === 200 || r.status === '200',
    'response has data': (r) => r.body && r.body.length > 2,
    'T95 < 200ms': (r) => r.timings.duration < 200,
  });

  sleep(0.1);
}

export function handleSummary(data) {
  const duration = data.metrics.http_req_duration;
  const requests = data.metrics.http_reqs;
  const failed = data.metrics.http_req_failed;

  const t95 = duration.values['p(95)'] || 0;
  const t99 = duration.values['p(99)'] || 0;
  const avg = duration.values['avg'] || 0;
  const passed = t95 < 200;

  const totalReqs = requests.values.count || 0;
  const errorRate = failed && failed.values.rate !== undefined
    ? ((1 - failed.values.rate) * 100).toFixed(2)
    : 'N/A';

  console.log('\n========== RESULTADOS DE CARGA ==========');
  console.log('Total peticiones: ' + totalReqs);
  console.log('Tasa de exito:    ' + errorRate + '%');
  console.log('Tiempo promedio:  ' + avg.toFixed(2) + 'ms');
  console.log('T95:              ' + t95.toFixed(2) + 'ms');
  console.log('T99:              ' + t99.toFixed(2) + 'ms');
  console.log('SLA T95 < 200ms:  ' + (passed ? 'CUMPLE' : 'NO CUMPLE'));
  console.log('=========================================\n');

  return {};
}
