.global _start

_start:
	
inicio:
	MOV R0, #9		// mov M,9   -> R0 = M
ciclo2:
	MOV R1, #9		// mov N,9   -> R1 = N
ciclo1:
	SUB R1, R1, #1		// dec N
	CMP R1, #0
	BNE ciclo1		// jnz ciclo1

	SUB R0, R0, #1		// dec M
	CMP R0, #0
	BNE ciclo2		// jnz ciclo2

	B inicio		// jmp inicio 

fin:
	B fin			