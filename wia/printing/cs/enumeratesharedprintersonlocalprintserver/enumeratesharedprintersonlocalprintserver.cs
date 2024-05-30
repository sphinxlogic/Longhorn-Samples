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
    /// TestPrintSystemClass class implements sample the code that 
    /// enumerats shared local printers on the local print server 
    /// lists the printer name and location.
    /// </summary>
    class TestPrintSystemClass
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("This sample enumerates shared local printers on the local server " +
                              "and lists the printer name and location.");

            Console.ReadLine();

            EnumerateSharedPrintersOnLocalPrintServer();

            Console.ReadLine();
        }

        /// <summary>
        /// Enumerate the shared print queues on the local print server.
        /// </summary>
        ///
        static public void EnumerateSharedPrintersOnLocalPrintServer()
        {
            try
            {
                EnumeratedPrintQueuesType[] enumerationFlags = {EnumeratedPrintQueuesType.Local, 
                                                                EnumeratedPrintQueuesType.Shared
                                                               };

                PrintQueueProperty[] printerProperties = { PrintQueueProperty.Name,
					                                       PrintQueueProperty.Location };

                LocalPrintServer printServer = new LocalPrintServer();

                PrintQueueCollection printQueuesOnLocalServer = printServer.GetPrintQueues(printerProperties, 
                                                                                           enumerationFlags);

                Console.WriteLine("Enumerate shared print queues:");

                foreach (PrintQueue printer in printQueuesOnLocalServer)
                {
                    Console.WriteLine("The shared printer " + printer.Name + " is located at " + printer.Location);
                }
            }
            catch (PrintSystemException printException)
            {
                Console.Write(printException.Message);
            }
        }

    }
}