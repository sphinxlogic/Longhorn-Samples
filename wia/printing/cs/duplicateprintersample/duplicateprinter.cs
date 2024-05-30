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
    /// duplicating a print queue on a local print server.
    /// </summary>
    class TestPrintSystemClass
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("This sample duplicates a print queue on a given server." + 
                              "Please enter the server name followed by ENTER:");

            String serverName = Console.ReadLine();

            Console.WriteLine("Please enter the printer name on the printer followed by ENTER:");

            String printerName = Console.ReadLine();

            Console.WriteLine("Please enter the name of the new printer to be installed followed by ENTER:");

            String duplicatedPrinterName = Console.ReadLine();

            DuplicatePrinter(serverName, printerName, duplicatedPrinterName);

            Console.ReadLine();            
        }

        /// <summary>
        /// Duplicate a printer on a given server.
        /// </summary>
        static public void DuplicatePrinter(String serverName, String printerName, String duplicatedPrinterName)
        {
            try
            {
                PrintServer server = new PrintServer(serverName, PrintSystemDesiredAccess.AdministrateServer);

                String[] printerProperties = {
                    "ShareName", 
                    "Comment", 
                    "Location", 
                    "Priority", 
                    "DefaultPriority", 
                    "StartTime", 
                    "UntilTime", 
                    "AveragePpm", 
                    "QueueAttributes", 
                    "QueueDriver", 
                    "QueuePort", 
                    "QueuePrintProcessor", 
                    "SepFile", 
                    "DefaultJobTicket"
                };
                PrintQueue printer = server.GetPrintQueue(printerName, printerProperties);

                String[] ports = new String[] { printer.QueuePort.Name };

				if (printer.IsShared)
				{ 
					Console.WriteLine("Please enter a share name followed by ENTER:");

					String duplicateShareName = Console.ReadLine();

					printer.PropertiesCollection["ShareName"].Value = duplicateShareName;
				}

                PrintQueue duplicatePrinter = server.InstallPrintQueue(duplicatedPrinterName, 
                                                                       printer.QueueDriver.Name, 
                                                                       ports, 
                                                                       printer.QueuePrintProcessor.Name, 
                                                                       printer.PropertiesCollection);

                Console.WriteLine("Operation succeeded.");
            }
            catch (PrintServerException printException)
            {
                Console.WriteLine(printException.Message);
            }
        }

    }
}