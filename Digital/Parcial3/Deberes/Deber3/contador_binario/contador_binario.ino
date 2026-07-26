//////////////////////////////////////CONTADOR BASICO BINARIO
/*
byte puerto[8]={0,1,2,3,4,5,6,7};
// the setup function runs once when you press reset or power the board
void setup() {
  // initialize digital pin LED_BUILTIN as an output.
  for(byte i=0;i<8;i++) {
    pinMode(puerto[i], OUTPUT);
  }
}

void mostrarBinario(byte numero) {

    digitalWrite(puerto[0], 1&numero>>0);
    digitalWrite(puerto[1], 1&numero>>1);
    digitalWrite(puerto[2], 1&numero>>2);
    digitalWrite(puerto[3], 1&numero>>3);
    digitalWrite(puerto[4], 1&numero>>4);
    digitalWrite(puerto[5], 1&numero>>5);
    digitalWrite(puerto[6], 1&numero>>6);
    digitalWrite(puerto[7], 1&numero>>7);
}
// the loop function runs over and over again forever
void loop() {
  mostrarBinario(5);

}*/


byte puerto[8] = {0,1,2,3,4,5,6,7};

byte digitos[16][8] = {
  {1,1,1,1,1,1,0,0}, 
  {0,1,1,0,0,0,0,0}, 
  {1,1,0,1,1,0,1,0}, 
  {1,1,1,1,0,0,1,0},
  {0,1,1,0,0,1,1,0}, 
  {1,0,1,1,0,1,1,0}, 
  {1,0,1,1,1,1,1,0}, 
  {1,1,1,0,0,0,0,0}, 
  {1,1,1,1,1,1,1,0}, 
  {1,1,1,1,0,1,1,0}, 
  {1,1,1,0,1,1,1,0}, 
  {0,0,1,1,1,1,1,0}, 
  {1,0,0,1,1,1,0,0}, 
  {0,1,1,1,1,0,1,0}, 
  {1,0,0,1,1,1,1,0},
  {1,0,0,0,1,1,1,0}  
};

void setup() {
  for(byte i=0;i<7;i++) {
    pinMode(puerto[i], OUTPUT);
  }
}

void mostrarDigito(byte numero) {
  for(byte i=0; i<7; i++){
    digitalWrite(puerto[i], digitos[numero][i]);
  }
}

void loop() {
  for(byte i=0; i<16; i++){
    mostrarDigito(i);
    delay(1000);
  }
}
