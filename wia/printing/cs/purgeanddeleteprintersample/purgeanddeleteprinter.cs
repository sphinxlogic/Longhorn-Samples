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
    /// deleting a printer and all its jobs.
    /// </summary>
    class TestPrintSystemClass
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("This sample deletes all the jobs on a printer, then it deletes the printer itself.\n" +
                              "Please enter the print server name followed by ENTER:");

            String serverName = Console.ReadLine();

            Console.WriteLine("Please enter the printer name on the printer followed by ENTER:");

            String printerName = Console.ReadLine();

            PurgeAndDeletePrinter(serverName, printerName);
        }

        /// <summary>
        /// Purge and delete a printer on a given server.
        /// </summary>
        static public void PurgeAndDeletePrinter(String printServerName, String printQueueName)
        {
            try
            {
                PrintServer printServer = new PrintServer(printServerName, PrintSystemDesiredAccess.AdministrateServer);

                PrintQueue printQueue = new PrintQueue(printServer, printQueueName, PrintSystemDesiredAccess.PrinterFullAccess);

                printQueue.Purge();

                printServer.DeletePrintQueue(printQueue);

                Console.WriteLine("Operation succeeded");

                Console.ReadLine();
            }
            catch (PrintSystemException printException)
            {
                Console.WriteLine(printException.Message);

                Console.ReadLine();
            }
        }

    }
}