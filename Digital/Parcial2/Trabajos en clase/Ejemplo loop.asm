
; You may customize this and other start-up templates; 
; The location of this template is c:\emu8086\inc\0_com_template.txt

org 100h

mov cl,20
inicio:
    mov bl,N
ciclo:
    dec bl
    jnz ciclo ;salta a la etiqueta indicada
    loop inicio  ;jmp hace lo mismo pero no tiene control, puede correr indefinidamente

ret
N db 10



