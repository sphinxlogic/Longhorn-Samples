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
                              "Please enter the printer name followed by ENTER:");

            String printerName = Console.ReadLine();

            LocalPrintQueueOperationsWithSubsetOfProperties(printerName);

            Console.ReadLine();
        }

        /// <summary>
        /// Sample code for Local Print Queue operations.
        /// </summary>
        static public void LocalPrintQueueOperationsWithSubsetOfProperties(String printQueueName)
        {
            PrintQueue localPrintQueue = null;

            try
            {
                Console.WriteLine("Sample code for Local Print Queue operations");

                //
                // Instantiating an instance of the Local Print Queue with a subset of all
                // possible properties populated. Since we are going to change only user 
                // properties on the PrintQueue, we don't need any special access.
                //
                PrintQueueProperty[] printQueueProperties = 
                {
                    PrintQueueProperty.Name, 
                    PrintQueueProperty.Comment, 
                    PrintQueueProperty.ShareName, 
                    PrintQueueProperty.UserJobTicket
                };

                localPrintQueue = new PrintQueue(new PrintServer(), printQueueName, printQueueProperties);

                //
                // Querying the name of the PrintQueue
                //
                String name = localPrintQueue.Name;

                //
                // Querying othe properties of the PrintQueue like Comment and ShareName
                //
                String queueComment = localPrintQueue.Comment;
                String share = localPrintQueue.ShareName;

                //
                // Querying the driver settings of the PrintQueue for User
                //
                JobTicket userJobTicket = new JobTicket(localPrintQueue.UserJobTicket);
                PrintSchema.OrientationValues orientationValueU = userJobTicket.PageOrientation.Value;

                //
                // Print out the queried values
                //
                Console.WriteLine("Local PrintQueue: " + name + " has the following properites:\n" + "Comment:          " + queueComment + "\n" + "Orientation:      " + orientationValueU + "\n" + "Share_Name:       " + share + "\n");

                //
                // Now lets try changing UserJobTicket 
                //
				Console.WriteLine("The user page orientation will be set to Portrait. Please hit ENTER to commit.");

                userJobTicket.PageOrientation.Value = PrintSchema.OrientationValues.Portrait;
                localPrintQueue.UserJobTicket = userJobTicket.XmlStream;

				Console.ReadLine();
                //
                // Commit the changes to the PrintQueue
                //
                localPrintQueue.Commit();

				Console.WriteLine("The property was commited.");
            }
                //
                // Example of exceptions that can be thrown could be due to 
                // access denied while trying to set the Print Queue properties.
                //
                catch (PrintQueueException printQueueException)
            {
                Console.WriteLine("Error Message: " + printQueueException.Message);
            }
            catch (PrintCommitAttributesException commitException)
            {
                //
                // Handle a PrintCommitAttributesException.
                //
                Console.WriteLine("Error Message: " + commitException.Message);
                Console.WriteLine("These attributes failed to commit...");

                IEnumerator failedAttributesEnumerator = commitException.FailToCommitAttributes.GetEnumerator();

                for (; failedAttributesEnumerator.MoveNext(); )
                {
                    String attributeName = (String)(failedAttributesEnumerator.Current);

                    Console.Write(attributeName + "\t");
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
                Console.WriteLine("Error Message: " + printSystemException.Message);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error Message " + exception.Message);
            }
        }

    }
}