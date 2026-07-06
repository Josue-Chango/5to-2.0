@echo off
title HotelWebApp
cd /d "%~dp0"

echo ============================================
echo  HotelWebApp - Inicio Rapido
echo ============================================
echo.
echo Requisitos: Java 11+ (JDK) instalado
echo.
echo Verificando JAVA_HOME...
if "%JAVA_HOME%"=="" (
    echo ERROR: JAVA_HOME no esta configurado.
    echo Configuralo a tu JDK, ej: C:\Program Files\Java\jdk-11
    pause
    exit /b 1
)
echo JAVA_HOME=%JAVA_HOME%
echo.

echo Iniciando la aplicacion con Maven Wrapper...
echo La aplicacion estara disponible en:
echo   http://localhost:8081/HotelWebApp
echo.
echo Presiona Ctrl+C para detener el servidor.
echo ============================================
echo.

cd /d "%~dp0HotelWebApp"

call mvnw.cmd tomcat7:run

pause
