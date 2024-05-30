@ REM Set command switch for building debug or retail (default is to build debug)
@ REM Type "build.bat -r" to build for retail
@ SET DEBUGSAMPLE=/debug+
@ IF "%1"=="-r" SET DEBUGSAMPLE=/debug-
@ IF "%1"=="-R" SET DEBUGSAMPLE=/debug-

vbc.exe /nologo /t:winexe %DEBUGSAMPLE% /optionstrict+ /res:IdentityViewer.FormMain.resources,FormMain.resources /r:System.Net.PeerToPeer.dll  /out:.\IdentityViewer.exe FormMain.vb FormIdentity.vb DlgSaveIdentityInfo.vb DlgNewIdentity.vb DlgImportIdentity.vb DlgExportIdentity.vb utilities.vb
