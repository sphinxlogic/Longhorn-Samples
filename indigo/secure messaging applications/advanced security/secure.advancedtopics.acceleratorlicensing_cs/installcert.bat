@echo off
IF "%1"=="/?" GOTO usage
SET REMOVECERTS=
IF "%1%"=="remove" SET REMOVECERTS="1"
setlocal

echo.++ cert tool for AcceloratorLicensing Sample

echo.++ removing cert for Hello Service 
certmgr -del -c -s My -n soap.tcp://localhost:46000/HelloService/
IF NOT "%REMOVECERTS%"=="" goto revertdds

echo.+++ making HelloService cert
makecert -n "CN=soap.tcp://localhost:46000/HelloService/" -ss MY -$ individual

:revertdds
endlocal

goto end

:usage
echo.
echo. Makes and installs certificate for AcceloratorLicensing sample
echo.    makecerts          :install HelloService certs
echo.    makecerts remove   :remove HelloService certs
echo.    makecerts /?       :displays usage instructions
echo.

:end
