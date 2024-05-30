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


/****************************************************************************
*                                                                           *
* WcsmSample1.cs - Sample application for WMI.Config managed API            *
*                                                                           *
* Component: WMI.Config - engine                                            *
*                                                                           *
* Copyright (c) 2002 - 2003, Microsoft Corporation                          *
*                                                                           *
****************************************************************************/


using System;                           
using System.Collections;                   // IEnumerator interface
using System.Configuration.Settings;        // WMI.Config API's

namespace WCMSample
{
    class SampleClient
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("wcmSample1: sample application for WMI.Config managed API's");
            
            SettingsEngine    engine = null;
            SettingsNamespace appNamespace = null;
            SettingsItem      appWindow    = null;
            SettingsItem      topleftX     = null;
            SettingsItem      windowTitle  = null;
            SettingsItem      playerNames  = null;

            try 
            {
                Console.WriteLine("Creating Engine");

                // Create the engine object
                engine = new SettingsEngine();

                //
                // Allocate a namespace identity
                //
                NamespaceIdentity namespaceID;

                namespaceID = new NamespaceIdentity(
                    "http://www.microsoft.com/state/WcmSample1",    // URI name
                    "1.0",                                          // version
                    "en-US",                                        // language ID
                    NamespaceContext.UserContext,                   // install context
                    null);      

                Console.WriteLine("Opening Namespace");

                //
                // Open the application namespace
                //
                appNamespace =  engine.GetNamespace(namespaceID,
                    NamespaceMode.SettingChangeMode,
                    System.IO.FileAccess.ReadWrite);

		        //
                // Get a setting using GetSettingByPath
                //	

                Console.WriteLine("GetSettingByPath(myAppWindow)");

                appWindow   = appNamespace.GetSettingByPath("myAppWindow");

                //
                // GetSettingByPath returns a setting item object.
                // Access the setting item information by using the item's fields.
                //

                Console.WriteLine("GetSettingByPath(topLeft/xCoord)");

                topleftX    = appWindow.GetSettingByPath("topLeft/xCoord");
                windowTitle = appWindow.GetSettingByPath("title");
                
                Console.WriteLine("GetSettingByPath(playerNames)");
                
                playerNames = appNamespace.GetSettingByPath("playerNames");

                Console.WriteLine("{0}: type={1}, value={2}", 
                    topleftX.Name, 
                    topleftX.Type, 
                    topleftX.Value);

                Console.WriteLine("{0}: type={1}, value={2}", 
                    windowTitle.Name, 
                    windowTitle.Type, 
                    windowTitle.Value);
                    
                Console.WriteLine("{0}: type={1}, value={2}",
                    playerNames.Name,
                    playerNames.Type,
                    String.Join(", ", (string[])playerNames.Value));
               
                //
                // Update the settings
                //

                topleftX.Value    = 300;    
                windowTitle.Value = "New Window Title 2";
                playerNames.Value = new string[] { "Robert", "Mary", "Jim" };

		        // 
                // Commit update to the store
                //
                appNamespace.Save();

                //
                // We can also read/write to the store by using SetSettingValue and
                // GetSettingValue
                //
                Console.WriteLine("----- Read/Write using very basic Get/Set API");

                String settingPath;
                
                settingPath = "myAppWindow/topLeft/xCoord";
                Console.WriteLine("Setting: {0} Value:{1}", 
                    settingPath,
                    appNamespace.GetSettingValue(settingPath));
 
                appNamespace.SetSettingValue(settingPath, 200);

                settingPath = "myAppWindow/title";
                Console.WriteLine("Setting: {0} Value:{1}", 
                    settingPath,
                    appNamespace.GetSettingValue(settingPath));

                appNamespace.SetSettingValue(settingPath, "My Window Title 1");
                
                settingPath = "playerNames";
                Console.WriteLine("Setting: {0} Value:{1}",
                    settingPath,
                    String.Join(", ", (string[])appNamespace.GetSettingValue(settingPath)));
                    
                appNamespace.SetSettingValue(settingPath, new string[] { "Jim", "Mary" });  
                
                //
                // Commit update to the store
                //
                appNamespace.Save();              
                
            }
            catch(SettingsException e)
            {
                Console.WriteLine("Exception: {0}, description={1}", e.Source, e.Message);
            }
        }
    }
}

