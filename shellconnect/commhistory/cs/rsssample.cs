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
using System.Reflection;
using System.Runtime.InteropServices;

[assembly: CLSCompliant(true)]
[assembly: ComVisible(false)]

// RSSimporter sample
//
// A console application that imports an RSS feed as messages
// in the Communication History.
namespace Microsoft.Samples.Communications
{
    public static class RssSample
    {
        [System.STAThread]
        static void Main(string[] args)
        {
            if (args.Length != 0)
            {
                RssImporter.Import(args[0]);
            }
            else
            {
                string defaultSource = "http://msdn.microsoft.com/rss.xml";

                Console.WriteLine("No source specified. Using default.");
                Console.WriteLine();
                Console.WriteLine("To download a different RSS feed, run the sample like this:");
                Console.WriteLine("    RssImporterSample RssFile");
                Console.WriteLine();
                RssImporter.Import(defaultSource);
            }
        }
    }
}