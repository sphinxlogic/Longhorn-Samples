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
			m_sOutput = "";
			m_Culture = null;
			m_fVerbose = false;
		}

		public string Output
		{
			get { return m_sOutput; }
			set { m_sOutput = value; }
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
			DisplayHelpLine("O", "Output", "The output file (Defaults to Standard Out)");
			DisplayHelpLine("C", "Culture", "Sets the culture of the engine (ex. -C 'en-us' or 'fr-ca').  Defaults to current culture. ");
			DisplayHelpLine("V", "Verbose", "Verbose output");
		} // DisplayHelp

		public bool ProcessOpts(string[] args)
		{
			for (int i = 0; i < args.Length; ++i)
			{
				if ((args[i].StartsWith("-")))
				{
					String thisArgument = args[i];

					thisArgument = thisArgument.Remove(0, 1).ToLower(System.Globalization.CultureInfo.CurrentCulture);
					if ((String.Compare(thisArgument, "output") == 0) || (String.Compare(thisArgument, "o") == 0))
					{
						if (++i >= args.Length)
						{   // make sure there's a next arg to move on to
							throw new System.ArgumentException(String.Concat("No Value for ", thisArgument));
						}

						// assign to the next arg
						m_sOutput = args[i];
					}

					if ((String.Compare(thisArgument, "culture") == 0) || (String.Compare(thisArgument, "c") == 0))
					{
						if (++i >= args.Length)
						{   // make sure there's a next arg to move on to
							throw new System.ArgumentException("No Value for ", thisArgument);
						}

						// assign to the next arg
						m_Culture = GetCultureFromArguments(args[i]);
					}

					if ((String.Compare(thisArgument, "verbose") == 0) || (String.Compare(thisArgument, "v") == 0))
					{
						m_fVerbose = true;
					}

					if ((String.Compare(thisArgument, "help") == 0) || (String.Compare(thisArgument, "?") == 0))
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

		private CultureInfo GetCultureFromArguments(String arguments)
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
				catch (ArgumentException ex)
				{
					Console.Error.WriteLine(ex.Message);
					return CultureInfo.CurrentCulture;
				}
			}
		}

		String m_sOutput;

		CultureInfo m_Culture;

		bool m_fVerbose;
	}; // class CmdOpts
}
