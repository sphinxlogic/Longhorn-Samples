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
    /// common printing scenarios.
    /// </summary>
    class TestPrintSystemClass
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("This sample reads a print server properties and then change them.\n" +
                              "Please enter the print server name followed by ENTER:");

            String serverName = Console.ReadLine();

            CreatePrintServerAndGetSetPropertiesSample(serverName);

            Console.ReadLine();
        }


        /// <summary>
        /// Create a print server and set properties object.
        /// </summary>
        static public void CreatePrintServerAndGetSetPropertiesSample(String serverName)
        {
            PrintServer printServer = null;

            try
            {
                String[] serverProperties = { "BeepEnabled", "DefaultSpoolDirectory" };

                //
                // The object is created and only the two properties above are initialized.
                // This is a way to optimize in the time required to load the object if only
                // a limited number of properties are required from the object
                //
                printServer = new PrintServer(serverName, 
                                              serverProperties, 
                                              PrintSystemDesiredAccess.AdministrateServer);

                //
                // Read print server properties. These properties are already initialized.
                // This operations doesn't require a call to the Print Spooler service.
                //
                System.String spoolDirectory = printServer.DefaultSpoolDirectory;
                Boolean       isBeepEnabled  = printServer.BeepEnabled;

                //
                // Read a property that wasn't initialized when the object was constructed.
                // This operation requires a call to to the Print Spooler service. The data 
                // will be cached in the printServer object instance.
                //
                System.Threading.ThreadPriority portThreadPriority = printServer.PortThreadPriority;

                //
                // A subsequent call to read this property will use the cached data. 
                // This data can change independently by this application. To synchronize 
                // printServer properties with the Print Spooler service, the caller must call Refresh().
                //
                Console.WriteLine("Print Server " + printServer.Name + " has the following properties:\n" + "Spool Directory:     " + spoolDirectory + "\n" + "Beep Enabled:        " + isBeepEnabled + "\n" + "Thread Priority:     " + portThreadPriority + "\n");

                //
                // Set print server properties.
                //
                Console.WriteLine("Please enter valid print spool directory followed by ENTER:");

                String newSpoolerDirectory = Console.ReadLine();

                Console.WriteLine("The port thread priority will be set to ThreadPriority.AboveNormal.");
                Console.ReadLine();

                printServer.PortThreadPriority    = System.Threading.ThreadPriority.AboveNormal;
                printServer.DefaultSpoolDirectory = newSpoolerDirectory;
                
                //
                // The already initialized properties of the print server object will 
                // be refresh with data from the Print Spooler service.
                // All changes made in previous calls are lost.
                //
                printServer.Refresh();

                //
                // Set print server properties for real.
                //
                printServer.PortThreadPriority    = System.Threading.ThreadPriority.Normal;
                printServer.DefaultSpoolDirectory = newSpoolerDirectory;

                //
                // Notice that only properties that whose values changed will be commited.
                // If the printServer object thread property was Normal before the set operation
                // above, then the Commit will operate only for the DefaultSpoolerDirectory property.
                //
                printServer.Commit();

                Console.WriteLine("Operation succeeded.");
            }
            catch (PrintServerException serverException)
            {
                //
                // Handle a PrintServerException.
                //
                Console.WriteLine("Server name is" + serverException.ServerName + serverException.Message);
            }
            catch (PrintCommitAttributesException commitException)
            {
                //
                // Handle a PrintCommitAttributesException.
                //
                Console.WriteLine(commitException.Message);
                Console.WriteLine("These attributes failed to commit:");

                IEnumerator failedAttributesEnumerator = commitException.FailToCommitAttributes.GetEnumerator();

                for (; failedAttributesEnumerator.MoveNext(); )
                {
                    String attributeName = (String)(failedAttributesEnumerator.Current);

                    Console.WriteLine(attributeName);
                }

                //
                // To have the printServer object in sync with the Spooler service, a refresh is forced.
                // The properties that failed to commit will be lost.
                // The caller has the choice to not refresh the failed properties and
                // try another Commit() later one.
                //
                printServer.Refresh();
            }
        }    
    }
}
