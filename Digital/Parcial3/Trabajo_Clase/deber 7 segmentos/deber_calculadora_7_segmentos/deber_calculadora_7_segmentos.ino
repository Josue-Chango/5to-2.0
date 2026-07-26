#include <Keypad.h>
#include "SevSeg.h"

SevSeg sevseg;

const byte FILAS = 4;
const byte COLS = 4;
char teclas[FILAS][COLS] = {
  {'1', '2', '3', '+'},
  {'4', '5', '6', '-'},
  {'7', '8', '9', '*'},
  {'/', '0', '=', 'C'}
};
byte filasPins[FILAS] = {21, 20, 19, 18};
byte colsPins[COLS] = {17, 16, 15, 14};
Keypad keypad = Keypad(makeKeymap(teclas), filasPins, colsPins, FILAS, COLS);

long num1 = 0;
long num2 = 0;
long resultado = 0;
char operacion = ' ';
bool esperandoNum2 = false;

void setup() {
  byte numDigits = 4;
  byte digitPins[] = {10, 11, 12, 13};
  byte segmentPins[] = {2, 3, 4, 5, 6, 7, 8, 9};

  sevseg.begin(COMMON_ANODE, numDigits, digitPins, segmentPins,
    false, false, false, false);
  sevseg.setBrightness(90);
}

void loop() {
  char tecla = keypad.getKey();

  if (tecla) {
    if (tecla >= '0' && tecla <= '9') {
      int digito = tecla - '0';
      if (!esperandoNum2) {
        num1 = num1 * 10 + digito;
        sevseg.setNumber(num1);
      } else {
        num2 = num2 * 10 + digito;
        sevseg.setNumber(num2);
      }
    }
    else if (tecla == '+' || tecla == '-' || tecla == '*' || tecla == '/') {
      operacion = tecla;
      esperandoNum2 = true;
    }
    else if (tecla == '=') {
      switch (operacion) {
        case '+': resultado = num1 + num2; break;
        case '-': resultado = num1 - num2; break;
        case '*': resultado = num1 * num2; break;
        case '/':
          if (num2 != 0) resultado = num1 / num2;
          else resultado = 0;
          break;
      }
      sevseg.setNumber(resultado);
      num1 = resultado;
      num2 = 0;
      operacion = ' ';
      esperandoNum2 = false;
    }
    else if (tecla == 'C') {
      num1 = 0;
      num2 = 0;
      resultado = 0;
      operacion = ' ';
      esperandoNum2 = false;
      sevseg.setNumber(0);
    }
  }

  sevseg.refreshDisplay();
}
