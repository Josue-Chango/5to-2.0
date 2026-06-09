
; You may customize this and other start-up templates; 
; The location of this template is c:\emu8086\inc\0_com_template.txt

org 100h

inicio:

    mov M, 9

ciclo2:

    mov N, 9

ciclo1:

    mov dl, M
    add dl, 48
    mov ah, 2
    int 21h

    mov dl, N
    add dl, 48 
    mov ah, 2
    int 21h
    
    mov dl, 10 
    mov ah, 2
    int 21h

    mov dl, 13 
    mov ah, 2
    int 21h      

    dec N
    jns ciclo1      

    dec M
    jns ciclo2
    
    jnz inicio      

ret

M db 9
N db 9



