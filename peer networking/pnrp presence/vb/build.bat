@ REM Set command switch for building debug or retail (default is to build debug)
@ REM Type "build.bat -r" to build for retail
@ SET DEBUGSAMPLE=/debug+
@ IF "%1"=="-r" SET DEBUGSAMPLE=/debug-
@ IF "%1"=="-R" SET DEBUGSAMPLE=/debug-

vbc.exe /nologo /t:winexe %DEBUGSAMPLE% /optionstrict+ /r:System.Net.PeerToPeer.dll /out:.\PnrpPresence.exe PnrpPresence.vb 
