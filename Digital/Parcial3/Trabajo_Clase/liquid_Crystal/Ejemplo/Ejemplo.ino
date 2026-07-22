#include <LiquidCrystal.h>

#define LCD_RS 21
#define LCD_EN 20
#define LCD_D4 19
#define LCD_D5 18
#define LCD_D6 17
#define LCD_D7 16

LiquidCrystal lcd(LCD_RS, LCD_EN, LCD_D4, LCD_D5, LCD_D6, LCD_D7);

void setup()
{
    pinMode(A0, INPUT);
    lcd.begin(16, 4);
    lcd.clear();
    lcd.noBlink();
    lcd.cursor();
    
    // Imprimimos el título estático una sola vez en el setup para evitar parpadeos
    lcd.setCursor(0, 0);
    lcd.print("LCD");
    
    lcd.setCursor(2, 1);
    lcd.print("EJEMPLO: ");
    delay(1000);
}

void loop()
{
    // 1. Leemos el valor real del pin A0
    int valor = analogRead(A0);
    
    // 2. Nos ubicamos justo después del texto "EJEMPLO: " para actualizar solo el número
    lcd.setCursor(11, 1); 
    
    // 3. Imprimimos espacios en blanco para borrar el número anterior por si cambia de dígitos
    lcd.print("    "); 
    
    // 4. Imprimimos el nuevo valor
    lcd.setCursor(11, 1);
    lcd.print(valor);
    
    delay(200); // Un retraso más corto para que responda rápido a los cambios
}