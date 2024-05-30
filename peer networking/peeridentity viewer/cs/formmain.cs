//=====================================================================
// File:      FormMain.cs
//
// Summary:   The main form with which Peer-to-Peer identities can be managed.
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
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Net.PeerToPeer;
using System.Windows.Forms;

namespace IdentityViewer
{
    /// <summary>
    /// The main form with which Peer-to-Peer identities can be managed.
    /// </summary>
    public class FormMain : System.Windows.Forms.Form
    {
        private System.Windows.Forms.MainMenu mainMenu;
        private System.Windows.Forms.MenuItem menuImport;
        private System.Windows.Forms.MenuItem menuExport;
        private System.Windows.Forms.MenuItem menuExit;
        private System.Windows.Forms.MenuItem menuHelp;
        private System.Windows.Forms.MenuItem menuAbout;
        private System.Windows.Forms.ListView listViewMain;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ContextMenu popupMenu;
        private System.Windows.Forms.MenuItem popupProperties;
        private System.Windows.Forms.MenuItem popupCreate;
        private System.Windows.Forms.MenuItem popupDelete;
        private System.Windows.Forms.MenuItem menuIdentityCreate;
        private System.Windows.Forms.MenuItem menuIdentityDelete;
        private System.Windows.Forms.MenuItem menuIdentity;
        private System.Windows.Forms.MenuItem menuFile;
        private System.Windows.Forms.MenuItem menuView;
        private System.Windows.Forms.MenuItem menuViewRefresh;
        private System.Windows.Forms.MenuItem menuProperties;
        private System.Windows.Forms.ColumnHeader colClassifier;
        private System.Windows.Forms.ColumnHeader colAuthority;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.MenuItem popupExport;
        private System.Windows.Forms.MenuItem menuSaveIdentityInfo;
        private System.Windows.Forms.MenuItem popupSaveIndentityInfo;
        private System.Windows.Forms.MenuItem menuSeparator4;
        private System.Windows.Forms.MenuItem menuSeparator5;
        private System.Windows.Forms.MenuItem menuSeparator1;
        private System.Windows.Forms.MenuItem menuSeparator2;
        private System.Windows.Forms.MenuItem menuSeparator3;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;


        /// <summary>
        /// Constructor to create the main form.
        /// </summary>
        public FormMain()
        {
            InitializeComponent();

            CmdRefreshView();
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

            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormMain));

            this.listViewMain = new System.Windows.Forms.ListView();
            this.colName = new System.Windows.Forms.ColumnHeader();
            this.colClassifier = new System.Windows.Forms.ColumnHeader();
            this.colAuthority = new System.Windows.Forms.ColumnHeader();
            this.popupMenu = new System.Windows.Forms.ContextMenu();
            this.popupProperties = new System.Windows.Forms.MenuItem();
            this.menuSeparator4 = new System.Windows.Forms.MenuItem();
            this.popupCreate = new System.Windows.Forms.MenuItem();
            this.popupDelete = new System.Windows.Forms.MenuItem();
            this.menuSeparator5 = new System.Windows.Forms.MenuItem();
            this.popupExport = new System.Windows.Forms.MenuItem();
            this.popupSaveIndentityInfo = new System.Windows.Forms.MenuItem();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.menuFile = new System.Windows.Forms.MenuItem();
            this.menuImport = new System.Windows.Forms.MenuItem();
            this.menuExport = new System.Windows.Forms.MenuItem();
            this.menuSeparator1 = new System.Windows.Forms.MenuItem();
            this.menuSaveIdentityInfo = new System.Windows.Forms.MenuItem();
            this.menuSeparator2 = new System.Windows.Forms.MenuItem();
            this.menuExit = new System.Windows.Forms.MenuItem();
            this.menuIdentity = new System.Windows.Forms.MenuItem();
            this.menuIdentityCreate = new System.Windows.Forms.MenuItem();
            this.menuIdentityDelete = new System.Windows.Forms.MenuItem();
            this.menuView = new System.Windows.Forms.MenuItem();
            this.menuViewRefresh = new System.Windows.Forms.MenuItem();
            this.menuSeparator3 = new System.Windows.Forms.MenuItem();
            this.menuProperties = new System.Windows.Forms.MenuItem();
            this.menuHelp = new System.Windows.Forms.MenuItem();
            this.menuAbout = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();

            // 
            // listViewMain
            // 
            this.listViewMain.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                this.colName, this.colClassifier, this.colAuthority
            });
            this.listViewMain.ContextMenu = this.popupMenu;
            this.listViewMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewMain.FullRowSelect = true;
            this.listViewMain.HideSelection = false;
            this.listViewMain.Location = new System.Drawing.Point(0, 0);
            this.listViewMain.Name = "listViewMain";
            this.listViewMain.Size = new System.Drawing.Size(494, 217);
            this.listViewMain.SmallImageList = this.imageList;
            this.listViewMain.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewMain.TabIndex = 0;
            this.listViewMain.View = System.Windows.Forms.View.Details;
            this.listViewMain.DoubleClick += new System.EventHandler(this.listViewMain_DoubleClick);

            // 
            // colName
            // 
            this.colName.Text = "Name";
            this.colName.Width = 100;

            // 
            // colClassifier
            // 
            this.colClassifier.Text = "Classifier";
            this.colClassifier.Width = 110;

            // 
            // colAuthority
            // 
            this.colAuthority.Text = "Authority";
            this.colAuthority.Width = 250;

            // 
            // popupMenu
            // 
            this.popupMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                this.popupProperties, this.menuSeparator4, this.popupCreate, this.popupDelete, this.menuSeparator5, this.popupExport, this.popupSaveIndentityInfo
            });
            this.popupMenu.Name = "popupMenu";
            this.popupMenu.Popup += new System.EventHandler(this.popupMenu_Popup);

            // 
            // popupProperties
            // 
            this.popupProperties.DefaultItem = true;
            this.popupProperties.Index = 0;
            this.popupProperties.Name = "popupProperties";
            this.popupProperties.Text = "&Properties";
            this.popupProperties.Click += new System.EventHandler(this.popupProperties_Click);

            // 
            // menuSeparator4
            // 
            this.menuSeparator4.Index = 1;
            this.menuSeparator4.Name = "menuSep3";
            this.menuSeparator4.Text = "-";

            // 
            // popupCreate
            // 
            this.popupCreate.Index = 2;
            this.popupCreate.Name = "popupCreate";
            this.popupCreate.Text = "&Create Identity...";
            this.popupCreate.Click += new System.EventHandler(this.popupCreate_Click);

            // 
            // popupDelete
            // 
            this.popupDelete.Index = 3;
            this.popupDelete.Name = "popupDelete";
            this.popupDelete.Text = "&Delete Identity...";
            this.popupDelete.Click += new System.EventHandler(this.popupDelete_Click);

            // 
            // menuSeparator5
            // 
            this.menuSeparator5.Index = 4;
            this.menuSeparator5.Name = "menuItem2";
            this.menuSeparator5.Text = "-";

            // 
            // popupExport
            // 
            this.popupExport.Index = 5;
            this.popupExport.Name = "popupExport";
            this.popupExport.Text = "&Export Identity...";
            this.popupExport.Click += new System.EventHandler(this.popupExport_Click);

            // 
            // popupSaveIndentityInfo
            // 
            this.popupSaveIndentityInfo.Index = 6;
            this.popupSaveIndentityInfo.Name = "menuItem3";
            this.popupSaveIndentityInfo.Text = "&Save Identity Info...";
            this.popupSaveIndentityInfo.Click += new System.EventHandler(this.popupSaveIndentityInfo_Click);

            // 
            // imageList
            // 
            this.imageList.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Magenta;
            this.imageList.Images.SetKeyName(0, "ImgIdentity.bmp");

            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                this.menuFile, this.menuIdentity, this.menuView, this.menuHelp
            });
            this.mainMenu.Name = "mainMenu";

            // 
            // menuFile
            // 
            this.menuFile.Index = 0;
            this.menuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                this.menuImport, this.menuExport, this.menuSeparator1, this.menuSaveIdentityInfo, this.menuSeparator2, this.menuExit
            });
            this.menuFile.Name = "menuFile";
            this.menuFile.ShowShortcut = false;
            this.menuFile.Text = "&File";

            // 
            // menuImport
            // 
            this.menuImport.Index = 0;
            this.menuImport.Name = "menuImport";
            this.menuImport.Shortcut = System.Windows.Forms.Shortcut.CtrlI;
            this.menuImport.ShowShortcut = false;
            this.menuImport.Text = "&Import Identity...";
            this.menuImport.Click += new System.EventHandler(this.menuImport_Click);

            // 
            // menuExport
            // 
            this.menuExport.Index = 1;
            this.menuExport.Name = "menuExport";
            this.menuExport.Shortcut = System.Windows.Forms.Shortcut.CtrlE;
            this.menuExport.ShowShortcut = false;
            this.menuExport.Text = "&Export Identity...";
            this.menuExport.Click += new System.EventHandler(this.menuExport_Click);

            // 
            // menuSeparator1
            // 
            this.menuSeparator1.Index = 2;
            this.menuSeparator1.Name = "menuSep1";
            this.menuSeparator1.Text = "-";

            // 
            // menuSaveIdentityInfo
            // 
            this.menuSaveIdentityInfo.Index = 3;
            this.menuSaveIdentityInfo.Name = "menuItem3";
            this.menuSaveIdentityInfo.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
            this.menuSaveIdentityInfo.ShowShortcut = false;
            this.menuSaveIdentityInfo.Text = "&Save Identity Info...";
            this.menuSaveIdentityInfo.Click += new System.EventHandler(this.menuSaveIdentityInfo_Click);

            // 
            // menuSeparator2
            // 
            this.menuSeparator2.Index = 4;
            this.menuSeparator2.Name = "menuItem3";
            this.menuSeparator2.Text = "-";

            // 
            // menuExit
            // 
            this.menuExit.Index = 5;
            this.menuExit.Name = "menuExit";
            this.menuExit.Shortcut = System.Windows.Forms.Shortcut.CtrlQ;
            this.menuExit.ShowShortcut = false;
            this.menuExit.Text = "E&xit";
            this.menuExit.Click += new System.EventHandler(this.menuExit_Click);

            // 
            // menuIdentity
            // 
            this.menuIdentity.Index = 1;
            this.menuIdentity.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                this.menuIdentityCreate, this.menuIdentityDelete
            });
            this.menuIdentity.Name = "menuIdentity";
            this.menuIdentity.Text = "&Identity";
            this.menuIdentity.Popup += new System.EventHandler(this.menuIdentity_Popup);

            // 
            // menuIdentityCreate
            // 
            this.menuIdentityCreate.Index = 0;
            this.menuIdentityCreate.Name = "menuIdentityCreate";
            this.menuIdentityCreate.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
            this.menuIdentityCreate.ShowShortcut = false;
            this.menuIdentityCreate.Text = "&Create...";
            this.menuIdentityCreate.Click += new System.EventHandler(this.menuIdentityCreate_Click);

            // 
            // menuIdentityDelete
            // 
            this.menuIdentityDelete.Index = 1;
            this.menuIdentityDelete.Name = "menuIdentityDelete";
            this.menuIdentityDelete.Shortcut = System.Windows.Forms.Shortcut.Del;
            this.menuIdentityDelete.ShowShortcut = false;
            this.menuIdentityDelete.Text = "&Delete";
            this.menuIdentityDelete.Click += new System.EventHandler(this.menuIdentityDelete_Click);

            // 
            // menuView
            // 
            this.menuView.Index = 2;
            this.menuView.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                this.menuViewRefresh, this.menuSeparator3, this.menuProperties
            });
            this.menuView.Name = "menuView";
            this.menuView.Text = "&View";
            this.menuView.Popup += new System.EventHandler(this.menuView_Popup);

            // 
            // menuViewRefresh
            // 
            this.menuViewRefresh.Index = 0;
            this.menuViewRefresh.Name = "menuViewRefresh";
            this.menuViewRefresh.Shortcut = System.Windows.Forms.Shortcut.F5;
            this.menuViewRefresh.Text = "&Refresh";
            this.menuViewRefresh.Click += new System.EventHandler(this.menuViewRefresh_Click);

            // 
            // menuSeparator3
            // 
            this.menuSeparator3.Index = 1;
            this.menuSeparator3.Name = "menuItem3";
            this.menuSeparator3.Text = "-";

            // 
            // menuProperties
            // 
            this.menuProperties.Index = 2;
            this.menuProperties.Name = "menuProperties";
            this.menuProperties.ShowShortcut = false;
            this.menuProperties.Text = "&Properties...";
            this.menuProperties.Click += new System.EventHandler(this.menuProperties_Click);

            // 
            // menuHelp
            // 
            this.menuHelp.Index = 3;
            this.menuHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                this.menuAbout
            });
            this.menuHelp.Name = "menuHelp";
            this.menuHelp.Text = "&Help";

            // 
            // menuAbout
            // 
            this.menuAbout.Index = 0;
            this.menuAbout.Name = "menuAbout";
            this.menuAbout.Shortcut = System.Windows.Forms.Shortcut.F1;
            this.menuAbout.ShowShortcut = false;
            this.menuAbout.Text = "&About";
            this.menuAbout.Click += new System.EventHandler(this.menuAbout_Click);

            // 
            // FormMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(494, 217);
            this.Controls.Add(this.listViewMain);
            this.Menu = this.mainMenu;
            this.Name = "FormMain";
            this.Text = "Identity Viewer";
            this.ResumeLayout(false);
        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new FormMain());
        }

        #region Menus

        private void menuImport_Click(object sender, System.EventArgs e)
        {
            CmdImport();
        }
        private void menuExport_Click(object sender, System.EventArgs e)
        {
            CmdExport();
        }
        private void menuSaveIdentityInfo_Click(object sender, System.EventArgs e)
        {
            CmdSaveIdentityInfo();
        }
        private void menuExit_Click(object sender, System.EventArgs e)
        {
            Close();
        }
        private void menuIdentityCreate_Click(object sender, System.EventArgs e)
        {
            CmdCreate();
        }
        private void menuIdentityDelete_Click(object sender, System.EventArgs e)
        {
            CmdDelete();
        }
        private void menuProperties_Click(object sender, System.EventArgs e)
        {
            CmdProperties();
        }
        private void menuViewRefresh_Click(object sender, System.EventArgs e)
        {
            CmdRefreshView();
        }
        private void menuAbout_Click(object sender, System.EventArgs e)
        {
            CmdAbout();
        }
        private void popupProperties_Click(object sender, System.EventArgs e)
        {
            CmdProperties();
        }
        private void popupCreate_Click(object sender, System.EventArgs e)
        {
            CmdCreate();
        }
        private void popupDelete_Click(object sender, System.EventArgs e)
        {
            CmdDelete();
        }
        private void popupExport_Click(object sender, System.EventArgs e)
        {
            CmdExport();
        }
        private void popupSaveIndentityInfo_Click(object sender, System.EventArgs e)
        {
            CmdSaveIdentityInfo();
        }

        #endregion // Menus

        #region Events

        private void menuIdentity_Popup(object sender, System.EventArgs e)
        {
            menuIdentityDelete.Enabled = IsAtLeastOneSelected;
        }
        
        private void menuView_Popup(object sender, System.EventArgs e)
        {
            menuProperties.Enabled = IsAtLeastOneSelected;
        }

        private void popupMenu_Popup(object sender, System.EventArgs e)
        {
            popupDelete.Enabled = IsAtLeastOneSelected;
            popupProperties.Enabled = IsAtLeastOneSelected;
        }

        private void listViewMain_DoubleClick(object sender, System.EventArgs e)
        {
            CmdProperties();
        }

        #endregion // Events

        #region Utility Routines

        /// <summary>
        /// Return the PeerIdentity based on the ListViewItem
        /// </summary>
        /// <param name="lvi"></param>
        /// <returns></returns>
        private PeerIdentity IdentityFromLvi(ListViewItem lvi)
        {
            return (PeerIdentity)lvi.Tag;
        }

        /// <summary>
        /// The currently selected identity in the list.
        /// </summary>
        public PeerIdentity SelectedIdentity
        {
            get
            {
                // Return the first selected PeerIdentity or null if there is no selection.
                PeerIdentity identity = null;
                if (listViewMain.SelectedItems.Count > 0)
                {
                    identity = IdentityFromLvi(listViewMain.SelectedItems[0]);
                }
                return identity;
            }

            set
            {
                // Try to find the identity in the listview and select it.
                PeerIdentity identity = value;
                foreach (ListViewItem lvi in listViewMain.Items)
                {
                    // Compare PeerIdentity objects using .Equals instead of ==
                    // so the values are compared, not the object instances.
                    if (identity.Equals(IdentityFromLvi(lvi)))
                    {
                        lvi.Selected = true;
                        break;
                    }
                }
            }
        }
        
        /// <summary>
        /// True if at least one line in the list view is selected.
        /// </summary>
        /// <value></value>
        public Boolean IsAtLeastOneSelected
        {
            get
            {
                return (listViewMain.SelectedItems.Count >= 1);
            }
        }
        
        /// <summary>
        /// Create a new ListViewItem for the PeerIdentity.
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public ListViewItem CreateListViewItemForIdentity(PeerIdentity identity)
        {
            ListViewItem lvi = new ListViewItem(identity.FriendlyName, 0);
            lvi.Tag = identity;

            lvi.SubItems.Add(identity.PeerName.Classifier);
            lvi.SubItems.Add(identity.PeerName.Authority);

            return lvi;
        }

        #endregion // Utility Routines

        #region Commands

        /// <summary>
        /// Refresh the view by retrieving all of the identities and
        /// adding them to the list.
        /// </summary>
        public void CmdRefreshView()
        {
            // This may take awhile, so change the cursor
            Cursor cursorSave = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            try
            {
                listViewMain.Items.Clear();

                List<PeerIdentity> identities = PeerIdentity.GetIdentities();

                foreach (PeerIdentity identity in identities)
                {
                    ListViewItem lvi = CreateListViewItemForIdentity(identity);

                    listViewMain.Items.Add(lvi);
                }
            }
            finally
            {
                // Restore the cursor
                this.Cursor = cursorSave;
            }
        }

        /// <summary>
        /// Display the properties for each of the selected identities.
        /// </summary>
        public void CmdProperties()
        {
            foreach (ListViewItem lvi in listViewMain.SelectedItems)
            {
                PeerIdentity identity = IdentityFromLvi(lvi);
                FormIdentity form = new FormIdentity(identity);

                form.Show();
            }
        }

        /// <summary>
        /// Show the dialog to create a new identity
        /// </summary>
        public void CmdCreate()
        {
            DlgNewIdentity dlg = new DlgNewIdentity();

            if (DialogResult.OK == dlg.ShowDialog(this))
            {
                CmdRefreshView();
                SelectedIdentity = dlg.Identity;
            }
        }

        /// <summary>
        /// Delete the selected identities.
        /// </summary>
        public void CmdDelete()
        {
            // Confirm the user want to delete the selected item(s)
            if (listViewMain.SelectedItems.Count == 1)
            {
                PeerIdentity identity = IdentityFromLvi(listViewMain.SelectedItems[0]);
                if (DialogResult.OK != MessageBox.Show(this,
                    "Are you sure you want to delete the Identity\r\n'" + identity.FriendlyName + "'?",
                    "Delete Identity", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {
                    return;
                }
            }
            else
            {
                if (DialogResult.OK != MessageBox.Show(this,
                    "Are you sure you want to delete the " + listViewMain.SelectedItems.Count.ToString() + " selected identities?",
                    "Delete Identities", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {
                    return;
                }
            }

            foreach (ListViewItem lvi in listViewMain.SelectedItems)
            {
                try
                {
                    PeerIdentity identity = IdentityFromLvi(lvi);

                    identity.Delete();
                }
                catch (System.Net.PeerToPeer.PeerNotFoundException)
                {
                    // The identity has already been deleted - ignore this error
                }
                catch (Exception e)
                {
                    Utilities.DisplayException(e, this);
                }
            }
            CmdRefreshView();
        }

        /// <summary>
        /// Show some version information about this application.
        /// </summary>
        public void CmdAbout()
        {
            MessageBox.Show(this, "Version 1.0", "About IdentityViewer",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Show the dialog to import an identity.
        /// </summary>
        public void CmdImport()
        {
            DlgImportIdentity dlg = new DlgImportIdentity();
            if (DialogResult.OK == dlg.ShowDialog(this))
            {
                CmdRefreshView();
                SelectedIdentity = dlg.Identity;
            }
        }

        /// <summary>
        /// Show the dialog to export an identity.
        /// </summary>
        public void CmdExport()
        {
            DlgExportIdentity dlg = new DlgExportIdentity(SelectedIdentity);
            dlg.ShowDialog(this);
        }

        /// <summary>
        /// Show the dialog to save the public information for an identity.
        /// </summary>
        public void CmdSaveIdentityInfo()
        {
            DlgSaveIdentityInfo dlg = new DlgSaveIdentityInfo(SelectedIdentity);
            dlg.ShowDialog(this);
        }

        #endregion // Commands
    }
}
