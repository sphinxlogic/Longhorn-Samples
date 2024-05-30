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


Namespace Microsoft.Samples.NaturalLanguage
    
   Public Class CmdOpts
      
      Public Sub New()
         m_sInput = ""
         m_sOutput = ""
         m_Culture = Nothing
         m_fVerbose = False
         m_fIsFindingPhrases = True
         m_fIsFindingPersons = True
         m_fIsFindingOrganizations = True
         m_fIsFindingLocations = True
         m_fIsFindingDateTimeMeasures = True
      End Sub 'New
      
      
      Public Property Output() As String
         Get
            Return m_sOutput
         End Get
         Set
            m_sOutput = value
         End Set
      End Property 
      
      Public Property Input() As String
         Get
            Return m_sInput
         End Get
         Set
            m_sInput = value
         End Set
      End Property 
      
      Public Property Culture() As CultureInfo
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
      
      Public Property IsFindingLocations() As Boolean
         Get
            Return m_fIsFindingLocations
         End Get
         Set
            m_fIsFindingLocations = value
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
       
      Public Sub DisplayHelp()
         DisplayHelpLine("I", "Input", "The text file or directory to use for text input (defaults to standard input)")
         DisplayHelpLine("O", "Output", "The output file (Defaults to Standard Out)")
         DisplayHelpLine("C", "Culture", "Sets the culture of the engine (ex. -C 'en-us' or 'fr-ca').  Defaults to current culture. ")
         DisplayHelpLine("V", "Verbose", "Verbose output")
         DisplayHelpLine("DT", "IsFindingDateTimeMeasures", "Turns off IsFindingDateTimeMeasures flag")
         DisplayHelpLine("PH", "IsFindingPhrases", "Turns off IsFindingPhrases flag")
         DisplayHelpLine("PE", "IsFindingPersons", "Turns off IsFindingPersons flag")
         DisplayHelpLine("OR", "IsFindingOrganizations", "Turns off IsFindingOrganizations flag")
         DisplayHelpLine("LO", "IsFindingLocations", "Turns off IsFindingLocations flag")
      End Sub 'DisplayHelp
       ' DisplayHelp
      Public Function ProcessOpts(args() As String) As Boolean
         Dim invariantCulture As CultureInfo = CultureInfo.InvariantCulture
         
         Dim i As Integer
         
         While i < args.Length
            If args(i).StartsWith("-") Then
               Dim thisarg As [String] = args(i)
               
               thisarg = thisarg.Remove(0, 1).ToLower(System.Globalization.CultureInfo.CurrentCulture)
               If [String].Compare(thisarg, "input", True, invariantCulture) = 0 Or [String].Compare(thisarg, "i", True, invariantCulture) = 0 Then
		  i = i + 1
                  If i >= args.Length Then
                     'ToDo: Unsupported operator
                     'ToDo: ++ operator not supported within expressions
                     ' make sure there's a next arg to move on to
                     Throw New System.ArgumentException([String].Concat("No Value for ", thisarg))
                  End If
                  
                  ' assign to the next arg
                  m_sInput = args(i)
               End If
               
               If [String].Compare(thisarg, "output", True, invariantCulture) = 0 Or [String].Compare(thisarg, "o", True, invariantCulture) = 0 Then
                  i = i + 1
                  If i >= args.Length Then
                     Throw New System.ArgumentException([String].Concat("No Value for ", thisarg))
                  End If
                  
                  ' assign to the next arg
                  m_sOutput = args(i)
               End If
               
               If [String].Compare(thisarg, "culture", True, invariantCulture) = 0 Or [String].Compare(thisarg, "c", True, invariantCulture) = 0 Then
                  i = i + 1
                  If i >= args.Length Then
                     Throw New System.ArgumentException("No Value for ", thisarg)
                  End If
                  
                  ' assign to the next arg
                  m_Culture = GetCultureFromArgument(args(i))
               End If
               
               If [String].Compare(thisarg, "verbose", True, invariantCulture) = 0 Or [String].Compare(thisarg, "v", True, invariantCulture) = 0 Then
                  m_fVerbose = True
               End If
               
               If [String].Compare(thisarg, "IsFindingPersons", True, invariantCulture) = 0 Or [String].Compare(thisarg, "pe", True, invariantCulture) = 0 Then
                  m_fIsFindingPersons = False
               End If
               
               If [String].Compare(thisarg, "IsFindingPhrases", True, invariantCulture) = 0 Or [String].Compare(thisarg, "ph", True, invariantCulture) = 0 Then
                  m_fIsFindingPhrases = False
               End If
               
               If [String].Compare(thisarg, "IsFindingOrganizations", True, invariantCulture) = 0 Or [String].Compare(thisarg, "or", True, invariantCulture) = 0 Then
                  m_fIsFindingOrganizations = False
               End If
               
               If [String].Compare(thisarg, "IsFindingLocations", True, invariantCulture) = 0 Or [String].Compare(thisarg, "lo", True, invariantCulture) = 0 Then
                  m_fIsFindingLocations = False
               End If
               
               If [String].Compare(thisarg, "IsFindingDateTimeMeasures", True, invariantCulture) = 0 Or [String].Compare(thisarg, "dt", True, invariantCulture) = 0 Then
                  m_fIsFindingDateTimeMeasures = False
               End If
               
               If [String].Compare(thisarg, "help", True, invariantCulture) = 0 Or [String].Compare(thisarg, "?", True, invariantCulture) = 0 Then
                  DisplayHelp()
                  Return False
               End If
            End If
	    i = i + 1
         End While ' for
         Return True
      End Function 'ProcessOpts
       ' ProcessOpts
      Private Sub DisplayHelpLine(shortname As [String], name As [String], description As [String])
         Console.WriteLine(" -{0,1} {1,-24} {2}", shortname, name, description)
      End Sub 'DisplayHelpLine
      
      
      Private Function GetCultureFromArgument(argument As [String]) As CultureInfo
         Dim culture As CultureInfo = CultureInfo.CurrentCulture
         
         Try
            culture = New CultureInfo(argument)
            Return culture
         Catch e As ArgumentException
            Console.Error.WriteLine(e.Message)
            Return CultureInfo.CurrentCulture
         End Try
      End Function 'GetCultureFromArgument
      
      Private m_sInput As [String]
      
      Private m_sOutput As [String]
      
      Private m_Culture As CultureInfo
      
      Private m_fVerbose As Boolean
      
      Private m_fIsFindingDateTimeMeasures As Boolean
      
      Private m_fIsFindingPhrases As Boolean
      
      Private m_fIsFindingPersons As Boolean
      
      Private m_fIsFindingOrganizations As Boolean
      
      Private m_fIsFindingLocations As Boolean
   End Class ' class CmdOpts
End Namespace 'Microsoft.Samples.NaturalLanguage
