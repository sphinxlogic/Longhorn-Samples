reg add hklm\software\microsoft\windows\currentversion\explorer\sidebar\modules\{BE8A040E-2977-4894-B6AA-BDBCFAA65819}

reg add hklm\software\microsoft\windows\currentversion\explorer\sidebar\modules\{BE8A040E-2977-4894-B6AA-BDBCFAA65819} /v AssemblyName /d %CD%\HelloWorldTileVB

reg add hklm\software\microsoft\windows\currentversion\explorer\sidebar\modules\{BE8A040E-2977-4894-B6AA-BDBCFAA65819} /v Type /d HelloWorldTileVB.MySidebarTiles.HelloWorldTile

reg add hklm\software\microsoft\windows\currentversion\explorer\sidebar\modules\{BE8A040E-2977-4894-B6AA-BDBCFAA65819} /v "Friendly Name" /d "Hello World VB"
