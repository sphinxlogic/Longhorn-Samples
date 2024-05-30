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
    /// local Print Queue operations.
    /// </summary>
    class TestPrintSystemClass
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("This sample shows how to get and set properties on a print queue.\n" + 
                              "Please enter the name of a local printer followed by ENTER:");

            String printerName = Console.ReadLine();

            LocalPrintQueueOperationsWithFullProperties(printerName);

            Console.ReadLine();
        }

        /// <summary>
        /// Sample code for Local Print Queue operations.
        /// </summary>
        static public void LocalPrintQueueOperationsWithFullProperties(String printQueueName)
        {
            PrintQueue localPrintQueue = null;

            try
            {
                Console.WriteLine("Sample code for Local Print Queue operations");

                //
                // Instantiating an instance of the Local Print Queue with all possible
                // properties populated. Changing some of the properties on the PrintQueue
                // mendates instantiating with a PrinterFullAccess access. Also having this
                // access would allow the sample to Pause / Resume Print Queues
                // 
                localPrintQueue = new PrintQueue(new PrintServer(), printQueueName, PrintSystemDesiredAccess.PrinterFullAccess);

                //
                // Querying the name of the PrintQueue
                //
                String name = localPrintQueue.Name;

                //
                // Querying the Port, Driver and Print Processor of the Print Queue
                //
                Port port = localPrintQueue.QueuePort;
                Driver driver = localPrintQueue.QueueDriver;
                PrintProcessor printProcessor = localPrintQueue.QueuePrintProcessor;

                //
                // Querying the priority by which the jobs are scheduled on this print queue
                //
                Int32 priority = localPrintQueue.Priority;

                //
                // Querying the number of Jobs queued on the PrintQueue
                //
                Int32 noJobs = localPrintQueue.NumberOfJobs;

                //
                // Querying othe properties of the PrintQueue like Comment,Location,SpeFile and ShareName
                //
                String queueLocation = localPrintQueue.Location;
                String queueComment = localPrintQueue.Comment;
                String separatorFile = localPrintQueue.SepFile;
                String share = localPrintQueue.ShareName;
                String description = localPrintQueue.Description;

                //
                // Querying the driver settings of the PrintQueue for both PrintQueue and User
                //
                JobTicket defaultJobTicket = new JobTicket(localPrintQueue.DefaultJobTicket);
                PrintSchema.OrientationValues orientationValueD = defaultJobTicket.PageOrientation.Value;
                JobTicket userJobTicket = new JobTicket(localPrintQueue.UserJobTicket);
                PrintSchema.OrientationValues orientationValueU = userJobTicket.PageOrientation.Value;

                //
                // Print out the queried values
                //
                Console.WriteLine("Local PrintQueue: {0} has the following properties:\n" + "Port:           {1}\nDriver:         {2}\nPrintProcessor: {3}\n" + "Location:       {4}\nComment:        {5}\nOrientation:    {6}\n" + "Description:    {7}\nSeperator_File: {8}\nShare_Name:     {9}\n" + "Number Of Jobs  {10}\nPaused:         {11}\n", name, port.Name, driver.Name, printProcessor.Name, queueLocation, queueComment, orientationValueU, description, separatorFile, share, noJobs, localPrintQueue.IsPaused.ToString());

                //
                // Now lets try changing some of the Local Print Queue Properties 
                //
				Console.WriteLine("Please enter a new name to set on the print queue:");
				localPrintQueue.Name = Console.ReadLine();

				Console.WriteLine("Please enter a port name to set on the print queue:");
				String newPortName = Console.ReadLine();
				localPrintQueue.QueuePort = new Port(newPortName);

				Console.WriteLine("Please enter a comment to set on the print queue:");
				localPrintQueue.Comment = Console.ReadLine();

				Console.WriteLine("Please enter a location to set on the print queue:");
				localPrintQueue.Location = Console.ReadLine();

				Console.WriteLine("Please enter a share name to set on the print queue:");
				localPrintQueue.ShareName = Console.ReadLine();

				Console.WriteLine("The printer priority will be set to 6.");
				localPrintQueue.Priority = 6;

				Console.WriteLine("The page orientation will be set to Landscape for the user job ticket.");

				Console.WriteLine("The page orientation will be set to Portrait for the user job ticket.");

                userJobTicket.PageOrientation.Value = PrintSchema.OrientationValues.Landscape;
                localPrintQueue.UserJobTicket = userJobTicket.XmlStream;
                defaultJobTicket.PageOrientation.Value = PrintSchema.OrientationValues.Portrait;
                localPrintQueue.DefaultJobTicket = defaultJobTicket.XmlStream;

				Console.WriteLine("Please hit ENTER to commit the changed properties.");
				Console.ReadLine();
                //
                // Commit the changes to the PrintQueue
                //
                localPrintQueue.Commit();

				Console.WriteLine("The properties were commited.");

				//
                // If the PrintQueue is paused - resume else pause
                //
                if (localPrintQueue.IsPaused)
                {
                    Console.WriteLine("Resuming the Print Queue");
                    localPrintQueue.Resume();
                }
                else
                {
                    Console.WriteLine("Pausing the Print Queue");
                    localPrintQueue.Pause();
                }

                //
                // If required, the latest settings could be retrieved from the Print Server once
                // more. This is a step to validate that what we commited is what is currently 
                // reflected on the Print Queue
                //
                localPrintQueue.Refresh();
                name = localPrintQueue.Name;
                port = localPrintQueue.QueuePort;
                priority = localPrintQueue.Priority;
                queueLocation = localPrintQueue.Location;
                queueComment = localPrintQueue.Comment;
                defaultJobTicket = new JobTicket(localPrintQueue.DefaultJobTicket);
                orientationValueD = defaultJobTicket.PageOrientation.Value;

                userJobTicket = new JobTicket(localPrintQueue.UserJobTicket);
                orientationValueU = userJobTicket.PageOrientation.Value;

                Console.WriteLine("\nReading updated Print Queue information ...");
                Console.WriteLine("Local PrintQueue: " + name + 
								  " has the following properties:\n" + 
								  "Port:				" + port.Name + "\n" + 
								  "Location:			" + queueLocation + "\n" + 
								  "Comment:				" + queueComment + "\n" + 
							      "User Orientation:    " + orientationValueU + "\n" + 
							      "Default Orientation: " + orientationValueD + "\n" + 
				                  "Paused:				" + localPrintQueue.IsPaused.ToString() + "\n");
            }
                //
                // Example of exceptions that can be thrown could be due to 
                // access denied while trying to set the Print Queue properties.
                //
                catch (PrintQueueException printQueueException)
            {
                Console.WriteLine("Error Message: {0}", printQueueException.Message);
            }
            catch (PrintCommitAttributesException commitException)
            {
                //
                // Handle a PrintCommitAttributesException.
                //
                Console.WriteLine("Error Message: {0}", commitException.Message);
                Console.WriteLine("These attributes failed to commit...");

                IEnumerator failedAttributesEnumerator = commitException.FailToCommitAttributes.GetEnumerator();

                for (; failedAttributesEnumerator.MoveNext(); )
                {
                    String attributeName = (String)(failedAttributesEnumerator.Current);

                    Console.Write("{0}\t", attributeName);
                }

                Console.WriteLine();

                //
                // To have the PrintQueue instance object in sync with the real Print Queue, 
                // a refresh could be used. The instance properties that failed to commit will 
                // be lost. The caller has the choice to not refresh the failed properties and
                // try another Commit() later one.
                //
                localPrintQueue.Refresh();
            }
            catch (PrintSystemException printSystemException)
            {
                Console.WriteLine("Error Message: {0}", printSystemException.Message);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error Message {0}", exception.Message);            }
        }
        
    }
}