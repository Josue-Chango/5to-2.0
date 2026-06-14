org 100h

; Pedir numero
mov dx, offset mensaje
mov ah, 09h
int 21h
mov ah, 01h
int 21h
sub al, 48
mov ch, al          ; guarda decena en CH
mov ah, 01h
int 21h
sub al, 48
mov cl, al          ; guarda unidad en CL

; Salto de linea
mov dl, 10
mov ah, 2
int 21h
mov dl, 13
mov ah, 2
int 21h

; Preguntar 1 o 2
lea dx, pregunta
mov ah, 9
int 21h

; Salto de linea
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
; Salto de linea
mov dl, 10
mov ah, 2
int 21h
mov dl, 13
mov ah, 2
int 21h
    mov M, ch       
    mov N, cl       

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
    jns guardar_N      

    ; N quedo negativo, sumar 10 para obtener el nuevo N correcto
    add al, 10
    mov N, al
    ; bajar M en 1
    mov al, M
    sub al, 1
    mov M, al
    jns ciclo1          
    jmp inicio          

guardar_N:
    mov N, al
    jmp ciclo1

ret

M        db 0
N        db 0
paso     db 1
mensaje  db 'Ingrese un numero de dos digitos: $'
pregunta db 'Cuanto quieres que decremente, en 1 o en 2? $'