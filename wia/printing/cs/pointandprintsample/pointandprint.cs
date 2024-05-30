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
    /// creating a connection to a remote print queue.
    /// </summary>
    class TestPrintSystemClass
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("This sample creates a printer connection and sets it as the default printer.\n" +
                              "Please enter the print server name followed by ENTER:");

            String serverName = Console.ReadLine();

            Console.WriteLine("Please enter the printer name on the printer followed by ENTER:");

            String printerName = Console.ReadLine();

            PointAndPrintSample(serverName, printerName);

            Console.ReadLine();
        }

        /// <summary>
        /// Create a Point and Printer connection and make it the default printer.
        /// </summary>
        static public void PointAndPrintSample(String printServerName, String printQueueName)
        {
            try
            {
                Console.WriteLine("Creating a connection to print queue " + printQueueName + 
                                  " on PrintServer " + printServerName);

                LocalPrintServer localPrintServer = new LocalPrintServer();
                PrintServer      printServer      = new PrintServer(printServerName);
                PrintQueue       remotePrinter    = new PrintQueue(printServer, printQueueName);

                //
                // Create a Connection
                //
                bool operationSucceeded = localPrintServer.ConnectToPrintQueue(remotePrinter);

                if (operationSucceeded)
                {
                    //
                    // Set Connection as default PrintQueue
                    //            
                    localPrintServer.DefaultPrintQueue = remotePrinter;
                    localPrintServer.Commit();

                    Console.WriteLine("The connection was created succesfully and set as default printer.");

					Console.ReadLine();
                }
            }
            catch (PrintSystemException printException)
            {
                Console.WriteLine(printException.Message);
            }
        }

    }
}