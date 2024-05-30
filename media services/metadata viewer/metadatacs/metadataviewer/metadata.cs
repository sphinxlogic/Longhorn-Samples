//---------------------------------------------------------------------
//
//  Copyright (C) Microsoft Corporation.  All rights reserved.
// 
// This source code is intended only as a supplement to Microsoft
// Development Tools and/or on-line documentation.  See these other
// materials for detailed information regarding Microsoft code samples.
// 
// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//---------------------------------------------------------------------

#define DEBUG

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Windows.Media.Core;
using System.Windows.Media.Types;
using System.Diagnostics;

namespace MetadataCS
{
    /// <summary>
    /// This sample application lists metadata in a media file.
    /// </summary>
    public class Form1 : System.Windows.Forms.Form
    {
        String m_fileName = String.Empty;
        
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.MenuItem menuFile;
        private System.Windows.Forms.MenuItem menuOpen;
        private System.Windows.Forms.MenuItem menuExit;
        private System.Windows.Forms.StatusBar statusBar1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader chProperty;
        private System.Windows.Forms.ColumnHeader chValue;
        private System.Windows.Forms.ColumnHeader chType;
        private System.Windows.Forms.RadioButton rbAll;
        private System.Windows.Forms.RadioButton rbNamed;
        private System.ComponentModel.IContainer components;


        /// <summary>
        /// Required designer variable.
        /// </summary>

        public Form1()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            // Resize column headers to fit window.
            listView1.Columns[0].Width = listView1.ClientRectangle.Width / 3;
            listView1.Columns[1].Width = listView1.ClientRectangle.Width / 3;
            listView1.Columns[2].Width = listView1.ClientRectangle.Width / 3;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose( bool disposing )
        {
            if( disposing )
            {
                if (components != null) 
                {
                    components.Dispose();
                }
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
			this.menuFile = new System.Windows.Forms.MenuItem();
			this.menuOpen = new System.Windows.Forms.MenuItem();
			this.menuExit = new System.Windows.Forms.MenuItem();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.statusBar1 = new System.Windows.Forms.StatusBar();
			this.listView1 = new System.Windows.Forms.ListView();
			this.chProperty = new System.Windows.Forms.ColumnHeader();
			this.chType = new System.Windows.Forms.ColumnHeader();
			this.chValue = new System.Windows.Forms.ColumnHeader();
			this.rbAll = new System.Windows.Forms.RadioButton();
			this.rbNamed = new System.Windows.Forms.RadioButton();
			this.SuspendLayout();

// 
// mainMenu1
// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
				this.menuFile
			});
			this.mainMenu1.Name = "mainMenu1";

// 
// menuFile
// 
			this.menuFile.Index = 0;
			this.menuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
				this.menuOpen, this.menuExit
			});
			this.menuFile.Name = "menuFile";
			this.menuFile.Text = "&File";

// 
// menuOpen
// 
			this.menuOpen.Index = 0;
			this.menuOpen.Name = "menuOpen";
			this.menuOpen.Text = "&Open...";
			this.menuOpen.Click += new System.EventHandler(this.menuOpen_Click);

// 
// menuExit
// 
			this.menuExit.Index = 1;
			this.menuExit.Name = "menuExit";
			this.menuExit.Text = "E&xit";
			this.menuExit.Click += new System.EventHandler(this.menuExit_Click);

// 
// openFileDialog1
// 
			this.openFileDialog1.AddExtension = false;
			this.openFileDialog1.Filter = "All files | *.*";

// 
// statusBar1
// 
			this.statusBar1.Location = new System.Drawing.Point(0, 420);
			this.statusBar1.Name = "statusBar1";
			this.statusBar1.Size = new System.Drawing.Size(544, 22);
			this.statusBar1.TabIndex = 0;
			this.statusBar1.Text = "No file loaded.";

// 
// listView1
// 
			this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
				this.chProperty, this.chType, this.chValue
			});
			this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listView1.Location = new System.Drawing.Point(40, 24);
			this.listView1.MultiSelect = false;
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(464, 336);
			this.listView1.TabIndex = 1;
			this.listView1.View = System.Windows.Forms.View.Details;

// 
// chProperty
// 
			this.chProperty.Text = "Property";
			this.chProperty.Width = 120;

// 
// chType
// 
			this.chType.Text = "Type";

// 
// chValue
// 
			this.chValue.Text = "Value";
			this.chValue.Width = 153;

// 
// rbAll
// 
			this.rbAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.rbAll.Checked = true;
			this.rbAll.Location = new System.Drawing.Point(168, 384);
			this.rbAll.Name = "rbAll";
			this.rbAll.TabIndex = 2;
			this.rbAll.TabStop = true;
			this.rbAll.Text = "&All properties";
			this.rbAll.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);

// 
// rbNamed
// 
			this.rbNamed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.rbNamed.Location = new System.Drawing.Point(296, 384);
			this.rbNamed.Name = "rbNamed";
			this.rbNamed.TabIndex = 3;
			this.rbNamed.Text = "&Named only";
			this.rbNamed.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);

// 
// Form1
// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(544, 442);
			this.Controls.Add(this.rbNamed);
			this.Controls.Add(this.rbAll);
			this.Controls.Add(this.listView1);
			this.Controls.Add(this.statusBar1);
			this.Menu = this.mainMenu1;
			this.Name = "Form1";
			this.Text = "Managed Media Foundation Metadata Viewer";
			this.ResumeLayout(false);
		}
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() 
        {
            Application.Run(new Form1());
        }


        #region AppDefinedMethods

        /// <summary>
        /// Creates a MediaSource, obtains metadata, and adds information to the ListView control.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        private void GetMetadata(string fileName)
        {
            System.Windows.Explorer.PropertyStore propertyStore;
            MediaSource mediaSource;
            PresentationDescriptor presDesc;
            System.Windows.Explorer.PropertyDescription propDescription;            
            string displayName;
            System.Windows.Explorer.PropertyKey[] keys;

            // Clear any previous data and put up an hourglass.
            listView1.Items.Clear();
            Cursor.Current = Cursors.WaitCursor;

            // Try to get metadata.
            try
            {
                mediaSource = new MediaSource(fileName);
                presDesc = mediaSource.CreatePresentationDescriptor();
                propertyStore = presDesc.Properties;
            }
            catch
            {
                MessageBox.Show("No metadata found.");
                return;
            }

            // Get the collection of keys and copy to a PropertyKey array.
            System.Collections.ICollection keyCollection = propertyStore.Keys;
            keys = new System.Windows.Explorer.PropertyKey[keyCollection.Count];
            keyCollection.CopyTo(keys, 0);

            // Iterate through the keys to obtain property descriptions and values.
            int x = -1;

            foreach (System.Windows.Explorer.PropertyKey k in keys)
            {
                x++;
                try
                {
                    try
                    {
                        propDescription = new System.Windows.Explorer.PropertyDescription(k);
                        displayName = propDescription.DisplayName;
                    }
                    catch
                    {
                        if (rbNamed.Checked == true)
                        {
                            // Display only properties that have friendly names.
                            continue;
                        }
                        else
                        {
                            displayName = "n/a";
                        }
                    }

                    // Display the name of the property.
                    ListViewItem lvItem = new ListViewItem(displayName);

                    // Display the data type.
                    lvItem.SubItems.Add(propertyStore[x].GetType().Name);

                    // Display the value.
                    lvItem.SubItems.Add(propertyStore.GetDisplayValue(k));

                    listView1.Items.Add(lvItem);
                }
                catch
                {
                    System.Diagnostics.Debug.WriteLine("Exception on retrieving item " + x);
                    continue;
                }
            }
            // Restore the cursor.
            Cursor.Current = Cursors.Default;
        }

        #endregion

        #region ControlHandlers

        private void menuExit_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        
        /// <summary>
        /// Open a file and get metadata.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuOpen_Click(object sender, System.EventArgs e)
        {
            string fileName;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog1.FileName;
                statusBar1.Text = fileName;
                GetMetadata(fileName);
                m_fileName = fileName;
            }
        }

        /// <summary>
        /// When radio button selection changes, update the ListView control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rb_CheckedChanged(object sender, System.EventArgs e)
        {
            if (m_fileName.Length > 0)
            {
                GetMetadata(m_fileName);
            }
        }
    }
}
