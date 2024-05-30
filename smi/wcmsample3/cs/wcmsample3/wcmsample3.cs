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
* WcmSample3.cs - Sample application for WMI.Config managed API            *
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
            Console.WriteLine("wcmSample3: sample application for WMI.Config managed API's");
            
            SettingsEngine    engine = null;
            SettingsNamespace appNamespace = null;
            Metadata          metaNode     = null;
            SettingsItem      players      = null;
            SettingsAllowedValue  value    = null;
            SettingsResult    errorResult=null;
            AssertionCollection assertionCollection = null;
            try 
            {
                // Create the engine object
                engine = new SettingsEngine();

                //
                // Allocate a namespace identity
                //
                NamespaceIdentity namespaceID;

                namespaceID = new NamespaceIdentity(
                    "http://www.microsoft.com/state/WcmSample3",    // URI name
                    "1.0",                                          // version
                    "en-US",                                        // language ID
                    NamespaceContext.UserContext,                   // install context
                    null);      

                //
                // Open the application namespace
                //
                appNamespace =  engine.GetNamespace(namespaceID,
                    NamespaceMode.MetadataChangeMode,
                    System.IO.FileAccess.ReadWrite);

                //
                // Get the metadata node
                //
                metaNode = appNamespace.Metadata;

                //
                // Simple read/write of setting
                //

                Console.WriteLine("----- Check AllowedValue of setting");
                
                String settingPath;
                settingPath = "players";
                
                players = appNamespace.GetSettingByPath(settingPath);
                value = players.GetAllowedValues();
                if (value == null)
                {
                    Console.WriteLine("Unknown value GetAllowedValues");
                }
                else
                {
                    switch (value.AllowedValueType)
                    {
                    case System.Configuration.Settings.AllowedValueType.NoLimit:
                        Console.WriteLine("No limit on this setting");
                        break;
                    case System.Configuration.Settings.AllowedValueType.SingleValueEqual:
                    case System.Configuration.Settings.AllowedValueType.SingleValueGreater:
                    case System.Configuration.Settings.AllowedValueType.SingleValueGreaterOrEqual:
                    case System.Configuration.Settings.AllowedValueType.SingleValueLess:
                    case System.Configuration.Settings.AllowedValueType.SingleValueLessOrEqual:
                    case System.Configuration.Settings.AllowedValueType.SingleValueNotEqual:
                        Object theValue = value.Value;
                        Console.WriteLine("Allowed value type is: {0} value: {1}", value.AllowedValueType, theValue.GetType());
                        break;
                    }
                }

                //
                // Create an operational assertion
                //
                String assertionName="OperationalAssertPlayers";
                AssertionType assertionType = AssertionType.OperationalAssertion;
                String expression = "#/settings/players >= 10";
                String assertionDescription = "This is an operational assertion on players setting";
                String displayText = null;
                String helpUrl = null;
                Boolean pending = false;
                Boolean silenced = false;
                String onfailedAction = null;
                String onsilencedAction = null;
                String[] affectedUsers = null;
                String[] silencelink = null;
                String[] backSilencelink = null;
                String source = "WcmSample3";
                String satisfyValues = "Value(#/settings/players , 10)";

                errorResult = metaNode.IsAssertionExpressionValid(expression);
                if (errorResult == null) // syntax correct
                {
                    Assertion assertion = metaNode.CreateAssertion(assertionName,
                                                                   assertionType,
                                                                   expression,
                                                                   satisfyValues,
                                                                   pending);

                    if (assertionDescription != null)
                        assertion.Description = assertionDescription;

                    if (displayText != null)
                        assertion.DisplayName = displayText;

                    if (helpUrl != null)
                        assertion.HelpUrl = helpUrl;

                    assertion.ExplicitSilenceState = silenced;

                    if (onfailedAction != null)
                        assertion.FailedAction = onfailedAction;

                    if (onsilencedAction != null)
                        assertion.SilencedAction = onsilencedAction;

                    if (affectedUsers != null)
                        assertion.SetUsers(affectedUsers);

                    if (silencelink != null)
                        assertion.SetSilenceLinks(silencelink);

                    if (backSilencelink != null)
                        assertion.SetBackwardSilenceLinks(backSilencelink);

                    assertion.Source = source;

                    Console.WriteLine("A new assertion {0} is created", assertion.Name);

                    // Get the list of assertions
                    assertionCollection = metaNode.Assertions;
                    Console.WriteLine("The number of assertions in the namespace is {0}", assertionCollection.Count);
                    
                    foreach (String assertItemName in assertionCollection)
                    {
                        Console.WriteLine("name: {0} Expression: {1}", assertItemName, assertionCollection[assertItemName].Expression);
                    }

                    // create a new assertion
                    Assertion newAssert = null;

                    assertionName = "assertionPlay3";
                    Console.WriteLine("Create new assertion {0}", assertionName);
                    newAssert = assertionCollection.Create(assertionName);
                    newAssert.Expression = "#/settings/players != 17";
                    newAssert.SatisfyValueExpression = "Set(Value(#/settings/players, 18))";
                    newAssert.IsPending = false;

                    foreach (String assertItemName in assertionCollection)
                    {
                        Console.WriteLine("name: {0} Expression: {1}", assertItemName, assertionCollection[assertItemName].Expression);
                    }
                }
                else
                {
                    Console.WriteLine("Assertion Expression {0} has syntax error at position:{1} ", 
                                      expression,
                                      errorResult.Column);
                }

                // Commit update to persistent store

                appNamespace.Save();
                
                //
                // Delete all operational assertions
                //

                assertionCollection = metaNode.Assertions;
                foreach (String assertName in assertionCollection)
                {
                    Console.WriteLine("Delete assertion: {0}", assertName);
                    try
                    {
                        assertionCollection.Remove(assertName);
                    }
                    catch (System.UnauthorizedAccessException e)
                    {
                        Console.WriteLine("Failure: {0}", e.Message);
                        Console.WriteLine("This failure is expected: this is a developer assertion.");
                    }
                }
                Console.WriteLine("assertions count: {0}", assertionCollection.Count);

                // Commit update to persistent store

                appNamespace.Save();
            }
            catch(SettingsException e)
            {
                Console.WriteLine("Exception: {0}, description={1}", e.Source, e.Message);
            }
            finally
            {
                //
                // We recommend to dispose the objects
                //
                if(players != null)
                    players.Dispose();

                if(metaNode != null)
                    metaNode.Dispose();

                if(appNamespace != null)
                    appNamespace.Dispose();
            }
        }
    }
}

