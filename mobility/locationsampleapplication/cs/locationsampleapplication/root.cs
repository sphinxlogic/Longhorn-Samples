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
using System.Location;

namespace Microsoft.Samples.Location
{
    /// <summary>
    /// Location Sample applications - hub for various small, self-contained sample cases
    /// </summary>
    public class LocationSample
    {
        /// <summary>
        /// Display the "usage" message to the console
        /// </summary>
        static void Usage ()
        {
            Console.WriteLine ("usage:");
            Console.WriteLine("    > Location.Sample.exe {query|monitor|{context [ctx_name]}}");
        }
        /// <summary>
        /// Main entry point for the Location Sample application
        /// </summary>
        static void Main (string[] args)
        {
            if (args.Length < 1)
            {
                Usage ();
                return;
            }
            try
            {
                switch (args[0])
                {
                case "query":
                    // call into the code processing the Location Query command
                    LocationQuerySample.QueryMain ();
                    break;
                case "monitor":
                    // call into the code processing the Location Monitor command
                    LocationMonitorSample.MonitorMain ();
                    break;
                case "context":
                    if (args.Length < 2)
                    {
                        LocationContextSample.ContextListMain ();
                    }
                    else
                    {
                        LocationContextSample.ContextAddMain (args[1]);
                    }
                    break;
                default:
                    Console.WriteLine ("Error: Incorrect parameter.");
                    Usage ();
                    break;
                }
            }
            catch (LocException le)
            {
                Console.WriteLine ("Location Exception: {0}", le);
                return;
            }
            catch (System.NullReferenceException)
            {
                Console.WriteLine ("Could not connect to the Location Service - is the service running?");
                return;
            }
        }
    }
}
