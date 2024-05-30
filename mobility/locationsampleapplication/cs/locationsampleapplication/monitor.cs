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
using System.Reflection;
using System.Location;
using System.Location.Extension;

namespace Microsoft.Samples.Location
{
    /// <summary>
    /// Location Monitor Sample code
    /// </summary>
    public class LocationMonitorSample: IDisposable
    {
        /// <summary>
        /// Data member referencing the remote location sensor instance
        /// </summary>
        private Sensor sensor;

        /// <summary>
        /// Notification handler to receive location update notifications
        /// </summary>
        /// <param name="notification"></param>
        public void UpdateNotificationHandler (Notification notification)
        {
            Console.WriteLine ("Notification received: \"{0}\"", notification);
            Console.WriteLine ("Current location described by {0} report(s).", sensor.ReportsList.Count);
            Console.WriteLine ();
        }

        #region Constructor and Disposal methods
        /// <summary>
        /// Class constructor
        /// </summary>
        public LocationMonitorSample ()
        {
            sensor = new Sensor ();
            NotificationHandler updateHandler = new NotificationHandler (UpdateNotificationHandler);
            sensor.Register(updateHandler);
        }

        /// <summary>
        /// Class disposal method. When the instance is disposed, make sure the connection to the Location
        /// Service is disposed as well. Otherwise the instance will still leave on the server until 
        /// a notification occurs. At that moment the server will get an exception causing the remote instance
        /// to eventually be disposed
        /// </summary>
        public void Dispose ()
        {
            if (sensor != null)
            {
                sensor.Dispose ();
                sensor = null;
            }
        }
        #endregion
        
        /// <summary>
        /// Static method containing the code needed to retrieve the current
        /// location from the Location Service
        /// </summary>
        public static void MonitorMain ()
        {
            LocationMonitorSample locationMonitor = null;
            try
            {
                // instantiate the monitor
                locationMonitor = new LocationMonitorSample ();
                Console.WriteLine ("<<Hit any key to stop listening for notifications>>");
                Console.WriteLine ();
                Console.ReadLine ();
            }
            finally
            {
                // Dispose the monitor as we're done listening for notifications
                if (locationMonitor != null)
                    locationMonitor.Dispose ();
            }
        }
    }
}
