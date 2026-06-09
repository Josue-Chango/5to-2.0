
; You may customize this and other start-up templates; 
; The location of this template is c:\emu8086\inc\0_com_template.txt

org 100h
inicio:
mov bl,N
ciclo:
    mov dl,bl   
    add dl,'0'
    mov ah,2
    int 21h
    dec bl
    jnz ciclo
    mov dl, 13
    mov ah, 2
    int 21h
    mov dl, 10
    mov ah, 2 
    int 21h
    jmp inicio

ret
N db 10



