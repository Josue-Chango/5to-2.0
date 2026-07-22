#define LED_A 10
#define PIN_A 0

void setup() {
  pinMode(LED_A, OUTPUT);
  pinMode(11, OUTPUT);
  pinMode(12, OUTPUT);
  pinMode(13, OUTPUT);
  pinMode(PIN_A, INPUT);
  pinMode(1, INPUT);
  pinMode(2,INPUT);
  pinMode(3, INPUT);
}

void loop() {
// put your main code here, to run repeatedly:
  int boton1 =digitalRead(PIN_A);
  int boton2=digitalRead(1);
  int boton3 =digitalRead(2);
  int boton4 =digitalRead(3);
  digitalWrite(LED_A, boton1);
  digitalWrite(11, boton2);
  digitalWrite(12, boton3);
  digitalWrite(13, boton4);
}