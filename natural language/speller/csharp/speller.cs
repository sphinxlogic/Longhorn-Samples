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
	public class SpellerService
	{
		public SpellerService(CmdOpts opts)
		{
			Context context = new Context();

			// Set the following context properties to false
			context.IsSpellChecking = true;
			context.IsCheckingRepeatedWords = true;
			context.IsComputingInflections = false;
			context.IsShowingCharacterNormalizations = false;
			context.IsShowingGaps = false;
			context.IsShowingWordNormalizations = false;
			context.IsSingleLanguage = false;
			context.IsSpellAlwaysSuggesting = false;
			context.IsSpellIgnoringAllUpperCase = false;
			context.IsSpellIgnoringWordsWithNumbers = false;
			context.IsSpellPreReform = false;
			context.IsSpellRequiringAccentedCapitals = false;
			context.IsComputingLemmas = false;
			context.IsFindingDateTimeMeasures = false;
			context.IsFindingLocations = false;
			context.IsFindingOrganizations = false;
			context.IsFindingPersons = false;
			context.IsFindingPhrases = false;
			context.IsComputingCompounds = false;
			if (opts.LexiconList.Count > 0)
				context.Lexicons = opts.LexiconList;

			m_TextChunk = new TextChunk(context);
			m_TextChunk.Culture = opts.Culture;
			m_fInteractiveMode = false;
			m_iNumFiles = 0;
			m_iNumSegments = 0;
			m_iNumSentences = 0;
			m_SpellingErrors = new SortedList();
		}

		public static int Main(string[] args)
		{
			int retCode = 0;
			CmdOpts opts = null;
			SpellerService ss = null;
			TextWriter outputWriter = null;
			TextReader inputReader = null;
			string input = "";

			try
			{
				opts = new CmdOpts();
				if (opts.ProcessOpts(args))
				{
					ss = new SpellerService(opts);
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
						ss.Parse(opts.Input, outputWriter);
					}
					else
					{
						ss.InteractiveMode = true;
						ss.PrintBanner(outputWriter);
						while (ss.InteractiveMode)
						{
							outputWriter.Write("nlg> ");
							outputWriter.Flush();
							input = Console.ReadLine();
							if (String.Compare(input, "stat", true, CultureInfo.InvariantCulture) == 0)
							{
								ss.ShowStatistics(outputWriter);
							}
							else
							{
								if (String.Compare(input, "quit", true, CultureInfo.InvariantCulture) == 0)
								{
									outputWriter.WriteLine("Goodbye!!");
									ss.InteractiveMode = false;
								}
								else
								{
									if (String.Compare(input, "help", true, CultureInfo.InvariantCulture) == 0)
									{
										ss.PrintHelp(outputWriter);
									}
									else
									{
										if (String.Compare(input, "clear", true, CultureInfo.InvariantCulture) == 0)
										{
											ss.ClearStatistics();
										}
										else
										{
											inputReader = new StringReader(input);
											ss.Parse(inputReader, outputWriter);
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
				if (outputWriter != null) outputWriter.Close();

				if (inputReader != null) inputReader.Close();
			}
			return retCode;
		}

		public bool InteractiveMode
		{
			get { return m_fInteractiveMode; }
			set { m_fInteractiveMode = value; }
		}

		public void PrintBanner(TextWriter outputWriter)
		{
			outputWriter.WriteLine("NLG Interactive Console");
			outputWriter.WriteLine("Copyright (c) 2004 Microsoft Corporation, Inc. All Rights Reserved");
			outputWriter.WriteLine("Type 'help' for more information");
			outputWriter.Flush();
		}

		public void ShowStatistics(TextWriter outputWriter)
		{
			if (m_fInteractiveMode)
			{
				outputWriter.WriteLine("{0,-30} {1}", "Num. Text Blocks:", m_iNumFiles);
			}
			else
			{
				outputWriter.WriteLine("{0,-30} {1}", "Num. Files:", m_iNumFiles);
			}

			outputWriter.WriteLine("{0,-30} {1}", "Num. Sentences:", m_iNumSentences);
			outputWriter.WriteLine("{0,-30} {1}", "Num. Segments:", m_iNumSegments);
			outputWriter.WriteLine();
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

		public void ClearStatistics()
		{
			m_SpellingErrors.Clear();
			m_iNumFiles = 0;
			m_iNumSentences = 0;
			m_iNumSegments = 0;
		}

		public void Parse(string inputPath, TextWriter outputWriter)
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
			m_iNumFiles++;

			string input = inputReader.ReadToEnd();

			m_TextChunk.InputText = input;

			int numMisspelled = 0;

			foreach (Sentence sentence in m_TextChunk)
			{
				m_iNumSentences++;
				foreach (Segment segment in sentence)
				{
					m_iNumSegments++;
					if (emitSegmentInfo(outputWriter, segment)) numMisspelled++;
				}
			}

			if (numMisspelled == 0)
				outputWriter.WriteLine("<No spelling errors found>");
		}

		private TextChunk m_TextChunk;

		private SortedList m_SpellingErrors;

		private void parseDir(string inputDir, TextWriter outputWriter)
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

		private bool emitSegmentInfo(TextWriter outputWriter, Segment segment)
		{
			bool retValue = false;

			if (segment.Role == RangeRole.Incorrect || segment.Role == RangeRole.AutoReplaceForm || segment.Role == RangeRole.Repeated)
			{
				outputWriter.WriteLine("{0} ({1})", segment.ToString(), segment.Role.ToString());
				outputWriter.WriteLine(" [Suggestions: {0}]", segment.Suggestions.Count.ToString(NumberFormatInfo.InvariantInfo));
				foreach (Segment suggestion in segment.Suggestions)
				{
					outputWriter.WriteLine("   {0} - {1}", suggestion.ToString(), suggestion.SpellingScore.ToString(NumberFormatInfo.InvariantInfo));
				}

				retValue = true;
			}

			outputWriter.Flush();
			return retValue;
		}

		int m_iNumFiles;

		int m_iNumSegments;

		int m_iNumSentences;

		bool m_fInteractiveMode;
	} // class SpellerService
}
