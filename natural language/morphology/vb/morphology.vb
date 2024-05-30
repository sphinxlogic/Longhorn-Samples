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


Namespace Microsoft.Samples.NaturalLanguage
   Public Class MorphService
      
      Sub New(opts As CmdOpts)
         Dim context As New Context()
         
         ' Set the following context properties
         context.IsComputingLemmas = True
         context.IsComputingInflections = True
         context.IsComputingCompounds = True
         context.IsSpellChecking = False
         context.IsCheckingRepeatedWords = False
         context.IsShowingCharacterNormalizations = False
         context.IsShowingGaps = False
         context.IsShowingWordNormalizations = False
         context.IsSingleLanguage = False
         context.IsSpellAlwaysSuggesting = False
         context.IsSpellIgnoringAllUpperCase = False
         context.IsSpellIgnoringWordsWithNumbers = False
         context.IsSpellPreReform = False
         context.IsSpellRequiringAccentedCapitals = False
         context.IsFindingDateTimeMeasures = False
         context.IsFindingLocations = False
         context.IsFindingOrganizations = False
         context.IsFindingPersons = False
         context.IsFindingPhrases = False
         m_fInteractiveMode = False
         m_TextChunk = New TextChunk(context)
         m_TextChunk.Culture = opts.Culture
         m_culture = opts.Culture
         m_opts = opts
      End Sub 'New
      
     
      Overloads Public Shared Function Main(args() As String) As Integer
         Dim retCode As Integer = 0
         Dim startTime As DateTime = DateTime.Now
         Dim endTime As DateTime = DateTime.Now
         Dim timeDiff As TimeSpan
         Dim opts As CmdOpts = Nothing
         Dim ms As MorphService = Nothing
         Dim outputWriter As TextWriter = Nothing
         Dim inputReader As TextReader = Nothing
         Dim input As String = ""
         
         Try
            startTime = DateTime.Now
            opts = New CmdOpts()
            If opts.ProcessOpts(args) Then
               ms = New MorphService(opts)
               If opts.Output.Length <> 0 Then
                  If File.Exists(opts.Output) Then
                     File.Delete(opts.Output)
                  End If 
                  outputWriter = New StreamWriter(opts.Output)
               Else
                  outputWriter = New StreamWriter(Console.OpenStandardOutput())
               End If
               
               If opts.Input.Length <> 0 Then
                  ms.Parse(opts.Input, outputWriter)
               Else
                  ' Interactive Console Mode
                  ms.InteractiveMode = True
                  ms.PrintBanner(outputWriter)
                  While ms.InteractiveMode
                     outputWriter.Write("nlg> ")
                     outputWriter.Flush()
                     input = Console.ReadLine()
                     outputWriter.Flush()
                     If [String].Compare(input, "stat", True, CultureInfo.InvariantCulture) = 0 Then
                        ms.ShowStatistics(outputWriter)
                     Else
                        If [String].Compare(input, "quit", True, CultureInfo.InvariantCulture) = 0 Then
                           outputWriter.WriteLine("Goodbye!!")
                           ms.InteractiveMode = False
                        Else
                           If [String].Compare(input, "help", True, CultureInfo.InvariantCulture) = 0 Then
                              ms.PrintHelp(outputWriter)
                           Else
                              If [String].Compare(input, "clear", True, CultureInfo.InvariantCulture) = 0 Then
                                 ms.ClearStatistics()
                              Else
                                 inputReader = New StringReader(input)
                                 ms.Parse(inputReader, outputWriter)
                              End If
                           End If
                        End If
                     End If
                  End While
               End If
            End If
            
            retCode = 0
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
               outputWriter.Close()
            End If 
            If Not (inputReader Is Nothing) Then
               inputReader.Close()
            End If
         End Try
         Return retCode
      End Function 'Main
      
      
      Public Sub PrintBanner(outputWriter As TextWriter)
         outputWriter.WriteLine("NLG Interactive Console")
         outputWriter.WriteLine("Copyright (c) 2004 Microsoft Corporation, Inc. All Rights Reserved")
         outputWriter.WriteLine("Type 'help' for more information")
         outputWriter.Flush()
      End Sub 'PrintBanner
      
      
      Public Sub PrintHelp(outputWriter As TextWriter)
         outputWriter.WriteLine("Text entered at the prompt will be analyzed by the NLG Engine")
         outputWriter.WriteLine("The following commands are supported in interactive console mode:")
         outputWriter.WriteLine("  STAT       Shows session statistics")
         outputWriter.WriteLine("  QUIT       Exits from interactive mode")
         outputWriter.WriteLine("  HELP       Prints this help message")
         outputWriter.WriteLine("  CLEAR      Clears session statistics")
         outputWriter.Flush()
      End Sub 'PrintHelp
      
      
      Overloads Public Sub Parse(inputPath As [String], outputWriter As TextWriter)
         Dim inputReader As StreamReader = Nothing
         
         If outputWriter Is Nothing Then
            Return
         End If 
         Try
            If inputPath.Length <> 0 Then
               If File.Exists(inputPath) Then
                  Dim f As New FileInfo(inputPath)
                  Dim fattrib As FileAttributes = f.Attributes
                  
                  'Skip empty files, hidden files, system files and temporary files
                  If f.Length > 0 And fattrib <>(fattrib Or FileAttributes.Hidden) And fattrib <>(fattrib Or FileAttributes.System) And fattrib <>(fattrib Or FileAttributes.Temporary) Then
                     inputReader = New StreamReader(inputPath)
                  End If
               Else
                  If Directory.Exists(inputPath) Then
                     parseDir(inputPath, outputWriter)
                  Else
                     Throw New ArgumentException("Specified Input File does not exist")
                  End If
               End If
            Else
               inputReader = New StreamReader(Console.OpenStandardInput())
            End If
            
            If Not (inputReader Is Nothing) Then
               Me.Parse(inputReader, outputWriter)
            End If
         Catch ae As ArgumentException
            Throw ae
         Finally
            If Not (inputReader Is Nothing) Then
               inputReader.Close()
            End If
         End Try
      End Sub 'Parse
       
      Overloads Public Sub Parse(inputReader As TextReader, outputWriter As TextWriter)
         m_iNumFilesParsed += 1
         
         Dim input As String = inputReader.ReadToEnd()
         
         m_TextChunk.InputText = input
         
         Dim numSentencesParsed As Integer = 0
         Dim numSegmentsParsed As Integer = 0
         
         Dim sentence As Sentence
         For Each sentence In  m_TextChunk
            m_iNumSentencesParsed += 1
            numSentencesParsed += 1
            outputWriter.WriteLine(sentence.ToString())
            Dim segment As Segment
            For Each segment In  sentence
               m_iNumSegmentsParsed += 1
               numSegmentsParsed += 1
               emitSegmentInfo(outputWriter, segment)
            Next segment
         Next sentence
         
         If m_fInteractiveMode And m_opts.Verbose Then
            outputWriter.WriteLine("{0,-30} {1}", "Num. Sentences:", numSentencesParsed)
            outputWriter.WriteLine("{0,-30} {1}", "Num. Segments:", numSegmentsParsed)
            outputWriter.WriteLine()
            outputWriter.Flush()
         End If
      End Sub 'Parse
      
      
      Public Sub ShowStatistics(outputWriter As TextWriter)
         If m_fInteractiveMode Then
            outputWriter.WriteLine("{0,-30} {1}", "Num. Text Blocks:", m_iNumFilesParsed)
         Else
            outputWriter.WriteLine("{0,-30} {1}", "Num. Files:", m_iNumFilesParsed)
         End If
         
         outputWriter.WriteLine("{0,-30} {1}", "Num. Sentences:", m_iNumSentencesParsed)
         outputWriter.WriteLine("{0,-30} {1}", "Num. Segments:", m_iNumSegmentsParsed)
         outputWriter.WriteLine()
         outputWriter.Flush()
      End Sub 'ShowStatistics
      
      
      Public Sub ClearStatistics()
         m_iNumFilesParsed = 0
         m_iNumSentencesParsed = 0
         m_iNumSegmentsParsed = 0
      End Sub 'ClearStatistics
      
      
      Public Property InteractiveMode() As Boolean
         Get
            Return m_fInteractiveMode
         End Get
         Set
            m_fInteractiveMode = value
         End Set
      End Property 
      Private m_culture As CultureInfo
      
      Private m_TextChunk As TextChunk
      
      Private m_opts As CmdOpts
      
      
      Private Sub parseDir(inputDir As [String], outputWriter As TextWriter)
         ' Process the list of files found in the target directory
         Dim fileEntries As String() = Directory.GetFiles(inputDir)
         
         Dim i As Integer
         For i = 0 To fileEntries.Length - 1
            Me.Parse(fileEntries(i), outputWriter)
         Next i
         
         ' Recurse into subdirectories of the target directory
         Dim subdirectoryEntries As String() = Directory.GetDirectories(inputDir)
         
         For i = 0 To subdirectoryEntries.Length - 1
            parseDir(subdirectoryEntries(i), outputWriter)
         Next i
      End Sub 'parseDir
      
      
      Private Sub emitSegmentInfo(outputWriter As TextWriter, segment As Segment)
         outputWriter.WriteLine(segment.ToString())
         outputWriter.WriteLine(" [Lemmas: {0}]", segment.Lemmas.Count)
         Dim i As Integer
         For i = 0 To segment.Lemmas.Count - 1
            outputWriter.WriteLine("  {0}", segment.Lemmas(i).ToString())
         Next i
         
         outputWriter.WriteLine(" [Inflections: {0}]", segment.Inflections.Count)
         For i = 0 To segment.Inflections.Count - 1
            outputWriter.WriteLine("  {0}", segment.Inflections(i).ToString())
         Next i
      End Sub 'emitSegmentInfo
      
      Private m_fInteractiveMode As Boolean
      
      Private m_iNumFilesParsed As Integer
      
      Private m_iNumSentencesParsed As Integer
      
      Private m_iNumSegmentsParsed As Integer
   End Class ' class MorphService
End Namespace 'Microsoft.Samples.NaturalLanguage