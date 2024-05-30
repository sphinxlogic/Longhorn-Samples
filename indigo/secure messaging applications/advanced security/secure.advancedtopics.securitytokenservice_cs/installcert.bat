@echo off
IF "%1"=="/?" GOTO usage
SET REMOVECERTS=
IF "%1%"=="remove" SET REMOVECERTS="1"
setlocal

echo.++ cert tool for  SecurityTokenService Sample

echo.++ removing cert for Hello Service and X509Client
certmgr -del -c -s My -n soap.tcp://localhost:46000/HelloService/
certmgr -del -c -s My -n JoeIndigo
IF NOT "%REMOVECERTS%"=="" goto revertdds

echo.+++ making HelloService cert
makecert -n "CN=soap.tcp://localhost:46000/HelloService/" -ss MY -$ individual

echo.+++ making X509Client cert
makecert -n "CN=JoeIndigo" -ss MY -$ individual -sky exchange -sk 8dbf7f0c-1f34-4a57-a275-7130241e9df0

:revertdds
endlocal

goto end

:usage
echo.
echo. Makes and installs certificate for SecurityTokenService sample
echo.    makecerts          :install HelloService and X509Client certs
echo.    makecerts remove   :remove HelloService and X509Client certs
echo.    makecerts /?       :displays usage instructions
echo.

:end
