@echo off
"%SDKTOOLPATH%\wsdlgen.exe" /nologo HelloService.dll
if errorlevel 1 goto ReportError
goto End
:ReportError
echo Project error: A tool returned an error code from the build event
exit 1
:End