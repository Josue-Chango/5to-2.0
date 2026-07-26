#!/bin/bash
# ============================================================
# BELLA MUJER - Ejecutar pruebas de carga comparativas
# Compara modo INITIAL vs OPTIMIZED
# ============================================================

set -e

SCRIPT_DIR="$(cd "$(dirname "$0")" && pwd)"
RESULTS_DIR="$SCRIPT_DIR/resultados"
mkdir -p "$RESULTS_DIR"

echo "============================================"
echo "  PRUEBA 1/2: MODO INICIAL (sin optimizar)"
echo "============================================"

# Matar API anterior
pkill -f "node server.js" 2>/dev/null || true
sleep 2

# Iniciar en modo initial
MODE=initial node "$SCRIPT_DIR/server.js" &
API_PID=$!
sleep 3

echo "Ejecutando k6 con 500 VUs (60 segundos)..."
k6 run \
  --summary-export="$RESULTS_DIR/initial.json" \
  "$SCRIPT_DIR/k6-test.js" 2>&1 | tee "$RESULTS_DIR/initial_log.txt"

echo ""
echo "Resultados iniciales guardados en: $RESULTS_DIR/initial.json"
echo ""

# Detener API
kill $API_PID 2>/dev/null || true
sleep 3

echo "============================================"
echo "  PRUEBA 2/2: MODO OPTIMIZADO"
echo "============================================"

# Iniciar en modo optimized
MODE=optimized node "$SCRIPT_DIR/server.js" &
API_PID=$!
sleep 3

echo "Ejecutando k6 con 500 VUs (60 segundos)..."
k6 run \
  --summary-export="$RESULTS_DIR/optimized.json" \
  "$SCRIPT_DIR/k6-test.js" 2>&1 | tee "$RESULTS_DIR/optimized_log.txt"

echo ""
echo "Resultados optimizados guardados en: $RESULTS_DIR/optimized.json"

# Detener API
kill $API_PID 2>/dev/null || true

echo ""
echo "============================================"
echo "  COMPARACION DE RESULTADOS"
echo "============================================"

if [ -f "$RESULTS_DIR/initial.json" ] && [ -f "$RESULTS_DIR/optimized.json" ]; then
    echo ""
    echo "  MODO INITIAL:"
    python3 -c "
import json
with open('$RESULTS_DIR/initial.json') as f:
    d = json.load(f)
    print(f'    Peticiones totales: {d[\"metrics\"][\"http_reqs\"][\"values\"][\"count\"]}')
    print(f'    Tasa de error:      {d[\"metrics\"][\"http_req_failed\"][\"values\"][\"rate\"]*100:.2f}%')
    print(f'    Tiempo promedio:    {d[\"metrics\"][\"http_req_duration\"][\"values\"][\"avg\"]:.2f}ms')
    print(f'    T95:                {d[\"metrics\"][\"http_req_duration\"][\"values\"][\"p(95)\"]:.2f}ms')
    print(f'    T99:                {d[\"metrics\"][\"http_req_duration\"][\"values\"][\"p(99)\"]:.2f}ms')
    print(f'    SLA T95<200ms:      {\"CUMPLE\" if d[\"metrics\"][\"http_req_duration\"][\"values\"][\"p(95)\"] < 200 else \"NO CUMPLE\"}')
" 2>/dev/null || echo "    (instala python3 para ver la comparacion)"

    echo ""
    echo "  MODO OPTIMIZADO:"
    python3 -c "
import json
with open('$RESULTS_DIR/optimized.json') as f:
    d = json.load(f)
    print(f'    Peticiones totales: {d[\"metrics\"][\"http_reqs\"][\"values\"][\"count\"]}')
    print(f'    Tasa de error:      {d[\"metrics\"][\"http_req_failed\"][\"values\"][\"rate\"]*100:.2f}%')
    print(f'    Tiempo promedio:    {d[\"metrics\"][\"http_req_duration\"][\"values\"][\"avg\"]:.2f}ms')
    print(f'    T95:                {d[\"metrics\"][\"http_req_duration\"][\"values\"][\"p(95)\"]:.2f}ms')
    print(f'    T99:                {d[\"metrics\"][\"http_req_duration\"][\"values\"][\"p(99)\"]:.2f}ms')
    print(f'    SLA T95<200ms:      {\"CUMPLE\" if d[\"metrics\"][\"http_req_duration\"][\"values\"][\"p(95)\"] < 200 else \"NO CUMPLE\"}')
" 2>/dev/null || echo "    (instala python3 para ver la comparacion)"
fi

echo ""
echo "============================================"
echo "  PRUEBAS COMPLETADAS"
echo "============================================"
echo ""
echo "  Revisa los resultados en: $RESULTS_DIR/"
echo "  Abre Grafana en Windows: http://192.168.100.41:3000"
echo ""
