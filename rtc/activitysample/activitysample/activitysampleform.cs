using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Collaboration;
using Microsoft.Collaboration;
using Microsoft.Collaboration.Activity;

namespace Microsoft.Samples.Collaboration.ActivitySample
{
    public class ActivitySample : System.Windows.Forms.Form
    {
        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TextRemote = new System.Windows.Forms.TextBox();
            this.LabelRemote = new System.Windows.Forms.Label();
            this.ButtonCall = new System.Windows.Forms.Button();
            this.ButtonStop = new System.Windows.Forms.Button();
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.statusBarPanel1 = new System.Windows.Forms.StatusBarPanel();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).BeginInit();
            this.SuspendLayout();
            // 
            // TextRemote
            // 
            this.TextRemote.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.TextRemote.Location = new System.Drawing.Point(104, 16);
            this.TextRemote.Name = "TextRemote";
            this.TextRemote.Size = new System.Drawing.Size(184, 22);
            this.TextRemote.TabIndex = 0;
            this.TextRemote.Text = "";
            // 
            // LabelRemote
            // 
            this.LabelRemote.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.LabelRemote.Location = new System.Drawing.Point(8, 16);
            this.LabelRemote.Name = "LabelRemote";
            this.LabelRemote.Size = new System.Drawing.Size(88, 16);
            this.LabelRemote.TabIndex = 0;
            this.LabelRemote.Text = "Peer Address";
            this.LabelRemote.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ButtonCall
            // 
            this.ButtonCall.Location = new System.Drawing.Point(120, 56);
            this.ButtonCall.Name = "ButtonCall";
            this.ButtonCall.TabIndex = 1;
            this.ButtonCall.Text = "Call";
            this.ButtonCall.Click += new System.EventHandler(this.ButtonCall_Click);
            // 
            // ButtonStop
            // 
            this.ButtonStop.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonStop.Location = new System.Drawing.Point(216, 56);
            this.ButtonStop.Name = "ButtonStop";
            this.ButtonStop.TabIndex = 2;
            this.ButtonStop.Text = "Stop";
            this.ButtonStop.Click += new System.EventHandler(this.ButtonStop_Click);
            // 
            // statusBar1
            // 
            this.statusBar1.Location = new System.Drawing.Point(0, 89);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
                                                                                          this.statusBarPanel1});
            this.statusBar1.ShowPanels = true;
            this.statusBar1.Size = new System.Drawing.Size(296, 22);
            this.statusBar1.TabIndex = 3;
            this.statusBar1.Text = "statusBar1";
            // 
            // statusBarPanel1
            // 
            this.statusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
            this.statusBarPanel1.Text = "statusBarPanel1";
            this.statusBarPanel1.Width = 280;
            // 
            // ActivitySample
            // 
            this.AcceptButton = this.ButtonCall;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.CancelButton = this.ButtonStop;
            this.ClientSize = new System.Drawing.Size(296, 111);
            this.Controls.Add(this.statusBar1);
            this.Controls.Add(this.ButtonStop);
            this.Controls.Add(this.ButtonCall);
            this.Controls.Add(this.LabelRemote);
            this.Controls.Add(this.TextRemote);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ActivitySample";
            this.Text = "Activity Sample";
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        #region Main, ctor, dispose
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() 
        {
            Application.Run(new ActivitySample());
        }

        public ActivitySample()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            this.InitializeRTC();
            this.ToggleCallandStop(true);
            this.statusBarPanel1.Text = "Idle";
			this.Closing += new CancelEventHandler(ActivitySample_Closing);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose( bool disposing )
        {
            if (this.activity != null)
            {
                this.activity.Dispose();
            }

            if( disposing )
            {
                if (components != null) 
                {
                    components.Dispose();
                }

                ////
                // Begin Microsoft.Collaboration Functionality Code
                ////
                //Dispose activity resource
                if (this.activity != null)
                {

                    this.activity.Dispose();
                    this.activity = null;
                }

                //Dispose session resource
                if (this.session != null)
                {
                    this.session.Leave();
                    this.session = null;
                }

                //Dispose profile resource
                if (this.profile != null)
                {
                    this.provider.RemoveRealTimeProfile(this.profile);
                    this.profile = null;
                }

                this.provider = null;
                ////
                // End Microsoft.Collaboration Functionality Code
                ////
            }
            base.Dispose( disposing );
        }
        #endregion

        #region RTC callback methods
        private void InviteCallback(IAsyncResult asyncResult)
        {
            try
            {
                ////
                // Begin Microsoft.Collaboration Functionality Code
                ////
                this.activity.EndInvite(asyncResult);
                ////
                // End Microsoft.Collaboration Functionality Code
                ////
            }
            catch (Exception ex)
            {
                string remote = asyncResult.AsyncState as string;
                MessageBox.Show(
                    "Error calling peer " + remote + ".\n" + ex.ToString(),
                    "Error");
                this.ToggleCallandStop(true);
            }
        }

        private void AcceptCallback(IAsyncResult asyncResult)
        {
            try
            {
                ////
                // Begin Microsoft.Collaboration Functionality Code
                ////
                this.activity.EndAccept(asyncResult);
                ////
                // End Microsoft.Collaboration Functionality Code
                ////
            }
            catch (Exception ex)
            {
                string remote = asyncResult.AsyncState as string;
                MessageBox.Show(
                    "Error accepting call from peer " + remote + ".\n" + ex.ToString(),
                    "Error");
                this.ToggleCallandStop(true);
            }
        }

        private void OnSessionReceived(
            object                      sender,
            IncomingSessionEventArgs    e
            )
        {
            SignalingSession session = e.Session;

            if (this.session != null)
            {
                MessageBox.Show("Busy. Reject call from " + session.Inviter.RealTimeAddress);
                return;
            }

            DialogResult result = MessageBox.Show(
                this,
                "Accept call from " + session.Inviter.RealTimeAddress,
                "Call Notification",
                MessageBoxButtons.YesNo
                );

            if (result == DialogResult.No)
            {
                session.Decline();
                return;
            }

            try
            {
                ////
                // Begin Microsoft.Collaboration Functionality Code
                ////
                // call accepted
                this.peer = session.Inviter.RealTimeAddress;
                this.session = session;
                this.activity = new AudioVideoActivity(session);

                IAsyncResult async = this.activity.BeginAccept(
                    this.acceptCallback,
                    this.session
                    );

                this.session.LocalParticipant.StateChanged += new EventHandler(this.OnParticipantStateChange);
                ////
                // End Microsoft.Collaboration Functionality Code
                ////

                this.ToggleCallandStop(false);
            }
            catch (Exception ex)
            {
                if (this.activity != null)
                {
                    this.activity.Dispose();
                }

                this.activity = null;

                this.session = null;

                MessageBox.Show(
                    "Error  accepting call.\n" + ex.ToString(),
                    "Error");
            }
        }

        private void OnParticipantStateChange(
            object      sender,
            EventArgs   e
            )
        {
            ////
            // Begin Microsoft.Collaboration Functionality Code
            ////
            SignalingParticipant participant = (SignalingParticipant)sender;
            
            if (participant.State == System.Collaboration.ParticipantState.Disconnected)
            {
                // stop the call
                if (this.ButtonStop.Enabled)
                {
                    this.ButtonStop_Click(this, null);
                }
            }
            ////
            // End Microsoft.Collaboration Functionality Code
            ////

            this.statusBarPanel1.Text = "Call with " + this.peer + " " + participant.State.ToString();
        }
        #endregion

        #region private methods
        private void InitializeRTC()
        {
            try
            {
                ////
                // Begin Microsoft.Collaboration Functionality Code
                ////
                this.provider = new RtcProvider();
                this.profile =
                    this.provider.CreateRealTimePeerToPeerProfile(SystemInformation.ComputerName);
                this.profile.Signaling.SessionReceived +=
                    new IncomingSessionEventHandler(this.OnSessionReceived);
                this.inviteCallback = new AsyncCallback(this.InviteCallback);
                this.acceptCallback = new AsyncCallback(this.AcceptCallback);
                ////
                // End Microsoft.Collaboration Functionality Code
                ////
            }
            catch (Exception ex)
            {
                this.inviteCallback = null;
                this.acceptCallback = null;

                this.profile = null;
                this.provider = null;

                MessageBox.Show(
                    "Error create RTC peer to peer profile.\n" + ex.ToString(),
                    "Error");

                this.Close();
            }
        }

        private void ToggleCallandStop(bool call)
        {
            this.ButtonCall.Enabled = call;
            this.ButtonStop.Enabled = !call;
            if (call)
            {
                this.statusBarPanel1.Text = "Call with " + this.peer + " Disconnected";
            }
        }
        #endregion

        #region UI callback methods
        private void ButtonCall_Click(object sender, System.EventArgs e)
        {
            try
            {
                ////
                // Begin Microsoft.Collaboration Functionality Code
                ////
                // create session
                this.session = profile.Signaling.CreateSession(
                    "AudioVideo",
                    "SessionID",
                    null
                    );

                // create activity
                this.activity = new AudioVideoActivity(this.session);

                // invite
                IAsyncResult asyncResult = this.activity.BeginInvite(
                    this.TextRemote.Text,
                    this.inviteCallback,
                    this.TextRemote.Text
                    );

                this.session.LocalParticipant.StateChanged += new EventHandler(this.OnParticipantStateChange);
                ////
                // End Microsoft.Collaboration Functionality Code
                ////

                this.peer = this.TextRemote.Text;

                this.ToggleCallandStop(false);
            }
            catch (Exception ex)
            {
                if (this.activity != null)
                {
                    this.activity.Dispose();
                    this.activity = null;
                }

                this.session = null;

                MessageBox.Show(
                    "Error calling peer" + ".\n" + ex.ToString(),
                    "Error");
            }
        }

        private void ButtonStop_Click(object sender, System.EventArgs e)
        {
            if (this.activity != null)
            {
                this.activity.Dispose();
            }

            //TODO need to dispose session???

            this.activity = null;
            this.session = null;

            this.ToggleCallandStop(true);
        }

		private void ActivitySample_Closing(object sender, CancelEventArgs e)
		{
			this.Hide();
		}

        #endregion

        #region private data members
        private RtcProvider             provider = null;
        private RtcPeerToPeerProfile    profile = null;
        private SignalingSession        session = null;
        private string                  peer;
        private AudioVideoActivity      activity = null;
        private AsyncCallback           inviteCallback = null;
        private AsyncCallback           acceptCallback = null;

        private System.Windows.Forms.Label LabelRemote;
        private System.Windows.Forms.TextBox TextRemote;
        private System.Windows.Forms.Button ButtonCall;
        private System.Windows.Forms.Button ButtonStop;
        private System.Windows.Forms.StatusBar statusBar1;
        private System.Windows.Forms.StatusBarPanel statusBarPanel1;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        #endregion
    }
}
