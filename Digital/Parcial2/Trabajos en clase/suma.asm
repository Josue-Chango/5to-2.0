
; You may customize this and other start-up templates; 
; The location of this template is c:\emu8086\inc\0_com_template.txt       

name "ejemplo1"

org 100h

    mov al,5 ;bin=00000101b
    mov bl,10 ; hex=0Ah or bin=00001010b
    add bl,al ; 5+10=15

ret





