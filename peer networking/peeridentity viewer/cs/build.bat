@ REM Set command switch for building debug or retail (default is to build debug)
@ REM Type "build.bat -r" to build for retail
@ SET DEBUGSAMPLE=/debug+
@ IF "%1"=="-r" SET DEBUGSAMPLE=/debug-
@ IF "%1"=="-R" SET DEBUGSAMPLE=/debug-

csc.exe /nologo /t:winexe %DEBUGSAMPLE% /res:IdentityViewer.FormMain.resources /r:System.Net.PeerToPeer.dll /out:.\IdentityViewer.exe FormMain.cs FormIdentity.cs DlgSaveIdentityInfo.cs DlgNewIdentity.cs DlgImportIdentity.cs DlgExportIdentity.cs utilities.cs
