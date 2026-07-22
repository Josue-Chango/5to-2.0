#include <SevSeg.h>

SevSeg sevseg;

// ========================
// PARAMETRO DE VELOCIDAD
// ========================
// 1   = velocidad normal
// 10  = 10x mas rapido
// 60  = 1 minuto real = 1 hora del reloj
// 3600 = 1 segundo real = 1 hora del reloj
const unsigned long SPEED_FACTOR = 60;

void setup() {
  byte numDigits = 4;
  byte digitPins[] = {10, 11, 12, 13};
  byte segmentPins[] = {2, 3, 4, 5, 6, 7, 8, 9};

  sevseg.begin(COMMON_ANODE, numDigits, digitPins, segmentPins,
               false, false, false, false);
  sevseg.setBrightness(90);
}

void loop() {
  static unsigned long lastUpdate = 0;
  static int seconds = 0;
  static int minutes = 0;
  static int hours = 0;

  unsigned long now = millis();
  unsigned long interval = 1000 / SPEED_FACTOR;
  if (interval < 1) interval = 1;

  if (now - lastUpdate >= interval) {
    lastUpdate += interval;
    seconds++;

    if (seconds >= 60) {
      seconds = 0;
      minutes++;
    }
    if (minutes >= 60) {
      minutes = 0;
      hours++;
    }
    if (hours >= 24) {
      hours = 0;
    }
  }

  int displayTime = hours * 100 + minutes;
  sevseg.setNumber(displayTime);

  // SIEMPRE refrescar el display lo mas rapido posible
  sevseg.refreshDisplay();
}
