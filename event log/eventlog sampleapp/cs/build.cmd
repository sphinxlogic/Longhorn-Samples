@echo off
if "%SDKTOOLPATH%"=="" goto :NoSDK
"%WinDir%\System32\WMI Config\WcmCompile" -n shared .\SampleApp.man
"%SDKTOOLPATH%\mc" SampleApp.man
"%SDKTOOLPATH%\rc" SampleApp.rc
csc SampleApp.cs /win32res:SampleApp.res /r:%LAPI%\System.Diagnostics.Events.dll
goto :EOF

:NoSDK
echo Longhorn SDK does not seem to be installed on this machine.
echo This sample needs the Longhorn SDK at build time.
goto :EOF
