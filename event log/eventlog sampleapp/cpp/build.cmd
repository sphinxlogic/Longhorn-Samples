@echo off

if "%SDKTOOLPATH%"=="" goto :NoSDK

set Include=%Include%;%SDKTOOLPATH%\..\src\crt

tracewpp -cfgdir:"%SDKTOOLPATH%\wppconfig\rev1" -odir:. sampleapp.cpp

"%SDKTOOLPATH%\mc" SampleAppevents.man

"%SDKTOOLPATH%\rc" SampleApp.rc

nmake SampleApp.mk
goto :EOF

:NoSDK
echo Longhorn SDK does not seem to be installed on this machine.
echo This sample needs the Longhorn SDK at build time.
goto :EOF

"%WinDir%\System32\WMI Config\WcmCompile" -n shared .\SampleAppevents.man