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
* WcmSample2.cs - Sample application for WMI.Config managed API            *
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
            Console.WriteLine("wcmSample2: sample application for WMI.Config managed API's");
            
            SettingsEngine    engine = null;
            SettingsNamespace appNamespace = null;
            SettingsItem      players      = null;                 
            SettingsItem      playerInfo   = null;
            SettingsItem      playerScore  = null;

            SettingsItemCollection    itemCollection = null;
            IEnumerator               itemEnumerator = null;

            try 
            {
                // Create the Settings Engine object
                engine = new SettingsEngine();

                //
                // Allocate a namespace identity
                //
                NamespaceIdentity namespaceID;

                namespaceID = new NamespaceIdentity(
                    "http://www.microsoft.com/state/WcmSample2",    // URI name
                    "1.0",                                          // version
                    "en-US",                                        // language ID
                    NamespaceContext.UserContext,                   // install context
                    null);      

                //
                // Open the application namespace
                //
                appNamespace =  engine.GetNamespace(namespaceID,
                    NamespaceMode.SettingChangeMode,
                    System.IO.FileAccess.ReadWrite);

                //
                // Read a setting from a list, and then delete it
                //

                Console.WriteLine("----- Read a setting from a list, and then delete it");

                String settingPath;
                
                try
                {
                    settingPath = "players/playersList[@player='Joe']";
                    playerInfo = appNamespace.GetSettingByPath(settingPath);

                    Console.WriteLine("Setting: {0}; score value={1}", 
                        settingPath,
                        playerInfo.GetSettingByPath("score").Value);
 
                    // The deletion can be verified in HKEY_CURRENT_USER\WcmSample2

                    settingPath = "players";
                    Console.WriteLine("Deleting this player info");

                    players = appNamespace.GetSettingByPath(settingPath);
                    players.Children.Remove(playerInfo.Name);
                }
                catch (SettingsException e)
                {
                    Console.WriteLine("Failure: {0}", e.Message);
                    Console.WriteLine("This failure is expected: at the first time execution, the setting is not there yet.");
                }

                // Commit update to the store

                appNamespace.Save();

                //
                // Add a list item  
                //
                Console.WriteLine("----- Add a list item");

                settingPath = "players";
                players = appNamespace.GetSettingByPath(settingPath);

                itemCollection = players.Children;

                settingPath = "playersList[@player='Joe']";
                playerInfo = itemCollection.Create(settingPath);
                
                Console.WriteLine("  List item added: {0}", settingPath);

                //
                // Update the settings
                //
                playerScore = playerInfo.GetSettingByPath("score");

                Object objectValue;
                Int32 scoreValue;

                objectValue = playerScore.Value;
                if (objectValue == null)
                {
                    Console.WriteLine("  This is expected: Player score has no value yet.");
                }
                else
                {
                    // should not reach here.
                    Console.WriteLine("  Previous score value={0}", (Int32)objectValue);
                }

                scoreValue = 300;
                playerScore.Value = scoreValue;
                Console.WriteLine("  score value changed to={0}", scoreValue);

                try
                {
                    // The following value should be rejected due to 
                    // restriction on the data type
                    playerScore.Value   = 1300;
                }
                catch (SettingsException e)
                {
                    Console.WriteLine("Failure: {0}", e.Message);
                    Console.WriteLine("This failure is expected: trying to set an invalid value.");
                }

                //
                // Enumerate child items under "player" setting
                //
                Console.WriteLine("----- Enumerate child items under \"players\" setting");

                itemEnumerator = itemCollection.GetEnumerator();

                while (itemEnumerator.MoveNext())
                {
                    String settingName = (String) itemEnumerator.Current;

                    // Get the current list item
                    playerInfo = itemCollection[settingName];

                    Console.WriteLine("  Setting: {0}; score value={1}", 
                        settingPath,
                        playerInfo.GetSettingByPath("score").Value);
                }

                //
                // Commit update to the store
                //
                try
                {
                    appNamespace.Save();
                }
                catch (ValidationException e)
                {
                    // At commit time, some assertion evaluation may happen at Configuration 
                    // Service side. Exception would be thrown if some assertion has failed.

                    // During Save, the settings are written into managed store, and also
                    // pushed into legacy store.
                    Console.WriteLine("Validation Failure in Save: {0}", e.Message);
                }
                catch (SettingsException e)
                {
                    // Handle other exceptions thrown from save.
                    Console.WriteLine("Failure in Save: {0}", e.Message);
                }

                //
                // Enumerate all the top-level settings
                //
                Console.WriteLine("----- Enumerate all the top-level settings:");

                itemCollection = appNamespace.Settings;

                itemEnumerator = itemCollection.GetEnumerator();

                while (itemEnumerator.MoveNext())
                {
                    Object settingName = itemEnumerator.Current;
                    Console.WriteLine("  {0}", (String)settingName);
                }

                //
                // Notifications for managed code will be implemented in the future
                //
            }
            catch(SettingsException e)
            {
                Console.WriteLine("Exception: {0}, description={1}", e.Source, e.Message);
            }
        }
    }
}

