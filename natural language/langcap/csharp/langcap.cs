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
using System.IO;
using System.Text;
using System.Globalization;
using System.NaturalLanguage;

namespace Microsoft.Samples.NaturalLanguage
{
	public class LanguageCapability
	{
		public LanguageCapability(CmdOpts opts)
		{
			m_context = new Context();

			// Set the following context properties 
			m_context.IsSpellChecking = false;
		}

		public static int Main(string[] args)
		{
			int retCode = 0;
			DateTime startTime = DateTime.Now;
			DateTime endTime = DateTime.Now;
			TimeSpan timeDiff;
			CmdOpts opts = null;
			LanguageCapability lc = null;
			StreamWriter outputWriter = null;

			try
			{
				startTime = DateTime.Now;
				opts = new CmdOpts();
				if (opts.ProcessOpts(args))
				{
					lc = new LanguageCapability(opts);
					if (opts.Output.Length != 0)
					{
						if (File.Exists(opts.Output)) File.Delete(opts.Output);

						outputWriter = new StreamWriter(opts.Output);
					}
					else
					{
						outputWriter = new StreamWriter(Console.OpenStandardOutput());
					}

					if (opts.Culture != null)
						lc.GetLangCapabilities(outputWriter, opts.Culture);
				}

				retCode = 0;
			}
			catch (FormatException fe)
			{
				if (opts.Verbose)
				{
					Console.Error.WriteLine("Message: {0}", fe.Message);
					Console.Error.WriteLine("Stack Trace: {0}", fe.StackTrace);
				}
			}
			catch (NullReferenceException nre)
			{
				if (opts.Verbose)
				{
					Console.Error.WriteLine("Message: {0}", nre.Message);
					Console.Error.WriteLine("Stack Trace: {0}", nre.StackTrace);
				}

				retCode = 1;
			}
			catch (ArgumentException e)
			{
				if (opts.Verbose)
				{
					Console.Error.WriteLine("Message: {0}", e.Message);
					Console.Error.WriteLine("Stack Trace: {0}", e.StackTrace);
				}

				retCode = 1;
			}
			finally
			{
				endTime = DateTime.Now;
				timeDiff = endTime - startTime;
				if (outputWriter != null)
				{
					if (opts.Verbose)
					{
						outputWriter.WriteLine();
						outputWriter.WriteLine("{0,-50} {1}", "Total elapsed time:", timeDiff.ToString());
					}

					outputWriter.Close();
				}
			}
			return retCode;
		}

		public void GetLangCapabilities(TextWriter outputWriter, CultureInfo culture)
		{
			LanguageCapabilities caps = m_context.GetCapabilitiesFor(culture);

			if (caps.NotSupported)
			{
				outputWriter.WriteLine("{0} {1}", culture.ToString(), "Not Supported");
				return;
			}
			else
			{
				outputWriter.WriteLine("{0} {1}:", culture.ToString(), "supported capabilites");
				outputWriter.WriteLine("{0,-40} {1}", "Lemmas:", getSupportedMessage(caps.SupportsLemmas));
				outputWriter.WriteLine("{0,-40} {1}", "Character Normalizations:", getSupportedMessage(caps.SupportsCharacterNormalizations));
				outputWriter.WriteLine("{0,-40} {1}", "DateTime Measure Named Entities:", getSupportedMessage(caps.SupportsDateTimeMeasureNamedEntities));
				outputWriter.WriteLine("{0,-40} {1}", "Person Named Entities:", getSupportedMessage(caps.SupportsPersonNamedEntities));
				outputWriter.WriteLine("{0,-40} {1}", "Location Named Entities:", getSupportedMessage(caps.SupportsLocationNamedEntities));
				outputWriter.WriteLine("{0,-40} {1}", "Organization Named Entities:", getSupportedMessage(caps.SupportsOrganizationNamedEntities));
				outputWriter.WriteLine("{0,-40} {1}", "Word Normalizations:", getSupportedMessage(caps.SupportsWordNormalizations));
				outputWriter.WriteLine("{0,-40} {1}", "Compounding:", getSupportedMessage(caps.SupportsCompounding));
				outputWriter.WriteLine("{0,-40} {1}", "Inflections:", getSupportedMessage(caps.SupportsInflections));
				outputWriter.WriteLine("{0,-40} {1}", "Chunking:", getSupportedMessage(caps.SupportsChunks));
				outputWriter.WriteLine("{0,-40} {1}", "SpellChecking:", getSupportedMessage(caps.SupportsSpellChecking));
			}
		}

		private string getSupportedMessage(bool supported)
		{
			if (supported)
			{
				return "Supported";
			}
			else
			{
				return "Not Supported";
			}
		}

		private Context m_context;
	} // class Langcap
}

