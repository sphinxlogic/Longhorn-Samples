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
using System.Text;
using System.Collections;
using System.Location.Extension;

namespace Microsoft.Samples.Location
{
    /// <summary>
    /// Class implementing a Sample Location Provider
    /// </summary>
    public class SampleProvider : AProvider
    {
        /// <summary>
        /// Object representing the connection to the underlying (simulated) device
        /// </summary>
        private SimulatedDeviceConnector _SimulatedDeviceConnector;

        /// <summary>
        /// Builds an ArrayList of location Ieee80211Report objects from the 
        /// device specific data.
        /// </summary>
        /// <param name="SimWifiBeacons">Array of device specific data objects</param>
        /// <returns>ArrayList of location reports</returns>
        private ArrayList BuildReportsFromBeacons(SimulatedBeacon[] SimWifiBeacons)
        {
            ArrayList newReports = new ArrayList();

            foreach (SimulatedBeacon simWiFiBeacon in SimWifiBeacons)
            {
                StringBuilder sbSsid = new StringBuilder();
                Byte[] Ssid = simWiFiBeacon.GetSsid();
                foreach (Byte b in Ssid)
                {
                    sbSsid.Append((char)b);
                }
                Ieee80211Report ieee80211Report = 
                                new Ieee80211Report(
                                        sbSsid.ToString(),
                                        simWiFiBeacon.GetMacAddress());
                newReports.Add(ieee80211Report);
            }
            return newReports;
        }

        /// <summary>
        /// Constructor for the plugin [optional];
        /// All initialization code should be implemented here;
        /// Error cases should be propagated as exceptions;
        /// </summary>
        public SampleProvider()
        {
            // Connect to the underlying (simulated) device by instantiated a connector
            _SimulatedDeviceConnector = new SimulatedDeviceConnector();
            // Probe the device for its initial data
            SimulatedBeacon[] beacons = _SimulatedDeviceConnector.ProbeDeviceForCurrentData();
            ArrayList simWifiReports = BuildReportsFromBeacons(beacons);

            // Push the result of the initial environment sensing to the Location Service
            PushReports(simWifiReports);
            _SimulatedDeviceConnector.DeviceNotification += 
                new SimulatedDeviceEventHandler(OnSimulatedDeviceNotification);
        }

        /// <summary>
        /// Disposal of the plugin [optional];
        /// All destruction / termination code should be implemented here;
        /// </summary>
        /// <param name="active">true, if explicit destruction</param>
        protected override void Dispose(bool active)
        {
            _SimulatedDeviceConnector.Dispose();
        }

        /// <summary>
        /// Event handler for the notifications sent by the underlying device
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSimulatedDeviceNotification(object sender, SimulatedDeviceEventArgs e)
        {
            SimulatedBeacon[] SimWifiBeacons = e.GetBeacons();
            ArrayList simWiFiReports = this.BuildReportsFromBeacons(SimWifiBeacons);
            PushReports(simWiFiReports);
        }
    }
}
