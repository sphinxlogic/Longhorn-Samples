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
    /// enumerating local printers on the local print server that are shared in DS and
    /// print RAW.
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
                              "that are shared in DS and print RAW.");

            Console.ReadLine();

            EnumeratePublishedRawPrintersOnLocalPrintServer();

            Console.ReadLine();
        }

        /// <summary>
        /// Enumerate the print queues on the local print server that are shared in DS and
        /// print RAW.
        /// </summary>
        static public void EnumeratePublishedRawPrintersOnLocalPrintServer()
        {
            try
            {
                EnumeratedPrintQueuesType[] enumerationFlags = {
                    EnumeratedPrintQueuesType.Local, 
                    EnumeratedPrintQueuesType.RawOnly, 
                    EnumeratedPrintQueuesType.PublishedInDS
                };
                PrintQueueProperty[] printerProperties = {
                    PrintQueueProperty.Name, 
                    PrintQueueProperty.Location, 
                    PrintQueueProperty.QueueDriver, 
                    PrintQueueProperty.QueuePort
                };

                PrintServer printServer = new PrintServer();

                PrintQueueCollection printQueuesOnLocalServer = printServer.GetPrintQueues(printerProperties, 
                                                                                           enumerationFlags);

                Console.WriteLine();

                foreach (PrintQueue printer in printQueuesOnLocalServer)
                {
                    Console.WriteLine("The printer " + printer.Name + 
                                      " using driver " + printer.QueueDriver.Name + 
                                      " and connected to port " + printer.QueuePort.Name + 
                                      " is publishd in DS and prints RAW.");
                }
            }
            catch (PrintSystemException printException)
            {
                Console.Write(printException.Message);
            }
        }

    }
}