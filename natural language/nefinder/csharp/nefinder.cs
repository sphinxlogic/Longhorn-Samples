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
	public class NEFinder
	{
		public NEFinder(CmdOpts opts)
		{
			m_opts = opts;

			Context context = new Context();

			// Set the following context properties 
			context.IsSpellChecking = false;
			context.IsFindingLocations = opts.IsFindingLocations;
			context.IsFindingOrganizations = opts.IsFindingOrganizations;
			context.IsFindingPersons = opts.IsFindingPersons;
			context.IsFindingPhrases = opts.IsFindingPhrases;
			context.IsShowingCharacterNormalizations = false;
			context.IsShowingWordNormalizations = false;
			context.IsFindingDateTimeMeasures = false;
			context.IsComputingLemmas = false;
			context.IsCheckingRepeatedWords = false;
			context.IsComputingInflections = false;
			context.IsShowingGaps = false;
			context.IsSingleLanguage = false;
			context.IsSpellAlwaysSuggesting = false;
			context.IsSpellIgnoringAllUpperCase = false;
			context.IsSpellIgnoringWordsWithNumbers = false;
			context.IsSpellPreReform = false;
			context.IsSpellRequiringAccentedCapitals = false;
			context.IsComputingCompounds = false;
			m_NamedEntityList = new SortedList();
			m_TextChunk = new TextChunk(context);
			m_TextChunk.Culture = opts.Culture;
			m_fInteractiveMode = false;
			m_iNumFiles = 0;
			m_iNumSentences = 0;
			m_iNumSegments = 0;
			m_iNumNamedEntities = 0;
		}

		public static int Main(string[] args)
		{
			int retCode = 0;
			DateTime startTime = DateTime.Now;
			DateTime endTime = DateTime.Now;
			TimeSpan timeDiff;
			String input;
			CmdOpts opts = null;
			NEFinder ne = null;
			TextWriter outputWriter = null;
			TextReader inputReader = null;

			try
			{
				startTime = DateTime.Now;
				opts = new CmdOpts();
				if (opts.ProcessOpts(args))
				{
					ne = new NEFinder(opts);
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
						ne.Parse(opts.Input, outputWriter);
					}
					else
					{
						ne.InteractiveMode = true;
						ne.PrintBanner(outputWriter);
						while (ne.InteractiveMode)
						{
							outputWriter.Write("nlg> ");
							outputWriter.Flush();
							input = Console.ReadLine();
							if (String.Compare(input, "stat", true, CultureInfo.InvariantCulture) == 0)
							{
								ne.ShowStatistics(outputWriter);
							}
							else
							{
								if (String.Compare(input, "quit", true, CultureInfo.InvariantCulture) == 0)
								{
									outputWriter.WriteLine("Goodbye!!");
									ne.InteractiveMode = false;
								}
								else
								{
									if (String.Compare(input, "help", true, CultureInfo.InvariantCulture) == 0)
									{
										ne.PrintHelp(outputWriter);
									}
									else
									{
										if (String.Compare(input, "clear", true, CultureInfo.InvariantCulture) == 0)
										{
											ne.ClearStatistics();
										}
										else
										{
											inputReader = new StringReader(input);
											ne.Parse(inputReader, outputWriter);
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
			int numNamedEntities = 0;

			m_iNumFiles++;

			String input = inputReader.ReadToEnd();

			m_TextChunk.InputText = input;
			foreach (Sentence sentence in m_TextChunk)
			{
				m_iNumSentences++;
				foreach (Segment segment in sentence)
				{
					m_iNumSegments++;
					if (emitSegmentInfo(outputWriter, segment, ""))
					{
						numNamedEntities++;
						m_iNumNamedEntities++;
					}
				}
			}

			if (numNamedEntities == 0) outputWriter.WriteLine("<No Named Entities Found>");
		}

		public void ShowStatistics(TextWriter outputWriter)
		{
			if (m_fInteractiveMode)
			{
				outputWriter.WriteLine("{0,-30} {1}", "Num. Text Entries:", m_iNumFiles);
			}
			else
			{
				outputWriter.WriteLine("{0,-30} {1}", "Num. Files Parsed:", m_iNumFiles);
			}

			outputWriter.WriteLine("{0,-30} {1}", "Num. Sentences Parsed:", m_iNumSentences);
			outputWriter.WriteLine("{0,-30} {1}", "Num. Segments Parsed:", m_iNumSegments);
			outputWriter.WriteLine("{0,-30} {1}", "Num. Named Entities:", m_iNumNamedEntities);
			outputWriter.WriteLine();
			outputWriter.Flush();
		}

		public void ClearStatistics()
		{
			m_iNumFiles = 0;
			m_iNumSentences = 0;
			m_iNumSegments = 0;
			m_iNumNamedEntities = 0;
		}

		public bool InteractiveMode
		{
			get { return m_fInteractiveMode; }
			set { m_fInteractiveMode = value; }
		}

		private TextChunk m_TextChunk;

		private SortedList m_NamedEntityList;

		private CmdOpts m_opts;

		private void parseDir(String inputDir, TextWriter outputWriter)
		{
			// Process the list of files found in the target directory
			String[] fileEntries = Directory.GetFiles(inputDir);

			for (int i = 0; i < fileEntries.Length; i++)
			{
				this.Parse(fileEntries[i], outputWriter);
			}

			// Recurse into subdirectories of the target directory
			String[] subdirectoryEntries = Directory.GetDirectories(inputDir);

			for (int i = 0; i < subdirectoryEntries.Length; i++)
			{
				parseDir(subdirectoryEntries[i], outputWriter);
			}
		}

		private bool emitSegmentInfo(TextWriter outputWriter, Segment segment, String padding)
		{
			bool retValue = false;
			String role = (Enum.Format(typeof(System.NaturalLanguage.RangeRole), segment.Role, "G"));
			String primaryType = (Enum.Format(typeof(System.NaturalLanguage.PrimaryRangeType), segment.PrimaryType, "G"));

			if (String.Compare(role, "NamedEntity", true, CultureInfo.InvariantCulture) == 0)
			{
				if ((m_opts.IsFindingDateTimeMeasures) && ((String.Compare(primaryType, "Date") == 0) || (String.Compare(primaryType, "Time") == 0)))
				{
					outputWriter.WriteLine("{0}{1} (Pri. Type={2}, Role={3})", padding, segment.ToString(), primaryType, role);
					retValue = true;
				}

				if ((m_opts.IsFindingLocations) && ((String.Compare(primaryType, "LocationName") == 0)))
				{
					outputWriter.WriteLine("{0}{1} (Pri. Type={2}, Role={3})", padding, segment.ToString(), primaryType, role);
					retValue = true;
				}

				if ((m_opts.IsFindingOrganizations) && ((String.Compare(primaryType, "OrganizationName") == 0)))
				{
					outputWriter.WriteLine("{0}{1} (Pri. Type={2}, Role={3})", padding, segment.ToString(), primaryType, role);
					retValue = true;
				}

				if ((m_opts.IsFindingPersons) && (String.Compare(primaryType, "PersonName") == 0))
				{
					outputWriter.WriteLine("{0}{1} (Pri. Type={2}, Role={3})", padding, segment.ToString(), primaryType, role);
					retValue = true;
				}

				if ((m_opts.IsFindingPhrases) && ((String.Compare(primaryType, "VerbPhrase") == 0) || (String.Compare(primaryType, "NounPhrase") == 0) || (String.Compare(primaryType, "PrepositionalPhrase") == 0)))
				{
					outputWriter.WriteLine("{0}{1} (Pri. Type={2}, Role={3})", padding, segment.ToString(), primaryType, role);
					retValue = true;
				}
			}

			foreach (Segment subsegment in segment.SubSegments)
			{
				if (emitSegmentInfo(outputWriter, subsegment, padding))
				{
					retValue = true;
				}
			}

			return retValue;
		}

		private bool m_fInteractiveMode;

		private int m_iNumFiles;

		private int m_iNumSentences;

		private int m_iNumSegments;

		private int m_iNumNamedEntities;
	} // class NEFinder
}
