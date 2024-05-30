using System;
using System.ComponentModel;
using System.Collections;
using System.Windows.Forms;
using System.Collaboration;
using System.Diagnostics;
using System.Text;

namespace Microsoft.Samples.Collaboration.RtcSample
{
    public class ChatForm : System.Windows.Forms.Form, IMediaNegotiation
    {
        public ChatForm(RealTimeProfile rtcProfile, SignalingSession signalingSession)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            Init(rtcProfile, signalingSession);
        }

        public ChatForm(RealTimeProfile rtcProfile, SignalingSession signalingSession, string inviteeUri)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            Init(rtcProfile, signalingSession);
            try
            {
                ////
                // Begin Microsoft.Collaboration Functionality Code
                ////
                //Invite participant
                IAsyncResult asyncResult = signalingSession.BeginInvite(
                    inviteeUri,
                    new AsyncCallback(this.InviteCallback), 
                    signalingSession);
                ////
                // End Microsoft.Collaboration Functionality Code
                ////
            }
            catch(Exception exp)
            {
                this.WriteLine("Invite " + inviteeUri + " Failed: " + exp.Message);
            }
        }
        
        /// <summary>
        /// A delegate signature used when media description request is sent.
        /// </summary>
        public MediaDescription GetMediaOffer(SignalingParticipant participant)
        {
            ////
            // Begin Microsoft.Collaboration Functionality Code
            ////
            SdpMediaDescription mediaDescription = new SdpMediaDescription();
            StringBuilder description = new StringBuilder();
            description.Append("v=0\r\n");
            description.Append("o=- 0 0 IN IP4 0.0.0.0\r\n");
            description.Append("s=session\r\n");
            description.Append("t=0 0\r\n");
            mediaDescription.GlobalDescription = description.ToString();
            description = new StringBuilder();
            description.Append("m=message 5060 ms-activity\r\n");
            description.Append("a=ContentType:text/plain utf-8 0\r\n");
            description.Append("a=ContentType:text/ utf-16 0\r\n");
            mediaDescription.MediaStreamDescriptions.Add(description.ToString());
            return mediaDescription;
            ////
            // End Microsoft.Collaboration Functionality Code
            ////
        }

        /// <summary>
        /// A delegate signature used when media description response is sent.
        /// </summary>
        public MediaDescription GetMediaAnswer(SignalingParticipant participant, MediaDescription descriptionRequest)
        {
            ////
            // Begin Microsoft.Collaboration Functionality Code
            ////
            return this.GetMediaOffer(null);
            ////
            // End Microsoft.Collaboration Functionality Code
            ////
        }

        /// <summary>
        /// A delegate signature used when media description response is received.
        /// </summary>
        public void SetMediaAnswer(SignalingParticipant participant, MediaDescription descriptionResponse)
        {
            ////
            // Begin Microsoft.Collaboration Functionality Code
            ////
            // Ignore it.
            ////
            // End Microsoft.Collaboration Functionality Code
            ////
        }

        protected override void Dispose( bool disposing )
        {
            if( disposing )
            {
                if(components != null)
                {
                    components.Dispose();
                }

                ////
                // Begin Microsoft.Collaboration Functionality Code
                ////
                if (this.signalingSession != null)
                {
                    //Dispose resources used by signaling session
                    this.signalingSession.Leave();
                    this.signalingSession = null;
                }
                ////
                // Begin Microsoft.Collaboration Functionality Code
                ////
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
            this.sendButton = new System.Windows.Forms.Button();
            this.typebox = new System.Windows.Forms.RichTextBox();
            this.mainMenu = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.closeMenuItem = new System.Windows.Forms.MenuItem();
            this.actionMenu = new System.Windows.Forms.MenuItem();
            this.inviteMenuItem = new System.Windows.Forms.MenuItem();
            this.statusBar = new System.Windows.Forms.StatusBar();
            this.sessionStateStatusBarPanel = new System.Windows.Forms.StatusBarPanel();
            this.messageStateStatusBarPanel = new System.Windows.Forms.StatusBarPanel();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.participantListView = new System.Windows.Forms.ListView();
            this.participantColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.stateColumnHeader = new System.Windows.Forms.ColumnHeader();
            ((System.ComponentModel.ISupportInitialize)(this.sessionStateStatusBarPanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.messageStateStatusBarPanel)).BeginInit();
            this.SuspendLayout();
            // 
            // sendButton
            // 
            this.sendButton.Location = new System.Drawing.Point(320, 336);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(40, 48);
            this.sendButton.TabIndex = 4;
            this.sendButton.Text = "Send";
            this.sendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // typebox
            // 
            this.typebox.Location = new System.Drawing.Point(0, 336);
            this.typebox.Name = "typebox";
            this.typebox.Size = new System.Drawing.Size(312, 48);
            this.typebox.TabIndex = 8;
            this.typebox.Text = "";
            this.typebox.TextChanged += new System.EventHandler(this.typebox_TextChanged);
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                     this.menuItem1,
                                                                                     this.actionMenu});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                      this.closeMenuItem});
            this.menuItem1.Text = "&File";
            // 
            // closeMenuItem
            // 
            this.closeMenuItem.Index = 0;
            this.closeMenuItem.Text = "&Close";
            // 
            // actionMenu
            // 
            this.actionMenu.Index = 1;
            this.actionMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                       this.inviteMenuItem});
            this.actionMenu.Text = "&Actions";
            // 
            // inviteMenuItem
            // 
            this.inviteMenuItem.Index = 0;
            this.inviteMenuItem.Text = "&Invite...";
            this.inviteMenuItem.Click += new System.EventHandler(this.invite_Click);
            // 
            // statusBar
            // 
            this.statusBar.Location = new System.Drawing.Point(0, 389);
            this.statusBar.Name = "statusBar";
            this.statusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
                                                                                         this.sessionStateStatusBarPanel,
                                                                                         this.messageStateStatusBarPanel});
            this.statusBar.ShowPanels = true;
            this.statusBar.Size = new System.Drawing.Size(360, 22);
            this.statusBar.TabIndex = 15;
            this.statusBar.Text = "statusBar";
            // 
            // messageStateStatusBarPanel
            // 
            this.messageStateStatusBarPanel.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
            this.messageStateStatusBarPanel.Width = 244;
            // 
            // richTextBox
            // 
            this.richTextBox.Location = new System.Drawing.Point(0, 80);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.Size = new System.Drawing.Size(360, 248);
            this.richTextBox.TabIndex = 16;
            this.richTextBox.Text = "";
            // 
            // participantListView
            // 
            this.participantListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                                                                                  this.participantColumnHeader,
                                                                                                  this.stateColumnHeader});
            this.participantListView.FullRowSelect = true;
            this.participantListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.participantListView.Location = new System.Drawing.Point(0, 0);
            this.participantListView.Name = "participantListView";
            this.participantListView.Size = new System.Drawing.Size(360, 72);
            this.participantListView.TabIndex = 17;
            this.participantListView.TabStop = false;
            this.participantListView.View = System.Windows.Forms.View.Details;
            // 
            // participantColumnHeader
            // 
            this.participantColumnHeader.Text = "Participant";
            this.participantColumnHeader.Width = 225;
            // 
            // stateColumnHeader
            // 
            this.stateColumnHeader.Text = "State";
            this.stateColumnHeader.Width = 130;
            // 
            // ChatForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(360, 411);
            this.Controls.Add(this.participantListView);
            this.Controls.Add(this.richTextBox);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.typebox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Menu = this.mainMenu;
            this.Name = "ChatForm";
            this.Text = "ChatForm";
            ((System.ComponentModel.ISupportInitialize)(this.sessionStateStatusBarPanel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.messageStateStatusBarPanel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private void SendButton_Click(object sender, EventArgs e)
        {
            if( (typebox.Text.Length == 0) || 
                ((typebox.Text.Length == 1) && (typebox.Text[0] == '\n')))
            {
                return; // Just a carriage return or send clicked with no text.
            }

            ////
            // Begin Microsoft.Collaboration Functionality Code
            ////
            if( (signalingSession == null) && (lastParticipant != null))
            {
                string tempParticipant = lastParticipant;

                //Create Instant Messaging session
                signalingSession = rtcProfile.Signaling.CreateSession(
                    "InstantMessaging", null, null);
                
                Init(rtcProfile, signalingSession);

                //Invite participant
                IAsyncResult asyncResult = signalingSession.BeginInvite(
                    tempParticipant,
                    new AsyncCallback(this.InviteCallback), 
                    signalingSession);
            }

            if (signalingSession != null)
            {
                Relay relay = signalingSession.Relay;

                if (relay != null)
                {
                    // Send the message in typebox
                    string chatMessage = typebox.Text.TrimEnd('\n');

                    UTF8Encoding encoding = new UTF8Encoding();
                    byte[] body = encoding.GetBytes(chatMessage);
                    try
                    {
                        IAsyncResult async = relay.BeginSendMessage(
                            new ContentType("text/plain", "UTF-8"), 
                            body, 
                            new AsyncCallback(this.SendMessageCallback), 
                            relay); 
                        string name = GetParticipantName(this.signalingSession.LocalParticipant);
                        this.WriteLine(String.Format("{0} says:", name));
                        this.WriteLine(chatMessage);
                        this.sessionStateStatusBarPanel.Text = this.signalingSession.LocalParticipant.State.ToString();
                    }
                    catch(Exception exp)
                    {
                        this.WriteLine("Send Failed: " + exp.Message);
                    }

                    this.typebox.Focus();
                    this.typebox.Clear();
                }
                else
                {
                }     
            }
            ////
            // End Microsoft.Collaboration Functionality Code
            ////
        }

        private void invite_Click(object sender, EventArgs e)
        {
            InviteForm          inviteForm = new InviteForm();

            DialogResult    result = inviteForm.ShowDialog(this);

            if(result == DialogResult.OK)
            {
                string      inviteeUri = inviteForm.InviteeUri;
                try
                {
                    ////
                    // Begin Microsoft.Collaboration Functionality Code
                    ////
                    if (this.signalingSession == null)
                    {
                        //Creat Instant Messaging session
                        signalingSession = rtcProfile.Signaling.CreateSession(
                            "InstantMessaging", 
                            Guid.NewGuid().ToString(), 
                            null);
                        Init(rtcProfile, signalingSession);
                    }

                    //Invite participant
                    IAsyncResult asyncResult = signalingSession.BeginInvite(
                        inviteeUri,
                        new AsyncCallback(this.InviteCallback), 
                        signalingSession);
                    ////
                    // End Microsoft.Collaboration Functionality Code
                    ////
                }
                catch(Exception exp)
                {
                    this.WriteLine("Invite " + inviteeUri + " Failed: " + exp.Message);
                }
            }            
        }

        private void closeMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void typebox_TextChanged(object sender, EventArgs e)
        {
            SignalCommand command = null;

            // When first character is typed, send typing message.
            if( (this.signalingSession != null) && 
                (this.typebox.Text.Length == 1) && 
                (this.typebox.Text[0] != '\n') && 
                (this.signalingSession.LocalParticipant.State == 
                ParticipantState.Connected))
            {
                //Create typing command
                command = new TypingCommand();
            }
            else if (typebox.Text.Length == 0)
            {
                //Create idle command
                command = new IdleCommand();
            }

            if (command != null && this.signalingSession != null)
            {
                ////
                // Begin Microsoft.Collaboration Functionality Code
                ////
                Signal chatSignal = this.signalingSession.Signal;

                if (chatSignal != null)
                {
                    try
                    {
                        //Send command
                        IAsyncResult async = chatSignal.BeginSendCommand(
                            command, 
                            new AsyncCallback(this.SendCommandCallback),
                            chatSignal);
                    }
                    catch
                    {
                    }
                }
                ////
                // End Microsoft.Collaboration Functionality Code
                ////

                return;
            }

            if(typebox.Text.Length > 1 && 
               typebox.Text[typebox.Text.Length-1] == '\n')
            {
                SendButton_Click(sender, e);
            }
            else if(typebox.Text.Length == 1 && 
                    typebox.Text[typebox.Text.Length-1] == '\n')
            {
                typebox.Text = ""; // Remove the single cr typed.
            }
        }


        #region Provider API event handlers
        private void OnCommandReceived(object sender, SignalCommandEventArgs e)
        {
            ////
            // Begin Microsoft.Collaboration Functionality Code
            ////

            string name = GetParticipantName(e.Participant);

            if (e.Command is TypingCommand)
            {
                //The participant is typing a message
                this.messageStateStatusBarPanel.Text = String.Format(
                    "{0} is typing a message.", 
                    name);
            }
            else if (e.Command is IdleCommand)
            {
                //The participan is idle
                DateTime currTime = DateTime.Now;
                this.messageStateStatusBarPanel.Text = String.Format(
                    "Last message received on {0} at {1}", 
                    currTime.ToShortDateString(), 
                    currTime.ToShortTimeString());
            }

            ////
            // End Microsoft.Collaboration Functionality Code
            ////
        }

        private void OnMessageReceived(object sender, RelayEventArgs e)
        {
            ////
            // Begin Microsoft.Collaboration Functionality Code
            ////
            string name = GetParticipantName(e.Participant);

            byte[]              body   = e.GetBody();

            //Decode the received message and display
            UTF8Encoding encoding = new UTF8Encoding();

            string chatMessage = encoding.GetString(body);
            this.WriteLine(String.Format("{0} says: ", name));
            this.WriteLine(chatMessage);
            this.typebox.Focus();

            DateTime currTime = DateTime.Now;
            this.messageStateStatusBarPanel.Text = String.Format(
                "Last message received on {0} at {1}", 
                currTime.ToShortDateString(), 
                currTime.ToShortTimeString());
            ////
            // End Microsoft.Collaboration Functionality Code
            ////
        }

        private void OnNewParticipant(object sender, CollectionEventArgs e)
        {
            ////
            // Begin Microsoft.Collaboration Functionality Code
            ////
            SignalingParticipant p = e.Target as SignalingParticipant;

            if (p != null)
            {
                //Register to the state change event of the new participant
                OnParticipantStateChanged(p, EventArgs.Empty);
                //Raise the event for this new participant
                p.StateChanged += new EventHandler(this.OnParticipantStateChanged);
            }
            ////
            // End Microsoft.Collaboration Functionality Code
            ////
        }

        private void OnParticipantStateChanged(object sender, EventArgs e)
        {
            ////
            // Begin Microsoft.Collaboration Functionality Code
            ////

            string name = GetParticipantName((SignalingParticipant) sender);
            SignalingParticipant p = (SignalingParticipant) sender;

            if (p != signalingSession.LocalParticipant)
            {
                //Local participan state changed
                this.UpdateParticipant(p.RealTimeAddress, p.State.ToString());

                if (p.State == ParticipantState.Disconnected)
                {
                    this.WriteLine(String.Format(
                        "{0} has left the conversation", name));
                }

                // If the last participant is gettting disconnected, remember 
                // this participant so that we can reinvite if any message 
                // is typed.
                if( (p.State == ParticipantState.Disconnected) && 
                    (signalingSession.LocalParticipant.State == 
                    ParticipantState.Disconnected))
                {
                    lastParticipant = p.RealTimeAddress;
                    this.signalingSession.Leave();
                    this.signalingSession = null;
                }
            }
            else
            {
                //Remote participan state changed
                this.sessionStateStatusBarPanel.Text = p.State.ToString();
                if (signalingSession.LocalParticipant.State == 
                    ParticipantState.Connected)
                {
                }
            }
            ////
            // End Microsoft.Collaboration Functionality Code
            ////
        }

        #endregion

        #region Provider API Callbacks
        private void InviteCallback(IAsyncResult result)
        {
            ////
            // Begin Microsoft.Collaboration Functionality Code
            ////
            try
            {
                SignalingSession session = (SignalingSession) result.AsyncState;
                session.EndInvite(result);
            }
            catch(Exception ex)
            {
                MessageBox.Show(
                    "Error inviting buddy.\n" + ex.ToString(), 
                    "Error");
            }
            ////
            // End Microsoft.Collaboration Functionality Code
            ////
        }

        private void SendMessageCallback(IAsyncResult result)
        {
            ////
            // Begin Microsoft.Collaboration Functionality Code
            ////
            try
            {
                Relay relay = (Relay) result.AsyncState;
                relay.EndSendMessage(result);
            }
            catch(Exception ex)
            {
                MessageBox.Show(
                    "Error sending message.\n" + ex.ToString(), 
                    "Error");
            }
            ////
            // End Microsoft.Collaboration Functionality Code
            ////
        }

        private void SendCommandCallback(IAsyncResult result)
        {
            ////
            // Begin Microsoft.Collaboration Functionality Code
            ////
            try
            {
                Signal chatSignal = (Signal) result.AsyncState;
                SignalingParticipant[] failedParticipants = chatSignal.EndSendCommand(result);
            }
            catch(Exception ex)
            {
                MessageBox.Show(
                    "Error sending command.\n" + ex.ToString(), 
                    "Error");
            }
            ////
            // End Microsoft.Collaboration Functionality Code
            ////
        }

        #endregion

        #region Private Helpers
        private string GetParticipantName(SignalingParticipant p)
        {
            string                 name;

            if (p.DisplayName != null && p.DisplayName.Length ==0)
            {
                name = p.DisplayName;
            }
            else
            {
                int index = p.RealTimeAddress.IndexOf('@');

                if (p.RealTimeAddress.StartsWith("sip:"))
                {
                    name = p.RealTimeAddress.Substring(4, index-4);
                }
                else
                {
                    name = p.RealTimeAddress.Substring(0, index);
                }
            }

            return name;
        }

        private void Init(RealTimeProfile rtcProfile, SignalingSession signalingSession)
        {
            this.Text = "Chat Form " + signalingSession.LocalParticipant.RealTimeAddress;
            this.closeMenuItem.Click += new EventHandler(closeMenuItem_Click);
            this.richTextBox.Clear();

            ////
            // Begin Microsoft.Collaboration Functionality Code
            ////
            this.signalingSession = signalingSession;
            this.rtcProfile = rtcProfile;
            this.signalingSession.MediaNegotiation = this;
            //if we receive an incoming session, the session will have one or more
            //participants.
            foreach (SignalingParticipant participant in this.signalingSession.Participants)
            {
                participant.StateChanged += 
                    new EventHandler(this.OnParticipantStateChanged);
            }

            Relay relay = signalingSession.Relay;
            if (relay != null)
            {
                relay.MessageReceived += new RelayEventHandler(OnMessageReceived);
            }

            Signal signal = signalingSession.Signal;
            if (signal != null)
            {
                signal.CommandReceived += new SignalCommandEventHandler(OnCommandReceived);
            }

            this.signalingSession.Participants.ItemAdded += new CollectionEventHandler(this.OnNewParticipant);
            foreach(SignalingParticipant p in signalingSession.Participants)
            {
                if (p != signalingSession.LocalParticipant)
                {
                    this.UpdateParticipant(p.RealTimeAddress, p.State.ToString());
                }
            }
            ////
            // End Microsoft.Collaboration Functionality Code
            ////

            typebox.Focus();
            lastParticipant = null;
        }

        private void UpdateParticipant(string participant, string state)
        {
            foreach (ListViewItem item in this.participantListView.Items)
            {
                if (item.SubItems[0].Text == participant)
                {
                    item.SubItems[1].Text = state;
                    return;
                }
            }

            string[] subItems = new string[2];
            
            subItems[0] = participant;
            subItems[1] = state;

            this.participantListView.Items.Add(new ListViewItem(subItems));
        }

        private void WriteLine(string message)
        {
            this.richTextBox.Focus();
            this.richTextBox.AppendText(message);
            this.richTextBox.AppendText("\n");
            this.richTextBox.SelectionStart = this.richTextBox.Text.Length;
            this.richTextBox.SelectionLength = 0;

            this.typebox.Focus();
        }

        #endregion

        #region Private fields
        private RealTimeProfile  rtcProfile;
        private SignalingSession signalingSession;
        private string lastParticipant; // Last talked to.

        private System.Windows.Forms.RichTextBox typebox;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.MainMenu mainMenu;
        private System.Windows.Forms.MenuItem actionMenu;
        private System.Windows.Forms.MenuItem inviteMenuItem;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.StatusBar statusBar;
        private System.Windows.Forms.MenuItem closeMenuItem;
        private System.Windows.Forms.StatusBarPanel sessionStateStatusBarPanel;
        private System.Windows.Forms.StatusBarPanel messageStateStatusBarPanel;
        private System.Windows.Forms.RichTextBox richTextBox;
        private System.Windows.Forms.ListView participantListView;
        private System.Windows.Forms.ColumnHeader participantColumnHeader;
        private System.Windows.Forms.ColumnHeader stateColumnHeader;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        #endregion

    }
}
