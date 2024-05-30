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
    /// Location Query Sample code.
    /// </summary>
    public class LocationQuerySample
    {
        /// <summary>
        /// Static method containing the code needed to retrieve the current
        /// location from the Location Service. The code connects first to the Location
        /// Service running on the same box, then issues several queries for the current
        /// location:
        /// 1) Queries the Name of the current location, if any has been determined
        /// 2) Queries all the location data that has been determined to describe most
        ///    accurately the current location (superset of the above)
        /// 3) Queries for the extended set of location data, including raw data provided
        ///    by the provider, data added by the resolvers and data determined by the 
        ///    fusing logic from within the service (superset of the above)
        /// </summary>
        public static void QueryMain ()
        {
            Sensor sensor = null;
            try
            {
                // Connect to the Location Service by instantiating a "Sensor" opbject
                sensor = new Sensor ();

                // Query the sensor for the "best guess" name of the current location.
                // If the current location is determined to have such a name, display it
                Report locationName = sensor.CurrentLocation (typeof(ContextReport));
                if (locationName == null)
                    Console.WriteLine ("Can't determine the name for the current location.");
                else
                    Console.WriteLine ("Current location name is {0}", locationName);

                // Query the sensor for all the "best guess" reports describing the current
                // location. It is a possibility the service could not conclude to a "best guess" 
                // location (i.e. due to equally weighted conflicting data), in which case the
                // list of reports will be empty
                ArrayList locationReports = sensor.CurrentLocation ();
                Console.WriteLine ("{0} 'best-guess' reports for the current location.", locationReports.Count);
                foreach (Report locationReport in locationReports)
                {
                    Console.WriteLine ("    {0}: \"{1}\"", locationReport.ReportData.GetType (), locationReport);
                }

                // Query the sensor for the extended set of Reports that was used to infer the current
                // location. This list includes all the raw (provider) reports, all the resolved reports
                // and all the fused reports generated for the current location snapshot
                locationReports = sensor.ReportsList;
                Console.WriteLine ("{0} reports for the current location.", locationReports.Count);
                foreach (Report locationReport in locationReports)
                {
                    Console.WriteLine ("    {0}: \"{1}\"", locationReport.ReportData.GetType (), locationReport);
                }
            }
            finally
            {
                // Dispose the sensor as we're done using it
                if (sensor != null)
                    sensor.Dispose ();
            }
        }
    }
}
