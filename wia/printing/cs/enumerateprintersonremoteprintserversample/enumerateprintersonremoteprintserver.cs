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
    /// enumerating printers on a remote print server that support 
    /// bidirectional communication to the device.
    /// </summary>
    class TestPrintSystemClass
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("This sample enumerates printers on the remote print server that support " + 
                              "bidirectional communication and prints some of the properties." + 
                              "Please enter the remote server name followed by ENTER:");

            String serverName = Console.ReadLine();

            EnumeratePrinterPrintersOnRemotePrintServer(serverName);

            Console.ReadLine();
        }

        /// <summary>
        /// Enumerate the print queues on the remote print server that are enable bidi queries.
        /// </summary>
        ///
        static public void EnumeratePrinterPrintersOnRemotePrintServer(String serverName)
        {
            try
            {
                EnumeratedPrintQueuesType[] enumerationFlags = { EnumeratedPrintQueuesType.EnableBidi };

                PrintQueueProperty[] printerProperties = {
                    PrintQueueProperty.Name, 
                    PrintQueueProperty.QueueDriver, 
                    PrintQueueProperty.QueuePort
                };

                PrintServer printServer = new PrintServer(serverName);

                PrintQueueCollection printQueuesOnRemoteServer = printServer.GetPrintQueues(printerProperties, 
                                                                                            enumerationFlags);

                Console.WriteLine("Enumerate print queues that enable bidi queries:");

                foreach (PrintQueue printer in printQueuesOnRemoteServer)
                {
                    Console.WriteLine("The printer "            + printer.Name              + 
                                      " using driver "          + printer.QueueDriver.Name  + 
                                      " and connected to port " + printer.QueuePort.Name    +
                                      " has bidirectional queries enables.");
                }
            }
            catch (PrintSystemException printException)
            {
                Console.Write(printException.Message);
            }
        }
    }
}