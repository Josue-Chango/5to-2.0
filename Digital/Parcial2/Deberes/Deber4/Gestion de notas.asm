org 100h

; ********** SOLICITAR CANTIDAD DE ESTUDIANTES **********

lea dx, msg_cantidad
mov ah, 9
int 21h

mov ah, 01h
int 21h
sub al, 48          ; Convertir ASCII a numero
mov cantidad, al    ; Guardar cantidad de estudiantes

mov contador, 0     ; Inicializar contador de notas

; ********** LECTURA DE NOTAS (pedir1 a pedir10) **********
; Cada bloque: pide decena+unidad, valida rango 00-20,
; si es invalida muestra error y repite y si es valida avanza.

pedir1:
    mov al, contador
    cmp al, cantidad
    je fin_lectura

    lea dx, msg_nota
    mov ah, 9
    int 21h

    mov ah, 01h
    int 21h
    sub al, 48
    mov decena1, al

    mov ah, 01h
    int 21h
    sub al, 48
    mov unidad1, al

    mov al, decena1
    cmp al, 2
    jg invalida1
    cmp al, 2
    jne valida1
    mov al, unidad1
    cmp al, 0
    jne invalida1
    jmp valida1

invalida1:
    lea dx, msg_error
    mov ah, 9
    int 21h
    jmp pedir1

valida1:
    inc contador
    jmp pedir2

pedir2:
    mov al, contador
    cmp al, cantidad
    je fin_lectura

    lea dx, msg_nota
    mov ah, 9
    int 21h

    mov ah, 01h
    int 21h
    sub al, 48
    mov decena2, al

    mov ah, 01h
    int 21h
    sub al, 48
    mov unidad2, al

    mov al, decena2
    cmp al, 2
    jg invalida2
    cmp al, 2
    jne valida2
    mov al, unidad2
    cmp al, 0
    jne invalida2
    jmp valida2

invalida2:
    lea dx, msg_error
    mov ah, 9
    int 21h
    jmp pedir2

valida2:
    inc contador
    jmp pedir3

pedir3:
    mov al, contador
    cmp al, cantidad
    je fin_lectura

    lea dx, msg_nota
    mov ah, 9
    int 21h

    mov ah, 01h
    int 21h
    sub al, 48
    mov decena3, al

    mov ah, 01h
    int 21h
    sub al, 48
    mov unidad3, al

    mov al, decena3
    cmp al, 2
    jg invalida3
    cmp al, 2
    jne valida3
    mov al, unidad3
    cmp al, 0
    jne invalida3
    jmp valida3

invalida3:
    lea dx, msg_error
    mov ah, 9
    int 21h
    jmp pedir3

valida3:
    inc contador
    jmp pedir4

pedir4:
    mov al, contador
    cmp al, cantidad
    je fin_lectura

    lea dx, msg_nota
    mov ah, 9
    int 21h

    mov ah, 01h
    int 21h
    sub al, 48
    mov decena4, al

    mov ah, 01h
    int 21h
    sub al, 48
    mov unidad4, al

    mov al, decena4
    cmp al, 2
    jg invalida4
    cmp al, 2
    jne valida4
    mov al, unidad4
    cmp al, 0
    jne invalida4
    jmp valida4

invalida4:
    lea dx, msg_error
    mov ah, 9
    int 21h
    jmp pedir4

valida4:
    inc contador
    jmp pedir5

pedir5:
    mov al, contador
    cmp al, cantidad
    je fin_lectura

    lea dx, msg_nota
    mov ah, 9
    int 21h

    mov ah, 01h
    int 21h
    sub al, 48
    mov decena5, al

    mov ah, 01h
    int 21h
    sub al, 48
    mov unidad5, al

    mov al, decena5
    cmp al, 2
    jg invalida5
    cmp al, 2
    jne valida5
    mov al, unidad5
    cmp al, 0
    jne invalida5
    jmp valida5

invalida5:
    lea dx, msg_error
    mov ah, 9
    int 21h
    jmp pedir5

valida5:
    inc contador
    jmp pedir6

pedir6:
    mov al, contador
    cmp al, cantidad
    je fin_lectura

    lea dx, msg_nota
    mov ah, 9
    int 21h

    mov ah, 01h
    int 21h
    sub al, 48
    mov decena6, al

    mov ah, 01h
    int 21h
    sub al, 48
    mov unidad6, al

    mov al, decena6
    cmp al, 2
    jg invalida6
    cmp al, 2
    jne valida6
    mov al, unidad6
    cmp al, 0
    jne invalida6
    jmp valida6

invalida6:
    lea dx, msg_error
    mov ah, 9
    int 21h
    jmp pedir6

valida6:
    inc contador
    jmp pedir7

pedir7:
    mov al, contador
    cmp al, cantidad
    je fin_lectura

    lea dx, msg_nota
    mov ah, 9
    int 21h

    mov ah, 01h
    int 21h
    sub al, 48
    mov decena7, al

    mov ah, 01h
    int 21h
    sub al, 48
    mov unidad7, al

    mov al, decena7
    cmp al, 2
    jg invalida7
    cmp al, 2
    jne valida7
    mov al, unidad7
    cmp al, 0
    jne invalida7
    jmp valida7

invalida7:
    lea dx, msg_error
    mov ah, 9
    int 21h
    jmp pedir7

valida7:
    inc contador
    jmp pedir8

pedir8:
    mov al, contador
    cmp al, cantidad
    je fin_lectura

    lea dx, msg_nota
    mov ah, 9
    int 21h

    mov ah, 01h
    int 21h
    sub al, 48
    mov decena8, al

    mov ah, 01h
    int 21h
    sub al, 48
    mov unidad8, al

    mov al, decena8
    cmp al, 2
    jg invalida8
    cmp al, 2
    jne valida8
    mov al, unidad8
    cmp al, 0
    jne invalida8
    jmp valida8

invalida8:
    lea dx, msg_error
    mov ah, 9
    int 21h
    jmp pedir8

valida8:
    inc contador
    jmp pedir9

pedir9:
    mov al, contador
    cmp al, cantidad
    je fin_lectura

    lea dx, msg_nota
    mov ah, 9
    int 21h

    mov ah, 01h
    int 21h
    sub al, 48
    mov decena9, al

    mov ah, 01h
    int 21h
    sub al, 48
    mov unidad9, al

    mov al, decena9
    cmp al, 2
    jg invalida9
    cmp al, 2
    jne valida9
    mov al, unidad9
    cmp al, 0
    jne invalida9
    jmp valida9

invalida9:
    lea dx, msg_error
    mov ah, 9
    int 21h
    jmp pedir9

valida9:
    inc contador
    jmp pedir10

pedir10:
    mov al, contador
    cmp al, cantidad
    je fin_lectura

    lea dx, msg_nota
    mov ah, 9
    int 21h

    mov ah, 01h
    int 21h
    sub al, 48
    mov decena10, al

    mov ah, 01h
    int 21h
    sub al, 48
    mov unidad10, al

    mov al, decena10
    cmp al, 2
    jg invalida10
    cmp al, 2
    jne valida10
    mov al, unidad10
    cmp al, 0
    jne invalida10
    jmp valida10

invalida10:
    lea dx, msg_error
    mov ah, 9
    int 21h
    jmp pedir10

valida10:
    inc contador
    jmp fin_lectura

; ********** CONVERTIR DIGITOS A VALORES NUMERICOS **********
; Multiplica decena x 10 (sumando 10 veces) y suma la unidad

fin_lectura:
    mov al, 0
    mov bl, decena1
    mov cl, 10
sumar_d1:
    cmp cl, 0
    je listo_d1
    add al, bl
    dec cl
    jmp sumar_d1
listo_d1:
    add al, unidad1
    mov valor1, al

    mov al, 0
    mov bl, decena2
    mov cl, 10
sumar_d2:
    cmp cl, 0
    je listo_d2
    add al, bl
    dec cl
    jmp sumar_d2
listo_d2:
    add al, unidad2
    mov valor2, al

    mov al, 0
    mov bl, decena3
    mov cl, 10
sumar_d3:
    cmp cl, 0
    je listo_d3
    add al, bl
    dec cl
    jmp sumar_d3
listo_d3:
    add al, unidad3
    mov valor3, al

    mov al, 0
    mov bl, decena4
    mov cl, 10
sumar_d4:
    cmp cl, 0
    je listo_d4
    add al, bl
    dec cl
    jmp sumar_d4
listo_d4:
    add al, unidad4
    mov valor4, al

    mov al, 0
    mov bl, decena5
    mov cl, 10
sumar_d5:
    cmp cl, 0
    je listo_d5
    add al, bl
    dec cl
    jmp sumar_d5
listo_d5:
    add al, unidad5
    mov valor5, al

    mov al, 0
    mov bl, decena6
    mov cl, 10
sumar_d6:
    cmp cl, 0
    je listo_d6
    add al, bl
    dec cl
    jmp sumar_d6
listo_d6:
    add al, unidad6
    mov valor6, al

    mov al, 0
    mov bl, decena7
    mov cl, 10
sumar_d7:
    cmp cl, 0
    je listo_d7
    add al, bl
    dec cl
    jmp sumar_d7
listo_d7:
    add al, unidad7
    mov valor7, al

    mov al, 0
    mov bl, decena8
    mov cl, 10
sumar_d8:
    cmp cl, 0
    je listo_d8
    add al, bl
    dec cl
    jmp sumar_d8
listo_d8:
    add al, unidad8
    mov valor8, al

    mov al, 0
    mov bl, decena9
    mov cl, 10
sumar_d9:
    cmp cl, 0
    je listo_d9
    add al, bl
    dec cl
    jmp sumar_d9
listo_d9:
    add al, unidad9
    mov valor9, al

    mov al, 0
    mov bl, decena10
    mov cl, 10
sumar_d10:
    cmp cl, 0
    je listo_d10
    add al, bl
    dec cl
    jmp sumar_d10
listo_d10:
    add al, unidad10
    mov valor10, al

; ********** CALCULAR SUMA, MAXIMA Y MINIMA **********
; Recorre las notas acumulando suma y actualizando max/min

    mov suma, 0
    mov maxima, 0
    mov minima, 99
    mov contador, 0

    mov al, contador
    cmp al, cantidad
    jge fin_calculo
    mov al, valor1
    add suma, al
    cmp al, maxima
    jle chk_min_1
    mov maxima, al
    
chk_min_1:
    cmp al, minima
    jge sig_1
    mov minima, al
    
sig_1:
    inc contador
    mov al, contador
    cmp al, cantidad
    jge fin_calculo
    mov al, valor2
    add suma, al
    cmp al, maxima
    jle chk_min_2
    mov maxima, al
    
chk_min_2:
    cmp al, minima
    jge sig_2
    mov minima, al
    
sig_2:
    inc contador
    mov al, contador
    cmp al, cantidad
    jge fin_calculo
    mov al, valor3
    add suma, al
    cmp al, maxima
    jle chk_min_3
    mov maxima, al
    
chk_min_3:
    cmp al, minima
    jge sig_3
    mov minima, al
    
sig_3:
    inc contador
    mov al, contador
    cmp al, cantidad
    jge fin_calculo
    mov al, valor4
    add suma, al
    cmp al, maxima
    jle chk_min_4
    mov maxima, al
    
chk_min_4:
    cmp al, minima
    jge sig_4
    mov minima, al
    
sig_4:
    inc contador
    mov al, contador
    cmp al, cantidad
    jge fin_calculo
    mov al, valor5
    add suma, al
    cmp al, maxima
    jle chk_min_5
    mov maxima, al
    
chk_min_5:
    cmp al, minima
    jge sig_5
    mov minima, al
    
sig_5:
    inc contador
    mov al, contador
    cmp al, cantidad
    jge fin_calculo
    mov al, valor6
    add suma, al
    cmp al, maxima
    jle chk_min_6
    mov maxima, al
    
chk_min_6:
    cmp al, minima
    jge sig_6
    mov minima, al
    
sig_6:
    inc contador
    mov al, contador
    cmp al, cantidad
    jge fin_calculo
    mov al, valor7
    add suma, al
    cmp al, maxima
    jle chk_min_7
    mov maxima, al
    
chk_min_7:
    cmp al, minima
    jge sig_7
    mov minima, al
    
sig_7:
    inc contador
    mov al, contador
    cmp al, cantidad
    jge fin_calculo
    mov al, valor8
    add suma, al
    cmp al, maxima
    jle chk_min_8
    mov maxima, al
    
chk_min_8:
    cmp al, minima
    jge sig_8
    mov minima, al
    
sig_8:
    inc contador
    mov al, contador
    cmp al, cantidad
    jge fin_calculo
    mov al, valor9
    add suma, al
    cmp al, maxima
    jle chk_min_9
    mov maxima, al
    
chk_min_9:
    cmp al, minima
    jge sig_9
    mov minima, al
    
sig_9:
    inc contador
    mov al, contador
    cmp al, cantidad
    jge fin_calculo
    mov al, valor10
    add suma, al
    cmp al, maxima
    jle chk_min_10
    mov maxima, al
    
chk_min_10:
    cmp al, minima
    jge sig_10
    mov minima, al
    
sig_10:
    inc contador

; ********** CALCULAR PROMEDIO **********
; Resta 'cantidad' de 'suma' hasta que ya no se pueda;
; la cantidad de restas es el promedio.

fin_calculo:
    mov promedio, 0
    mov al, suma

restar_promedio:
    mov bl, cantidad
    cmp al, bl
    jb fin_promedio
    sub al, bl
    inc promedio
    jmp restar_promedio

; ********** MOSTRAR RESULTADOS **********
; Convierte cada valor a 2 digitos restando 10 sucesivamente
; y muestra: Promedio, Nota mas alta, Nota mas baja.

fin_promedio:
    lea dx, msg_resumen
    mov ah, 9
    int 21h
    lea dx, msg_promedio
    mov ah, 9
    int 21h
    mov al, promedio
    mov bl, 0

mostrar_dec_prom:
    cmp al, 10
    jl listo_dec_prom
    sub al, 10
    inc bl
    jmp mostrar_dec_prom

listo_dec_prom:
    mov temp_unidad, al
    mov dl, bl
    add dl, 48
    mov ah, 2
    int 21h
    mov dl, temp_unidad
    add dl, 48
    mov ah, 2
    int 21h
    lea dx, msg_maxima
    mov ah, 9
    int 21h
    mov al, maxima
    mov bl, 0

mostrar_dec_max:
    cmp al, 10
    jl listo_dec_max
    sub al, 10
    inc bl
    jmp mostrar_dec_max

listo_dec_max:
    mov temp_unidad, al
    mov dl, bl
    add dl, 48
    mov ah, 2
    int 21h
    mov dl, temp_unidad
    add dl, 48
    mov ah, 2
    int 21h
    lea dx, msg_minima
    mov ah, 9
    int 21h
    mov al, minima
    mov bl, 0

mostrar_dec_min:
    cmp al, 10
    jl listo_dec_min
    sub al, 10
    inc bl
    jmp mostrar_dec_min

listo_dec_min:
    mov temp_unidad, al
    mov dl, bl
    add dl, 48
    mov ah, 2
    int 21h
    mov dl, temp_unidad
    add dl, 48
    mov ah, 2
    int 21h

ret

; ********** VARIABLES Y MENSAJES **********

msg_cantidad db 0Dh,0Ah,"Cuantos estudiantes? (1-9): $"
msg_nota     db 0Dh,0Ah,"Ingrese nota como 2 digitos, ej 15 = 1 y 5: $"
msg_error    db 0Dh,0Ah,"Nota invalida (debe ser 00 a 20). Intente de nuevo: $"
msg_resumen  db 0Dh,0Ah,0Ah,"----- RESUMEN -----$"
msg_promedio db 0Dh,0Ah,"Promedio: $"
msg_maxima   db 0Dh,0Ah,"Nota mas alta: $"
msg_minima   db 0Dh,0Ah,"Nota mas baja: $"

cantidad db 0
contador db 0

decena1  db 0
unidad1  db 0
decena2  db 0
unidad2  db 0
decena3  db 0
unidad3  db 0
decena4  db 0
unidad4  db 0
decena5  db 0
unidad5  db 0
decena6  db 0
unidad6  db 0
decena7  db 0
unidad7  db 0
decena8  db 0
unidad8  db 0
decena9  db 0
unidad9  db 0
decena10 db 0
unidad10 db 0

valor1  db 0
valor2  db 0
valor3  db 0
valor4  db 0
valor5  db 0
valor6  db 0
valor7  db 0
valor8  db 0
valor9  db 0
valor10 db 0

suma      db 0
promedio  db 0
maxima    db 0
minima    db 99
temp_unidad db 0
