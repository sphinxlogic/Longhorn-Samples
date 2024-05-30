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
using System.Timers;
using System.Collections;
using System.Text;

namespace Microsoft.Samples.Location
{
    /// <summary>
    /// Data this simulated device is capable of providing which is relevant
    /// from the location sensing point of view
    /// </summary>
    public class SimulatedBeacon
    {
        /// <summary>
        /// Private data member giving the root name of the Ssid in the beacon
        /// </summary>
        private static Byte[] _SsidRoot = new Byte[] {
                (Byte)'M', 
                (Byte)'y', 
                (Byte)'_', 
                (Byte)'S', 
                (Byte)'s', 
                (Byte)'i', 
                (Byte)'d', 
                (Byte)'#'
            };

        /// <summary>
        /// Private data member mapped to the Ssid property
        /// </summary>
        private Byte[] _Ssid;

        /// <summary>
        /// Private data member mapped to the MacAddress property
        /// </summary>
        private Byte[] _MacAddress;

        /// <summary>
        /// Public SimulatedBeacon
        /// </summary>
        /// <param name="randomGenerator">Random generator to use</param>
        public SimulatedBeacon(Random randomGenerator)
        {
            _Ssid = new Byte[_SsidRoot.Length + 1];
            Array.Copy(_SsidRoot, _Ssid, _SsidRoot.Length);
            _Ssid[_SsidRoot.Length] = (Byte)((int)'0' + randomGenerator.Next(0, 9));
            _MacAddress = new Byte[6];
            randomGenerator.NextBytes(_MacAddress);
        }

        /// <summary>
        /// Returns the MAC address of this beacon
        /// </summary>
        /// <returns></returns>
        public Byte[] GetMacAddress()
        {
            return _MacAddress;
        }

        /// <summary>
        /// Returns the Ssid of this beacon
        /// </summary>
        /// <returns></returns>
        public Byte[] GetSsid()
        {
            return _Ssid;
        }
    }

    /// <summary>
    /// Event arguments for the simulated device notification
    /// </summary>
    public class SimulatedDeviceEventArgs : EventArgs
    {
        /// <summary>
        /// Internal data member mapped to the SimWiFiBeacon property
        /// </summary>
        private SimulatedBeacon[] _SimWifiBeacons;

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="beacons"></param>
        public SimulatedDeviceEventArgs(params SimulatedBeacon[] beacons)
        {
            _SimWifiBeacons = beacons;
        }
        /// <summary>
        /// Returns the list of beacons embedded in this simulated device event
        /// </summary>
        /// <returns></returns>
        public SimulatedBeacon[] GetBeacons()
        {
            return _SimWifiBeacons;
        }
    }

    /// <summary>
    /// Delegate definition for applications to implement in order to
    /// receive device notifications
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void SimulatedDeviceEventHandler(object sender, SimulatedDeviceEventArgs e);

    /// <summary>
    /// Class definition for the simulated lower layer device
    /// </summary>
    public class SimulatedDeviceConnector : IDisposable
    {
        #region Private data members needed to simulate the device functioning
        /// <summary>
        /// Size of the WiFi beacons pool.
        /// </summary>
        private static int _SimWifiBeaconsPoolSize = 10;

        /// <summary>
        /// Random number generator simulating device activity
        /// </summary>
        private Random _simDevRndGen;

        /// <summary>
        /// Internal private timer to simulate device activity
        /// </summary>
        private Timer _simDevHeartbeat;

        /// <summary>
        /// Internal pool of simulated WiFi beacons
        /// </summary>
        private SimulatedBeacon[] _SimWifiBeaconsPool;
        
        /// <summary>
        /// Array of indices for the WiFiBeacons
        /// </summary>
        private int[] _SimWifiBeaconsIndices;
        #endregion

        /// <summary>
        /// Private static class constructor.
        /// </summary>
        public SimulatedDeviceConnector()
        {
            _simDevRndGen = new Random();
            // construct the pool of wifi beacons and indices
            _SimWifiBeaconsPool = new SimulatedBeacon[_SimWifiBeaconsPoolSize];
            _SimWifiBeaconsIndices = new int[_SimWifiBeaconsPoolSize];
            for (int i = 0; i < _SimWifiBeaconsPool.Length; i++)
            {
                _SimWifiBeaconsPool[i] = new SimulatedBeacon(_simDevRndGen);
                _SimWifiBeaconsIndices[i] = i;
            }
            _simDevHeartbeat = new Timer();
            _simDevHeartbeat.AutoReset = false;
            _simDevHeartbeat.Elapsed += new ElapsedEventHandler(OnTimerElapsed);
            _simDevHeartbeat.Interval = TimeSpan.FromSeconds(_simDevRndGen.Next(5, 15)).TotalMilliseconds;
            _simDevHeartbeat.Start();
        }

        /// <summary>
        /// IDisposable implementation
        /// </summary>
        public void Dispose()
        {
            if (_simDevHeartbeat != null)
            {
                _simDevHeartbeat.Dispose();
                _simDevHeartbeat = null;
            }
        }

        #region Internal methods and event handlers needed for simulating the device
        /// <summary>
        /// Private handler for the timer notification (simulated device notification)
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void OnTimerElapsed(object source, ElapsedEventArgs e)
        {
            int nextHeartBeat = _simDevRndGen.Next(5, 15);

            _simDevHeartbeat.Stop();
            if (DeviceNotification.GetInvocationList().Length > 0)
            {
                SimulatedBeacon[] currentWiFiBeacons = ProbeDeviceForCurrentData();
                SimulatedDeviceEventArgs sdeArgs = new SimulatedDeviceEventArgs(currentWiFiBeacons);

                DeviceNotification(this, sdeArgs);
            }
            _simDevHeartbeat.Interval = TimeSpan.FromSeconds(nextHeartBeat).TotalMilliseconds;
            _simDevHeartbeat.Start();
        }

        /// <summary>
        /// Shuffles the array of indices in the WiFi beacons pool in order to simulate
        /// the variations in the environment sensed by the device
        /// </summary>
        private void ShufflePoolIndices()
        {
            for (int i = 0; i < _SimWifiBeaconsPoolSize; i++)
            {
                int j = _simDevRndGen.Next(0, _SimWifiBeaconsPoolSize - 1);
                if (j != i)
                {
                    int swap = _SimWifiBeaconsIndices[i];
                    _SimWifiBeaconsIndices[i] = _SimWifiBeaconsIndices[j];
                    _SimWifiBeaconsIndices[j] = swap;
                }
            }
        }
        #endregion

        /// <summary>
        /// Event for applications to register their delegates with in order
        /// to receive notifications coming from the simulated device.
        /// </summary>
        public event SimulatedDeviceEventHandler DeviceNotification;

        /// <summary>
        /// Retrieves the current data at the simulated device level
        /// </summary>
        /// <returns></returns>
        public SimulatedBeacon[] ProbeDeviceForCurrentData()
        {
            int numberOfBeacons = _simDevRndGen.Next(0, _SimWifiBeaconsPoolSize);
            SimulatedBeacon[] currentBeacons = new SimulatedBeacon[numberOfBeacons];

            // shuffle the pool indices array to simulate variations in the environment
            this.ShufflePoolIndices();
            // copy in the result array the first numberOfBeacons elemenents
            for (int i = 0; i < numberOfBeacons; i++)
            {
                currentBeacons[i] = _SimWifiBeaconsPool[_SimWifiBeaconsIndices[i]];
            }
            return currentBeacons;
        }
    }
}
