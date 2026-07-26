int led = 3;
int brightness = 0;
void setup() {
// put your setup code here, to run once:
pinMode(led, OUTPUT);
}
// the PWM pin the LED is attached to

void loop() {
// put your main code here, to run repeatedly:
if (brightness >= 255) {
brightness=0;
}
brightness++;
analogWrite(led, brightness);
delay(50);
}