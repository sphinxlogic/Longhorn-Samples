//---------------------------------------------------------------------
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
using System.Windows.Media.Core;
using System.Windows.Media.Types;

namespace ConsolePlayerC
{
    /// <summary>
    /// This simple console application plays the audio file specified on the command line.
    /// </summary>
    class MediaEngineDemo
    {
        private MediaEngine m_mediaEngine = null;
        private System.Threading.AutoResetEvent engineCanBeDisposedEvent = null;
        private System.Threading.AutoResetEvent engineOpenForPlayEvent = null;
        private Boolean m_validFile = true;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (0 < args.Length)
            {
                MediaEngineDemo player = new MediaEngineDemo(args[0]);
            }
            else
            {
                System.Console.WriteLine("No media file specified.");
            }
        }

        MediaEngineDemo(string mediaFilePath)
        {
            // Create MediaEngine.
            m_mediaEngine = new MediaEngine();

            // Add event handlers.
            m_mediaEngine.MediaEnded += new System.Windows.Media.Core.MediaEventHandler(this.MediaEnded);
            m_mediaEngine.MediaOpened += new System.Windows.Media.Core.MediaEventHandler(this.MediaOpened);

            // Create private events to be signaled by media event handlers.
            engineCanBeDisposedEvent = new System.Threading.AutoResetEvent(false);
            engineOpenForPlayEvent = new System.Threading.AutoResetEvent(false);

            // Open and play the file. 
                // Because media engine methods are asynchronous, they always return "success" 
                // and any errors must be caught in the event handlers. In this case, if the file
                // is not valid, a global variable is set.
                m_mediaEngine.Open(mediaFilePath, null, typeof(AudioMediaType));
                engineOpenForPlayEvent.WaitOne();
            if (m_validFile)
            {
                m_mediaEngine.Start();
                // Wait till playback is finished before exiting.
                engineCanBeDisposedEvent.WaitOne();
                }
            else
            {
                Console.WriteLine("Could not play file.");
            }
            engineCanBeDisposedEvent.Close();
            engineOpenForPlayEvent.Close();
        }

        /// <summary>
        /// Handler for MediaEnded event. This event is signaled when playback has reached
        /// the end of the file.
        /// </summary>
        private void MediaEnded(Object sender, MediaEventArgs args)
        {
            engineCanBeDisposedEvent.Set();
        }

        /// <summary>
        /// Handler for MediaOpened event. Any exception raised by trying to open an invalid file
        /// must be caught here.
        /// </summary>
        private void MediaOpened(Object sender, MediaEventArgs args)
        {
            if (args.Exception != null)
            {
                m_validFile = false;
            }
            engineOpenForPlayEvent.Set();
        }
        
    }
}
