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
Imports System.IO
Imports System.Globalization

Namespace Microsoft.Samples.NaturalLanguage
   Public Class CmdOpts
      Private m_strInputFile As String
      Private m_strOutputFile As String
      Private m_strWord As String
      Private m_iMaxWordsToDisplay As Integer = 1
      Private m_fVerbose As Boolean = False
  
      Public ReadOnly Property InputFile() As String
         Get
            Return m_strInputFile
         End Get
      End Property 
      
      Public ReadOnly Property OutputFile() As String
         Get
            Return m_strOutputFile
         End Get
      End Property 
      
      Public ReadOnly Property Word() As String
         Get
            Return m_strWord
         End Get
      End Property 
      
      Public ReadOnly Property MaxWordsToDisplay() As Integer
         Get
            Return m_iMaxWordsToDisplay
         End Get
      End Property 
      
      Public ReadOnly Property Verbose() As Boolean
         Get
            Return m_fVerbose
         End Get
      End Property
       
      Public Function ProcessOpts(arguments() As String) As Boolean
         If arguments.Length = 0 Then
            DisplayHelp()
            Return False
         End If
         Dim iargc As Integer
         
         While iargc < arguments.Length
                If arguments(iargc).StartsWith("-") Then
                    Dim thisarg As String = arguments(iargc).Remove(0, 1).ToLower(CultureInfo.CurrentCulture)

                    Select Case thisarg
                        Case "inputfile", "i"
                            iargc = iargc + 1
                            If iargc >= arguments.Length Then
                                ' make sure there's a next arg to move on to
                                Throw New System.Exception("No Value for " + arguments((iargc - 1)))
                            End If

                            ' assign to the next arg
                            m_strInputFile = arguments(iargc)

                        Case "outputfile", "o"
                            iargc = iargc + 1
                            If iargc >= arguments.Length Then
                                ' make sure there's a next arg to move on to
                                Throw New System.Exception("No Value for " + arguments((iargc - 1)))
                            End If

                            ' assign to the next arg
                            m_strOutputFile = arguments(iargc)

                        Case "word", "w"
                            iargc = iargc + 1
                            If iargc >= arguments.Length Then
                                ' make sure there's a next arg to move on to
                                Throw New System.Exception("No Value for " + arguments((iargc - 1)))
                            End If

                            ' assign to the next arg
                            m_strWord = arguments(iargc)

                        Case "maxwordstodisplay", "n"
                            iargc = iargc + 1
                            If iargc >= arguments.Length Then
                                ' make sure there's a next arg to move on to
                                Throw New System.Exception("No Value for " + arguments((iargc - 1)))
                            End If

                            ' assign to the next arg
                            m_iMaxWordsToDisplay = Int32.Parse(arguments(iargc), CultureInfo.CurrentCulture.NumberFormat)

                        Case "verbose", "v"
                            m_fVerbose = True

                        Case "help", "?"
                            DisplayHelp()
                    End Select
                End If
	        iargc = iargc + 1
            End While
         ' for
         Return True
      End Function 'ProcessOpts
       ' ProcessOpts
      
      
      Public Sub DisplayHelp()

         
         DisplayHelpLine("i", "InputFile", "The Lexicon (.lex) file to use")
         DisplayHelpLine("o", "OutputFile", "The output file (Defaults to Console)")
         DisplayHelpLine("w", "Word", "The word to move to")
         DisplayHelpLine("n", "MaxWordsToDisplay", "The number of words to display")
         DisplayHelpLine("v", "Verbose", "Verbose output")

      End Sub 'DisplayHelp
       ' DisplayHelp
      Private Sub DisplayHelpLine(shortname As String, name As String, description As String)
         Console.WriteLine(" -{0,1} {1,-24} {2}", shortname, name, description)
      End Sub 'DisplayHelpLine
   End Class ' class CmdOpts
End Namespace 'Microsoft.NaturalLanguage.Samples

' namespace Samples
