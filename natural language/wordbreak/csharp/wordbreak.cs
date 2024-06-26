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
    public class WordBreaker
    {
        public WordBreaker(CmdOpts opts) {
            // Initialize members
            m_iNumSentences = 0;
            m_iNumSegments = 0;
            m_iNumFiles = 0;
            m_iMaxWidth = 80;
    
            // Create a context object
            Context context = new Context();
            // Set the following context properties to false
            context.IsSpellChecking = false;
            context.IsCheckingRepeatedWords = false;
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
            // Set the following context properties based upon command line arguments
            context.IsComputingLemmas = opts.Lemma;
            context.IsFindingDateTimeMeasures = opts.IsFindingDateTimeMeasures;
            context.IsFindingLocations = opts.IsFindingLocations;
            context.IsFindingOrganizations = opts.IsFindingOrganizations;
            context.IsFindingPersons = opts.IsFindingPersons;
            context.IsFindingPhrases = opts.IsFindingPhrases;
            context.IsComputingCompounds = opts.Compounds;
            m_fInteractiveMode = false;
            // Create a textchunk object and set locale based based upon command line argument
            m_TextChunk = new TextChunk (context);
            m_TextChunk.Culture = opts.Culture;
            m_culture = opts.Culture;
        }
        
        public static int Main (string[] args)
        {
            CmdOpts opts = null;
            WordBreaker wb = null;
            string input;
            TextReader inputReader = null;
            TextWriter outputWriter = null;
            int retCode = 0;
        
            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now;
            TimeSpan timeDiff;
        
            try 
            {
                opts = new CmdOpts();
                if(opts.ProcessOpts(args))
                {
                    wb = new WordBreaker(opts);
                    if (opts.Output.Length != 0)
                    {
                        if (File.Exists(opts.Output)) File.Delete(opts.Output);
                            outputWriter = new StreamWriter(opts.Output);
                    } else {
                        outputWriter = new StreamWriter(Console.OpenStandardOutput());
                    }
                   wb.MaxWidth = Console.WindowWidth;
                   if (opts.Input.Length != 0)
                   {           
                           wb.Parse(opts.Input, outputWriter);
                   } else {
                       wb.InteractiveMode = true;
                       wb.PrintBanner(outputWriter);
                       while(wb.InteractiveMode)
                       {
                           outputWriter.Write("nlg> ");
                           outputWriter.Flush();
                           
                           input = Console.ReadLine();
                           
                           if(String.Compare(input, "stat", true, CultureInfo.InvariantCulture) == 0)
                           {
                              wb.ShowStatistics(outputWriter);
                           } else {
                           if(String.Compare(input, "quit", true, CultureInfo.InvariantCulture) == 0)
                           {
                              outputWriter.WriteLine("Goodbye!!");
                              wb.InteractiveMode = false;
                           } else {
                           if(String.Compare(input, "help", true, CultureInfo.InvariantCulture) == 0)
                           {
                              wb.PrintHelp(outputWriter);
                           } else {
                           if(String.Compare(input, "clear", true, CultureInfo.InvariantCulture) == 0)
                           {
                              wb.ClearStatistics();
                           } else {
                              inputReader = new StringReader(input);
                              wb.Parse(inputReader, outputWriter);
                           }
                           }
                           }
                           }
                       }
                   }
               }
    
                retCode = 0;
            } 
            catch(NullReferenceException nre) 
            {
                Console.Error.WriteLine ("Message: {0}", nre.Message);
                Console.Error.WriteLine ("Stack Trace: {0}", nre.StackTrace);
                retCode = 1;
            } 
            catch(ArgumentException e)
            {
                Console.Error.WriteLine ("Message: {0}", e.Message);
                Console.Error.WriteLine ("Stack Trace: {0}", e.StackTrace);
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

        public int MaxWidth 
        { 
            get { return m_iMaxWidth; }
            set { m_iMaxWidth = value; }
        }
        
        public bool InteractiveMode { 
            get { return m_fInteractiveMode; }
            set { m_fInteractiveMode = value; }
        }
        
        public int NumFiles { 
            get {return m_iNumFiles; }
        }
        
        public int NumSegments { 
            get { return m_iNumSegments; }
        }
        
        public int NumSentences { 
            get { return m_iNumSentences; }
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
            if(m_fInteractiveMode)
            {
                outputWriter.WriteLine("{0,-30} {1}", "Num. Text Blocks Parsed:", m_iNumFiles);
            } else {
                outputWriter.WriteLine("{0,-30} {1}", "Num. Files Parsed:", m_iNumFiles);
            }
            outputWriter.WriteLine("{0,-30} {1}", "Num. Sentences Parsed:", m_iNumSentences);
            outputWriter.WriteLine("{0,-30} {1}", "Num. Segments Parsed:", m_iNumSegments);
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
            m_iNumFiles= 0;
            m_iNumSentences= 0;
            m_iNumSegments= 0;
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
                    Parse(inputReader, outputWriter);
                }
            }
            catch(ArgumentException ae)
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
            string padding = "";
            string input = inputReader.ReadToEnd();
            int numSentences = 0;
            int numSegments = 0;
            m_TextChunk.InputText = input;
            foreach(Sentence sentence in m_TextChunk)
            {
                m_iNumSentences++;
                numSentences++;
                padding = "";
                outputWriter.WriteLine("Sentence #{0}: {1}", numSentences, sentence.ToString());
                outputWriter.WriteLine("(Culture={0}, StartPos={1}, Len={2}, EOP={3})", 
                sentence.Culture.ToString(), 
                sentence.Range.Start.ToString(System.Globalization.CultureInfo.CurrentCulture.NumberFormat),
                sentence.Range.Length.ToString(System.Globalization.CultureInfo.CurrentCulture.NumberFormat),
                sentence.IsEndOfParagraph.ToString());
                foreach(Segment segment in sentence)
                {
                    m_iNumSegments++;
                    numSegments++;
                    emitSegmentInfo(segment, outputWriter, padding);
                }
            }
            outputWriter.WriteLine();
            outputWriter.Flush();
        }
    
        private void parseDir(string inputDir, TextWriter outputWriter)
        {
                
            // Process the list of files found in the target directory
            string[] fileEntries = Directory.GetFiles(inputDir);
        
            for (int i = 0; i < fileEntries.Length; i++)
            {
                Parse(fileEntries[i], outputWriter);
            }
        
            // Recurse into subdirectories of the target directory
            string[] subdirectoryEntries = Directory.GetDirectories(inputDir);
        
            for (int i = 0; i < subdirectoryEntries.Length; i++)
            {
                parseDir(subdirectoryEntries[i], outputWriter);
            }
        }
        private void emitSegmentInfo(System.NaturalLanguage.Segment segment, TextWriter outputWriter, string padding)
        {
            StringBuilder sb = new StringBuilder();
            string segmentInfo = "";
            string format = "";
            string format2 = "";
        
            sb.Append(" (");
            if (segment.IsFeminine) sb.Append("Fem., ");
            if (segment.IsMasculine) sb.Append("Masc., ");
            if (segment.IsNeuter) sb.Append("Neuter, ");
            if (segment.IsNoun) sb.Append("N., ");
            if (segment.IsVerb) sb.Append("V., ");
            if (segment.IsPronoun) sb.Append("Pron., ");
            if (segment.IsModalVerb) sb.Append("Modal V., ");
            if (segment.IsAbbreviation) sb.Append("Abbrev., ");
            if (segment.IsAdjective) sb.Append("Adj., ");
            if (segment.IsAdverb) sb.Append("Adv., ");
            if (segment.IsAuxiliaryVerb) sb.Append("Aux. Verb, ");
            if (segment.IsCharacter) sb.Append("Char., ");
            if (segment.IsConjunction) sb.Append("Conj., ");
            if (segment.IsPreposition) sb.Append("Prep., ");
            if (segment.IsSingular) sb.Append("Sing., ");
            if (segment.IsPlural) sb.Append("Plural, ");
            if (segment.IsCharacter) sb.Append("Char., ");
            if ((segment.IsEndPunctuation) || (segment.IsPunctuation)) sb.Append("Punc., ");
            if (segment.IsFutureTense) sb.Append("Future, ");
            if (segment.IsPresentTense) sb.Append("Present, " );
            if (segment.IsPastTense) sb.Append("Past, ");
            if (segment.IsFirstPerson) sb.Append("1st person, ");
            if (segment.IsThirdPerson) sb.Append("3rd person, ");
            if (segment.IsSmiley) sb.Append("Smiley, ");
            if (segment.IsInterjection) sb.Append("Interjection, ");
            sb.Append(Enum.Format(typeof(System.NaturalLanguage.RangeRole), segment.Role, "G"));
            sb.Append(")");
        
            segmentInfo = sb.ToString();
            
            format = "{0}{1} {2}";
            
            outputWriter.WriteLine(format, padding, segment.ToString(), segmentInfo);
            format  = "{0}   {1}";
            format2 = "{0}    {1}";

            if (segment.Alternatives.Count > 0)
            {
                outputWriter.WriteLine(format, padding, String.Concat("[Alternatives: ", segment.Alternatives.Count.ToString(CultureInfo.CurrentCulture.NumberFormat), "]"));
                foreach(Segment alternative in segment.Alternatives)
                {
                    outputWriter.WriteLine(format2, padding, alternative.ToString());
                }
            }
        
            if (segment.CharacterNormalizations.Count > 0)
            {
                outputWriter.WriteLine(format, padding, String.Concat("[Character Normalizations: ", segment.CharacterNormalizations.Count.ToString(CultureInfo.CurrentCulture.NumberFormat), "]"));
                foreach(Segment normalization in segment.CharacterNormalizations)
                {
                    outputWriter.WriteLine(format2, padding, normalization.ToString());
                }
            }
        
            if (segment.Inflections.Count > 0)
            {
                outputWriter.WriteLine(format, padding, String.Concat("[Inflections: ", segment.Inflections.Count.ToString(CultureInfo.CurrentCulture.NumberFormat), "]"));
                foreach(Segment inflection in segment.Inflections)
                {
                    outputWriter.WriteLine(format2, padding, inflection.ToString());
                }
            }
        
            if (segment.Lemmas.Count > 0)
            {
                outputWriter.WriteLine(format, padding, String.Concat("[Lemmas: ", segment.Lemmas.Count.ToString(CultureInfo.CurrentCulture.NumberFormat), "]"));
                foreach(Segment lemma in segment.Lemmas)
                {
                    outputWriter.WriteLine(format2, padding, lemma.ToString());
                }
            }
        
            if (segment.Representations.Count > 0)
            {
                outputWriter.WriteLine(format, padding, String.Concat("[Representations: ", segment.Representations.Count.ToString(CultureInfo.CurrentCulture.NumberFormat), "]"));
                foreach(Object reps in segment.Representations)
                {
                    outputWriter.WriteLine(format2, padding, reps.ToString());
                }
            }
        
            if (segment.SpellingVariations.Count > 0)
            {
                outputWriter.WriteLine(format, padding, String.Concat("[SpellingVariations: ", segment.SpellingVariations.Count.ToString(CultureInfo.CurrentCulture.NumberFormat), "]"));
                
                foreach(Segment variation in segment.SpellingVariations)
                {
                    outputWriter.WriteLine(format2, padding, variation);
                }
            }
        
            if (segment.Suggestions.Count > 0)
            {
                outputWriter.WriteLine(format, padding, String.Concat("[Suggestions: ", segment.Suggestions.Count.ToString(CultureInfo.CurrentCulture.NumberFormat), "]"));
                
                foreach(Segment segSuggestion in segment.Suggestions)
                {
                    outputWriter.WriteLine(format2, padding, segSuggestion.ToString() );        
                }
            }
        
        
            if (segment.SubSegments.Count > 0)
            {
                outputWriter.WriteLine(format, padding, String.Concat("[SubSegments: ", segment.SubSegments.Count.ToString(CultureInfo.CurrentCulture.NumberFormat), "]"));
                padding = String.Concat(padding, "    ");
                foreach(Segment subSegment in segment.SubSegments)
                {
                    emitSegmentInfo(subSegment, outputWriter, padding);
                }
            }
        
            outputWriter.Flush();
        }
        
        private CultureInfo m_culture;
        private TextChunk m_TextChunk;
        
        private int m_iNumSentences;
        private int m_iNumSegments;
        private int m_iNumFiles;
        private int m_iMaxWidth;
        
        private bool m_fInteractiveMode; 
    
    } // class WordBreaker
} // namespace
