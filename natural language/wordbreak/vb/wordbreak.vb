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
   Public Class WordBreaker
    
      Public Sub New(opts As CmdOpts)
         ' Initialize members
         m_iNumSentences = 0
         m_iNumSegments = 0
         m_iNumFiles = 0
         m_iMaxWidth = 80
         
         ' Create a context object
         Dim context As New Context()
         ' Set the following context properties to false
         context.IsSpellChecking = False
         context.IsCheckingRepeatedWords = False
         context.IsComputingInflections = False
         context.IsShowingCharacterNormalizations = False
         context.IsShowingGaps = False
         context.IsShowingWordNormalizations = False
         context.IsSingleLanguage = False
         context.IsSpellAlwaysSuggesting = False
         context.IsSpellIgnoringAllUpperCase = False
         context.IsSpellIgnoringWordsWithNumbers = False
         context.IsSpellPreReform = False
         context.IsSpellRequiringAccentedCapitals = False
         ' Set the following context properties based upon command line arguments
         context.IsComputingLemmas = opts.Lemma
         context.IsFindingDateTimeMeasures = opts.IsFindingDateTimeMeasures
         context.IsFindingLocations = opts.IsFindingLocations
         context.IsFindingOrganizations = opts.IsFindingOrganizations
         context.IsFindingPersons = opts.IsFindingPersons
         context.IsFindingPhrases = opts.IsFindingPhrases
         context.IsComputingCompounds = opts.Compounds
         m_fInteractiveMode = False
         ' Create a textchunk object and set locale based based upon command line argument
         m_TextChunk = New TextChunk(context)
         m_TextChunk.Culture = opts.Culture
         m_culture = opts.Culture
      End Sub 'New
      
      Overloads Public Shared Function Main(args() As String) As Integer
         Dim opts As CmdOpts = Nothing
         Dim wb As WordBreaker = Nothing
         Dim input As String
         Dim inputReader As TextReader = Nothing
         Dim outputWriter As TextWriter = Nothing
         Dim retCode As Integer = 0
         
         Dim startTime As DateTime = DateTime.Now
         Dim endTime As DateTime = DateTime.Now
         Dim timeDiff As TimeSpan
         
         Try
            opts = New CmdOpts()
            If opts.ProcessOpts(args) Then
               wb = New WordBreaker(opts)
               If opts.Output.Length <> 0 Then
                  If File.Exists(opts.Output) Then
                     File.Delete(opts.Output)
                  End If
                  outputWriter = New StreamWriter(opts.Output)
               Else
                  outputWriter = New StreamWriter(Console.OpenStandardOutput())
               End If
               wb.MaxWidth = Console.WindowWidth
               If opts.Input.Length <> 0 Then
                  wb.Parse(opts.Input, outputWriter)
               Else
                  wb.InteractiveMode = True
                  wb.PrintBanner(outputWriter)
                  While wb.InteractiveMode
                     outputWriter.Write("nlg> ")
                     outputWriter.Flush()
                     
                     input = Console.ReadLine()
                     
                     If [String].Compare(input, "stat", True, CultureInfo.InvariantCulture) = 0 Then
                        wb.ShowStatistics(outputWriter)
                     Else
                        If [String].Compare(input, "quit", True, CultureInfo.InvariantCulture) = 0 Then
                           outputWriter.WriteLine("Goodbye!!")
                           wb.InteractiveMode = False
                        Else
                           If [String].Compare(input, "help", True, CultureInfo.InvariantCulture) = 0 Then
                              wb.PrintHelp(outputWriter)
                           Else
                              If [String].Compare(input, "clear", True, CultureInfo.InvariantCulture) = 0 Then
                                 wb.ClearStatistics()
                              Else
                                 inputReader = New StringReader(input)
                                 wb.Parse(inputReader, outputWriter)
                              End If
                           End If
                        End If
                     End If
                  End While
               End If
            End If
            
            retCode = 0
         Catch nre As NullReferenceException
            Console.Error.WriteLine("Message: {0}", nre.Message)
            Console.Error.WriteLine("Stack Trace: {0}", nre.StackTrace)
            retCode = 1
         Catch e As ArgumentException
            Console.Error.WriteLine("Message: {0}", e.Message)
            Console.Error.WriteLine("Stack Trace: {0}", e.StackTrace)
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
      
      
      Public Property MaxWidth() As Integer
         Get
            Return m_iMaxWidth
         End Get
         Set
            m_iMaxWidth = value
         End Set
      End Property 
      
      Public Property InteractiveMode() As Boolean
         Get
            Return m_fInteractiveMode
         End Get
         Set
            m_fInteractiveMode = value
         End Set
      End Property 
      
      Public ReadOnly Property NumFiles() As Integer
         Get
            Return m_iNumFiles
         End Get
      End Property 
      
      Public ReadOnly Property NumSegments() As Integer
         Get
            Return m_iNumSegments
         End Get
      End Property 
      
      Public ReadOnly Property NumSentences() As Integer
         Get
            Return m_iNumSentences
         End Get
      End Property
       
      Public Sub PrintBanner(outputWriter As TextWriter)
         outputWriter.WriteLine("NLG Interactive Console")
         outputWriter.WriteLine("Copyright (c) 2004 Microsoft Corporation, Inc. All Rights Reserved")
         outputWriter.WriteLine("Type 'help' for more information")
         outputWriter.Flush()
      End Sub 'PrintBanner
      
      Public Sub ShowStatistics(outputWriter As TextWriter)
         If m_fInteractiveMode Then
            outputWriter.WriteLine("{0,-30} {1}", "Num. Text Blocks Parsed:", m_iNumFiles)
         Else
            outputWriter.WriteLine("{0,-30} {1}", "Num. Files Parsed:", m_iNumFiles)
         End If
         outputWriter.WriteLine("{0,-30} {1}", "Num. Sentences Parsed:", m_iNumSentences)
         outputWriter.WriteLine("{0,-30} {1}", "Num. Segments Parsed:", m_iNumSegments)
         outputWriter.WriteLine()
         outputWriter.Flush()
      End Sub 'ShowStatistics
      
      
      Public Sub PrintHelp(outputWriter As TextWriter)
         outputWriter.WriteLine("Text entered at the prompt will be analyzed by the NLG Engine")
         outputWriter.WriteLine("The following commands are supported in interactive console mode:")
         outputWriter.WriteLine("  STAT       Shows session statistics")
         outputWriter.WriteLine("  QUIT       Exits from interactive mode")
         outputWriter.WriteLine("  HELP       Prints this help message")
         outputWriter.WriteLine("  CLEAR      Clears session statistics")
         outputWriter.Flush()
      End Sub 'PrintHelp
      
      Public Sub ClearStatistics()
         m_iNumFiles = 0
         m_iNumSentences = 0
         m_iNumSegments = 0
      End Sub 'ClearStatistics
      
      
      Overloads Public Sub Parse(inputPath As String, outputWriter As TextWriter)
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
               Parse(inputReader, outputWriter)
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
         m_iNumFiles += 1
         Dim padding As String = ""
         Dim input As String = inputReader.ReadToEnd()
         Dim numSentences As Integer = 0
         Dim numSegments As Integer = 0
         m_TextChunk.InputText = input
         Dim sentence As Sentence
         For Each sentence In  m_TextChunk
            m_iNumSentences += 1
            numSentences += 1
            padding = ""
            outputWriter.WriteLine("Sentence #{0}: {1}", numSentences, sentence.ToString())
            outputWriter.WriteLine("(Culture={0}, StartPos={1}, Len={2}, EOP={3})", sentence.Culture.ToString(), sentence.Range.Start.ToString(System.Globalization.CultureInfo.CurrentCulture.NumberFormat), sentence.Range.Length.ToString(System.Globalization.CultureInfo.CurrentCulture.NumberFormat), sentence.IsEndOfParagraph.ToString())
            Dim segment As Segment
            For Each segment In  sentence
               m_iNumSegments += 1
               numSegments += 1
               emitSegmentInfo(segment, outputWriter, padding)
            Next segment
         Next sentence
         outputWriter.WriteLine()
         outputWriter.Flush()
      End Sub 'Parse
      
      
      Private Sub parseDir(inputDir As String, outputWriter As TextWriter)
         
         ' Process the list of files found in the target directory
         Dim fileEntries As String() = Directory.GetFiles(inputDir)
         
         Dim i As Integer
         For i = 0 To fileEntries.Length - 1
            Parse(fileEntries(i), outputWriter)
         Next i
         
         ' Recurse into subdirectories of the target directory
         Dim subdirectoryEntries As String() = Directory.GetDirectories(inputDir)
         
         For i = 0 To subdirectoryEntries.Length - 1
            parseDir(subdirectoryEntries(i), outputWriter)
         Next i
      End Sub 'parseDir
      
      Private Sub emitSegmentInfo(segment As System.NaturalLanguage.Segment, outputWriter As TextWriter, padding As String)
         Dim sb As New StringBuilder()
         Dim segmentInfo As String = ""
         Dim format As String = ""
         Dim format2 As String = ""
         
         sb.Append(" (")
         If segment.IsFeminine Then
            sb.Append("Fem., ")
         End If
         If segment.IsMasculine Then
            sb.Append("Masc., ")
         End If
         If segment.IsNeuter Then
            sb.Append("Neuter, ")
         End If
         If segment.IsNoun Then
            sb.Append("N., ")
         End If
         If segment.IsVerb Then
            sb.Append("V., ")
         End If
         If segment.IsPronoun Then
            sb.Append("Pron., ")
         End If
         If segment.IsModalVerb Then
            sb.Append("Modal V., ")
         End If
         If segment.IsAbbreviation Then
            sb.Append("Abbrev., ")
         End If
         If segment.IsAdjective Then
            sb.Append("Adj., ")
         End If
         If segment.IsAdverb Then
            sb.Append("Adv., ")
         End If
         If segment.IsAuxiliaryVerb Then
            sb.Append("Aux. Verb, ")
         End If
         If segment.IsCharacter Then
            sb.Append("Char., ")
         End If
         If segment.IsConjunction Then
            sb.Append("Conj., ")
         End If
         If segment.IsPreposition Then
            sb.Append("Prep., ")
         End If
         If segment.IsSingular Then
            sb.Append("Sing., ")
         End If
         If segment.IsPlural Then
            sb.Append("Plural, ")
         End If
         If segment.IsCharacter Then
            sb.Append("Char., ")
         End If
         If segment.IsEndPunctuation Or segment.IsPunctuation Then
            sb.Append("Punc., ")
         End If
         If segment.IsFutureTense Then
            sb.Append("Future, ")
         End If
         If segment.IsPresentTense Then
            sb.Append("Present, ")
         End If
         If segment.IsPastTense Then
            sb.Append("Past, ")
         End If
         If segment.IsFirstPerson Then
            sb.Append("1st person, ")
         End If
         If segment.IsThirdPerson Then
            sb.Append("3rd person, ")
         End If
         If segment.IsSmiley Then
            sb.Append("Smiley, ")
         End If
         If segment.IsInterjection Then
            sb.Append("Interjection, ")
         End If
         sb.Append([Enum].Format(GetType(System.NaturalLanguage.RangeRole), segment.Role, "G"))
         sb.Append(")")
         
         segmentInfo = sb.ToString()
         
         format = "{0}{1} {2}"
         
         outputWriter.WriteLine(format, padding, segment.ToString(), segmentInfo)
         format = "{0}   {1}"
         format2 = "{0}    {1}"
         
         If segment.Alternatives.Count > 0 Then
            outputWriter.WriteLine(format, padding, [String].Concat("[Alternatives: ", segment.Alternatives.Count.ToString(CultureInfo.CurrentCulture.NumberFormat), "]"))
            Dim alternative As Segment
            For Each alternative In  segment.Alternatives
               outputWriter.WriteLine(format2, padding, alternative.ToString())
            Next alternative
         End If
         
         If segment.CharacterNormalizations.Count > 0 Then
            outputWriter.WriteLine(format, padding, [String].Concat("[Character Normalizations: ", segment.CharacterNormalizations.Count.ToString(CultureInfo.CurrentCulture.NumberFormat), "]"))
            Dim normalization As Segment
            For Each normalization In  segment.CharacterNormalizations
               outputWriter.WriteLine(format2, padding, normalization.ToString())
            Next normalization
         End If
         
         If segment.Inflections.Count > 0 Then
            outputWriter.WriteLine(format, padding, [String].Concat("[Inflections: ", segment.Inflections.Count.ToString(CultureInfo.CurrentCulture.NumberFormat), "]"))
            Dim inflection As Segment
            For Each inflection In  segment.Inflections
               outputWriter.WriteLine(format2, padding, inflection.ToString())
            Next inflection
         End If
         
         If segment.Lemmas.Count > 0 Then
            outputWriter.WriteLine(format, padding, [String].Concat("[Lemmas: ", segment.Lemmas.Count.ToString(CultureInfo.CurrentCulture.NumberFormat), "]"))
            Dim lemma As Segment
            For Each lemma In  segment.Lemmas
               outputWriter.WriteLine(format2, padding, lemma.ToString())
            Next lemma
         End If
         
         If segment.Representations.Count > 0 Then
            outputWriter.WriteLine(format, padding, [String].Concat("[Representations: ", segment.Representations.Count.ToString(CultureInfo.CurrentCulture.NumberFormat), "]"))
            Dim reps As Object
            For Each reps In  segment.Representations
               outputWriter.WriteLine(format2, padding, reps.ToString())
            Next reps
         End If
         
         If segment.SpellingVariations.Count > 0 Then
            outputWriter.WriteLine(format, padding, [String].Concat("[SpellingVariations: ", segment.SpellingVariations.Count.ToString(CultureInfo.CurrentCulture.NumberFormat), "]"))
            
            Dim variation As Segment
            For Each variation In  segment.SpellingVariations
               outputWriter.WriteLine(format2, padding, variation)
            Next variation
         End If
         
         If segment.Suggestions.Count > 0 Then
            outputWriter.WriteLine(format, padding, [String].Concat("[Suggestions: ", segment.Suggestions.Count.ToString(CultureInfo.CurrentCulture.NumberFormat), "]"))
            
            Dim segSuggestion As Segment
            For Each segSuggestion In  segment.Suggestions
               outputWriter.WriteLine(format2, padding, segSuggestion.ToString())
            Next segSuggestion
         End If
         
         
         If segment.SubSegments.Count > 0 Then
            outputWriter.WriteLine(format, padding, [String].Concat("[SubSegments: ", segment.SubSegments.Count.ToString(CultureInfo.CurrentCulture.NumberFormat), "]"))
            padding = [String].Concat(padding, "    ")
            Dim subSegment As Segment
            For Each subSegment In  segment.SubSegments
               emitSegmentInfo(subSegment, outputWriter, padding)
            Next subSegment
         End If
         
         outputWriter.Flush()
      End Sub 'emitSegmentInfo
      
      Private m_culture As CultureInfo
      Private m_TextChunk As TextChunk
      
      Private m_iNumSentences As Integer
      Private m_iNumSegments As Integer
      Private m_iNumFiles As Integer
      Private m_iMaxWidth As Integer
      
      Private m_fInteractiveMode As Boolean
   End Class 'WordBreaker 
End Namespace 'Microsoft.Samples.NaturalLanguage ' class WordBreaker
' namespace


