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
using System.Collections;
using System.Runtime.Remoting;
using System.Location;
using System.Location.Extension;

namespace Microsoft.Samples.Location
{
    /// <summary>
    /// Location Context Sample application
    /// </summary>
    public class LocationContextSample
    {
        /// <summary>
        /// Lists the contexts in the Location service ContextStore 
        /// </summary>
        public static void ContextListMain()
        {
            ContextStore contextStore = null;
            try
            {
                // Connect to the Location Service by instantiating a read-write "ContextStore" object.
                contextStore = new ContextStore(AccessType.AccessRO);

                // Retrieve the list of contexts from the service, print their count, and then, for each
                // context print the list of AReport instances in their pattern
                ArrayList contextList = contextStore.EnumerateContexts();
                Console.WriteLine("{0} contexts in the Context Store", contextList.Count);
                int i = 0;
                foreach (ContextReport contextReport in contextList)
                {
                    Console.WriteLine ("[{0}] Context '{1}' pattern:", i++, contextReport.Name);
                    foreach (AReport reportData in contextReport.Pattern)
                    {
                        Console.WriteLine ("      {0}", reportData);
                    }
                }
            }
            finally
            {
                // Dispose the instance connected to the ContextStore
                if (contextStore != null)
                    contextStore.Dispose();
            }
        }
        /// <summary>
        /// Static method containing the code needed to retrieve the current
        /// location from the Location Service and define a pushpin for it, mapping to
        /// a name given as parameter. The code connects to the Location Service with a Sensor 
        /// and a ContextStore instance, then performs the following actions:
        /// </summary>
        public static void ContextAddMain(string name)
        {
            Sensor sensor = null;
            ContextStore contextStore = null;
            try
            {
                // Connect to the Location Service by instantiating a "Sensor" object and
                // a read-write "ContextStore" object.
                sensor = new Sensor();
                contextStore = new ContextStore(AccessType.AccessRW);

                // Query the sensor for all the "best guess" reports describing the current
                // location. It is a possibility the service could not conclude to a "best guess"
                // location (i.e. due to equally weighted conflicting data), in which case the
                // list of reports will be empty
                ArrayList locationReports = sensor.ReportsList;
                if (locationReports.Count == 0)
                {
                    Console.WriteLine ("Can't determine the current location.");
                }
                else
                {
                    // at this point, we have the "best guess" current location. Create then a ContextReport
                    // to be added to the Context Store. Then, collect all ReportData for the "best-guess"
                    // reports into one ArrayList and add them to the pattern of the ContextReport. At the end,
                    // add the pushpin to the Context Store
                    ArrayList contextPattern = new ArrayList();
                    foreach (Report locationReport in locationReports)
                    {
                        if (!(locationReport.ReportData is ContextReport) &&
                            !contextPattern.Contains(locationReport.ReportData))
                            contextPattern.Add(locationReport.ReportData);
                    }
                    ContextReport contextReport = new ContextReport();
                    contextReport.Name = name;
                    contextReport.Pattern = contextPattern;
                    contextStore.SetContext(SetContextFlags.New, contextReport);
                }
            }
            finally
            {
                // Dispose the remote instances as we're done using them
                if (sensor != null)
                    sensor.Dispose ();
                if (contextStore != null)
                    contextStore.Dispose();
            }
        }
    }
}
