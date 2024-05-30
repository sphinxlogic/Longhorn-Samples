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
Imports System.Storage
Imports System.Storage.Contacts
Imports System.Storage.Core
Imports System.Runtime.InteropServices

<Assembly: ComVisible(False)> 
Module AppView

    Sub Main()
        Dim itemContext As ItemContext
        Try
            itemContext = itemContext.Open()

            Dim viewDef As ViewDefinition = New ViewDefinition()
            viewDef.Fields.Add("DisplayName")
            viewDef.SortOptions.Add("DisplayName", SortOrder.Descending)
            Dim appView As ApplicationView = ApplicationView.CreateView(Person.GetSearcher(itemContext), viewDef)
            Dim viewRecord As ViewRecordReader = appView.GetRange(1, 100)
            While viewRecord.Read()
                Dim displayName As String = viewRecord("DisplayName")
                Console.WriteLine("DisplayName: " & displayName)
            End While
        Finally
            If Not (itemContext Is Nothing) Then itemContext.Close()
        End Try
    End Sub

End Module
