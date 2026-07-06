@echo off
title HotelWebApp - Build and Deploy
cd /d "%~dp0"

echo ============================================
echo  HotelWebApp - Compilar y Desplegar en Tomcat
echo ============================================
echo.
echo Requisitos: Java 11+ (JDK) instalado
echo.

if "%JAVA_HOME%"=="" (
    echo ERROR: JAVA_HOME no esta configurado.
    pause
    exit /b 1
)
echo JAVA_HOME=%JAVA_HOME%
echo.

echo [1/2] Compilando el proyecto WAR...
cd /d "%~dp0HotelWebApp"
call mvnw.cmd clean package -DskipTests
if %errorlevel% neq 0 (
    echo ERROR: La compilacion fallo.
    pause
    exit /b 1
)
echo Compilacion exitosa.
echo.

echo [2/2] Desplegando en Tomcat...
copy /Y "%~dp0HotelWebApp\target\HotelWebApp.war" "%~dp0apache-tomcat-9.0.119\webapps\"
if %errorlevel% neq 0 (
    echo ERROR: No se pudo copiar el WAR a Tomcat.
    pause
    exit /b 1
)
echo WAR desplegado correctamente.
echo.

echo Iniciando Tomcat...
echo La aplicacion estara disponible en:
echo   http://localhost:8080/HotelWebApp
echo.
echo Presiona Ctrl+C para detener Tomcat.
echo ============================================
echo.

cd /d "%~dp0apache-tomcat-9.0.119\bin"
call startup.bat

echo.
echo Tomcat iniciado. Abre tu navegador en:
echo   http://localhost:8080/HotelWebApp
echo.
pause
