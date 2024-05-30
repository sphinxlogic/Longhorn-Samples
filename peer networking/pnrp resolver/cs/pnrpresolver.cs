//=====================================================================
// File:      PnrpResolver.cs
//
// Summary:   Sample application to resolve a PeerName.
//
//---------------------------------------------------------------------
// This file is part of the Microsoft .NET Framework SDK Code Samples.
// 
// Copyright (C) Microsoft Corporation.  All rights reserved.
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

using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Net;
using System.Net.PeerToPeer;
using System.Windows.Forms;

namespace PnrpResolver
{
	/// <summary>
	/// Sample application to resolve for PeerNames.
	/// </summary>
	public class FormMain : System.Windows.Forms.Form
    {
        private PnrpEndPointResolver resolver;

        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Label labelPeerName;
        private System.Windows.Forms.TextBox textBoxPeerName;
        private System.Windows.Forms.Label labelCloud;
        private System.Windows.Forms.ComboBox dropListCloud;
        private System.Windows.Forms.Label labelMaxTime;
        private System.Windows.Forms.NumericUpDown textBoxTimeout;
        private System.Windows.Forms.Label labelMaxResults;
        private System.Windows.Forms.NumericUpDown textBoxMaxResults;
        private System.Windows.Forms.Label labelCriteria;
        private System.Windows.Forms.ComboBox dropListCriteria;
        private System.Windows.Forms.Button buttonResolve;
        private System.Windows.Forms.TextBox textBoxResults;

		private System.ComponentModel.Container components = null;

        /// <summary>
        /// Constructor for this form.
        /// </summary>
		public FormMain()
        {		    
		    InitializeComponent();

            // Initialize the lists.
		    InitializeCloudList();
		    InitializeCriteria();
		    InitializeResolver();
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
            this.panel = new System.Windows.Forms.Panel();
            this.dropListCriteria = new System.Windows.Forms.ComboBox();
            this.labelCriteria = new System.Windows.Forms.Label();
            this.labelMaxTime = new System.Windows.Forms.Label();
            this.dropListCloud = new System.Windows.Forms.ComboBox();
            this.labelPeerName = new System.Windows.Forms.Label();
            this.textBoxPeerName = new System.Windows.Forms.TextBox();
            this.labelMaxResults = new System.Windows.Forms.Label();
            this.textBoxMaxResults = new System.Windows.Forms.NumericUpDown();
            this.textBoxTimeout = new System.Windows.Forms.NumericUpDown();
            this.labelCloud = new System.Windows.Forms.Label();
            this.buttonResolve = new System.Windows.Forms.Button();
            this.textBoxResults = new System.Windows.Forms.TextBox();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxMaxResults)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxTimeout)).BeginInit();
            this.SuspendLayout();

            // 
            // panel
            // 
            this.panel.Controls.Add(this.dropListCriteria);
            this.panel.Controls.Add(this.labelCriteria);
            this.panel.Controls.Add(this.labelMaxTime);
            this.panel.Controls.Add(this.dropListCloud);
            this.panel.Controls.Add(this.labelPeerName);
            this.panel.Controls.Add(this.textBoxPeerName);
            this.panel.Controls.Add(this.labelMaxResults);
            this.panel.Controls.Add(this.textBoxMaxResults);
            this.panel.Controls.Add(this.textBoxTimeout);
            this.panel.Controls.Add(this.labelCloud);
            this.panel.Controls.Add(this.buttonResolve);
            this.panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(517, 111);
            this.panel.TabIndex = 0;

            // 
            // dropListCriteria
            // 
            this.dropListCriteria.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dropListCriteria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropListCriteria.Location = new System.Drawing.Point(76, 77);
            this.dropListCriteria.Name = "dropListCriteria";
            this.dropListCriteria.Size = new System.Drawing.Size(243, 21);
            this.dropListCriteria.Sorted = true;
            this.dropListCriteria.TabIndex = 5;

            // 
            // labelCriteria
            // 
            this.labelCriteria.Location = new System.Drawing.Point(10, 77);
            this.labelCriteria.Name = "labelCriteria";
            this.labelCriteria.Size = new System.Drawing.Size(54, 16);
            this.labelCriteria.TabIndex = 4;
            this.labelCriteria.Text = "Cr&iteria";

            // 
            // labelMaxTime
            // 
            this.labelMaxTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMaxTime.Location = new System.Drawing.Point(329, 46);
            this.labelMaxTime.Name = "labelMaxTime";
            this.labelMaxTime.Size = new System.Drawing.Size(94, 16);
            this.labelMaxTime.TabIndex = 8;
            this.labelMaxTime.Text = "Maximum &Time:";

            // 
            // dropListCloud
            // 
            this.dropListCloud.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dropListCloud.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropListCloud.Location = new System.Drawing.Point(76, 46);
            this.dropListCloud.Name = "dropListCloud";
            this.dropListCloud.Size = new System.Drawing.Size(243, 21);
            this.dropListCloud.Sorted = true;
            this.dropListCloud.TabIndex = 3;

            // 
            // labelPeerName
            // 
            this.labelPeerName.Location = new System.Drawing.Point(10, 20);
            this.labelPeerName.Name = "labelPeerName";
            this.labelPeerName.Size = new System.Drawing.Size(61, 16);
            this.labelPeerName.TabIndex = 0;
            this.labelPeerName.Text = "&PeerName:";

            // 
            // textBoxPeerName
            // 
            this.textBoxPeerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPeerName.Location = new System.Drawing.Point(76, 17);
            this.textBoxPeerName.MaxLength = 255;
            this.textBoxPeerName.Name = "textBoxPeerName";
            this.textBoxPeerName.Size = new System.Drawing.Size(243, 19);
            this.textBoxPeerName.TabIndex = 1;
            this.textBoxPeerName.Text = "0.0";

            // 
            // labelMaxResults
            // 
            this.labelMaxResults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMaxResults.Location = new System.Drawing.Point(329, 16);
            this.labelMaxResults.Name = "labelMaxResults";
            this.labelMaxResults.Size = new System.Drawing.Size(98, 16);
            this.labelMaxResults.TabIndex = 6;
            this.labelMaxResults.Text = "Maximum &Results:";

            // 
            // textBoxMaxResults
            // 
            this.textBoxMaxResults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMaxResults.Location = new System.Drawing.Point(429, 16);
            this.textBoxMaxResults.Maximum = new System.Decimal(new int[] {
                10000, 0, 0, 0
            });
            this.textBoxMaxResults.Minimum = new System.Decimal(new int[] {
                1, 0, 0, 0
            });
            this.textBoxMaxResults.Name = "textBoxMaxResults";
            this.textBoxMaxResults.Size = new System.Drawing.Size(75, 20);
            this.textBoxMaxResults.TabIndex = 7;
            this.textBoxMaxResults.Value = new System.Decimal(new int[] {
                999, 0, 0, 0
            });

            // 
            // textBoxTimeout
            // 
            this.textBoxTimeout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTimeout.Location = new System.Drawing.Point(429, 46);
            this.textBoxTimeout.Maximum = new System.Decimal(new int[] {
                600, 0, 0, 0
            });
            this.textBoxTimeout.Name = "textBoxTimeout";
            this.textBoxTimeout.Size = new System.Drawing.Size(75, 20);
            this.textBoxTimeout.TabIndex = 9;
            this.textBoxTimeout.Value = new System.Decimal(new int[] {
                30, 0, 0, 0
            });

            // 
            // labelCloud
            // 
            this.labelCloud.Location = new System.Drawing.Point(10, 46);
            this.labelCloud.Name = "labelCloud";
            this.labelCloud.Size = new System.Drawing.Size(38, 16);
            this.labelCloud.TabIndex = 2;
            this.labelCloud.Text = "&Cloud:";

            // 
            // buttonResolve
            // 
            this.buttonResolve.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonResolve.Location = new System.Drawing.Point(429, 77);
            this.buttonResolve.Name = "buttonResolve";
            this.buttonResolve.TabIndex = 10;
            this.buttonResolve.Text = "Resolve";
            this.buttonResolve.Click += new System.EventHandler(this.buttonResolve_Click);

            // 
            // textBoxResults
            // 
            this.textBoxResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxResults.Location = new System.Drawing.Point(0, 111);
            this.textBoxResults.Multiline = true;
            this.textBoxResults.Name = "textBoxResults";
            this.textBoxResults.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxResults.Size = new System.Drawing.Size(517, 200);
            this.textBoxResults.TabIndex = 1;
            this.textBoxResults.TabStop = false;

            // 
            // FormMain
            // 
            this.AcceptButton = this.buttonResolve;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(517, 311);
            this.Controls.Add(this.textBoxResults);
            this.Controls.Add(this.panel);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 200);
            this.Name = "FormMain";
            this.Text = "PNRP Resolver";
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxMaxResults)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxTimeout)).EndInit();
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
            Application.EnableVisualStyles();
            Application.Run(new FormMain());
        }

        /// <summary>
        /// Initialize the Cloud drop down list.
        /// </summary>
        private void InitializeCloudList()
        {
            List<Cloud> clouds = CloudWatcher.GetClouds();

            foreach (Cloud cloud in clouds)
            {
                dropListCloud.Items.Add(cloud);
            }

            // Always select the first one (Global)
            dropListCloud.SelectedIndex = 0;
        }

        /// <summary>
        /// Initialize the ResolutionCriteria drop down list.
        /// </summary>
        private void InitializeCriteria()
        {
            foreach (Object obj in Enum.GetValues(typeof(ResolutionCriteria)))
            {
                dropListCriteria.Items.Add(obj);
            }

            dropListCriteria.SelectedIndex = 0;
        }

        /// <summary>
        /// Initialize the global resolver object and associated callbacks.
        /// </summary>
        private void InitializeResolver()
        {
            resolver = new PnrpEndPointResolver();
            resolver.SynchronizingObject = this;
            resolver.PeerNameFound += new PeerNameFoundEventHandler(OnPeerNameFound);
            resolver.ResolutionCompleted += new ResolutionCompletedEventHandler(OnResolutionCompleted);
        }


        /// <summary>
        /// Retrieve the currently selected cloud.
        /// </summary>
        /// <value></value>
        public Cloud SelectedCloud
        {
            get
            {
                return (Cloud)dropListCloud.SelectedItem;
            }
        }

        /// <summary>
        /// Handle the button click to start or stop the resolution process.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonResolve_Click(object sender, System.EventArgs e)
        {
            if (resolver.Resolving)
            {
                StopResolve();
            }
            else
            {
                StartResolve();
            }

            UpdateControls();
        }

        /// <summary>
        /// Stop the resolve process.
        /// </summary>
        private void StopResolve()
        {
            if (resolver.Resolving)
            {
                resolver.EndResolve();
            }
        }

        /// <summary>
        /// Start the resolve process.
        /// </summary>
        private void StartResolve()
        {
            ClearMessages();

            try
            {
                // Retrieve the settings and fill out the resolver properties.
                PeerName peerName = new PeerName(textBoxPeerName.Text);
                resolver.PeerName = peerName;
                resolver.Cloud = SelectedCloud;
                resolver.MaxResults = (int) textBoxMaxResults.Value;
                resolver.TimeOut = new TimeSpan(0, 0, (int) textBoxTimeout.Value);
                resolver.ResolutionCriteria = (ResolutionCriteria)dropListCriteria.SelectedItem;

                DisplayMessage("Resolving for " + peerName.ToString());
                resolver.BeginResolve();
            }
            catch (Exception e)
            {
                // Display all exceptions.
                MessageBox.Show(this, e.Message + "\r\n\r\n" + e.StackTrace,
                    "Exception from " + e.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// Enable (or disable) the controls.
        /// </summary>
        private void UpdateControls()
        {
            // Disable the controls if we are resolving
            bool resolving = resolver.Resolving;

            foreach (Control control in panel.Controls)
            {
                if (!(control is Label))
                {
                    if (!resolving)
                    {
                        if (control.Tag is bool)
                        {
                            // Restore the previous state
                            control.Enabled = (bool)control.Tag;
                        }
                    }
                    else
                    {
                        // Save the previous state
                        control.Tag = control.Enabled;
                        control.Enabled = false;
                    }
                }
            }

            // Update the button (always enabled)
            buttonResolve.Text = resolving ? "Stop" : "Resolve";
            buttonResolve.Enabled = true;
        }

        /// <summary>
        /// Handle the event of finding a result.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPeerNameFound(object sender, PeerNameFoundEventArgs e)
        {
            DisplayMessage("");
            DisplayMessage("Found result with comment='" + e.PnrpEndPoint.Comment + "'");

            // Display each address
            IPEndPoint[] ipEndPoints = e.PnrpEndPoint.IPEndPoints;
            for (int i = 0; i < ipEndPoints.Length; i++)
            {
                DisplayMessage("  IPEndPoint " + (i+1).ToString()
                    + ": " + e.PnrpEndPoint.IPEndPoints[i].ToString());
            }
        }

        /// <summary>
        /// Handle the event that is raised when the resolve is complete.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnResolutionCompleted(object sender, ResolutionCompletedEventArgs e)
        {
            DisplayMessage("");
            DisplayMessage("Resolve Completed: " + e.Reason.ToString());

            UpdateControls();
        }

        /// <summary>
        /// Display a message in the text control.
        /// </summary>
        /// <param name="message">The text message to display.</param>
        private void DisplayMessage(string message)
        {
            textBoxResults.AppendText(message);
            textBoxResults.AppendText("\r\n");
        }

        /// <summary>
        /// Clear the message area.
        /// </summary>
        private void ClearMessages()
        {
            textBoxResults.Clear();
        }
    }
}
