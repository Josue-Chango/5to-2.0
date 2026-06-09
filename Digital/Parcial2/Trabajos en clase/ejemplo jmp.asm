
; You may customize this and other start-up templates; 
; The location of this template is c:\emu8086\inc\0_com_template.txt

org 100h

mov bl,N
ciclo:
    MOV DL,BL   
    add dl,48
    mov ah,2
    int 21h
    dec bl
    jmp ciclo

ret
N db 10



