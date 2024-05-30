@echo off
setlocal

set TILE_GUID={9FBE8E90-8CC7-4171-A9BC-D300999380D3}


echo.
echo NOTE: The folder that contains this setup file must also include the following
echo files:
echo - NewsFeed.dll (see readme.txt for instructions on how to build)
echo - bullet.png
echo.
echo See readme.txt for more information.
echo.

reg add "hklm\Software\NewsFeedTile" /f > nul
reg add "hklm\Software\NewsFeedTile" /f /v ImagePath /d "%CD%"\ > nul

reg add "hklm\Software\Microsoft\Windows\CurrentVersion\Explorer\Sidebar\Modules\%TILE_GUID%" /f > nul
reg add "hklm\Software\Microsoft\Windows\CurrentVersion\Explorer\Sidebar\Modules\%TILE_GUID%" /f /v AssemblyName /d "%CD%\NewsFeed" > nul
reg add "hklm\Software\Microsoft\Windows\CurrentVersion\Explorer\Sidebar\Modules\%TILE_GUID%" /f /v Type /d "MySidebarTiles.NewsFeedTile" > nul
reg add "hklm\Software\Microsoft\Windows\CurrentVersion\Explorer\Sidebar\Modules\%TILE_GUID%" /f /v "Friendly Name" /d "News Feed Sample" > nul

echo.
echo Successfully added (or updated) tile information in Registry.
echo.
goto done

:Error
goto Done

:Done
