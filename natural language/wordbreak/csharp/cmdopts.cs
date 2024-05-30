//---------------------------------------------------------------------
//  This file is part of the Microsoft .NET Framework SDK Code Samples.
// 
//  Copyright (C) Microsoft Corporation.  All rights reserved.
// 
//This source code is intended only as a supplement to Microsoft
//Development Tools and/or on-line documentation.  See these other
//materials for detailed information regarding Microsoft code samples.
// 
//THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
//KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//PARTICULAR PURPOSE.
//---------------------------------------------------------------------
using System;
using System.IO;


namespace Microsoft.Samples.NaturalLanguage
{
    public class CmdOpts
    {
        private string  m_strInput = "";
        private string  m_strOutput = "";
        private bool    m_fNamedEntity = false;
        private bool    m_fLemma = false;
        private bool    m_fCompounds = false;
        private System.Globalization.CultureInfo m_Culture = null;
        private bool    m_fVerbose = false;
        private bool    m_fIsFindingDateTimeMeasures = false; 
        private bool    m_fIsFindingPersons = false;
        private bool    m_fIsFindingPhrases = false;
        private bool    m_fIsFindingOrganizations = false;
        private bool    m_fIsFindingLocations = false; 
        
        public string Input
        {
           get { return m_strInput; }
           set { m_strInput = value; }
        }

        public string Output
        {
           get { return m_strOutput; }
           set { m_strOutput = value; }
        }

        public bool NamedEntity
        {
           get { return m_fNamedEntity; }
           set { m_fNamedEntity = value; }
        }

        public bool Lemma
        {
           get { return m_fLemma; }
           set { m_fLemma = value; }
        }

        public bool Compounds
        {
           get { return m_fCompounds; }
           set { m_fCompounds = value; }
        }

        public System.Globalization.CultureInfo Culture
        {
           get { return m_Culture; }
           set { m_Culture = value; }
        }

        public bool Verbose
        {
           get { return m_fVerbose; }
           set { m_fVerbose = value; }
        }
        
        public bool IsFindingDateTimeMeasures
        {
           get { return m_fIsFindingDateTimeMeasures; }
           set { m_fIsFindingDateTimeMeasures = value; }
        }

        public bool IsFindingPersons
        {
           get { return m_fIsFindingPersons; }
           set { m_fIsFindingPersons = value; }
        }

        public bool IsFindingPhrases
        {
           get { return m_fIsFindingPhrases; }
           set { m_fIsFindingPhrases = value; }
        }

        public bool IsFindingOrganizations
        {
           get { return m_fIsFindingOrganizations; }
           set { m_fIsFindingOrganizations = value; }
        }

        public bool IsFindingLocations
        {
           get { return m_fIsFindingLocations; }
           set { m_fIsFindingLocations = value; }
        }

        System.Globalization.CultureInfo getCultureFromArg(string sArg)
        {
        
            System.Globalization.CultureInfo culture = System.Globalization.CultureInfo.CurrentCulture;
                        
            try
            {
                culture = new System.Globalization.CultureInfo(sArg);
                return culture;
            }
            catch(ArgumentException e)
            {
                Console.Error.WriteLine(e.Message);
                try 
                {
                    culture = new System.Globalization.CultureInfo(Int32.Parse(sArg, System.Globalization.CultureInfo.CurrentCulture.NumberFormat), true);              
                    return culture;
                }
                catch(ArgumentException ex)
                {
                    Console.Error.WriteLine(ex.Message);
                    return System.Globalization.CultureInfo.CurrentCulture;
                }
            }
        }

        public bool ProcessOpts(string [] arguments)
        {
            for (int i = 0; i < arguments.Length; ++i)
            {
                if (arguments[i].StartsWith("-"))
                {
                    string thisarg = arguments[i].Remove(0, 1).ToLower(System.Globalization.CultureInfo.CurrentCulture);
                    switch (thisarg) 
                    {

                       case "input" :
                       case "i" :
                          if (++i >= arguments.Length)
                          {   // make sure there's a next arg to move on to
                             throw new System.ArgumentException("No Value for "+arguments[i-1]);
                          }
                          // assign to the next arg
                          m_strInput = arguments[i];
                          break;
                       case "output" :
                       case "o" :
                          if (++i >= arguments.Length)
                          {   // make sure there's a next arg to move on to
                              throw new System.ArgumentException("No Value for " + arguments[i - 1]);
                          }
                          // assign to the next arg
                          m_strOutput = arguments[i];
                          break;
                       case "namedentity" :
                       case "ne" :
                          m_fNamedEntity = true;
                          break;
                       case "lemma" :
                       case "le" :
                          m_fLemma = true;
                          break;
                       case "compounds" :
                       case "co" :
                          m_fCompounds = true;
                          break;
                       case "culture" :
                       case "c" :
                          if (++i >= arguments.Length)
                          {   // make sure there's a next arg to move on to
                              throw new System.ArgumentException("No Value for " + arguments[i - 1]);
                          }
                          // assign to the next arg
                          m_Culture = getCultureFromArg(arguments[i]);
                          break;
                       case "verbose" :
                       case "v" :
                          m_fVerbose = true;
                          break;
                       case "isfindingdatetimemeasures" :
                       case "dt" :
                          m_fIsFindingDateTimeMeasures = true;
                          break;
                       case "isfindingpersons" :
                       case "pe" :
                          m_fIsFindingPersons = true;
                          break;
                       case "isfindingphrases" :
                       case "ph" :
                          m_fIsFindingPhrases = true;
                          break;
                       case "isfindingorganizations" :
                       case "fo" :
                          m_fIsFindingOrganizations = true;
                          break;
                       case "isfindinglocations" :
                       case "lo" :
                          m_fIsFindingLocations = true;
                          break;
                        case "help" :
                        case "?" :
                            DisplayHelp();
                            return false;
                        default :
                            // wprintf(L"%s: error - Illegal Option %s\n", m_wzProgramName, *arguments);
                            DisplayHelp();
                            throw new System.ArgumentException("Illegal Option " + arguments[i]);
                    }
                }
            } // for
            if (m_fNamedEntity) {
                m_fIsFindingDateTimeMeasures = true;
                m_fIsFindingPersons = true;
                m_fIsFindingPhrases = true;
                m_fIsFindingOrganizations = true;
                m_fIsFindingLocations = true;
            }
            return true;
        } // ProcessOpts
        
        
        
        public void DisplayHelp()
        {
            Console.WriteLine("wordbreak.exe options:");
            DisplayHelpLine("I", "Input", "The text file to parse.  If a directory path is used, will recurse the directory tree for all non-system and non-hidden files and parse each one.  If no parameter is passed, Standard input will be used by default.");
            DisplayHelpLine("O", "Output", "The output file (Defaults to Console)");
            DisplayHelpLine("NE", "NamedEntity", "Sets the engine to parse named entities.");
            DisplayHelpLine("LE", "Lemma", "Sets the engine to parse lemmas");
            DisplayHelpLine("CO", "Compounds", "Sets the engine to compute compounds");
            DisplayHelpLine("L", "LangId", "Sets the langID of the engine.");
            DisplayHelpLine("C", "Culture", "Sets the culture of the engine (ex. -C 'en-us' or 'fr-ca').  Defaults to current culture. ");
            DisplayHelpLine("V", "Verbose", "Verbose output");
            DisplayHelpLine("DE", "IsFindingDateTimeMeasures", "Sets the IsFindingDateTimeMeasures option on the context object");
            DisplayHelpLine("PE", "IsFindingPersons", "Sets the IsFindingPersons option on the context object");
            DisplayHelpLine("PH", "IsFindingPhrases", "Sets the IsFindingPhrases option on the context object");
            DisplayHelpLine("OR", "IsFindingOrganizations", "Sets the IsFindingOrganizations option on the context object");
            DisplayHelpLine("LO", "IsFindingLocations", "Sets the IsFindingLocations option on the context object");
            DisplayHelpLine("NE", "NamedEntity", "Sets all Named Entity options on the context object (same as -de -pe -ph -or -lo)");            
        } // DisplayHelp
        
        private void DisplayHelpLine(string shortname, string name, string description)
        {
            Console.WriteLine(" -{0,1} {1,-24} {2}", shortname, name, description);
        }
    } // class CmdOpts
} // namespace Microsoft.NaturalLanguage.Samples
