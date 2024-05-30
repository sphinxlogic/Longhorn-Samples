'---------------------------------------------------------------------
'  This file is part of the Microsoft .NET Framework SDK Code Samples.
' 
'  Copyright (C) Microsoft Corporation.  All rights reserved.
' 
'This source code is intended only as a supplement to Microsoft
'Development Tools and/or on-line documentation.  See these other
'materials for detailed information regarding Microsoft code samples.
' 
'THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
'KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
'IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
'PARTICULAR PURPOSE.
'---------------------------------------------------------------------


'____________________________________________________________
'
' Constant definitions
'____________________________________________________________

Const WcmProgID     = "Wcm.SettingsEngine"

Const ReadWriteAccess           = 2

Const SettingChangeMode         = 1

Const UserContext               = 1

'____________________________________________________________
'
' Variable declarations
'____________________________________________________________

Dim SettingsEngine
Dim NamespaceIdentity
Dim ThisNamespace
Dim SettingItem
Dim SettingValue

Call Main()

'____________________________________________________________
'
' Main function
'____________________________________________________________

Sub Main()

' Create Engine object

Call GetEngine()

' Retrieve namespace

Call GetNamespace("http://www.microsoft.com/state/WcmSample1", "1.0", "en-US")

' Get the setting item

Set SettingItem = GetSetting("myAppWindow/topLeft/xCoord")

Wscript.Echo "The value of xCoord is: " & SettingItem.Value

' Set the setting item

Call UpdateSetting("myAppWindow/topLeft/xCoord", 250, "WcmSample1 script")

End Sub


Sub GetEngine()

Wscript.Echo "---------- Get engine object"
set SettingsEngine = CreateObject(WcmProgID)

End Sub     ' GetEngine


Sub GetNamespace(Name, Version, Culture)

' Get an empty namespace identity

Wscript.Echo "---------- Set namespace ID"
set NamespaceIdentity = SettingsEngine.CreateNamespaceIdentity

' Specify the manifest

Wscript.Echo "  put_Name"
NamespaceIdentity.Name = Name

Wscript.Echo "  put_Version"
NamespaceIdentity.Version = Version

Wscript.Echo "  put_Language"
NamespaceIdentity.Language = Culture

Wscript.Echo "  put_Context"
NamespaceIdentity.Context = UserContext

' Get the application namespace

Wscript.Echo "  GetNamespace"
set ThisNamespace = SettingsEngine.GetNamespace(NamespaceIdentity, SettingChangeMode, ReadWriteAccess)

End Sub     ' GetNamespace


Function GetSetting(Path)

' Get a setting given the Path, using GetSettingByPath

WScript.Echo "---------- Get this setting: " & Path

Wscript.Echo "  get setting"
set GetSetting = ThisNamespace.GetSettingByPath(Path)

Wscript.Echo "  get setting: path=" & GetSetting.Path

End Function    ' GetSetting


Sub UpdateSetting(Path, NewValue, SaveReason)

Dim objItemTemp

WScript.Echo "---------- Set value on item: " & Path

' Get the setting

Wscript.Echo "  get setting"
set objItemTemp = ThisNamespace.GetSettingByPath(Path)

Wscript.Echo "  get setting: path=" & objItemTemp.Path

' Set the setting

Wscript.Echo "  Set new value"
objItemTemp.Value = NewValue

' Commit to the store

Wscript.Echo "  Save setting"
Call ThisNamespace.Save(SaveReason)

End Sub     ' UpdateSetting