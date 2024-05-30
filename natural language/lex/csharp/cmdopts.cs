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
using System.Globalization;

namespace Microsoft.NaturalLanguage.Samples
{
	public class CmdOpts
	{
		private string	m_strInputFile;
        private string	m_strOutputFile;
        private string	m_strWord;
        private int m_iMaxWordsToDisplay = 1;
        private bool m_fVerbose = false;

        public string InputFile
        {
            get { return m_strInputFile; }
        }

        public string OutputFile
        {
            get { return m_strOutputFile; }
        }

        public string Word
        {
            get { return m_strWord; }
        }

        public int MaxWordsToDisplay
        {
            get { return m_iMaxWordsToDisplay; }
        }

        public bool Verbose
        {
            get { return m_fVerbose; }
        }

		public bool ProcessOpts(string [] arguments)
		{
			if (arguments.Length == 0)
			{
				DisplayHelp();
				return false;
			}
			for (int iargc = 0; iargc < arguments.Length; ++iargc)
			{
				if (arguments[iargc][0] == '-') 
				{
					string thisarg = arguments[iargc].Remove(0, 1).ToLower(CultureInfo.CurrentCulture);

                    switch (thisarg)
                    {
                        case "inputfile":
                        case "i":
                            iargc = iargc + 1;
                            if (iargc >= arguments.Length)
                            {	 // make sure there's a next arg to move on to
                                throw new System.Exception("No Value for " + arguments[iargc - 1]);
                            }

                            // assign to the next arg
                            m_strInputFile = arguments[iargc];
                            break;

                        case "outputfile":
                        case "o":
                            iargc = iargc + 1;
                            if (iargc >= arguments.Length)
                            {	 // make sure there's a next arg to move on to
                                throw new System.Exception("No Value for " + arguments[iargc - 1]);
                            }

                            // assign to the next arg
                            m_strOutputFile = arguments[iargc];
                            break;

                        case "word":
                        case "w":
                            iargc = iargc + 1;
                            if (iargc >= arguments.Length)
                            {	 // make sure there's a next arg to move on to
                                throw new System.Exception("No Value for " + arguments[iargc - 1]);
                            }

                            // assign to the next arg
                            m_strWord = arguments[iargc];
                            break;

                        case "maxwordstodisplay":
                        case "n":
                            iargc = iargc + 1;
                            if (iargc >= arguments.Length)
                            {	 // make sure there's a next arg to move on to
                                throw new System.Exception("No Value for " + arguments[iargc - 1]);
                            }

                            // assign to the next arg
                            m_iMaxWordsToDisplay = Int32.Parse(arguments[iargc], null);
                            break;

                        case "verbose":
                        case "v":
                            m_fVerbose = true;
                            break;

                        case "help":
                        case "?":
                            DisplayHelp();
                            break;
                    }
                }
			} // for
			return true;
		} // ProcessOpts
		
		
		
		public void DisplayHelp()
		{
			Console.WriteLine("========================\n");
			
			DisplayHelpLine("i", "InputFile", "The Lexicon (.lex) file to use");
			DisplayHelpLine("o", "OutputFile", "The output file (Defaults to Console)");
			DisplayHelpLine("w", "Word", "The word to move to");
			DisplayHelpLine("n", "MaxWordsToDisplay", "The number of words to display");
			DisplayHelpLine("v", "Verbose", "Verbose output");
			Console.WriteLine("\n========================");
		} // DisplayHelp

        private void DisplayHelpLine(string shortname, string name, string description)
        {
            Console.WriteLine(" -{0,1} {1,-24} {2}", shortname, name, description);
        }
	}; // class CmdOpts


}; // namespace Samples
