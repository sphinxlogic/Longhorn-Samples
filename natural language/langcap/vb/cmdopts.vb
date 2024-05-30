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
Imports System
Imports System.Globalization


Public Class CmdOpts
    Public Sub New()
        m_sOutput = ""
        m_Culture = Nothing
        m_fVerbose = False
    End Sub 'New

    Public Property Output() As String
        Get
            Return m_sOutput
        End Get
        Set(ByVal Value As String)
            m_sOutput = Value
        End Set
    End Property

    Public Property Culture() As CultureInfo
        Get
            Return m_Culture
        End Get
        Set(ByVal Value As CultureInfo)
            m_Culture = Value
        End Set
    End Property

    Public Property Verbose() As Boolean
        Get
            Return m_fVerbose
        End Get
        Set(ByVal Value As Boolean)
            m_fVerbose = Value
        End Set
    End Property

    Public Sub DisplayHelp()
        DisplayHelpLine("O", "Output", "The output file (Defaults to Standard Out)")
        DisplayHelpLine("C", "Culture", "Sets the culture of the engine (ex. -C 'en-us' or 'fr-ca').  Defaults to current culture. ")
        DisplayHelpLine("V", "Verbose", "Verbose output")
    End Sub 'DisplayHelp

    ' DisplayHelp
    Public Function ProcessOpts(ByVal args() As String) As Boolean
        Dim i As Integer = 0

        While i < args.Length
            If args(i).StartsWith("-") Then
                Dim thisarg As [String] = args(i)
                thisarg = thisarg.Remove(0, 1).ToLower(System.Globalization.CultureInfo.CurrentCulture)

                If [String].Compare(thisarg, "output") = 0 Or [String].Compare(thisarg, "o") = 0 Then
                    If (i + 1) >= args.Length Then
                        ' make sure there's a next arg to move on to
                        Throw New System.ArgumentException([String].Concat("No Value for ", thisarg))
                    End If
                    ' assign to the next arg
                    m_sOutput = args(i + 1)
                End If

                If [String].Compare(thisarg, "culture") = 0 Or [String].Compare(thisarg, "c") = 0 Then
                    If (i + 1) >= args.Length Then
                        ' make sure there's a next arg to move on to
                        Throw New System.ArgumentException("No Value for ", thisarg)
                    End If
                    ' assign to the next arg
                    m_Culture = getCultureFromArg(args(i + 1))
                End If

                If [String].Compare(thisarg, "verbose") = 0 Or [String].Compare(thisarg, "v") = 0 Then
                    m_fVerbose = True
                End If

                If [String].Compare(thisarg, "help") = 0 Or [String].Compare(thisarg, "?") = 0 Then
                    DisplayHelp()
                    Return False
                End If
            End If
            i = i + 1
        End While ' for
        Return True
    End Function 'ProcessOpts
    ' ProcessOpts

    Private Sub DisplayHelpLine(ByVal shortname As [String], ByVal name As [String], ByVal description As [String])
        Console.WriteLine(" -{0,1} {1,-24} {2}", shortname, name, description)
    End Sub 'DisplayHelpLine


    Private Function getCultureFromArg(ByVal sArg As [String]) As CultureInfo
        Dim culture As CultureInfo = CultureInfo.CurrentCulture

        Try
            culture = New CultureInfo(sArg)
            Return culture
        Catch e As ArgumentException
            Console.Error.WriteLine(e.Message)
            Try
                culture = New CultureInfo(Int32.Parse(sArg, CultureInfo.CurrentCulture.NumberFormat), True)
                Return culture
            Catch ex As ArgumentException
                Console.Error.WriteLine(ex.Message)
                Return CultureInfo.CurrentCulture
            End Try
        End Try
    End Function 'getCultureFromArg

    Private m_sOutput As [String]
    Private m_Culture As CultureInfo
    Private m_fVerbose As Boolean
End Class 'CmdOpts 
' class CmdOpts
