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
using System.Diagnostics;
using System.Text;
using System.NaturalLanguage;
using System.Collections;

namespace Microsoft.Samples.NaturalLanguage
{
	/// <summary>
	/// Summary description for SearchTuner.
	/// </summary>
	public class SearchTuner
	{
		#region Variables
		Context context;

		TextChunk textChunk;

		string m_LinkFormat = "http://search.msn.com/results.aspx?q={0}&FORM=SMCRT";
		#endregion

		#region Construction
		public SearchTuner()
		{
			context = new Context();
			context.IsSpellAlwaysSuggesting = true;
			context.IsSpellChecking = true;

			context.IsComputingLemmas = false;

			context.IsSpellIgnoringAllUpperCase = true;
			context.IsSpellIgnoringWordsWithNumbers = true;

			string path = System.Windows.Forms.Application.ExecutablePath;
			int index = path.LastIndexOf('\\');
			if (index > 0)
			{
				path = path.Substring(0, index) + @"\CiPTB.lex";
				System.IO.FileInfo fileInfo = new System.IO.FileInfo(path);

				if (fileInfo.Exists)
				{
					context.Lexicons = new Lexicon[] { new Lexicon(path) };
				}
			}
			// Need one text chunk per language
			textChunk = new TextChunk(context);

			textChunk.Culture = new System.Globalization.CultureInfo(1033);
		}
		#endregion

		#region Code
		public string SpellCheck(string inputText)
		{
			StringBuilder builder = new StringBuilder(inputText.Length);
			int start = 0;

			textChunk.InputText = inputText;
			foreach (Sentence sentence in textChunk)
				foreach (Segment segment in sentence)
				{
					IList list = segment.Suggestions;

					if (list != null && list.Count > 0)
					{
						TextRange range = segment.Range;
						int errorStart = range.Start;

						if (errorStart >= start)
						{
							builder.Append(inputText, start, errorStart - start);
							builder.Append(list[0]);
							start = errorStart + range.Length;
						}
					}
				}

			if (start < inputText.Length)
				builder.Append(inputText, start, inputText.Length - start);

			return builder.ToString();
		}

		public string NormalizeText(string inputText)
		{
			StringBuilder stringBuilder = new StringBuilder();

			foreach (char character in inputText)
			{
				switch (character)
				{
					case ' ':
						stringBuilder.Append('+');
						break;

					case '+':
						break;

					default:
						if (character > ' ' || (int)character < 127)
							stringBuilder.Append(character);
						else
							stringBuilder.AppendFormat("%{0:X2}", (int)character);

						break;
				}
			}

			return stringBuilder.ToString();
		}

		public String LinkFormat
		{
			get
			{
				return m_LinkFormat;
			}
			set
			{
				m_LinkFormat = value;
			}
		}

		public string Link(string textToSearchFor)
		{
			return String.Format(System.Globalization.CultureInfo.CurrentUICulture, LinkFormat, NormalizeText(textToSearchFor));
		}
		#endregion
	}
}
