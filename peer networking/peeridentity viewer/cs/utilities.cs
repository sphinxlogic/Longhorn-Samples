//=====================================================================
// File:      utilities.cs
//
// Summary:   Utility routines for this application.
//
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
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Net.PeerToPeer;
using System.Windows.Forms;
using System.Diagnostics;

namespace IdentityViewer
{
    /// <summary>
    /// Utility routines.
    /// </summary>
    public sealed class Utilities
    {
        // This class has only static methods.
        private Utilities()
        {
        }

        /// <summary>
        /// Fill a ComboBox with the available identities.
        /// </summary>
        /// <param name="lstBox">The ComboBox to popuplate.</param>
        /// <param name="identityDefault">The PeerIdentity to select.</param>
        public static void FillIdentityComboBox(System.Windows.Forms.ComboBox comboBox, PeerIdentity identityDefault)
        {
            comboBox.BeginUpdate();

            // Remove any previous entries
            comboBox.Items.Clear();

            List<PeerIdentity> identities = PeerIdentity.GetIdentities();

            Object selectedItem = null;
            foreach (PeerIdentity identity in identities)
            {
                comboBox.Items.Add(identity);

                // Check if this is the identity we want to select.
                if (identity.Equals(identityDefault))
                {
                    selectedItem = identity;
                }
            }

            // Select the default identity or the first one in the list.
            if (selectedItem != null)
            {
                comboBox.SelectedItem = selectedItem;
            }
            else if (identities.Count > 0)
            {
                comboBox.SelectedIndex = 0;
            }

            comboBox.EndUpdate();
        }


        /// <summary>
        /// Read the information from a text file and return it as a string.
        /// </summary>
        /// <param name="fileName">The full path to the file.</param>
        /// <returns>A string that contains the text from the file.</returns>
        public static string ReadTextFile(string fileName)
        {
            string data = string.Empty;

            // Create an instance of StreamReader to read from a file.
            // The using statement also closes the StreamReader.
            using (StreamReader sr = new StreamReader(fileName, System.Text.Encoding.Unicode))
            {
                data = sr.ReadToEnd();
            }

            return data;
        }

        /// <summary>
        /// Write the text data to a file.
        /// </summary>
        /// <param name="fileName">The full path to the file.</param>
        /// <param name="data">The string data to write.</param>
        public static void WriteTextFile(string fileName, string data)
        {
            UnicodeEncoding unicode = new UnicodeEncoding(false, false);
            StreamWriter sw = new StreamWriter(fileName, false, unicode);
            sw.WriteLine(data);
            sw.Flush();
            sw.Close();
        }

        /// <summary>
        /// Display an Exception message in a dialog
        /// </summary>
        /// <param name="e">The Exception to display.</param>
        /// <param name="formParent">The parent window for the dialog.</param>
        public static void DisplayException(Exception e, Form formParent)
        {
            DialogResult idbutton = MessageBox.Show(formParent, e.Message + "\r\n\r\n" + e.StackTrace, "Exception from " + e.Source, MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);

            switch (idbutton)
            {
                case DialogResult.Retry :
                    {
                        Debugger.Break();
                        break;
                    }

                case DialogResult.Abort :
                    {
                        formParent.Close();
                        break;
                    }

                default :
                    break;
            }
        }
    }
}
