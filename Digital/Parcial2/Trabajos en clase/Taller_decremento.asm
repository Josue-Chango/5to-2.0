org 100h

; Preguntar 1 o 2

lea dx, pregunta
mov ah, 9
int 21h

mov dl, 10
mov ah, 2
int 21h
mov dl, 13
mov ah, 2
int 21h


; Leer opcion

mov ah, 01h
int 21h

cmp al, '1'
je  dec_uno
mov paso, 2
jmp inicio

dec_uno:
mov paso, 1


inicio:
    mov dl, 10
    mov ah, 2
    int 21h
    mov dl, 13
    mov ah, 2
    int 21h
    mov M, 9
ciclo2:
    mov N, 9
ciclo1:
    
    ; imprimir M (decena)
    mov dl, M
    add dl, 48
    mov ah, 2
    int 21h

    ; imprimir N (unidad)
    mov dl, N
    add dl, 48
    mov ah, 2
    int 21h

    ; salto de linea
    mov dl, 10
    mov ah, 2
    int 21h
    mov dl, 13
    mov ah, 2
    int 21h

    ; decrementar N segun paso
    mov al, N
    sub al, paso
    mov N, al
    jns ciclo1

    ; decrementar M segun paso
    mov al, M
    sub al, 1
    mov M, al
    jns ciclo2
    jnz inicio

ret
M    db 9
N    db 9
paso db 1
pregunta db "Cuanto quieres que decremente en 1 o en 2? $"