@REM ----------------------------------------------------------------------------
@REM Licensed to the Apache Software Foundation (ASF) under one
@REM or more contributor license agreements.  See the NOTICE file
@REM distributed with this work for additional information
@REM regarding copyright ownership.  The ASF licenses this file
@REM to you under the Apache License, Version 2.0 (the
@REM "License"); you may not use this file except in compliance
@REM with the License.  You may obtain a copy of the License at
@REM
@REM    https://www.apache.org/licenses/LICENSE-2.0
@REM
@REM Unless required by applicable law or agreed to in writing,
@REM software distributed under the License is distributed on an
@REM "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
@REM KIND, either express or implied.  See the License for the
@REM specific language governing permissions and limitations
@REM under the License.
@REM ----------------------------------------------------------------------------

@REM ----------------------------------------------------------------------------
@REM Maven Start Up Batch script
@REM
@REM Required ENV vars:
@REM JAVA_HOME - location of a JDK home dir
@REM
@REM Optional ENV vars
@REM M2_HOME - location of maven2's installed home dir
@REM MAVEN_BATCH_ECHO - set to 'on' to enable the echoing of the batch commands
@REM MAVEN_BATCH_PAUSE - set to 'on' to wait for a key stroke before ending
@REM MAVEN_OPTS - parameters passed to the Java VM when running Maven
@REM     e.g. to debug Maven itself, use
@REM set MAVEN_OPTS=-Xdebug -Xrunjdwp:transport=dt_socket,server=y,suspend=y,address=8000
@REM MAVEN_SKIP_RC - flag to disable loading of mavenrc files
@REM ----------------------------------------------------------------------------

@REM Begin all REM lines with '@' in case MAVEN_BATCH_ECHO is 'on'
@echo off
@REM set title of command window
title %0
@REM enable echoing by setting MAVEN_BATCH_ECHO to 'on'
@if "%MAVEN_BATCH_ECHO%" == "on"  echo %MAVEN_BATCH_ECHO%

@REM set %HOME% to equivalent of $HOME
if "%HOME%" == "" (set "HOME=%HOMEDRIVE%%HOMEPATH%")

@REM Execute a user defined script before this one
if not "%MAVEN_SKIP_RC%" == "" goto skipRcPre
@REM check for pre script, once with legacy .bat ending and once with .cmd ending
if exist "%USERPROFILE%\mavenrc_pre.bat" call "%USERPROFILE%\mavenrc_pre.bat" 2>nul
if exist "%USERPROFILE%\mavenrc_pre.cmd" call "%USERPROFILE%\mavenrc_pre.cmd" 2>nul
:skipRcPre

@setlocal

set LOCAL_JAVA_EXE="%JAVA_HOME%\bin\java.exe"
if "%JAVA_HOME%" == "" (
    echo JAVA_HOME not set. Please set JAVA_HOME to your JDK installation directory.
    exit /b 1
)
if not exist "%JAVA_HOME%\bin\java.exe" (
    echo JAVA_HOME is set to an invalid directory: "%JAVA_HOME%"
    exit /b 1
)

setlocal enabledelayedexpansion

@REM Collect command line arguments
set MAVEN_CMD_LINE_ARGS=%*

@REM Determine Maven Wrapper jar
set MW_JAR="%USERPROFILE%\.m2\wrapper\maven-wrapper.jar"
if not exist "%MW_JAR%" (
    echo Downloading Maven Wrapper jar...
    set MW_URL=https://repo.maven.apache.org/maven2/org/apache/maven/wrapper/maven-wrapper/3.3.2/maven-wrapper-3.3.2.jar
    if not exist "%USERPROFILE%\.m2\wrapper\" mkdir "%USERPROFILE%\.m2\wrapper\"
    @REM Try PowerShell download first, fallback to bitsadmin
    powershell -Command "& {[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12; Invoke-WebRequest -Uri '%MW_URL%' -OutFile '%MW_JAR%'}" 2>nul
    if not exist "%MW_JAR%" (
        echo Failed to download Maven Wrapper jar. Please check your internet connection.
        exit /b 1
    )
)

@REM Execute Maven
"%JAVA_EXE%" ^
    %MAVEN_OPTS% ^
    -classpath "%MW_JAR%" ^
    "-Dmaven.multiModuleProjectDirectory=%CD%" ^
    org.apache.maven.wrapper.MavenWrapperMain ^
    %MAVEN_CMD_LINE_ARGS%

@endlocal
