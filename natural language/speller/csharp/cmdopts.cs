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
using System.Collections;
using System.Globalization;

namespace Microsoft.Samples.NaturalLanguage
{
	public class CmdOpts
	{
		public CmdOpts()
		{
			m_sInput = "";
			m_sOutput = "";
			m_Culture = null;
			m_fVerbose = false;
			m_LexList = new ArrayList();
		}

		public string Output
		{
			get { return m_sOutput; }
			set { m_sOutput = value; }
		}

		public string Input
		{
			get { return m_sInput; }
			set { m_sInput = value; }
		}

		public CultureInfo Culture
		{
			get { return m_Culture; }
			set { m_Culture = value; }
		}

		public bool Verbose
		{
			get { return m_fVerbose; }
			set { m_fVerbose = value; }
		}

		public ArrayList LexiconList
		{
			get { return m_LexList; }
		}

		public void DisplayHelp()
		{
			DisplayHelpLine("I", "Input", "The text file or directory to use for intput text (defaults to standard input)");
			DisplayHelpLine("O", "Output", "The output file (Defaults to Standard Out)");
			DisplayHelpLine("C", "Culture", "Sets the culture of the engine (ex. -C 'en-us' or 'fr-ca').  Defaults to current culture. ");
			DisplayHelpLine("V", "Verbose", "Verbose output");
			DisplayHelpLine("LEX", "Lexicon", "Adds a lexicon to the speller engine");
		} // DisplayHelp

		public bool ProcessOpts(string[] args)
		{
			CultureInfo invariantCulture = CultureInfo.InvariantCulture;

			for (int i = 0; i < args.Length; ++i)
			{
				if (args[i].StartsWith("-"))
				{
					string thisarg = args[i];

					thisarg = thisarg.Remove(0, 1).ToLower(System.Globalization.CultureInfo.CurrentCulture);
					if (thisarg == "input" || thisarg =="i" )
					{
						if (++i >= args.Length)
						{   // make sure there's a next arg to move on to
							throw new System.ArgumentException(String.Concat("No Value for ", thisarg));
						}

						// assign to the next arg
						m_sInput = args[i];
					}

					if (thisarg == "output" || thisarg == "o")
					{
						if (++i >= args.Length)
						{   // make sure there's a next arg to move on to
							throw new System.ArgumentException(String.Concat("No Value for ", thisarg));
						}

						// assign to the next arg
						m_sOutput = args[i];
					}

					if (thisarg == "culture" || thisarg == "c")
					{
						if (++i >= args.Length)
						{   // make sure there's a next arg to move on to
							throw new System.ArgumentException("No Value for ", thisarg);
						}

						// assign to the next arg
						m_Culture = GetCultureFromArguments(args[i]);
					}

					if ((String.Compare(thisarg, "lexicon", true, invariantCulture) == 0) || (String.Compare(thisarg, "lex", true, invariantCulture) == 0))
					{
						if (++i >= args.Length)
						{
							throw new System.ArgumentException("No value for ", thisarg);
						}

						m_LexList.Add(new System.NaturalLanguage.Lexicon(args[i]));
					}

					if (thisarg == "verbose" || thisarg == "v")
					{
						m_fVerbose = true;
					}

					if (thisarg == "help" || thisarg == "?")
					{
						DisplayHelp();
						return false;
					}
				}
			} // for

			return true;
		} // ProcessOpts

		private void DisplayHelpLine(string shortname, string name, string description)
		{
			Console.WriteLine(" -{0,1} {1,-24} {2}", shortname, name, description);
		}

		private CultureInfo GetCultureFromArguments(string arguments)
		{
			CultureInfo culture = CultureInfo.CurrentCulture;

			try
			{
				culture = new CultureInfo(arguments);
				return culture;
			}
			catch (ArgumentException e)
			{
				Console.Error.WriteLine(e.Message);
				try
				{
					culture = new CultureInfo(Int32.Parse(arguments, CultureInfo.CurrentCulture.NumberFormat), true);
					return culture;
				}
				catch (FormatException)
				{
					Console.Error.WriteLine("defaulting to current culture.");
					return CultureInfo.CurrentCulture;
				}
				catch (ArgumentException ex)
				{
					Console.Error.WriteLine(ex.Message);
					return CultureInfo.CurrentCulture;
				}
			}
		}

		private string m_sInput;

		private string m_sOutput;

		private CultureInfo m_Culture;

		private ArrayList m_LexList;

		private bool m_fVerbose;
	}; // class CmdOpts
}
