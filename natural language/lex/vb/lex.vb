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
Imports System.Collections
Imports System.NaturalLanguage
Imports System.Text
Imports System.Globalization


Namespace Microsoft.Samples.NaturalLanguage
    
   Class LexiconConsole
      

      '/ <summary>
      '/ The main entry point for the Console.
      '/ </summary>
      Overloads Public Shared Function Main(args() As String) As Integer
         Dim lexSample As LexiconExamples = Nothing
         Dim cmdOpts As CmdOpts = Nothing
         Dim outputWriter As StreamWriter = Nothing
         
         Try
            'Process commandline arguments
            cmdOpts = New CmdOpts()
            If Not cmdOpts.ProcessOpts(args) Then
               Throw New ArgumentException("Parsing commandline arguments failed.")
            End If
            
            lexSample = New LexiconExamples()
            If Not (cmdOpts.OutputFile Is Nothing) Then
               outputWriter = New StreamWriter(cmdOpts.OutputFile)
            Else
               outputWriter = New StreamWriter(Console.OpenStandardOutput())
            End If
            
            lexSample.DisplayWords(outputWriter, cmdOpts.InputFile, cmdOpts.Word, cmdOpts.MaxWordsToDisplay)
            Return 0
         Catch nre As System.NullReferenceException
            If cmdOpts.Verbose Then
               Console.Error.WriteLine("Message: {0}", nre.Message)
               Console.Error.WriteLine("Stack Trace: {0}", nre.StackTrace)
            End If
            Return 1
         Catch ae As System.ArgumentException
            If cmdOpts.Verbose Then
               Console.Error.WriteLine("Message: {0}", ae.Message)
               Console.Error.WriteLine("Stack Trace: {0}", ae.StackTrace)
            End If
            
            Return 1
         Finally
            If Not (outputWriter Is Nothing) Then
               outputWriter.Close()
            End If
         End Try
      End Function 'Main
   End Class 'LexiconConsole
    
   Public Class LexiconExamples
      
      Public Sub New()
      End Sub 'New
      
      
      Protected Sub DisplayWordProps(sw As StreamWriter, props As IDictionary, prefix As String)
         Dim pair As DictionaryEntry
         For Each pair In  props
            sw.Write("{0}name: {1}", prefix, pair.Key.ToString())
            
            Dim subProps As IDictionary = Nothing
            Dim subList As ICollection = Nothing
            
            If TypeOf pair.Value Is IDictionary Then
               subProps = CType(pair.Value, IDictionary)
               sw.WriteLine()
               DisplayWordProps(sw, subProps, prefix + "  ")
            Else
               If TypeOf pair.Value Is ICollection Then
                  subList = CType(pair.Value, ICollection)
                        Dim prefix2 As String = prefix & "  "
                  
                  sw.WriteLine()
                  Dim member As Object
                  For Each member In  subList
                     sw.WriteLine("{0} value: {1}", prefix2, member.ToString())
                  Next member
               Else
                  sw.WriteLine(" value: {0}", pair.Value.ToString())
               End If
            End If
         Next pair 
         sw.Flush()
      End Sub 'DisplayWordProps
      
      
      Public Sub DisplayWords(sw As StreamWriter, inputFile As String, word As String, maxWordsToDisplay As Integer)
         Dim lexEnum As LexiconEntryEnumerator = Nothing
         Dim lex As Lexicon = Nothing
         
         If sw Is Nothing Or inputFile Is Nothing Or word Is Nothing Or maxWordsToDisplay <= 0 Then
            Return
         End If 
         Try
            If File.Exists(inputFile) Then
               'Create lexicon based on the lexicon file specified in the commandline
               lex = New Lexicon(inputFile)
               
               'Get a LexiconEntryEnumerator
               lexEnum = CType(lex.GetEnumerator(), LexiconEntryEnumerator)
               
               'Move to the target word if it is specified in the commandline
               If word.Length <> 0 Then
                  Dim id As Integer = lex.IndexOf(word)
                  sw.WriteLine("  IndexOf({0}) = ID:{1}", word, id)
                  If lexEnum.MoveTo(word) = False Then
                     Throw New ArgumentException("Could not position to " + word)
                  End If
                  sw.WriteLine("  MoveTo({0})  = ID:{1}", word, lexEnum.Index)
                  sw.Flush()
                  
               End If
            
            
            Else
               Throw New ArgumentException(inputFile + " does not exist.")
            End If
         Catch 
	   Throw
         Finally
            If Not (lex Is Nothing) Then
               lex.Dispose()
            End If
         End Try 
      End Sub 'DisplayWords
   End Class 'LexiconExamples
    _ 
   
End Namespace 'Microsoft.NaturalLanguage.Samples 

