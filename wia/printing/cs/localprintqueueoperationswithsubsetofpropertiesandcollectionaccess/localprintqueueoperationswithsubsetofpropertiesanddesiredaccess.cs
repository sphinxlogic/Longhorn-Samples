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
using System.IO;
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

            LocalPrintQueueOperationsWithSubsetOfPropertiesAndCollectionAccess(printerName);

            Console.ReadLine();
        }

        /// <summary>
        /// Instantiating an instance of the Local Print Queue with a subset of all
        /// possible properties populated.
        /// </summary>
        static public void
            LocalPrintQueueOperationsWithSubsetOfPropertiesAndCollectionAccess(
            String     printQueueName
            )
        {
            PrintQueue localPrintQueue = null;

            try
            {
                Console.WriteLine("Sample code for Local Print Queue operations");
                //
                // Instantiating an instance of the Local Print Queue with a subset of all
                // possible properties populated. Since we are going to change both PrintQueue
                // and User properties on the PrintQueue, we need Full Access.
                //
                PrintQueueProperty[] printQueueProperties = 
                {
                    PrintQueueProperty.Name,
                    PrintQueueProperty.Comment,
                    PrintQueueProperty.QueuePort,
                    PrintQueueProperty.UserJobTicket
                };

                localPrintQueue = new PrintQueue(new PrintServer(), 
                                                 printQueueName,
                                                 printQueueProperties,
                                                 PrintSystemDesiredAccess.PrinterFullAccess);
                //
                // Querying the name of the PrintQueue
                //
            
                String           name                   = (String)(localPrintQueue.PropertiesCollection["Name"].Value);
                //
                // Querying othe properties of the PrintQueue like Comment and Port
                //
                String           queueComment           = (String)(localPrintQueue.PropertiesCollection["Comment"].Value);
                Port             port                   = (Port)(localPrintQueue.PropertiesCollection["QueuePort"].Value);
                //
                // Querying the driver settings of the PrintQueue for User
                //
                JobTicket        userJobTicket          = new JobTicket((Stream)(localPrintQueue.PropertiesCollection["UserJobTicket"].Value));

                PrintSchema.OrientationValues orientationValueU      
                                                        = userJobTicket.PageOrientation.Value;
                //
                // Print out the queried values
                //
                Console.WriteLine("Local PrintQueue: " + name + " has the following properites:\n" +
                                  "Comment:          " + queueComment           + "\n" +
                                  "Orientation:      " + orientationValueU      + "\n" +
                                  "Port:             " + port.Name              + "\n");
                //
                // Now lets try changing UserJobTicket & Comment
                //

				
                userJobTicket.PageOrientation.Value     = PrintSchema.OrientationValues.Landscape;

                localPrintQueue.PropertiesCollection["UserJobTicket"].Value        
                                                        = userJobTicket.XmlStream;

				Console.WriteLine("Please enter a new comment to be set:");

                localPrintQueue.PropertiesCollection["Comment"].Value
                                                        = Console.ReadLine();

				Console.WriteLine("User page orientation will be set to Landscape.");
                //
                // Commit the changes to the PrintQueue
                //

				Console.WriteLine("Please hit ENTER to commit the changed properties.");

				Console.ReadLine();

                localPrintQueue.Commit();

				Console.WriteLine("The properties were commited.");
            }
            //
            // Example of exceptions that can be thrown could be due to 
            // access denied while trying to set the Print Queue properties.
            //
            catch(PrintQueueException printQueueException)
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
            catch(PrintSystemException printSystemException)
            {
                Console.WriteLine("Error Message: " + printSystemException.Message);
            }
            catch(Exception exception)
            {
                Console.WriteLine("Error Message " + exception.Message);
            }
        }
        
    }
}