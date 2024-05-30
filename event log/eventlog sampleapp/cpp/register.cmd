@echo off
echo Registering Legacy part of SampleApp (ReportEvent)...
reg add HKLM\System\CurrentControlSet\Services\Eventlog\Application\SampleApp /f
reg add HKLM\System\CurrentControlSet\Services\Eventlog\Application\SampleApp /v EventMessageFile /t REG_SZ /f /d %CD%\SampleApp.exe
reg add HKLM\System\CurrentControlSet\Services\Eventlog\Application\SampleApp /v TypesSupported /t REG_DWORD /f /d 7
reg add HKLM\System\CurrentControlSet\Services\Eventlog\Application\SampleApp /v AppID /t REG_SZ /f /d Microsoft.SampleApp
echo .

echo Registering the manifest of SampleApp (takes care of EvtReport())...
call %WINDIR%\system32\regman %CD%\SampleApp.man

echo Placing the MUI resources in the correct location for message rendering...
mkdir %CD%\mui\0409
copy /Y %CD%\sampleApp.exe.mui %CD%\mui\0409

echo Creating the Trace Format (.TMF) files and placing them in the TraceFormat directory...
tracepdb -f %CD%\SampleApp.pdb -v
copy /Y %CD%\*.tmf %windir%\system32\WinEvt\TraceFormat