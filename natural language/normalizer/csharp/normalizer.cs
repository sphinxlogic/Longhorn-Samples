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
	public class Normalizer
	{
		public Normalizer(CmdOpts opts)
		{
			Context context = new Context();

			// Set the following context properties 
			context.IsSpellChecking = false;
			context.IsShowingCharacterNormalizations = true;
			context.IsFindingDateTimeMeasures = true;
			context.IsComputingLemmas = true;
			context.IsCheckingRepeatedWords = false;
			context.IsComputingInflections = false;
			context.IsShowingGaps = false;
			context.IsSingleLanguage = false;
			context.IsSpellAlwaysSuggesting = false;
			context.IsSpellIgnoringAllUpperCase = false;
			context.IsSpellIgnoringWordsWithNumbers = false;
			context.IsSpellPreReform = true;
			context.IsSpellRequiringAccentedCapitals = false;
			context.IsFindingLocations = false;
			context.IsFindingOrganizations = false;
			context.IsFindingPersons = false;
			context.IsFindingPhrases = false;
			context.IsComputingCompounds = true;
			m_TextChunk = new TextChunk(context);
			m_TextChunk.Culture = opts.Culture;
			m_fInteractiveMode = false;
		}

		public static int Main(string[] args)
		{
			int retCode = 0;
			DateTime startTime = DateTime.Now;
			DateTime endTime = DateTime.Now;
			TimeSpan timeDiff;
			String input;
			CmdOpts opts = null;
			Normalizer no = null;
			TextWriter outputWriter = null;
			TextReader inputReader = null;

			try
			{
				startTime = DateTime.Now;
				opts = new CmdOpts();
				if (opts.ProcessOpts(args))
				{
					no = new Normalizer(opts);
					if (opts.Output.Length != 0)
					{
						if (File.Exists(opts.Output)) File.Delete(opts.Output);

						outputWriter = new StreamWriter(opts.Output);
					}
					else
					{
						outputWriter = new StreamWriter(Console.OpenStandardOutput());
					}

					if (opts.Input.Length != 0)
					{
						no.Parse(opts.Input, outputWriter);
					}
					else
					{
						no.InteractiveMode = true;
						no.PrintBanner(outputWriter);
						while (no.InteractiveMode)
						{
							outputWriter.Write("nlg> ");
							outputWriter.Flush();
							input = Console.ReadLine();
							outputWriter.Flush();
							if (String.Compare(input, "stat", true, CultureInfo.InvariantCulture) == 0)
							{
								no.ShowStatistics(outputWriter);
							}
							else
							{
								if (String.Compare(input, "quit", true, CultureInfo.InvariantCulture) == 0)
								{
									outputWriter.WriteLine("Goodbye!!");
									no.InteractiveMode = false;
								}
								else
								{
									if (String.Compare(input, "help", true, CultureInfo.InvariantCulture) == 0)
									{
										no.PrintHelp(outputWriter);
									}
									else
									{
										if (String.Compare(input, "clear", true, CultureInfo.InvariantCulture) == 0)
										{
											no.ClearStatistics();
										}
										else
										{
											inputReader = new StringReader(input);
											no.Parse(inputReader, outputWriter);
										}
									}
								}
							}
						}
					}
				}

				retCode = 0;
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
					outputWriter.Close();
				}

				if (inputReader != null) inputReader.Close();
			}
			return retCode;
		}

		public void PrintBanner(TextWriter outputWriter)
		{
			outputWriter.WriteLine("NLG Interactive Console");
			outputWriter.WriteLine("Copyright (c) 2004 Microsoft Corporation, Inc. All Rights Reserved");
			outputWriter.WriteLine("Type 'help' for more information");
			outputWriter.Flush();
		}

		public void PrintHelp(TextWriter outputWriter)
		{
			outputWriter.WriteLine("Text entered at the prompt will be analyzed by the NLG Engine");
			outputWriter.WriteLine("The following commands are supported in interactive console mode:");
			outputWriter.WriteLine("  STAT       Shows session statistics");
			outputWriter.WriteLine("  QUIT       Exits from interactive mode");
			outputWriter.WriteLine("  HELP       Prints this help message");
			outputWriter.WriteLine("  CLEAR      Clears session statistics");
			outputWriter.Flush();
		}

		public void Parse(String inputPath, TextWriter outputWriter)
		{
			StreamReader inputReader = null;

			if (outputWriter == null) return;

			try
			{
				if (inputPath.Length != 0)
				{
					if (File.Exists(inputPath))
					{
						FileInfo f = new FileInfo(inputPath);
						FileAttributes fattrib = f.Attributes;

						//Skip empty files, hidden files, system files and temporary files
						if ((f.Length > 0) && (fattrib != (fattrib | FileAttributes.Hidden)) && (fattrib != (fattrib | FileAttributes.System)) && (fattrib != (fattrib | FileAttributes.Temporary)))
						{
							inputReader = new StreamReader(inputPath);
						}
					}
					else
					{
						if (Directory.Exists(inputPath))
						{
							parseDir(inputPath, outputWriter);
						}
						else
						{
							throw new ArgumentException("Specified Input File does not exist");
						}
					}
				}
				else
				{
					inputReader = new StreamReader(Console.OpenStandardInput());
				}

				if (inputReader != null)
				{
					this.Parse(inputReader, outputWriter);
				}
			}
			catch (ArgumentException ae)
			{
				throw ae;
			}
			finally
			{
				if (inputReader != null) inputReader.Close();
			}
		}

		public void Parse(TextReader inputReader, TextWriter outputWriter)
		{
			m_iNumFilesParsed++;

			String input = inputReader.ReadToEnd();
			int numNormalizations = 0;

			m_TextChunk.InputText = input;

			foreach (Sentence sentence in m_TextChunk)
			{
				m_iNumSentencesParsed++;
				foreach (Segment segment in sentence)
				{
					m_iNumSegmentsParsed++;
					if (emitSegmentInfo(outputWriter, segment, "")) numNormalizations++;
				}
			}

			if (numNormalizations == 0) outputWriter.WriteLine("<No Normalizations Found>");
		}

		public void ShowStatistics(TextWriter outputWriter)
		{
			if (m_fInteractiveMode)
			{
				outputWriter.WriteLine("{0,-30} {1}", "Num. Text:", m_iNumFilesParsed);
			}
			else
			{
				outputWriter.WriteLine("{0,-30} {1}", "Num. Files:", m_iNumFilesParsed);
			}

			outputWriter.WriteLine("{0,-30} {1}", "Num. Sentences:", m_iNumSentencesParsed);
			outputWriter.WriteLine("{0,-30} {1}", "Num. Segments:", m_iNumSegmentsParsed);
			outputWriter.WriteLine();
			outputWriter.Flush();
		}

		public void ClearStatistics()
		{
			m_iNumFilesParsed = 0;
			m_iNumSentencesParsed = 0;
			m_iNumSegmentsParsed = 0;
		}

		public bool InteractiveMode
		{
			get { return m_fInteractiveMode; }
			set { m_fInteractiveMode = value; }
		}

		private TextChunk m_TextChunk;

		private void parseDir(String inputDir, TextWriter outputWriter)
		{
			// Process the list of files found in the target directory
			string[] fileEntries = Directory.GetFiles(inputDir);

			for (int i = 0; i < fileEntries.Length; i++)
			{
				this.Parse(fileEntries[i], outputWriter);
			}

			// Recurse into subdirectories of the target directory
			string[] subdirectoryEntries = Directory.GetDirectories(inputDir);

			for (int i = 0; i < subdirectoryEntries.Length; i++)
			{
				parseDir(subdirectoryEntries[i], outputWriter);
			}
		}

		private bool emitSegmentInfo(TextWriter outputWriter, Segment segment, String padding)
		{
			bool retValue = false;

			if (segment.CharacterNormalizations.Count > 0)
			{
				retValue = true;
				outputWriter.WriteLine("{0}{1}", padding, segment.ToString());
				outputWriter.WriteLine("{0} [Character Normalizations: {1}]", padding, segment.CharacterNormalizations.Count.ToString(NumberFormatInfo.InvariantInfo));
				padding = String.Concat(padding, " ");
			}

			foreach (Segment subsegment in segment.SubSegments)
			{
				if (emitSegmentInfo(outputWriter, subsegment, padding)) retValue = true;
			}

			return retValue;
		}

		private bool m_fInteractiveMode;

		private int m_iNumFilesParsed;

		private int m_iNumSentencesParsed;

		private int m_iNumSegmentsParsed;
	} // class Normalizer
}
