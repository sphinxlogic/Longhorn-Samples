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
Imports System.Collections
Imports System.IO
Imports System.Text
Imports System.Globalization
Imports System.NaturalLanguage


Public Class Langcap

    Public Sub New(ByVal opts As CmdOpts)
        m_context = New Context()
        ' Set the following context properties 
        m_context.IsSpellChecking = False
    End Sub 'New


    Public Overloads Shared Function Main(ByVal args() As String) As Integer
        Dim retCode As Integer = 0

        Dim startTime As DateTime = DateTime.Now
        Dim endTime As DateTime = DateTime.Now
        Dim timeDiff As TimeSpan
        Dim opts As CmdOpts = Nothing
        Dim lc As Langcap = Nothing
        Dim outputWriter As StreamWriter = Nothing

        Try
            startTime = DateTime.Now
            opts = New CmdOpts()

            If opts.ProcessOpts(args) Then

                lc = New Langcap(opts)

                If opts.Output <> "" Then
                    If File.Exists(opts.Output) Then
                        File.Delete(opts.Output)
                    End If
                    outputWriter = New StreamWriter(opts.Output)
                Else
                    outputWriter = New StreamWriter(Console.OpenStandardOutput())
                End If
                If Not (opts.Culture Is Nothing) Then
                    lc.GetLangCapabilities(outputWriter, opts.Culture)
                End If
            End If
            retCode = 0
        Catch fe As FormatException
            If opts.Verbose Then
                Console.Error.WriteLine("Message: {0}", fe.Message)
                Console.Error.WriteLine("Stack Trace: {0}", fe.StackTrace)
            End If
        Catch nre As NullReferenceException
            If opts.Verbose Then
                Console.Error.WriteLine("Message: {0}", nre.Message)
                Console.Error.WriteLine("Stack Trace: {0}", nre.StackTrace)
            End If
            retCode = 1
        Catch e As ArgumentException
            If opts.Verbose Then
                Console.Error.WriteLine("Message: {0}", e.Message)
                Console.Error.WriteLine("Stack Trace: {0}", e.StackTrace)
            End If
            retCode = 1
        Finally
            endTime = DateTime.Now
            timeDiff = endTime - startTime

            If Not (outputWriter Is Nothing) Then
                If opts.Verbose Then
                    outputWriter.WriteLine()
                    outputWriter.WriteLine("{0,-50} {1}", "Total elapsed time:", timeDiff.ToString())
                End If
                outputWriter.Close()
            End If
        End Try

        Return retCode
    End Function 'Main


    Public Sub GetLangCapabilities(ByVal outputWriter As StreamWriter, ByVal culture As CultureInfo)
        Dim caps As LanguageCapabilities = m_context.GetCapabilitiesFor(culture)
        If caps.NotSupported Then
            outputWriter.WriteLine("{0} {1}", culture.ToString(), "Not Supported")
            Return
        Else
            outputWriter.WriteLine("{0} {1}:", culture.ToString(), "supported capabilites")
            outputWriter.WriteLine("{0,-40} {1}", "Lemmas:", getSupportedMessage(caps.SupportsLemmas))
            outputWriter.WriteLine("{0,-40} {1}", "Character Normalizations:", getSupportedMessage(caps.SupportsCharacterNormalizations))
            outputWriter.WriteLine("{0,-40} {1}", "DateTime Measure Named Entities:", getSupportedMessage(caps.SupportsDateTimeMeasureNamedEntities))
            outputWriter.WriteLine("{0,-40} {1}", "Person Named Entities:", getSupportedMessage(caps.SupportsPersonNamedEntities))
            outputWriter.WriteLine("{0,-40} {1}", "Location Named Entities:", getSupportedMessage(caps.SupportsLocationNamedEntities))
            outputWriter.WriteLine("{0,-40} {1}", "Organization Named Entities:", getSupportedMessage(caps.SupportsOrganizationNamedEntities))
            outputWriter.WriteLine("{0,-40} {1}", "Word Normalizations:", getSupportedMessage(caps.SupportsWordNormalizations))
            outputWriter.WriteLine("{0,-40} {1}", "Compounding:", getSupportedMessage(caps.SupportsCompounding))
            outputWriter.WriteLine("{0,-40} {1}", "Inflections:", getSupportedMessage(caps.SupportsInflections))
            outputWriter.WriteLine("{0,-40} {1}", "Chunking:", getSupportedMessage(caps.SupportsChunks))
            outputWriter.WriteLine("{0,-40} {1}", "SpellChecking:", getSupportedMessage(caps.SupportsSpellChecking))
        End If
    End Sub 'GetLangCapabilities

    Private Function getSupportedMessage(ByVal supported As Boolean) As String
        If supported Then
            Return "Supported"
        Else
            Return "Not Supported"
        End If
    End Function 'getSupportedMessage 
    Private m_context As Context
End Class 'Langcap ' class Langcap
