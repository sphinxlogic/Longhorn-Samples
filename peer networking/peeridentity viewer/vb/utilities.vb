'=====================================================================
'  File:      Utilities.vb
'
'  Summary:   Utility routines for IdentityViewer sample.
'
'---------------------------------------------------------------------
' This file is part of the Microsoft .NET Framework SDK Code Samples.
' 
' Copyright (C) Microsoft Corporation.  All rights reserved.
' 
' This source code is intended only as a supplement to Microsoft
' Development Tools and/or on-line documentation.  See these other
' materials for detailed information regarding Microsoft code samples.
' 
' THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
' KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
' IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
' PARTICULAR PURPOSE.
'---------------------------------------------------------------------

Option Explicit On
Option Strict On

Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Text
Imports System.Net.PeerToPeer
Imports System.Windows.Forms
Imports System.Diagnostics

NotInheritable Public Class Utilities

    ' This class has only static methods.
    Private Sub New()

    End Sub 'New

    ' Fill a ComboBox with the available identities.
    '
    Public Shared Sub FillIdentityComboBox(ByVal comboBox As System.Windows.Forms.ComboBox, ByVal identityDefault As PeerIdentity) 
        comboBox.BeginUpdate()
        
        ' Remove any previous entries
        comboBox.Items.Clear()
        
        Dim identities As List(Of PeerIdentity)
        identities = PeerIdentity.GetIdentities()
        
        Dim selectedItem As [Object] = Nothing
        Dim identity As PeerIdentity
        For Each identity In  identities
            comboBox.Items.Add(identity)
            
            ' Check if this is the identity we want to select.
            If identity.Equals(identityDefault) Then
                selectedItem = identity
            End If
        Next identity
        
        ' Select the default identity or the first one in the list.
        If Not (selectedItem Is Nothing) Then
            comboBox.SelectedItem = selectedItem
        ElseIf identities.Count > 0 Then
            comboBox.SelectedIndex = 0
        End If
        
        comboBox.EndUpdate()
    
    End Sub 'FillIdentityComboBox

    ' Read the information from a text file and return it as a string.
    '
    Public Shared Function ReadTextFile(ByVal fileName As String) As String
        Dim data As String = String.Empty

        ' Create an instance of StreamReader to read from a file.
        ' The using statement also closes the StreamReader.
        Dim sr As New StreamReader(fileName, System.Text.Encoding.Unicode)
        Return sr.ReadToEnd()

    End Function 'ReadTextFile

    ' Write the text data to a file.
    '
    Public Shared Sub WriteTextFile(ByVal fileName As String, ByVal data As String)
        Dim [unicode] As New UnicodeEncoding(False, False)
        Dim sw As New StreamWriter(fileName, False, [unicode])
        sw.WriteLine(data)
        sw.Flush()
        sw.Close()
    End Sub 'WriteTextFile

    ' Display an Exception message in a dialog
    '
    Public Shared Sub DisplayException(ByVal e As Exception, ByVal formParent As Form)
        Dim id As DialogResult
        id = MessageBox.Show(formParent, _
            e.Message & vbCrLf & vbCrLf & e.StackTrace, _
            "Exception from " + e.Source, _
            MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error)

        Select Case id
            Case DialogResult.Retry
                Debugger.Break()

            Case DialogResult.Abort
                formParent.Close()

            Case Else
        End Select

    End Sub 'DisplayException
End Class 'Utilities