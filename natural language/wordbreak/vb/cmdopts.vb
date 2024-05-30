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

Namespace Microsoft.Samples.NaturalLanguage
   Public Class CmdOpts
      Private m_strInput As String = ""
      Private m_strOutput As String = ""
      Private m_fNamedEntity As Boolean = False
      Private m_fLemma As Boolean = False
      Private m_fCompounds As Boolean = False
      Private m_Culture As System.Globalization.CultureInfo = Nothing
      Private m_fVerbose As Boolean = False
      Private m_fIsFindingDateTimeMeasures As Boolean = False
      Private m_fIsFindingPersons As Boolean = False
      Private m_fIsFindingPhrases As Boolean = False
      Private m_fIsFindingOrganizations As Boolean = False
      Private m_fIsFindingLocations As Boolean = False
      
      
      Public Property Input() As String
         Get
            Return m_strInput
         End Get
         Set
            m_strInput = value
         End Set
      End Property 
      
      Public Property Output() As String
         Get
            Return m_strOutput
         End Get
         Set
            m_strOutput = value
         End Set
      End Property 
      
      Public Property NamedEntity() As Boolean
         Get
            Return m_fNamedEntity
         End Get
         Set
            m_fNamedEntity = value
         End Set
      End Property 
      
      Public Property Lemma() As Boolean
         Get
            Return m_fLemma
         End Get
         Set
            m_fLemma = value
         End Set
      End Property 
      
      Public Property Compounds() As Boolean
         Get
            Return m_fCompounds
         End Get
         Set
            m_fCompounds = value
         End Set
      End Property 
      
      Public Property Culture() As System.Globalization.CultureInfo
         Get
            Return m_Culture
         End Get
         Set
            m_Culture = value
         End Set
      End Property 
      
      Public Property Verbose() As Boolean
         Get
            Return m_fVerbose
         End Get
         Set
            m_fVerbose = value
         End Set
      End Property 
      
      Public Property IsFindingDateTimeMeasures() As Boolean
         Get
            Return m_fIsFindingDateTimeMeasures
         End Get
         Set
            m_fIsFindingDateTimeMeasures = value
         End Set
      End Property 
      
      Public Property IsFindingPersons() As Boolean
         Get
            Return m_fIsFindingPersons
         End Get
         Set
            m_fIsFindingPersons = value
         End Set
      End Property 
      
      Public Property IsFindingPhrases() As Boolean
         Get
            Return m_fIsFindingPhrases
         End Get
         Set
            m_fIsFindingPhrases = value
         End Set
      End Property 
      
      Public Property IsFindingOrganizations() As Boolean
         Get
            Return m_fIsFindingOrganizations
         End Get
         Set
            m_fIsFindingOrganizations = value
         End Set
      End Property 
      
      Public Property IsFindingLocations() As Boolean
         Get
            Return m_fIsFindingLocations
         End Get
         Set
            m_fIsFindingLocations = value
         End Set
      End Property
       
      Function GetCultureFromArgument(argument As String) As System.Globalization.CultureInfo
         
         Dim culture As System.Globalization.CultureInfo = System.Globalization.CultureInfo.CurrentCulture
         
         Try
            culture = New System.Globalization.CultureInfo(argument)
            Return culture
         Catch e As ArgumentException
            Console.Error.WriteLine(e.Message)
            Try
               culture = New System.Globalization.CultureInfo(Int32.Parse(argument, System.Globalization.CultureInfo.CurrentCulture.NumberFormat), True)
               Return culture
            Catch ex As ArgumentException
               Console.Error.WriteLine(ex.Message)
               Return System.Globalization.CultureInfo.CurrentCulture
            End Try
         End Try
      End Function 'getCultureFromArg
      
      
      Public Function ProcessOpts(arguments() As String) As Boolean
         Dim i As Integer
         
         While i < arguments.Length
            If arguments(i).StartsWith("-") Then
               Dim thisarg As String = arguments(i).Remove(0, 1).ToLower(System.Globalization.CultureInfo.CurrentCulture)
               Select Case thisarg
                  
                  Case "input", "i"
                  	 i = i + 1
                     If i >= arguments.Length Then
                        ' make sure there's a next arg to move on to
                        Throw New System.ArgumentException("No Value for " + arguments((i - 1)))
                     End If
                     ' assign to the next arg
                     m_strInput = arguments(i)
                  Case "output", "o"
                     i = i + 1
                     If i >= arguments.Length Then
                        ' make sure there's a next arg to move on to
                        Throw New System.ArgumentException("No Value for " + arguments((i - 1)))
                     End If
                     ' assign to the next arg
                     m_strOutput = arguments(i)
                  Case "namedentity", "ne"
                     m_fNamedEntity = True
                  Case "lemma", "le"
                     m_fLemma = True
                  Case "compounds", "co"
                     m_fCompounds = True
                  Case "culture", "c"
                     i = i + 1
                     If i >= arguments.Length Then
                        ' make sure there's a next arg to move on to
                        Throw New System.ArgumentException("No Value for " + arguments((i - 1)))
                     End If
                     ' assign to the next arg
                     m_Culture = GetCultureFromArgument(arguments(i))
                  Case "verbose", "v"
                     m_fVerbose = True
                  Case "isfindingdatetimemeasures", "dt"
                     m_fIsFindingDateTimeMeasures = True
                  Case "isfindingpersons", "pe"
                     m_fIsFindingPersons = True
                  Case "isfindingphrases", "ph"
                     m_fIsFindingPhrases = True
                  Case "isfindingorganizations", "fo"
                     m_fIsFindingOrganizations = True
                  Case "isfindinglocations", "lo"
                     m_fIsFindingLocations = True
                  Case "help", "?"
                     DisplayHelp()
                        Return False
                  Case Else
                     ' wprintf(L"%s: error - Illegal Option %s\n", m_wzProgramName, *arguments);
                     DisplayHelp()
                        Throw New System.ArgumentException("Illegal Option " + arguments(i))
               End Select
            End If
	    i = i + 1
         End While ' for
         If m_fNamedEntity Then
            m_fIsFindingDateTimeMeasures = True
            m_fIsFindingPersons = True
            m_fIsFindingPhrases = True
            m_fIsFindingOrganizations = True
            m_fIsFindingLocations = True
         End If
         Return True
      End Function 'ProcessOpts
       ' ProcessOpts
      
      
      Public Sub DisplayHelp()
         Console.WriteLine("wordbreak.exe options:")
         DisplayHelpLine("I", "Input", "The text file to parse.  If a directory path is used, will recurse the directory tree for all non-system and non-hidden files and parse each one.  If no parameter is passed, Standard input will be used by default.")
         DisplayHelpLine("O", "Output", "The output file (Defaults to Console)")
         DisplayHelpLine("NE", "NamedEntity", "Sets the engine to parse named entities.")
         DisplayHelpLine("LE", "Lemma", "Sets the engine to parse lemmas")
         DisplayHelpLine("CO", "Compounds", "Sets the engine to compute compounds")
         DisplayHelpLine("L", "LangId", "Sets the langID of the engine.")
         DisplayHelpLine("C", "Culture", "Sets the culture of the engine (ex. -C 'en-us' or 'fr-ca').  Defaults to current culture. ")
         DisplayHelpLine("V", "Verbose", "Verbose output")
         DisplayHelpLine("DE", "IsFindingDateTimeMeasures", "Sets the IsFindingDateTimeMeasures option on the context object")
         DisplayHelpLine("PE", "IsFindingPersons", "Sets the IsFindingPersons option on the context object")
         DisplayHelpLine("PH", "IsFindingPhrases", "Sets the IsFindingPhrases option on the context object")
         DisplayHelpLine("OR", "IsFindingOrganizations", "Sets the IsFindingOrganizations option on the context object")
         DisplayHelpLine("LO", "IsFindingLocations", "Sets the IsFindingLocations option on the context object")
         DisplayHelpLine("NE", "NamedEntity", "Sets all Named Entity options on the context object (same as -de -pe -ph -or -lo)")
      End Sub 'DisplayHelp
       ' DisplayHelp
      Private Sub DisplayHelpLine(shortname As String, name As String, description As String)
         Console.WriteLine(" -{0,1} {1,-24} {2}", shortname, name, description)
      End Sub 'DisplayHelpLine
   End Class ' class CmdOpts
End Namespace ' namespace Microsoft.NaturalLanguage.Samples

