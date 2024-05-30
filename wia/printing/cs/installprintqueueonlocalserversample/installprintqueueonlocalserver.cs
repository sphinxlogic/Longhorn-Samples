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
    /// installing a print queue on a local print server.
    /// </summary>
    class TestPrintSystemClass
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("This sample installs a print queue on the local server." + 
                              "The driver, print processor and port must be preinstalled.\n" +
                              "Please enter the printer name followed by ENTER:");

            String printerName = Console.ReadLine();

            Console.WriteLine("Please enter the driver name on the printer followed by ENTER:");

            String driverName = Console.ReadLine();

            Console.WriteLine("Please enter the port name on the printer followed by ENTER:");

            String portName = Console.ReadLine();

            Console.WriteLine("Please enter the print processor name on the printer followed by ENTER:");

            String printProcessorName = Console.ReadLine();

            String[] ports = new String[1];
            ports[0] = portName;

            InstallPrintQueueOnLocalServer(printerName, driverName, ports, printProcessorName);

            Console.ReadLine();
        }

        /// <summary>
        /// Install a print queue on a local print server, 
        /// publish it in DS and set it as default printer.
        /// </summary>
        static public void InstallPrintQueueOnLocalServer(
            String      printerName, 
            String      driverName, 
            String[]    portNames, 
            String      printProcessor)
        {
            try
            {
                LocalPrintServer server = new LocalPrintServer(PrintSystemDesiredAccess.AdministrateServer);

                PrintQueue printer = server.InstallPrintQueue(printerName, 
                                                              driverName, 
                                                              portNames, 
                                                              printProcessor, 
                                                              PrintQueueAttributes.Published);

                Console.WriteLine("Operation succeeded.");
            }
            catch (PrintServerException printException)
            {
                Console.WriteLine(printException.Message);
            }
        }

    }
}