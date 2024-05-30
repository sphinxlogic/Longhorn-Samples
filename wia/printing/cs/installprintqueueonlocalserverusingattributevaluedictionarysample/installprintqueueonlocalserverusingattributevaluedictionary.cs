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
            Console.WriteLine("This sample installs a shared print queue on the local server." + 
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

            Console.WriteLine("Please enter the share name on the printer followed by ENTER:");

            String shareName = Console.ReadLine();

            Console.WriteLine("Please enter the comment on the printer followed by ENTER:");

            String comment = Console.ReadLine();

            Console.WriteLine("Please enter the location on the printer followed by ENTER:");

            String location = Console.ReadLine();

            InstallPrintQueueOnLocalServerUsingAttributeValueDictionary(printerName, 
                                                                        driverName, 
                                                                        ports, 
                                                                        printProcessorName,
                                                                        PrintQueueAttributes.Shared,
                                                                        shareName,
                                                                        comment,
                                                                        location,
                                                                        1);

            Console.ReadLine();
        }

        /// <summary>
        /// Install a print queue using the PrintSystemAttributeValueDictionary.
        /// </summary>        
        static public void InstallPrintQueueOnLocalServerUsingAttributeValueDictionary(
            String                  printerName, 
            String                  driverName, 
            String[]                portNames, 
            String                  printProcessorName, 
            PrintQueueAttributes    attributes, 
            String                  shareName, 
            String                  comment, 
            String                  location, 
            Int32                   priority)
        {
            try
            {
                PrintSystemAttributeValueDictionary printQueueAttributeValueCollection = new PrintSystemAttributeValueDictionary();
                PrintQueueAttributeAttributeValue queueAttributesAttribute = new PrintQueueAttributeAttributeValue("QueueAttributes", attributes);
                PrintSystemStringAttributeValue shareNameAttribute = new PrintSystemStringAttributeValue("ShareName", shareName);
                PrintSystemStringAttributeValue commentAttribute = new PrintSystemStringAttributeValue("Comment", comment);
                PrintSystemStringAttributeValue locationAttribute = new PrintSystemStringAttributeValue("Location", location);
                PrintSystemInt32AttributeValue priorityAttribute = new PrintSystemInt32AttributeValue("Priority", priority);

                printQueueAttributeValueCollection.Add(queueAttributesAttribute);
                printQueueAttributeValueCollection.Add(shareNameAttribute);
                printQueueAttributeValueCollection.Add(commentAttribute);
                printQueueAttributeValueCollection.Add(locationAttribute);
                printQueueAttributeValueCollection.Add(priorityAttribute);

                LocalPrintServer server = new LocalPrintServer(PrintSystemDesiredAccess.AdministrateServer);
                PrintQueue printer = server.InstallPrintQueue(printerName, 
                                                              driverName, 
                                                              portNames, 
                                                              printProcessorName, 
                                                              printQueueAttributeValueCollection);

                Console.WriteLine("Operation succeeded.");
            }
            catch (PrintServerException printException)
            {
                Console.WriteLine(printException.Message);
            }
        }

    }
}