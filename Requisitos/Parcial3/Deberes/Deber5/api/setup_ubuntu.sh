#!/bin/bash
# ============================================================
# BELLA MUJER - Setup completo en Ubuntu
# API + Prometheus + Grafana + k6
# ============================================================

set -e

API_PORT=8080
GRAFANA_IP="http://localhost:3000"
PROMETHEUS_IP="http://localhost:9090"

echo "============================================"
echo "  BELLA MUJER - Instalacion completa"
echo "============================================"

# 1. Instalar Node.js si no existe
if ! command -v node &> /dev/null; then
    echo "[1/7] Instalando Node.js..."
    curl -fsSL https://deb.nodesource.com/setup_18.x | sudo -E bash -
    sudo apt-get install -y nodejs
else
    echo "[1/7] Node.js ya instalado: $(node --version)"
fi

# 2. Instalar k6 si no existe
if ! command -v k6 &> /dev/null; then
    echo "[2/7] Instalando k6..."
    sudo gpg -k
    sudo gpg --no-default-keyring --keyring /usr/share/keyrings/k6-archive-keyring.gpg --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys C5AD17C747E3415A3642D57D77C6C491D6AC1D68
    echo "deb [signed-by=/usr/share/keyrings/k6-archive-keyring.gpg] https://dl.k6.io/deb stable main" | sudo tee /etc/apt/sources.list.d/k6.list
    sudo apt-get update
    sudo apt-get install -y k6
else
    echo "[2/7] k6 ya instalado: $(k6 version)"
fi

# 3. Instalar Docker si no existe
if ! command -v docker &> /dev/null; then
    echo "[3/7] Instalando Docker..."
    sudo apt-get update
    sudo apt-get install -y ca-certificates curl gnupg
    sudo mkdir -m 0755 -p /etc/apt/keyrings
    curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo gpg --dearmor -o /etc/apt/keyrings/docker.gpg
    echo "deb [arch=$(dpkg --print-architecture) signed-by=/etc/apt/keyrings/docker.gpg] https://download.docker.com/linux/ubuntu $(lsb_release -cs) stable" | sudo tee /etc/apt/sources.list.d/docker.list > /dev/null
    sudo apt-get update
    sudo apt-get install -y docker-ce docker-ce-cli containerd.io docker-compose-plugin
    sudo usermod -aG docker $USER
    echo "CIERRA y ABRE de nuevo la terminal para que docker funcione sin sudo"
else
    echo "[3/7] Docker ya instalado"
fi

# 4. Preparar la API
echo "[4/7] Preparando API Bella Mujer..."
SCRIPT_DIR="$(cd "$(dirname "$0")" && pwd)"
cd "$SCRIPT_DIR"

if [ ! -d "node_modules" ]; then
    npm install
fi
echo "   API lista en puerto $API_PORT"

# 5. Detener contenedores anteriores
echo "[5/7] Deteniendo contenedores anteriores..."
docker compose down 2>/dev/null || true

# 6. Levantar Prometheus + Grafana
echo "[6/7] Levantando Prometheus + Grafana..."
docker compose up -d

echo ""
echo "============================================"
echo "  INSTALACION COMPLETADA"
echo "============================================"
echo ""
echo "  PARA INICIAR LA API (modo inicial - lento):"
echo "    cd $SCRIPT_DIR"
echo "    MODE=initial node server.js"
echo ""
echo "  PARA INICIAR LA API (modo optimizado - rapido):"
echo "    cd $SCRIPT_DIR"
echo "    MODE=optimized node server.js"
echo ""
echo "  PARA CORRER LA PRUEBA DE CARGA (500 VUs):"
echo "    k6 run $SCRIPT_DIR/k6-test.js"
echo ""
echo "  PARA CORRER PRUEBA RAPIDA (5 VUs, prueba):"
echo "    k6 run --vus 5 --duration 15s $SCRIPT_DIR/k6-test.js"
echo ""
echo "  VER METRICAS EN:"
echo "    Grafana:     $GRAFANA_IP  (admin / admin123)"
echo "    Prometheus:  $PROMETHEUS_IP"
echo "    API /metrics: http://localhost:$API_PORT/metrics"
echo ""
echo "  DESDE WINDOWS (abre en el navegador):"
echo "    Grafana:     http://192.168.100.41:3000"
echo "    Prometheus:  http://192.168.100.41:9090"
echo ""
