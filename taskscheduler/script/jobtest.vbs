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


'****************************************************************************
'                                                                           *
' Main.cs - Sample application for Task Scheduler V2 managed API            *
'                                                                           *
' Component: Task Scheduler                                                 *
'                                                                           *
' Copyright (c) 2002 - 2003, Microsoft Corporation                          *
'                                                                           *
'****************************************************************************/



On Error Resume Next
Err.clear

'Get the User name from the argument
Set args = Wscript.Arguments
if args.Count <> 1 Then
	wscript.echo "Usage: JobTest <UserName>"
	wscript.quit
End If

Set objPassword = CreateObject("ScriptPW.Password") 
WScript.echo "Input password for the user " & args(0) 

strPassword = objPassword.GetPassword() 

If Err.Number <> 0 Then
	wscript.echo "Error creating task: Error code " & Err.Number & " Error message " & Err.Description	
	wscript.quit
End If

' Create the Job Builder Object
set builder = CreateObject("MSTASK.JobBuilder")

If Err.Number <> 0 Then
	wscript.echo "Error creating task: Error code " & Err.Number & " Error message " & Err.Description	
	wscript.quit
End If

' Set the User Id and password. 

builder.UserId = args(0)
builder.Password = strPassword

'Add a step to run calc.exe

set step = builder.steps.Add(0)
Windir = createobject("wscript.shell").expandenvironmentstrings("%windir%") 

step.path = Windir & "\system32\calc.exe"

'Add a Time trigger to the job
'Set the starttime to 1 minute from the current time

set trigger = builder.triggers.Add(1)


'First increment the time by one minute

cHour = Hour(Time )
cMinute = Minute(Time )
cSecond = Second( Time )

cMinute = cMinute + 1
If cMinute > 59 Then
	cMinute = 0
	cHour = cHour + 1
	if cHour > 23 Then
		cHour = 0
	End If
End If 

cHour   = "0" & cHour
cMinute = "0" & cMinute
cSecond = "0" & cSecond

NewTime = Right(cHour, 2 ) & ":" & Right( cMinute, 2 ) & ":" & Right( csecond, 2)


'Get the datetime in XML Format
DateConv = split(Date,"/")

cMonth = "0" & DateConv(0)
cDay   = "0" & DateConv(1)

tDate = DateConv(2) & "-" & Right(cMonth, 2 ) & "-" & Right( cDay, 2 )

XmlTime = tDate & "T" & NewTime
wscript.echo "Setting starttime of the job as " & XmlTime

trigger.StartTime = XmlTime


'Delete the Job if it already exists

set service = CreateObject("WmiJobs.Service")

If Err.Number <> 0 Then
	wscript.echo "Error creating task: Error code " & Err.Number & " Error message " & Err.Description	
	wscript.quit
End If


set rootFolder = service.GetFolder( "\\" )

If service.HasJob( "TestVBJob" ) Then
	rootFolder.RemoveJob( "TestVBJob" )
End If

'Save the job in root folder

call builder.Save("TestVBJob", "\")

If Err.Number <> 0 Then
	wscript.echo "Error creating task: Error code " & Err.Number & " Error message " & Err.Description	
	wscript.quit
End If

wscript.echo "Success !!! Job registered "
