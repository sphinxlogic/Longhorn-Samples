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
			m_fIsFindingPhrases = true;
			m_fIsFindingPersons = true;
			m_fIsFindingOrganizations = true;
			m_fIsFindingLocations = true;
			m_fIsFindingDateTimeMeasures = true;
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

		public bool IsFindingDateTimeMeasures
		{
			get { return m_fIsFindingDateTimeMeasures; }
			set { m_fIsFindingDateTimeMeasures = value; }
		}

		public bool IsFindingLocations
		{
			get { return m_fIsFindingLocations; }
			set { m_fIsFindingLocations = value; }
		}

		public bool IsFindingOrganizations
		{
			get { return m_fIsFindingOrganizations; }
			set { m_fIsFindingOrganizations = value; }
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

		public void DisplayHelp()
		{
			DisplayHelpLine("I", "Input", "The text file or directory to use for text input (defaults to standard input)");
			DisplayHelpLine("O", "Output", "The output file (Defaults to Standard Out)");
			DisplayHelpLine("C", "Culture", "Sets the culture of the engine (ex. -C 'en-us' or 'fr-ca').  Defaults to current culture. ");
			DisplayHelpLine("V", "Verbose", "Verbose output");
			DisplayHelpLine("DT", "IsFindingDateTimeMeasures", "Turns off IsFindingDateTimeMeasures flag");
			DisplayHelpLine("PH", "IsFindingPhrases", "Turns off IsFindingPhrases flag");
			DisplayHelpLine("PE", "IsFindingPersons", "Turns off IsFindingPersons flag");
			DisplayHelpLine("OR", "IsFindingOrganizations", "Turns off IsFindingOrganizations flag");
			DisplayHelpLine("LO", "IsFindingLocations", "Turns off IsFindingLocations flag");
		} // DisplayHelp

		public bool ProcessOpts(string[] args)
		{
			CultureInfo invariantCulture = CultureInfo.InvariantCulture;

			for (int i = 0; i < args.Length; ++i)
			{
				if ((args[i].StartsWith("-")))
				{
					String thisarg = args[i];

					thisarg = thisarg.Remove(0, 1).ToLower(System.Globalization.CultureInfo.CurrentCulture);
					if ((String.Compare(thisarg, "input", true, invariantCulture) == 0) || (String.Compare(thisarg, "i", true, invariantCulture) == 0))
					{
						if (++i >= args.Length)
						{   // make sure there's a next arg to move on to
							throw new System.ArgumentException(String.Concat("No Value for ", thisarg));
						}

						// assign to the next arg
						m_sInput = args[i];
					}

					if ((String.Compare(thisarg, "output", true, invariantCulture) == 0) || (String.Compare(thisarg, "o", true, invariantCulture) == 0))
					{
						if (++i >= args.Length)
						{   // make sure there's a next arg to move on to
							throw new System.ArgumentException(String.Concat("No Value for ", thisarg));
						}

						// assign to the next arg
						m_sOutput = args[i];
					}

					if ((String.Compare(thisarg, "culture", true, invariantCulture) == 0) || (String.Compare(thisarg, "c", true, invariantCulture) == 0))
					{
						if (++i >= args.Length)
						{   // make sure there's a next arg to move on to
							throw new System.ArgumentException("No Value for ", thisarg);
						}

						// assign to the next arg
						m_Culture = GetCultureFromArgument(args[i]);
					}

					if ((String.Compare(thisarg, "verbose", true, invariantCulture) == 0) || (String.Compare(thisarg, "v", true, invariantCulture) == 0))
					{
						m_fVerbose = true;
					}

					if ((String.Compare(thisarg, "IsFindingPersons", true, invariantCulture) == 0) || (String.Compare(thisarg, "pe", true, invariantCulture) == 0))
					{
						m_fIsFindingPersons = false;
					}

					if ((String.Compare(thisarg, "IsFindingPhrases", true, invariantCulture) == 0) || (String.Compare(thisarg, "ph", true, invariantCulture) == 0))
					{
						m_fIsFindingPhrases = false;
					}

					if ((String.Compare(thisarg, "IsFindingOrganizations", true, invariantCulture) == 0) || (String.Compare(thisarg, "or", true, invariantCulture) == 0))
					{
						m_fIsFindingOrganizations = false;
					}

					if ((String.Compare(thisarg, "IsFindingLocations", true, invariantCulture) == 0) || (String.Compare(thisarg, "lo", true, invariantCulture) == 0))
					{
						m_fIsFindingLocations = false;
					}

					if ((String.Compare(thisarg, "IsFindingDateTimeMeasures", true, invariantCulture) == 0) || (String.Compare(thisarg, "dt", true, invariantCulture) == 0))
					{
						m_fIsFindingDateTimeMeasures = false;
					}

					if ((String.Compare(thisarg, "help", true, invariantCulture) == 0) || (String.Compare(thisarg, "?", true, invariantCulture) == 0))
					{
						DisplayHelp();
						return false;
					}
				}
			} // for

			return true;
		} // ProcessOpts

		private void DisplayHelpLine(String shortname, String name, String description)
		{
			Console.WriteLine(" -{0,1} {1,-24} {2}", shortname, name, description);
		}

		private CultureInfo GetCultureFromArgument(String argument)
		{
			CultureInfo culture = CultureInfo.CurrentCulture;

			try
			{
				culture = new CultureInfo(argument);
				return culture;
			}
			catch (ArgumentException e)
			{
				Console.Error.WriteLine(e.Message);
				return CultureInfo.CurrentCulture;
			}
		}

		private String m_sInput;

		private String m_sOutput;

		private CultureInfo m_Culture;

		private bool m_fVerbose;

		private bool m_fIsFindingDateTimeMeasures;

		private bool m_fIsFindingPhrases;

		private bool m_fIsFindingPersons;

		private bool m_fIsFindingOrganizations;

		private bool m_fIsFindingLocations;
	} // class CmdOpts
}
