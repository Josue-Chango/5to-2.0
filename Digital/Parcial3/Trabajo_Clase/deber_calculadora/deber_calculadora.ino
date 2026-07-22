#include <Keypad.h>
#include <LiquidCrystal.h>

LiquidCrystal lcd(7, 8, 9, 10, 11, 12);

const byte FILAS = 5;
const byte COLS = 4;
char teclas[FILAS][COLS] = {
  {'1', '2', '3', '+'},
  {'4', '5', '6', '-'},
  {'7', '8', '9', '*'},
  {'.', '0', '=', '/'},
  {'C',  0,   0,   0 }
};
byte filasPins[FILAS] = {22, 23, 24, 25, 26};
byte colsPins[COLS] = {27, 28, 29, 30};
Keypad keypad = Keypad(makeKeymap(teclas), filasPins, colsPins, FILAS, COLS);

float num1 = 0;
float num2 = 0;
float resultado = 0;
char operacion = ' ';
bool esperandoNum2 = false;
bool decimales = false;
float factor = 0.1;

void setup() {
  lcd.begin(16, 2);
  lcd.setCursor(0, 0);
  lcd.print("Calculadora");
  lcd.setCursor(0, 1);
  lcd.print("Listo...");
  delay(1500);
  lcd.clear();
  mostrarPantalla();
}

void mostrarPantalla() {
  lcd.clear();
  lcd.setCursor(0, 0);
  if (operacion != ' ' && !esperandoNum2) {
    lcd.print(num1);
    lcd.print(operacion);
  } else if (esperandoNum2) {
    lcd.print(num1);
    lcd.print(operacion);
    lcd.print(num2);
  } else {
    lcd.print(num1);
  }
  lcd.setCursor(0, 1);
  lcd.print("=");
  lcd.setCursor(2, 1);
  lcd.print(resultado);
}

void loop() {
  char tecla = keypad.getKey();

  if (tecla) {
    if (tecla >= '0' && tecla <= '9') {
      int digito = tecla - '0';
      if (!esperandoNum2) {
        if (!decimales) {
          num1 = num1 * 10 + digito;
        } else {
          num1 = num1 + digito * factor;
          factor = factor / 10;
        }
      } else {
        if (!decimales) {
          num2 = num2 * 10 + digito;
        } else {
          num2 = num2 + digito * factor;
          factor = factor / 10;
        }
      }
      mostrarPantalla();
    }
    else if (tecla == '.') {
      decimales = true;
      factor = 0.1;
    }
    else if (tecla == '+' || tecla == '-' || tecla == '*' || tecla == '/') {
      operacion = tecla;
      esperandoNum2 = true;
      decimales = false;
      factor = 0.1;
      mostrarPantalla();
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
      mostrarPantalla();
      num1 = resultado;
      num2 = 0;
      operacion = ' ';
      esperandoNum2 = false;
      decimales = false;
      factor = 0.1;
    }
    else if (tecla == 'C') {
      num1 = 0;
      num2 = 0;
      resultado = 0;
      operacion = ' ';
      esperandoNum2 = false;
      decimales = false;
      factor = 0.1;
      mostrarPantalla();
    }
  }
}
