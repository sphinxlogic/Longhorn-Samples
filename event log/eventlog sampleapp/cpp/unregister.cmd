@echo off
reg delete HKLM\System\CurrentControlSet\Services\Eventlog\Application\SampleApp /f > nul

"%WINDIR%\system32\WMI Config\WcmCompile.exe" -r -n shared %CD%\SampleApp.man
