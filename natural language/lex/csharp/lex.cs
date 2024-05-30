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
using System.IO;
using System.Collections;
using System.NaturalLanguage;
using System.Text;
using System.Globalization;

namespace Microsoft.NaturalLanguage.Samples
{
    class LexiconConsole
    {
        /// <summary>
        /// The main entry point for the Console.
        /// </summary>
        static int Main (string[] args)
        {
            LexiconExamples lexSample = null;
            LexiconCmdOpts cmdOpts = null;
            StreamWriter outputWriter = null;

            try
            {
                //Process commandline arguments
                cmdOpts = new LexiconCmdOpts();
                if (!cmdOpts.ProcessOpts(args))
                {
                    throw new ArgumentException("Parsing commandline arguments failed.");
                }

                lexSample = new LexiconExamples();
                if (cmdOpts.OutputFile != null)
                {
                    outputWriter = new StreamWriter(cmdOpts.OutputFile);
                }
                else
                {
                    outputWriter = new StreamWriter(Console.OpenStandardOutput());
                }

                lexSample.DisplayWords(outputWriter, cmdOpts.InputFile, cmdOpts.Word, cmdOpts.MaxWordsToDisplay);
                return 0;
            } 
            catch (System.NullReferenceException nre) 
            {
                if(cmdOpts.Verbose)
                {
                    Console.Error.WriteLine("Message: {0}", nre.Message);
                    Console.Error.WriteLine("Stack Trace: {0}", nre.StackTrace);
                }
                return 1;	
            } 
            catch (System.ArgumentException ae)
            {
                if (cmdOpts.Verbose)
                {
                    Console.Error.WriteLine("Message: {0}", ae.Message);
                    Console.Error.WriteLine("Stack Trace: {0}", ae.StackTrace);
                }

                return 1;
            }
            finally
            {
                if (outputWriter != null) outputWriter.Close();
            }
        }
    }

    public class LexiconExamples 
    {
        public LexiconExamples()
        {
        }

        protected void DisplayWordProps(StreamWriter sw, IDictionary props, string prefix)
        {
            foreach (DictionaryEntry pair in props)
            {
                sw.Write("{0}name: {1}", prefix, pair.Key.ToString());

                IDictionary subProps = null;
                ICollection subList = null;

                if (pair.Value is IDictionary)
                {
                    subProps = (IDictionary) pair.Value;
                    sw.WriteLine();
                    DisplayWordProps(sw, subProps, prefix + "  ");
                }
                else if (pair.Value is ICollection)
                {
                    subList = (ICollection) pair.Value;
                    string prefix2 = prefix + "\t";

                    sw.WriteLine();
                    foreach (object member in subList)
                    {
                        sw.WriteLine("{0} value: {1}", prefix2, member.ToString());
                    }
                }
                else
                {
                    sw.WriteLine(" value: {0}", pair.Value.ToString());
                }
            }

            sw.Flush();
        }

        public void DisplayWords (StreamWriter sw, string inputFile, string word, int maxWordsToDisplay)
        {
                LexiconEntryEnumerator lexEnum = null;
                Lexicon lex = null;

            if ((sw == null) || (inputFile == null) || (word == null) || (maxWordsToDisplay <= 0)) return;

            try
            {
                    if (File.Exists (inputFile))
                    {
                            //Create lexicon based on the lexicon file specified in the commandline
                            lex = new Lexicon (inputFile);
                            
                            //Get a LexiconEntryEnumerator
                            lexEnum = (LexiconEntryEnumerator)lex.GetEnumerator();

                            //Move to the target word if it is specified in the commandline
                            if (word.Length != 0)
                            {
                                    int id = lex.IndexOf (word);
                                    sw.WriteLine ("  IndexOf({0}) = ID:{1}", word, id);
                                    if (lexEnum.MoveTo(word) == false)
                                    {
                                            throw new ArgumentException("Could not position to " + word);
                                    }
                                    sw.WriteLine ("  MoveTo({0})  = ID:{1}", word, lexEnum.Index);
                                    sw.Flush();

                            }

                            
                    }
                    else
                    {
                            throw new ArgumentException (inputFile + " does not exist.");
                    }
            }
            catch (Exception)
            {
                    throw;
            }
            finally
            {
                if (lex != null)
                    lex.Dispose ();
            }

        }

    }

        /// <summary>
        /// The Lexicon Command Options class processes commandline arguments
        /// </summary>
        class LexiconCmdOpts : CmdOpts
        {
       
        }
}
