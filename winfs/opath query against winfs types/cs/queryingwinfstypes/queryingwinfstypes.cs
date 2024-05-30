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
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Storage;
using System.Reflection;
using System.Text;
using System.Runtime.InteropServices;

// Enumerate Contents of My Contacts Folder - Shows basic folder enumeration.
// Enumerate Contents of Party Guests List Folder - Shows folder enumeration using links and navigating by name.
// Enumerate Members of Household - Shows enumerating a household (very simular to enumearing a folder).
// Add Person to Party Guests List Folder 
// Remove Person from Party Guests List Folder
// Create new Person
// Add Person to Household
// Remove Person from Household
// Find Person by Name - Shows simple query.
// Find Pictures of People in Party Guests List Folder - Shows complex query.
[assembly: ComVisible(false)]
[assembly: CLSCompliant(true)]
namespace Microsoft.Samples.WinFS
{
    public class OPathBuilderForm : System.Windows.Forms.Form
    {
        private System.ComponentModel.Container components = null;

        private StringBuilder sbText = new StringBuilder();
        private bool update = true;
        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.TextBox queryText;
        private System.Data.DataSet returnedData;
        private System.Windows.Forms.DataGrid displayGrid;
        private System.Data.DataTable returnedTable;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.StatusBar statusBar;
        private System.Windows.Forms.StatusBarPanel sbpResultCount;
        private System.Windows.Forms.StatusBarPanel sbpStatus;
        private System.Windows.Forms.TabPage tabPageGrid;
        private System.Windows.Forms.TabPage tabPageText;
		private System.Windows.Forms.TextBox textBox1;

		private System.Windows.Forms.ComboBox typeText;
        private System.Windows.Forms.StatusBarPanel sbpEmpty;

        public OPathBuilderForm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }

            base.Dispose(disposing);
        }

		#region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.runButton = new System.Windows.Forms.Button();
			this.queryText = new System.Windows.Forms.TextBox();
			this.returnedData = new System.Data.DataSet();
			this.returnedTable = new System.Data.DataTable();
			this.displayGrid = new System.Windows.Forms.DataGrid();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPageGrid = new System.Windows.Forms.TabPage();
			this.tabPageText = new System.Windows.Forms.TabPage();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.statusBar = new System.Windows.Forms.StatusBar();
			this.sbpStatus = new System.Windows.Forms.StatusBarPanel();
			this.sbpResultCount = new System.Windows.Forms.StatusBarPanel();
			this.sbpEmpty = new System.Windows.Forms.StatusBarPanel();
			this.typeText = new System.Windows.Forms.ComboBox();
			((System.ComponentModel.ISupportInitialize)(this.returnedData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.returnedTable)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.displayGrid)).BeginInit();
			this.tabControl.SuspendLayout();
			this.tabPageGrid.SuspendLayout();
			this.tabPageText.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.sbpStatus)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.sbpResultCount)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.sbpEmpty)).BeginInit();
			this.SuspendLayout();

			// 
			// runButton
			// 
			this.runButton.Location = new System.Drawing.Point(792, 16);
			this.runButton.Name = "runButton";
			this.runButton.Size = new System.Drawing.Size(112, 24);
			this.runButton.TabIndex = 2;
			this.runButton.Text = "Run OPath Query";
			this.runButton.Click += new System.EventHandler(this.btnRun_Click);

			// 
			// queryText
			// 
			this.queryText.Location = new System.Drawing.Point(344, 8);
			this.queryText.Multiline = true;
			this.queryText.Name = "queryText";
			this.queryText.Size = new System.Drawing.Size(440, 40);
			this.queryText.TabIndex = 1;
			this.queryText.Text = "Source(System.Storage.Contacts.GroupMembership).Group.DisplayName = 'Document Control'";

			// 
			// returnedData
			// 
			this.returnedData.DataSetName = "NewDataSet";
			this.returnedData.Locale = new System.Globalization.CultureInfo("en-US");
			this.returnedData.Tables.AddRange(new System.Data.DataTable[] {
				this.returnedTable
			});

			// 
			// returnedTable
			// 
			this.returnedTable.TableName = "Table1";

			// 
			// displayGrid
			// 
			this.displayGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
			this.displayGrid.DataMember = "Table1";
			this.displayGrid.DataSource = this.returnedData;
			this.displayGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.displayGrid.Location = new System.Drawing.Point(0, 0);
			this.displayGrid.Name = "displayGrid";
			this.displayGrid.ReadOnly = true;
			this.displayGrid.RowHeaderWidth = 45;
			this.displayGrid.Size = new System.Drawing.Size(896, 537);
			this.displayGrid.TabIndex = 0;

			// 
			// tabControl
			// 
			this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl.Controls.Add(this.tabPageGrid);
			this.tabControl.Controls.Add(this.tabPageText);
			this.tabControl.Location = new System.Drawing.Point(8, 56);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(904, 560);
			this.tabControl.TabIndex = 3;

			// 
			// tabPageGrid
			// 
			this.tabPageGrid.Controls.Add(this.displayGrid);
			this.tabPageGrid.Location = new System.Drawing.Point(4, 24);
			this.tabPageGrid.Name = "tabPageGrid";
			this.tabPageGrid.Size = new System.Drawing.Size(896, 532);
			this.tabPageGrid.TabIndex = 0;
			this.tabPageGrid.Text = "Grid";

			// 
			// tabPageText
			// 
			this.tabPageText.Controls.Add(this.textBox1);
			this.tabPageText.Location = new System.Drawing.Point(4, 22);
			this.tabPageText.Name = "tabPageText";
			this.tabPageText.Size = new System.Drawing.Size(896, 534);
			this.tabPageText.TabIndex = 1;
			this.tabPageText.Text = "Text";

			// 
			// textBox1
			// 
			this.textBox1.AcceptsReturn = true;
			this.textBox1.AcceptsTab = true;
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.AutoSize = false;
			this.textBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.textBox1.Location = new System.Drawing.Point(0, 0);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBox1.Size = new System.Drawing.Size(896, 538);
			this.textBox1.TabIndex = 2;
			this.textBox1.WordWrap = false;

			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(40, 23);
			this.label1.TabIndex = 12;
			this.label1.Text = "Type: ";

			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(264, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(80, 23);
			this.label2.TabIndex = 12;
			this.label2.Text = "OPath Query: ";

			// 
			// statusBar
			// 
			this.statusBar.Location = new System.Drawing.Point(0, 616);
			this.statusBar.Name = "statusBar";
			this.statusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
				this.sbpStatus, this.sbpResultCount, this.sbpEmpty
			});
			this.statusBar.ShowPanels = true;
			this.statusBar.Size = new System.Drawing.Size(920, 22);
			this.statusBar.TabIndex = 13;

			// 
			// sbpStatus
			// 
			this.sbpStatus.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			this.sbpStatus.Name = "sbpStatus";
			this.sbpStatus.Text = "Ready";
			this.sbpStatus.Width = 774;

			// 
			// sbpResultCount
			// 
			this.sbpResultCount.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
			this.sbpResultCount.MinWidth = 30;
			this.sbpResultCount.Name = "sbpResultCount";
			this.sbpResultCount.Width = 30;

			// 
			// sbpEmpty
			// 
			this.sbpEmpty.Name = "sbpEmpty";

			// 
			// typeText
			// 
			this.typeText.Items.AddRange(new object[] {
				"System.Storage.Contacts.Person", "System.Storage.Contacts.Group", "System.Storage.Contacts.Household", "System.Storage.Contacts.Organization"
			});
			this.typeText.Location = new System.Drawing.Point(51, 16);
			this.typeText.Name = "typeText";
			this.typeText.Size = new System.Drawing.Size(208, 23);
			this.typeText.TabIndex = 14;
			this.typeText.Text = "System.Storage.Contacts.Person";
			this.typeText.TextChanged += new System.EventHandler(this.typeText_SelectedIndexChanged);

			// 
			// OPathBuilderForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(920, 638);
			this.Controls.Add(this.typeText);
			this.Controls.Add(this.statusBar);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.queryText);
			this.Controls.Add(this.runButton);
			this.Controls.Add(this.label2);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "OPathBuilderForm";
			this.Text = "Querying \'WinFS\' Types";
			((System.ComponentModel.ISupportInitialize)(this.returnedData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.returnedTable)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.displayGrid)).EndInit();
			this.tabControl.ResumeLayout(false);
			this.tabPageGrid.ResumeLayout(false);
			this.tabPageText.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.sbpStatus)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.sbpResultCount)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.sbpEmpty)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		#endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new OPathBuilderForm());
        }

        private void BeginUpdate()
        {
            update = false;
            returnedData.Tables[0].BeginLoadData();
        }

        private void EndUpdate()
        {
            update = true;
            returnedData.Tables[0].EndLoadData();
            textBox1.Text += sbText.ToString();
        }

        private void Output(string format, params object[] args)
        {
            if (update)
            {
				textBox1.Text += "\r\n" + String.Format(System.Globalization.CultureInfo.CurrentUICulture, format, args);
			}
            else
            {
                sbText.AppendFormat(((sbText.Length > 0) ? "\r\n" : "") + format, args);
            }
        }

        private void ClearOutput()
        {
            textBox1.Clear();
            returnedData.Clear();
            returnedData.Tables[0].Columns.Clear();
            sbText.Length = 0;
        }

        private void DoQuery(string szQuery)
        {
            sbpStatus.Text = "Executing Query...";
            this.Cursor = Cursors.WaitCursor;

            int cRows = 0;
            ClearOutput();
            BeginUpdate();
            try
            {
                string typeName = typeText.Text;
                string filter = szQuery;
				string assemblyName = typeName.Substring(0, typeName.LastIndexOf("."));
				Type type = Assembly.LoadWithPartialName(assemblyName).GetType(typeName, true, true);
                PropertyInfo[] props = type.GetProperties();
                ItemContext context = ItemContext.Open();

                Output("<results type=\"{0}\" filter=\"{1}\">", type.FullName, filter);

				ItemSearcher searcher=context.GetSearcher(type);
				if( filter.Length > 0 )
					searcher.Filters.Add(filter);
				FindResult results = searcher.FindAll();

                foreach (Item item in results)
                {
                    Output("  <{0}>", type.FullName);

                    // Make sure all the columns exist
                    foreach (PropertyInfo prop in props)
                    {
                        if (!returnedData.Tables[0].Columns.Contains(prop.Name))
                        {
                            returnedData.Tables[0].Columns.Add(prop.Name);
                        }
                    }

                    DataRow workRow = returnedData.Tables[0].NewRow();

                    foreach (PropertyInfo prop in props)
                    {
						try
						{
							workRow[prop.Name] = prop.GetValue(item, new object[]{});
						}
						catch { }
                    }

                    foreach (PropertyInfo prop in props)
                    {
						try
						{
							Output("    <{0}>{1}</{0}>", prop.Name, prop.GetValue(item, new object[]{}));
						}
						catch { }
                    }

                    Output("  </{0}>", type.FullName);

                    returnedData.Tables[0].Rows.Add(workRow);
                    cRows++;
                }

                Output("</results>");
            }
            catch (Exception e)
            {
                do
                {
                    Output("");
                    Output("Exception {0}:", e.GetType().FullName);
                    Output(e.Message.Replace("\n", "\r\n"));
                    Output(e.StackTrace);
                    tabControl.SelectedTab = tabPageText;
                } while ((e = e.InnerException) != null);
            }
            finally
            {
                EndUpdate();
                displayGrid.Update();
				sbpResultCount.Text = cRows.ToString(System.Globalization.CultureInfo.CurrentUICulture) + " rows";
				sbpStatus.Text = "Query completed.";
                this.Cursor = Cursors.Default;
            }
        }

        private void btnRun_Click(System.Object sender, System.EventArgs e)
        {
            DoQuery(queryText.Text);
		}


		private void typeText_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			switch(typeText.Text)
			{
				case "System.Storage.Contacts.Person":
					queryText.Text = "Source(System.Storage.Contacts.GroupMembership).Group.DisplayName = 'Document Control'";
					break;
				case "System.Storage.Contacts.Organization" :
					queryText.Text = "DisplayName like 'M%'";
					break;
				case "System.Storage.Contacts.Household" :
					queryText.Text = "DisplayName = 'Michael''s Friends'";
					break;
				case "System.Storage.Contacts.Group" :
					queryText.Text = "Target(System.Storage.Contacts.GroupMembership).Members.DisplayName like 'Sean %'";
					break;
				default:
					queryText.Text = "";
					break;
			}
		}
    }
}

