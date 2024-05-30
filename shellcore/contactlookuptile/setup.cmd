@echo off
setlocal

set TILE_GUID={9606130C-09B9-499D-9A7F-5652A0A57010}

echo.
echo NOTE: The folder that contains this setup file must also include the following
echo files:
echo - ContactLookupTile.dll (see readme.txt for instructions on how to build)
echo - GenericContact.png
echo - ProgressIndicator.png
echo - SearchIcon.png
echo - SearchIconDisabled.png
echo.
echo See readme.txt for more information.
echo.

reg add "hklm\Software\ContactLookupTile" /f > nul
reg add "hklm\Software\ContactLookupTile" /f /v ImagePath /d "%CD%"\ > nul

reg add "hklm\Software\Microsoft\Windows\CurrentVersion\Explorer\SideBar\Modules\%TILE_GUID%" /f > nul
reg add "hklm\Software\Microsoft\Windows\CurrentVersion\Explorer\SideBar\Modules\%TILE_GUID%" /f /v AssemblyName /d "%CD%\ContactLookupTile" > nul
reg add "hklm\Software\Microsoft\Windows\CurrentVersion\Explorer\SideBar\Modules\%TILE_GUID%" /f /v Type /d "MySidebarTiles.ContactLookupTile" > nul
reg add "hklm\Software\Microsoft\Windows\CurrentVersion\Explorer\SideBar\Modules\%TILE_GUID%" /f /v "Friendly Name" /d "Contact Lookup" > nul

echo.
echo Successfully added (or updated) tile information in Registry.
echo.
goto done

:Error
goto Done

:Done
