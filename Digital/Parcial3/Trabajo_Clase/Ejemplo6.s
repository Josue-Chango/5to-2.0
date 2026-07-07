.global _start

_start:
	MOV R0, #20		// mov cl,20

inicio:
	MOV R1, #10		// mov bl,N
ciclo:
	SUB R1, R1, #1		// dec bl
	CMP R1, #0
	BNE ciclo		// jnz ciclo

	SUBS R0, R0, #1		// loop
	BNE inicio

fin:
	B fin			