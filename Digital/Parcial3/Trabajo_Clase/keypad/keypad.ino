#include <Keypad.h>

byte LEDs[8]={6, 7, 8,9,10,11,12,13};

/* Keypad setup */
const byte KEYPAD_ROWS = 4;
const byte KEYPAD_COLS = 4;
byte rowPins[KEYPAD_ROWS] = {21, 20, 19, 18};  //entrdas
byte colPins[KEYPAD_COLS] = {17,16, 15, 14};  //salidas
char keys[KEYPAD_ROWS][KEYPAD_COLS] = {
  {1, 2, 3, 10},
  {4, 5, 6, 11},
  {7, 8, '9', 12},
  {13, '0', 14, 15}
};

Keypad keypad = Keypad(makeKeymap(keys), rowPins, colPins, KEYPAD_ROWS, KEYPAD_COLS);

void setup()
{
  for(byte i=0;i<8;i++) {
  pinMode(LEDs[i], OUTPUT);  }
}

void loop()
{
  // obtiene tecla presionada y asigna a variable
  char key = keypad.getKey();
  // comprueba que se haya presionado una tecla
  if (key) {
        if (key == '0') {
            key = 255;  }
      digitalWrite(LEDs[0], 1&key>>0);  // 
      digitalWrite(LEDs[1], 1&key>>1);  // 
      digitalWrite(LEDs[2], 1&key>>2);  // 
      digitalWrite(LEDs[3], 1&key>>3);  // 
      digitalWrite(LEDs[4], 1&key>>4);  // 
      digitalWrite(LEDs[5], 1&key>>5);  // 
      digitalWrite(LEDs[6], 1&key>>6);  // 
      digitalWrite(LEDs[7], 1&key>>7);  // 

  }
  
}