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
    /// TestPrintSystemClass class implements sample code that
    /// shows how to read properties of the default printer
    /// </summary>
    class TestPrintSystemClass
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("This sample shows how to read properties of the default printer");

            Console.ReadLine();

            DefaultPrintQueueOperations();

            Console.ReadLine();
        }

        /// <summary>
        /// Sample code for Default Print Queue operations.
        /// </summary>
        static public void DefaultPrintQueueOperations()
        {
            try
            {
                Console.WriteLine("Sample code for Default Print Queue operations");

                //
                // Instantiating an instance of the Local Print Server and this is the only
                // way to get to the default print queue
                //
                LocalPrintServer lclPrintServer = new LocalPrintServer();

                //
                // Out of the Local Print Server Properties, we are only interested 
                // in the Default Print Queue for this sample
                //
                PrintQueue defaultPrintQueue = lclPrintServer.DefaultPrintQueue;

                //
                // Query different Properties of the Print Queue
                // HostingPrintServerName: If the default PrintQueue is a connection then this 
                //                         points out to the server from which the PrintQueue is 
                //                         shared out
                //
                String hostingPrintServerName = defaultPrintQueue.HostingPrintServer.Name;

                //
                // FullName              :This is the full \\Server\PrintQueue name
                //
                String printQueueFullName = defaultPrintQueue.FullName;

                //
                // Querying the name of the PrintQueue
                //
                String name = defaultPrintQueue.Name;

                //
                // Querying the Port, Driver and Print Processor of the Print Queue
                //
                Port port = defaultPrintQueue.QueuePort;
                Driver driver = defaultPrintQueue.QueueDriver;
                PrintProcessor printProcessor = defaultPrintQueue.QueuePrintProcessor;

                //
                // Querying the priority by which the jobs are scheduled on this print queue
                //
                Int32 priority = defaultPrintQueue.Priority;

                //
                // Querying the number of Jobs queued on the PrintQueue
                //
                Int32 noJobs = defaultPrintQueue.NumberOfJobs;

                //
                // Querying othe properties of the PrintQueue like Comment,Location,SepFile and ShareName
                //
                String queueLocation = defaultPrintQueue.Location;
                String queueComment = defaultPrintQueue.Comment;
                String separatorFile = defaultPrintQueue.SepFile;
                String share = defaultPrintQueue.ShareName;
                String description = defaultPrintQueue.Description;

                //
                // Querying the driver settings of the PrintQueue for both Printer and User
                //
                JobTicket defaultJobTicket = new JobTicket(defaultPrintQueue.DefaultJobTicket);
                PrintSchema.OrientationValues orientationValueD = defaultJobTicket.PageOrientation.Value;
                JobTicket userJobTicket = new JobTicket(defaultPrintQueue.UserJobTicket);
                PrintSchema.OrientationValues orientationValueU = userJobTicket.PageOrientation.Value;

                //
                // Print out the queried values
                //
                Console.WriteLine("Default PrintQueue: " + name + " on Print Server: " + hostingPrintServerName + " has the following properites:\n" + "Port:             " + port.Name + "\n" + "Driver:           " + driver.Name + "\n" + "PrintProcessor:   " + printProcessor.Name + "\n" + "Location:         " + queueLocation + "\n" + "Comment:          " + queueComment + "\n" + "Orientation:      " + orientationValueU + "\n" + "Description:      " + description + "\n" + "Separator_File:   " + separatorFile + "\n" + "Share_Name:       " + share + "\n" + "Number Of Jobs    " + noJobs + "\n" + "Paused:           " + defaultPrintQueue.IsPaused.ToString() + "\n");

                //
                // If required, the latest settings could be retrieved from the Print Server once
                // more
                //
                defaultPrintQueue.Refresh();
            }
                //
                // Example of exceptions that can be thrown could be due to 
                // access denied on trying to pause the Print Queue if you are
                // not an administrator on that queue
                //
                catch (PrintQueueException printSystemException)
            {
                Console.WriteLine("PrintQueueException:" + printSystemException.Message);
            }
            catch (PrintSystemException printSystemException)
            {
                Console.WriteLine("System exception:" + printSystemException.Message);
            }
        }
    }
}