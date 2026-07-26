import http from 'k6/http';
import { check } from 'k6';

export const options = {
  vus: 10,
  duration: '10s',
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

  console.log(`status=${res.status}, body=${res.body.substring(0, 100)}`);

  check(res, {
    'status es 201': (r) => r.status === 201,
  });
}
