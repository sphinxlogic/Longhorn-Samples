reg add hklm\software\microsoft\windows\currentversion\explorer\sidebar\modules\{DA76DB38-1BF4-482f-A573-6F5FC4A5B4BF}

reg add hklm\software\microsoft\windows\currentversion\explorer\sidebar\modules\{DA76DB38-1BF4-482f-A573-6F5FC4A5B4BF} /v AssemblyName /d %CD%\HelloWorldTile

reg add hklm\software\microsoft\windows\currentversion\explorer\sidebar\modules\{DA76DB38-1BF4-482f-A573-6F5FC4A5B4BF} /v Type /d MySidebarTiles.HelloWorldTile

reg add hklm\software\microsoft\windows\currentversion\explorer\sidebar\modules\{DA76DB38-1BF4-482f-A573-6F5FC4A5B4BF} /v "Friendly Name" /d "Hello World"
