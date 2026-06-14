
; You may customize this and other start-up templates; 
; The location of this template is c:\emu8086\inc\0_com_template.txt

org 100h

;Ordenar de 'a' a 'z' 4 letras 


mov al, L1
sub al, L2
jc ciclo1
jz ciclo1

mov al, L1
mov bl, L2
mov L1, bl
mov L2, al

mov al, P1
mov bl, P2
mov P1, bl
mov P2, al

ciclo1:

    mov al, L2
    sub al, L3
    jc ciclo2
    jz ciclo2

    mov al, L2
    mov bl, L3
    mov L2, bl
    mov L3, al

    mov al, P2
    mov bl, P3
    mov P2, bl
    mov P3, al

ciclo2:

    mov al, L3
    sub al, L4
    jc ciclo3
    jz ciclo3

    mov al, L3
    mov bl, L4
    mov L3, bl
    mov L4, al

    mov al, P3
    mov bl, P4
    mov P3, bl
    mov P4, al

ciclo3:

    mov al, L1
    sub al, L2
    jc ciclo4
    jz ciclo4

    mov al, L1
    mov bl, L2
    mov L1, bl
    mov L2, al

    mov al, P1
    mov bl, P2
    mov P1, bl
    mov P2, al

ciclo4:

    mov al, L2
    sub al, L3
    jc ciclo5
    jz ciclo5

    mov al, L2
    mov bl, L3
    mov L2, bl
    mov L3, al

    mov al, P2
    mov bl, P3
    mov P2, bl
    mov P3, al

ciclo5:

    mov al, L1
    sub al, L2
    jc mostrar
    jz mostrar

    mov al, L1
    mov bl, L2
    mov L1, bl
    mov L2, al

    mov al, P1
    mov bl, P2
    mov P1, bl
    mov P2, al

mostrar:
      
    lea dx, msg
    mov ah, 9
    int 21h
    mov dl, P1
    add dl, 48
    mov ah, 2
    int 21h
    mov dl, 32
    mov ah, 2
    int 21h
    mov dl, L1
    mov ah, 2
    int 21h
    mov dl, 10
    mov ah, 2
    int 21h
    mov dl, 13
    mov ah, 2
    int 21h
    
    lea dx, msg
    mov ah, 9
    int 21h
    mov dl, P2
    add dl, 48
    mov ah, 2
    int 21h
    mov dl, 32
    int 21h
    mov dl, L2
    int 21h
    mov dl, 10
    int 21h
    mov dl, 13
    int 21h
    
    lea dx, msg
    mov ah, 9
    int 21h
    mov dl, P3
    add dl, 48
    mov ah, 2
    int 21h
    mov dl, 32
    int 21h
    mov dl, L3
    int 21h
    mov dl, 10
    int 21h
    mov dl, 13
    int 21h
    
    lea dx, msg
    mov ah, 9
    int 21h
    mov dl, P4
    add dl, 48
    mov ah, 2
    int 21h
    mov dl, 32
    int 21h
    mov dl, L4
    int 21h

ret

L1 db 'f'
L2 db 'l'
L3 db 'h'
L4 db 'o'

P1 db 1
P2 db 2
P3 db 3
P4 db 4 

msg db 'Letra $'



