@echo off
"%SDKTOOLPATH%\wsdlgen.exe"  /nologo /l:VB ..\server\www_tempuri_org.quickstarts.wsdl ..\server\schemas_microsoft_com.serialization.2003.02.DefaultDocumentElement.xsd ..\server\www_tempuri_org.quickstarts.xsd
if errorlevel 1 goto ReportError
goto End
:ReportError
echo Project error: A tool returned an error code from the build event
exit 1
:End