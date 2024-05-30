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

		public void DisplayHelp()
		{
			Console.WriteLine("normalizer_cs.exe usage:");
			DisplayHelpLine("I", "Input", "The text file or directory to use for text input (defaults to standard input)");
			DisplayHelpLine("O", "Output", "The output file (Defaults to Standard Out)");
			DisplayHelpLine("C", "Culture", "Sets the culture of the engine (ex. -C 'en-us' or 'fr-ca').  Defaults to current culture. ");
			DisplayHelpLine("V", "Verbose", "Verbose output");
		} // DisplayHelp

		public bool ProcessOpts(string[] args)
		{
			for (int i = 0; i < args.Length; ++i)
			{
				if (args[i].StartsWith("-"))
				{
					String thisarg = args[i];

					thisarg = thisarg.Remove(0, 1).ToLower(System.Globalization.CultureInfo.CurrentCulture);
					if ((String.Compare(thisarg, "input") == 0) || (String.Compare(thisarg, "i") == 0))
					{
						if (++i >= args.Length)
						{   // make sure there's a next arg to move on to
							throw new System.ArgumentException(String.Concat("No Value for ", thisarg));
						}

						// assign to the next arg
						m_sInput = args[i];
					}

					if ((String.Compare(thisarg, "output") == 0) || (String.Compare(thisarg, "o") == 0))
					{
						if (++i >= args.Length)
						{   // make sure there's a next arg to move on to
							throw new System.ArgumentException(String.Concat("No Value for ", thisarg));
						}

						// assign to the next arg
						m_sOutput = args[i];
					}

					if ((String.Compare(thisarg, "culture") == 0) || (String.Compare(thisarg, "c") == 0))
					{
						if (++i >= args.Length)
						{   // make sure there's a next arg to move on to
							throw new System.ArgumentException("No Value for ", thisarg);
						}

						// assign to the next arg
						m_Culture = GetCultureFromArgument(args[i]);
					}

					if ((String.Compare(thisarg, "verbose") == 0) || (String.Compare(thisarg, "v") == 0))
					{
						m_fVerbose = true;
					}

					if ((String.Compare(thisarg, "help") == 0) || (String.Compare(thisarg, "?") == 0))
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

		private CultureInfo GetCultureFromArgument(string argument)
		{
			CultureInfo culture = CultureInfo.CurrentCulture;

			try
			{
				culture = new CultureInfo(argument);
				return culture;
			}
			catch (ArgumentException)
			{
				try
				{
					culture = new CultureInfo(Int32.Parse(argument, CultureInfo.CurrentCulture.NumberFormat), true);
					return culture;
				}
				catch (FormatException)
				{
					Console.Error.WriteLine("{0} is not a valid culture, defaulting to current culture.", argument);
					return CultureInfo.CurrentCulture;
				}
				catch (ArgumentException e)
				{
					Console.Error.WriteLine("{0}, defaulting to current culture.", e.Message);
					return CultureInfo.CurrentCulture;
				}
			}
		}

		private String m_sInput;

		private String m_sOutput;

		private CultureInfo m_Culture;

		private bool m_fVerbose;
	} // class CmdOpts	
}
