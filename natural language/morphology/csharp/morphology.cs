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
	public class MorphService
	{
		MorphService(CmdOpts opts)
		{
			Context context = new Context();

			// Set the following context properties
			context.IsComputingLemmas = true;
			context.IsComputingInflections = true;
			context.IsComputingCompounds = true;
			context.IsSpellChecking = false;
			context.IsCheckingRepeatedWords = false;
			context.IsShowingCharacterNormalizations = false;
			context.IsShowingGaps = false;
			context.IsShowingWordNormalizations = false;
			context.IsSingleLanguage = false;
			context.IsSpellAlwaysSuggesting = false;
			context.IsSpellIgnoringAllUpperCase = false;
			context.IsSpellIgnoringWordsWithNumbers = false;
			context.IsSpellPreReform = false;
			context.IsSpellRequiringAccentedCapitals = false;
			context.IsFindingDateTimeMeasures = false;
			context.IsFindingLocations = false;
			context.IsFindingOrganizations = false;
			context.IsFindingPersons = false;
			context.IsFindingPhrases = false;
			m_fInteractiveMode = false;
			m_TextChunk = new TextChunk(context);
			m_TextChunk.Culture = opts.Culture;
			m_culture = opts.Culture;
			m_opts = opts;
		}

		public static int Main(string[] args)
		{
			int retCode = 0;
			DateTime startTime = DateTime.Now;
			DateTime endTime = DateTime.Now;
			TimeSpan timeDiff;
			CmdOpts opts = null;
			MorphService ms = null;
			TextWriter outputWriter = null;
			TextReader inputReader = null;
			string input = "";

			try
			{
				startTime = DateTime.Now;
				opts = new CmdOpts();
				if (opts.ProcessOpts(args))
				{
					ms = new MorphService(opts);
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
						ms.Parse(opts.Input, outputWriter);
					}
					else
					{
						// Interactive Console Mode
						ms.InteractiveMode = true;
						ms.PrintBanner(outputWriter);
						while (ms.InteractiveMode)
						{
							outputWriter.Write("nlg> ");
							outputWriter.Flush();
							input = Console.ReadLine();
							outputWriter.Flush();
							if (String.Compare(input, "stat", true, CultureInfo.InvariantCulture) == 0)
							{
								ms.ShowStatistics(outputWriter);
							}
							else
							{
								if (String.Compare(input, "quit", true, CultureInfo.InvariantCulture) == 0)
								{
									outputWriter.WriteLine("Goodbye!!");
									ms.InteractiveMode = false;
								}
								else
								{
									if (String.Compare(input, "help", true, CultureInfo.InvariantCulture) == 0)
									{
										ms.PrintHelp(outputWriter);
									}
									else
									{
										if (String.Compare(input, "clear", true, CultureInfo.InvariantCulture) == 0)
										{
											ms.ClearStatistics();
										}
										else
										{
											inputReader = new StringReader(input);
											ms.Parse(inputReader, outputWriter);
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
				if (outputWriter != null) outputWriter.Close();

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

			string input = inputReader.ReadToEnd();

			m_TextChunk.InputText = input;

			int numSentencesParsed = 0;
			int numSegmentsParsed = 0;

			foreach (Sentence sentence in m_TextChunk)
			{
				m_iNumSentencesParsed++;
				numSentencesParsed++;
				outputWriter.WriteLine(sentence.ToString());
				foreach (Segment segment in sentence)
				{
					m_iNumSegmentsParsed++;
					numSegmentsParsed++;
					emitSegmentInfo(outputWriter, segment);
				}
			}

			if ((m_fInteractiveMode) && (m_opts.Verbose))
			{
				outputWriter.WriteLine("{0,-30} {1}", "Num. Sentences:", numSentencesParsed);
				outputWriter.WriteLine("{0,-30} {1}", "Num. Segments:", numSegmentsParsed);
				outputWriter.WriteLine();
				outputWriter.Flush();
			}
		}

		public void ShowStatistics(TextWriter outputWriter)
		{
			if (m_fInteractiveMode)
			{
				outputWriter.WriteLine("{0,-30} {1}", "Num. Text Blocks:", m_iNumFilesParsed);
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

		private CultureInfo m_culture;

		private TextChunk m_TextChunk;

		private CmdOpts m_opts;

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

		private void emitSegmentInfo(TextWriter outputWriter, Segment segment)
		{
			outputWriter.WriteLine(segment.ToString());
			outputWriter.WriteLine(" [Lemmas: {0}]", segment.Lemmas.Count);
			for (int i = 0; i < segment.Lemmas.Count; i++)
			{
				outputWriter.WriteLine("  {0}", segment.Lemmas[i].ToString());
			}

			outputWriter.WriteLine(" [Inflections: {0}]", segment.Inflections.Count);
			for (int i = 0; i < segment.Inflections.Count; i++)
			{
				outputWriter.WriteLine("  {0}", segment.Inflections[i].ToString());
			}
		}

		bool m_fInteractiveMode;

		int m_iNumFilesParsed;

		int m_iNumSentencesParsed;

		int m_iNumSegmentsParsed;
	} // class MorphService
}
