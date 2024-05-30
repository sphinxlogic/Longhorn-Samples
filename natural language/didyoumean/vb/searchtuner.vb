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
Imports System.Diagnostics
Imports System.Text
Imports System.NaturalLanguage
Imports System.Collections


Namespace Microsoft.Samples.NaturalLanguage
   '/ <summary>
   '/ Summary description for SearchTuner.
   '/ </summary>
   Public Class SearchTuner
      Private context As Context
      Private textChunk As TextChunk
      Private m_LinkFormat As String = "http://search.msn.com/results.aspx?q={0}&FORM=SMCRT"

      Public Sub New()

         context = New Context()
         context.IsSpellAlwaysSuggesting = True
         context.IsSpellChecking = True
         
         context.IsComputingLemmas = False
         
         context.IsSpellIgnoringAllUpperCase = True
         context.IsSpellIgnoringWordsWithNumbers = True
         
         Dim path As String = System.Windows.Forms.Application.ExecutablePath
         Dim index As Integer = path.LastIndexOf("\"c)
         If index > 0 Then
            path = path.Substring(0, index) + ""
            Dim fileInfo As New System.IO.FileInfo(path) '

            
            If fileInfo.Exists Then
               context.Lexicons = New Lexicon() {New Lexicon(path)}
            End If
         End If
         ' Need one text chunk per language
         textChunk = New TextChunk(context)
         
         textChunk.Culture = New System.Globalization.CultureInfo(1033)
      End Sub 'New

      Public Function SpellCheck(inputText As String) As String
         Dim builder As New StringBuilder(inputText.Length)
         Dim start As Integer = 0
         
         textChunk.InputText = inputText
         Dim sentence As Sentence
         For Each sentence In  textChunk
            Dim segment As Segment
            For Each segment In  sentence
               Dim list As IList = segment.Suggestions
               
               If Not (list Is Nothing) And list.Count > 0 Then
                  Dim range As TextRange = segment.Range
                  Dim errorStart As Integer = range.Start
                  
                  If errorStart >= start Then
                     builder.Append(inputText, start, errorStart - start)
                     builder.Append(list(0))
                     start = errorStart + range.Length
                  End If
               End If
            Next segment
         Next sentence 
         If start < inputText.Length Then
            builder.Append(inputText, start, inputText.Length - start)
         End If 
         Return builder.ToString()
      End Function 'SpellCheck
      
      
      Public Function NormalizeText(inputText As String) As String
         Dim stringBuilder As New StringBuilder()
         
         Dim character As Char
         For Each character In  inputText
            Select Case character
               Case " "c
                  stringBuilder.Append("+"c)
               
               Case "+"c
               
               Case Else
                        If character > " "c Or Asc(character) < 127 Then
                            stringBuilder.Append(character)
                        Else
                            stringBuilder.AppendFormat("%{0:X2}", Asc(character))
                        End If
            End Select
         Next character
         
         Return stringBuilder.ToString()
      End Function 'NormalizeText
      
      
      Public Property LinkFormat() As [String]
         Get
            Return m_LinkFormat
         End Get
         Set
            m_LinkFormat = value
         End Set
      End Property
      
      
      Public Function Link(textToSearchFor As String) As String
         Return [String].Format(System.Globalization.CultureInfo.CurrentUICulture, LinkFormat, NormalizeText(textToSearchFor))
      End Function 'Link
   End Class 'SearchTuner '
End Namespace 'Microsoft.Samples.NaturalLanguage
