// ============================================================
// REFRIGERADORA MODERNA v2 - Simulacion Arduino Mega + SimulIDE
// ============================================================
// Pantalla idle: Hora simulada + Fecha + Temp + Setpoint + Modo
// Pantalla operacion: Detalles del compresor y alarma
// Potenciometro A1: Control de velocidad del reloj
// ============================================================

#include <LiquidCrystal.h>

// --- Pines LCD (RS, EN, D4, D5, D6, D7) ---
LiquidCrystal lcd(12, 11, 5, 4, 3, 2);

// --- Pines de entrada ---
const int PIN_LM35      = A0;
const int PIN_SPEED_POT = A1;
const int PIN_BTN_UP    = 30;
const int PIN_BTN_DOWN  = 31;
const int PIN_BTN_MODE  = 32;
const int PIN_DOOR_SW   = 33;

// --- Pines de salida ---
const int PIN_RELAY    = 8;
const int PIN_FAN      = 9;
const int PIN_BUZZER   = 10;
const int PIN_LED_ON   = 22;
const int PIN_LED_COOL = 24;
const int PIN_LED_DOOR = 26;

// --- Constantes del sistema ---
const float TEMP_MIN        = 1.0;
const float TEMP_MAX        = 8.0;
const float TEMP_HISTERESIS = 0.5;
const float UMBRALALARMA    = 5.0;
const int   TEMP_SETPOINT_INIT = 4;

// --- Caracteres personalizados LCD ---
byte termometro[8] = {
  0b00100, 0b01010, 0b01010, 0b01010,
  0b01110, 0b11111, 0b11111, 0b01110
};
byte termometroMedio[8] = {
  0b00100, 0b01010, 0b01010, 0b01010,
  0b01110, 0b01110, 0b11111, 0b01110
};
byte termometroBajo[8] = {
  0b00100, 0b01010, 0b01010, 0b01010,
  0b01010, 0b01110, 0b11111, 0b01110
};
byte gota[8] = {
  0b00100, 0b00100, 0b01010, 0b01010,
  0b10001, 0b10001, 0b10001, 0b01110
};
byte compresorIcon[8] = {
  0b11111, 0b10001, 0b10101, 0b10001,
  0b10101, 0b10001, 0b11111, 0b00000
};
byte puertaIcon[8] = {
  0b11111, 0b10001, 0b10011, 0b10001,
  0b10001, 0b10011, 0b10001, 0b11111
};
byte puertaAbiertaIcon[8] = {
  0b11111, 0b10000, 0b10010, 0b10000,
  0b10000, 0b10010, 0b10000, 0b11111
};
byte reloj[8] = {
  0b01110, 0b10001, 0b10101, 0b10111,
  0b10001, 0b01110, 0b00000, 0b00000
};

// --- Modos de operacion ---
enum ModoOp { MODO_NORMAL, MODO_ECO, MODO_VACACIONES };
ModoOp modoActual = MODO_NORMAL;
const char* nombreModo[] = {"Normal", "Eco    ", "Vacacion"};

// --- Pantallas ---
enum Pantalla { PANT_HOME, PANT_INFO, PANT_MODO };
Pantalla pantallaActual = PANT_HOME;

// --- Variables globales ---
float tempActual   = 0.0;
int   tempSetpoint = TEMP_SETPOINT_INIT;
bool  compresorOn  = false;
bool  puertaAbierta = false;
bool  alarmaActiva  = false;

// --- Reloj simulado ---
int relojHoras   = 12;
int relojMinutos = 0;
int relojSegundos = 0;
int relojDia     = 22;
int relojMes     = 7;
int relojAnio    = 2026;
float velocidadReloj = 1.0;
unsigned long ultimoTickReloj = 0;
unsigned long intervaloReloj  = 1000;

// --- Tiempos ---
unsigned long tiempoPuertaAbierta = 0;
unsigned long tiempoLcd          = 0;
unsigned long tiempoBuzzer       = 0;
unsigned long tiempoBtnMode      = 0;
bool          buzzerState        = false;

// --- Botones ---
bool estadoBtnUp    = HIGH;
bool estadoBtnDown  = HIGH;
bool estadoBtnMode  = HIGH;
unsigned long ultimoCambioBtnUp   = 0;
unsigned long ultimoCambioBtnDown = 0;
unsigned long ultimoCambioBtnMode = 0;
const unsigned long DEBOUNCE_DELAY = 200;

// ============================================================
// SETUP
// ============================================================
void setup() {
  lcd.begin(16, 2);

  // Crear caracteres personalizados
  lcd.createChar(0, termometro);
  lcd.createChar(1, gota);
  lcd.createChar(2, compresorIcon);
  lcd.createChar(3, puertaIcon);
  lcd.createChar(4, puertaAbiertaIcon);
  lcd.createChar(5, reloj);
  lcd.createChar(6, termometroMedio);
  lcd.createChar(7, termometroBajo);

  pinMode(PIN_BTN_UP,    INPUT_PULLUP);
  pinMode(PIN_BTN_DOWN,  INPUT_PULLUP);
  pinMode(PIN_BTN_MODE,  INPUT_PULLUP);
  pinMode(PIN_DOOR_SW,   INPUT_PULLUP);

  pinMode(PIN_RELAY,    OUTPUT);
  pinMode(PIN_FAN,      OUTPUT);
  pinMode(PIN_BUZZER,   OUTPUT);
  pinMode(PIN_LED_ON,   OUTPUT);
  pinMode(PIN_LED_COOL, OUTPUT);
  pinMode(PIN_LED_DOOR, OUTPUT);

  digitalWrite(PIN_RELAY,    LOW);
  digitalWrite(PIN_FAN,      LOW);
  digitalWrite(PIN_BUZZER,   LOW);
  digitalWrite(PIN_LED_ON,   HIGH);
  digitalWrite(PIN_LED_COOL, LOW);
  digitalWrite(PIN_LED_DOOR, LOW);

  lcd.setCursor(0, 0);
  lcd.print(" REFRIGERADORA  ");
  lcd.setCursor(0, 1);
  lcd.print("   MODERNA v2   ");
  delay(2000);
  lcd.clear();
}

// ============================================================
// LOOP PRINCIPAL
// ============================================================
void loop() {
  leerTemperatura();
  leerBotones();
  leerPuerta();
  controlCompresor();
  controlAlarma();
  actualizarSalidas();
  actualizarReloj();
  actualizarLCD();
}

// ============================================================
// LECTURA DE TEMPERATURA (LM35)
// ============================================================
void leerTemperatura() {
  int lectura = analogRead(PIN_LM35);
  float voltaje = lectura * (5.0 / 1023.0);
  tempActual = voltaje * 100.0;
}

// ============================================================
// VELOCIDAD DEL RELOJ (potenciometro A1)
// ============================================================
void actualizarVelocidadReloj() {
  int lectura = analogRead(PIN_SPEED_POT);
  // 0 => x0.1, 512 => x1, 1023 => x100
  if (lectura < 50) {
    velocidadReloj = 0.0;
  } else if (lectura < 512) {
    velocidadReloj = 0.1 + (lectura / 512.0) * 0.9;
  } else {
    velocidadReloj = 1.0 + ((lectura - 512.0) / 512.0) * 99.0;
  }
  intervaloReloj = (unsigned long)(1000.0 / velocidadReloj);
  if (intervaloReloj < 10) intervaloReloj = 10;
}

// ============================================================
// RELOJ SIMULADO
// ============================================================
void actualizarReloj() {
  actualizarVelocidadReloj();
  unsigned long ahora = millis();

  if (velocidadReloj <= 0.0) return;

  if (ahora - ultimoTickReloj >= intervaloReloj) {
    ultimoTickReloj = ahora;
    relojSegundos++;
    if (relojSegundos >= 60) {
      relojSegundos = 0;
      relojMinutos++;
    }
    if (relojMinutos >= 60) {
      relojMinutos = 0;
      relojHoras++;
    }
    if (relojHoras >= 24) {
      relojHoras = 0;
      relojDia++;
    }
    // Simplificacion: meses de 30 dias
    int diasMes[] = {31,28,31,30,31,30,31,31,30,31,30,31};
    if (relojDia > diasMes[relojMes - 1]) {
      relojDia = 1;
      relojMes++;
    }
    if (relojMes > 12) {
      relojMes = 1;
      relojAnio++;
    }
  }
}

// ============================================================
// LECTURA DE BOTONES
// ============================================================
void leerBotones() {
  unsigned long ahora = millis();

  // --- BOTON UP ---
  if (digitalRead(PIN_BTN_UP) == LOW && estadoBtnUp == HIGH) {
    if (ahora - ultimoCambioBtnUp > DEBOUNCE_DELAY) {
      accionBotonUp();
      ultimoCambioBtnUp = ahora;
    }
  }
  estadoBtnUp = digitalRead(PIN_BTN_UP);

  // --- BOTON DOWN ---
  if (digitalRead(PIN_BTN_DOWN) == LOW && estadoBtnDown == HIGH) {
    if (ahora - ultimoCambioBtnDown > DEBOUNCE_DELAY) {
      accionBotonDown();
      ultimoCambioBtnDown = ahora;
    }
  }
  estadoBtnDown = digitalRead(PIN_BTN_DOWN);

  // --- BOTON MODE ---
  if (digitalRead(PIN_BTN_MODE) == LOW && estadoBtnMode == HIGH) {
    if (ahora - ultimoCambioBtnMode > DEBOUNCE_DELAY) {
      accionBotonMode();
      ultimoCambioBtnMode = ahora;
    }
  }
  estadoBtnMode = digitalRead(PIN_BTN_MODE);
}

// ============================================================
// ACCIONES DE BOTONES SEGUN PANTALLA
// ============================================================
void accionBotonUp() {
  switch (pantallaActual) {
    case PANT_HOME:
      tempSetpoint++;
      if (tempSetpoint > TEMP_MAX) tempSetpoint = TEMP_MAX;
      break;
    case PANT_INFO:
      break;
    case PANT_MODO:
      modoActual = (ModoOp)((modoActual + 1) % 3);
      break;
  }
}

void accionBotonDown() {
  switch (pantallaActual) {
    case PANT_HOME:
      tempSetpoint--;
      if (tempSetpoint < TEMP_MIN) tempSetpoint = TEMP_MIN;
      break;
    case PANT_INFO:
      break;
    case PANT_MODO:
      modoActual = (ModoOp)((modoActual + 2) % 3);
      break;
  }
}

void accionBotonMode() {
  pantallaActual = (Pantalla)((pantallaActual + 1) % 3);
  lcd.clear();
}

// ============================================================
// LECTURA DE PUERTA
// ============================================================
void leerPuerta() {
  unsigned long ahora = millis();
  bool puertaAnterior = puertaAbierta;

  puertaAbierta = (digitalRead(PIN_DOOR_SW) == LOW);

  if (puertaAbierta && !puertaAnterior) {
    tiempoPuertaAbierta = ahora;
    alarmaActiva = false;
    tiempoBuzzer = ahora;
  }

  if (!puertaAbierta) {
    alarmaActiva = false;
    digitalWrite(PIN_BUZZER, LOW);
    buzzerState = false;
  }
}

// ============================================================
// CONTROL DEL COMPRESOR (con histeresis y modos)
// ============================================================
void controlCompresor() {
  if (puertaAbierta) {
    compresorOn = false;
    return;
  }

  float histeresis = TEMP_HISTERESIS;
  if (modoActual == MODO_ECO)       histeresis = 1.0;
  if (modoActual == MODO_VACACIONES) histeresis = 2.0;

  if (tempActual >= (tempSetpoint + histeresis)) {
    compresorOn = true;
  } else if (tempActual <= (tempSetpoint - histeresis)) {
    compresorOn = false;
  }
}

// ============================================================
// CONTROL DE ALARMA
// ============================================================
void controlAlarma() {
  unsigned long ahora = millis();

  if (puertaAbierta && !alarmaActiva) {
    if ((ahora - tiempoPuertaAbierta) >= (unsigned long)(UMBRALALARMA * 1000)) {
      alarmaActiva = true;
      tiempoBuzzer = ahora;
    }
  }

  if (alarmaActiva) {
    if (ahora - tiempoBuzzer >= 300) {
      buzzerState = !buzzerState;
      digitalWrite(PIN_BUZZER, buzzerState ? HIGH : LOW);
      tiempoBuzzer = ahora;
    }
  }
}

// ============================================================
// ACTUALIZAR SALIDAS
// ============================================================
void actualizarSalidas() {
  digitalWrite(PIN_RELAY,    compresorOn ? HIGH : LOW);
  digitalWrite(PIN_FAN,      compresorOn ? HIGH : LOW);
  digitalWrite(PIN_LED_COOL, compresorOn ? HIGH : LOW);
  digitalWrite(PIN_LED_DOOR, puertaAbierta ? HIGH : LOW);
}

// ============================================================
// ACTUALIZAR LCD - SEGUN PANTALLA ACTUAL
// ============================================================
void actualizarLCD() {
  unsigned long ahora = millis();
  if (ahora - tiempoLcd < 400) return;
  tiempoLcd = ahora;

  switch (pantallaActual) {
    case PANT_HOME: mostrarPantallaHome();   break;
    case PANT_INFO: mostrarPantallaInfo();   break;
    case PANT_MODO: mostrarPantallaModo();   break;
  }
}

// ============================================================
// PANTALLA 1: HOME (idle) - Hora + Fecha + Temp + Setpoint
// ============================================================
void mostrarPantallaHome() {
  // --- Fila 1: Hora + Velocidad ---
  lcd.setCursor(0, 0);
  lcd.write(byte(5));
  lcd.print(" ");
  if (relojHoras < 10)   lcd.print("0");
  lcd.print(relojHoras);
  lcd.print(":");
  if (relojMinutos < 10) lcd.print("0");
  lcd.print(relojMinutos);
  lcd.print("  ");

  // Velocidad del reloj
  if (velocidadReloj < 1.0) {
    lcd.print("x0");
    lcd.print((int)(velocidadReloj * 10));
  } else if (velocidadReloj >= 10.0) {
    lcd.print("x");
    lcd.print((int)velocidadReloj);
  } else {
    lcd.print("x");
    lcd.print(velocidadReloj, 1);
  }
  lcd.print("  ");

  // --- Fila 2: Temp + Setpoint + Modo ---
  lcd.setCursor(0, 1);
  lcd.write(byte(0));
  lcd.print(tempActual, 1);
  lcd.print("C ");

  lcd.write(byte(1));
  lcd.print(tempSetpoint);
  lcd.print("C  ");

  lcd.print(nombreModo[modoActual]);
}

// ============================================================
// PANTALLA 2: INFO - Detalles del sistema
// ============================================================
void mostrarPantallaInfo() {
  // --- Fila 1: Compresor + Puerta ---
  lcd.setCursor(0, 0);
  lcd.write(byte(2));
  lcd.print(" ");
  lcd.print(compresorOn ? "ON " : "OFF");
  lcd.print("   ");

  lcd.write(byte(puertaAbierta ? 4 : 3));
  lcd.print(" ");
  lcd.print(puertaAbierta ? "ABIERTA  " : "CERRADA  ");

  // --- Fila 2: Fecha ---
  lcd.setCursor(0, 1);
  lcd.print("   ");
  if (relojDia < 10)    lcd.print("0");
  lcd.print(relojDia);
  lcd.print("/");
  if (relojMes < 10)    lcd.print("0");
  lcd.print(relojMes);
  lcd.print("/");
  lcd.print(relojAnio);
  lcd.print("   ");

  // Indicador de alarma
  if (alarmaActiva) {
    lcd.print("!");
  }
}

// ============================================================
// PANTALLA 3: MODO - Seleccion de modo de operacion
// ============================================================
void mostrarPantallaModo() {
  // --- Fila 1: Titulo ---
  lcd.setCursor(0, 0);
  lcd.print(">> MODO:        ");

  // --- Fila 2: Modo seleccionado con indicador ---
  lcd.setCursor(0, 1);
  lcd.print("  ");
  for (int i = 0; i < 3; i++) {
    if (i == (int)modoActual) {
      lcd.write(byte(0));
    } else {
      lcd.print(" ");
    }
    lcd.print(nombreModo[i]);
    lcd.print(" ");
  }
  lcd.print("  ");
}
