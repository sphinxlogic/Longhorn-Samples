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
using System.Printing;
using System.Printing.PrintSubSystem;
using System.Printing.Configuration;
using Microsoft.Printing.DeviceCapabilities;
using Microsoft.Printing.JobTicket;
using System.Collections;


namespace PrintSystemSample
{
    /// <summary>
    /// TestPrintSystemClass class implements sample the code for 
    /// enumerating printer connections on the local print server
    /// installed using the push printer connection policy.
    /// </summary>
    class TestPrintSystemClass
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("This sample enumerates local printers on the local server " +
                              "installed using the push printer connection policy.");

            Console.ReadLine();

            EnumeratePushedPrinterConnectionsPrintersOnLocalPrintServer();

            Console.ReadLine();
        }

        /// <summary>
        /// Enumerate the printer connections on the local print server installed by policy 
        /// and the location and the number of print jobs.
        /// </summary>
        static public void EnumeratePushedPrinterConnectionsPrintersOnLocalPrintServer()
        {
            try
            {
                EnumeratedPrintQueuesType[] enumerationFlags = {EnumeratedPrintQueuesType.Local, 
                                                                EnumeratedPrintQueuesType.Connections, 
                                                                EnumeratedPrintQueuesType.PushedUserConnection
                                                                };

                PrintQueueProperty[] printerProperties = {PrintQueueProperty.Name, 
                                                          PrintQueueProperty.Location, 
                                                          PrintQueueProperty.NumberOfJobs
                                                         };

                LocalPrintServer printServer = new LocalPrintServer();

                PrintQueueCollection printQueuesOnLocalServer = printServer.GetPrintQueues(printerProperties, 
                                                                                           enumerationFlags);

                Console.Write("Enumerate print connections installed using Push Printer Connections policy:\n");

                foreach (PrintQueue printer in printQueuesOnLocalServer)
                {
                    Console.WriteLine("The pushed printer connection " + printer.Name + " located at " + printer.Location + " has " + printer.NumberOfJobs  + " jobs.");
                }
            }
            catch (PrintSystemException printException)
            {
                Console.Write(printException.Message);
            }
        }	

    }
}