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


// NOTE: In this preview release, the sample might not run within the Visual Studio development environment. 
// If you encounter problems, run from the command line or Windows Explorer.


using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Windows.Media.Core;
using System.Windows.Media.Types;


namespace Player
{
    /// <summary>
    /// This is a simple media player using "Avalon" media services.
    /// It demonstrates the following:
    /// -- using the media engine for playback.
    /// -- starting, stopping, pausing, and seeking.
    /// -- working with asynchronous methods and handling events.
    /// -- retrieving metadata for a file.
    /// -- using volume, mute, rate, and video controls.
    /// </summary>
    public class Form1 : System.Windows.Forms.Form
    {
        private System.ComponentModel.IContainer components;

        public Form1()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            InitEngine();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if(disposing)
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
			this.tbPosition = new System.Windows.Forms.TrackBar();
			this.btnPlay = new System.Windows.Forms.Button();
			this.btnPause = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.statusBar1 = new System.Windows.Forms.StatusBar();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.components = new System.ComponentModel.Container();
			this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
			this.menuFile = new System.Windows.Forms.MenuItem();
			this.menuFileOpen = new System.Windows.Forms.MenuItem();
			this.menuFileExit = new System.Windows.Forms.MenuItem();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.tbVolume = new System.Windows.Forms.TrackBar();
			this.label1 = new System.Windows.Forms.Label();
			this.upDownRate = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.cbMute = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.tbPosition)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tbVolume)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.upDownRate)).BeginInit();
			this.SuspendLayout();

// 
// tbPosition
// 
			this.tbPosition.Enabled = false;
			this.tbPosition.LargeChange = 100;
			this.tbPosition.Location = new System.Drawing.Point(32, 408);
			this.tbPosition.Maximum = 1000;
			this.tbPosition.Name = "tbPosition";
			this.tbPosition.Size = new System.Drawing.Size(448, 45);
			this.tbPosition.TabIndex = 4;
			this.tbPosition.TabStop = false;
			this.tbPosition.TickFrequency = 0;
			this.tbPosition.TickStyle = System.Windows.Forms.TickStyle.None;
			this.tbPosition.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tbPosition_MouseUp);
			this.tbPosition.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tbPosition_MouseDown);

// 
// btnPlay
// 
			this.btnPlay.Enabled = false;
			this.btnPlay.Location = new System.Drawing.Point(144, 358);
			this.btnPlay.Name = "btnPlay";
			this.btnPlay.Size = new System.Drawing.Size(64, 40);
			this.btnPlay.TabIndex = 0;
			this.btnPlay.Text = "&Start";
			this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);

// 
// btnPause
// 
			this.btnPause.Enabled = false;
			this.btnPause.Location = new System.Drawing.Point(232, 358);
			this.btnPause.Name = "btnPause";
			this.btnPause.Size = new System.Drawing.Size(64, 40);
			this.btnPause.TabIndex = 1;
			this.btnPause.Text = "P&ause";
			this.btnPause.Click += new System.EventHandler(this.btnPause_Click);

// 
// pictureBox1
// 
			this.pictureBox1.Location = new System.Drawing.Point(40, 16);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(424, 328);
			this.pictureBox1.TabIndex = 4;
			this.pictureBox1.TabStop = false;

// 
// statusBar1
// 
			this.statusBar1.Location = new System.Drawing.Point(0, 459);
			this.statusBar1.Name = "statusBar1";
			this.statusBar1.Size = new System.Drawing.Size(600, 22);
			this.statusBar1.TabIndex = 5;

// 
// openFileDialog1
// 
			this.openFileDialog1.Filter = "All files|*.*";

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
				this.menuFileOpen, this.menuFileExit
			});
			this.menuFile.Name = "menuFile";
			this.menuFile.Text = "&File";

// 
// menuFileOpen
// 
			this.menuFileOpen.Index = 0;
			this.menuFileOpen.Name = "menuFileOpen";
			this.menuFileOpen.Text = "&Open...";
			this.menuFileOpen.Click += new System.EventHandler(this.menuFileOpen_Click);

// 
// menuFileExit
// 
			this.menuFileExit.Index = 1;
			this.menuFileExit.Name = "menuFileExit";
			this.menuFileExit.Text = "E&xit";
			this.menuFileExit.Click += new System.EventHandler(this.menuFileExit_Click);

// 
// timer1
// 
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);

// 
// tbVolume
// 
			this.tbVolume.LargeChange = 10;
			this.tbVolume.Location = new System.Drawing.Point(504, 128);
			this.tbVolume.Maximum = 100;
			this.tbVolume.Name = "tbVolume";
			this.tbVolume.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.tbVolume.Size = new System.Drawing.Size(45, 200);
			this.tbVolume.TabIndex = 7;
			this.tbVolume.TickFrequency = 10;
			this.tbVolume.Value = 50;
			this.tbVolume.Scroll += new System.EventHandler(this.tbVolume_Scroll);

// 
// label1
// 
			this.label1.Location = new System.Drawing.Point(496, 88);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(64, 24);
			this.label1.TabIndex = 6;
			this.label1.Text = "&Volume";

// 
// upDownRate
// 
			this.upDownRate.DecimalPlaces = 1;
			this.upDownRate.Enabled = false;
			this.upDownRate.Increment = new System.Decimal(new int[] {
				5, 0, 0, 65536
			});
			this.upDownRate.Location = new System.Drawing.Point(384, 368);
			this.upDownRate.Maximum = new System.Decimal(new int[] {
				4, 0, 0, 0
			});
			this.upDownRate.Minimum = new System.Decimal(new int[] {
				5, 0, 0, 65536
			});
			this.upDownRate.Name = "upDownRate";
			this.upDownRate.Size = new System.Drawing.Size(40, 20);
			this.upDownRate.TabIndex = 3;
			this.upDownRate.Value = new System.Decimal(new int[] {
				1, 0, 0, 0
			});
			this.upDownRate.ValueChanged += new System.EventHandler(this.upDownRate_ValueChanged);

// 
// label2
// 
			this.label2.Location = new System.Drawing.Point(344, 368);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(32, 23);
			this.label2.TabIndex = 2;
			this.label2.Text = "&Rate";

// 
// cbMute
// 
			this.cbMute.Location = new System.Drawing.Point(496, 32);
			this.cbMute.Name = "cbMute";
			this.cbMute.Size = new System.Drawing.Size(72, 16);
			this.cbMute.TabIndex = 5;
			this.cbMute.Text = "&Mute";
			this.cbMute.CheckedChanged += new System.EventHandler(this.cbMute_CheckedChanged);

// 
// Form1
// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(600, 481);
			this.Controls.Add(this.cbMute);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.upDownRate);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.tbVolume);
			this.Controls.Add(this.statusBar1);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.btnPause);
			this.Controls.Add(this.btnPlay);
			this.Controls.Add(this.tbPosition);
			this.Menu = this.mainMenu1;
			this.Name = "Form1";
			this.Text = "Media Player Sample";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.Form1_Closing);
			((System.ComponentModel.ISupportInitialize)(this.tbPosition)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tbVolume)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.upDownRate)).EndInit();
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
            Application.Run(new Form1());
        }

        #region Application-defined methods

        /// <summary>
        /// Create the media engine and store the default size of the display window.
        /// </summary>
        void InitEngine()
        {
            m_mediaEngine = new MediaEngine();
            m_mediaEngine.MediaOpened += new System.Windows.Media.Core.MediaEventHandler(this.MediaOpened);
            m_mediaEngine.MediaClosed += new System.Windows.Media.Core.MediaEventHandler(this.MediaClosed);
            m_mediaEngine.MediaStarted += new System.Windows.Media.Core.MediaEventHandler(this.MediaStarted);
            m_mediaEngine.MediaEnded += new System.Windows.Media.Core.MediaEventHandler(this.MediaEnded);
            m_mediaEngine.MediaStopped += new System.Windows.Media.Core.MediaEventHandler(this.MediaStopped);
            tbVolume.Value = tbVolume.Maximum; 
            m_maxScreenHeight = pictureBox1.Height;
            m_maxScreenWidth = pictureBox1.Width;
        }
      
        /// <summary>
        /// Return duration of source in milliseconds.
        /// </summary>
        private Int64 GetDuration(MediaEngine mediaEngine)
        {
            Int64 dur = 0; 

            if (null != mediaEngine)
            {
                object objDuration = null;
                System.Windows.Explorer.PropertyStore properties = mediaEngine.Properties;
                {
                    try
                    {
                        objDuration = properties[PropertyKeys.Duration];
                        if (null != objDuration)
                        {
                            dur = (Int64)objDuration;
                        }
                        else
                        {
                            // Optional diagnostics.
                            // System.Diagnostics.Debug.WriteLine("Couldn't retrieve property by key.");
                        }
                    }
                    catch
                    {
                        // System.Diagnostics.Debug.WriteLine("Couldn't retrieve properties.");
                    }
                }
            }

            // Convert 100-ns units to ms
            dur = dur / 10000;
            return dur;
        }

        

        /// <summary>
        /// Set the state of the UI depending on whether a file is loaded.
        /// </summary>
        /// <param name="enabled">True if a file is loaded.</param>
        private void EnableUI(bool enabled)
        {
            upDownRate.Value = 1;
            tbPosition.Enabled = enabled;
            btnPlay.Enabled = enabled;     
            if (enabled)
            {
                upDownRate.Enabled = (m_rateControl != null);
                tbVolume.Enabled = (m_volumeControl != null);
                statusBar1.Text = m_fileName;
                btnPlay.Focus();
            }
            else
            {
                upDownRate.Enabled = false;
                tbVolume.Enabled = false;
                statusBar1.Text = "No file loaded.";
            }
            cbMute.Enabled = tbVolume.Enabled;
        }

        /// <summary>
        /// Open the media engine and get the duration of the file.
        /// We always ask for both video and audio; this does no harm and is
        /// easier than figuring out what media types are actually present.
        /// <returns>Returns true if file successfully opened.</returns>
        /// </summary>
        private bool OpenEngine(string fileName)
        {
            this.Refresh();
            Cursor.Current = Cursors.WaitCursor;
            // m_mediaEngine.Stop();
            // m_mediaEngine.Close();
            engineIsOpenEvent = new System.Threading.AutoResetEvent(false);

            // Rather than trying to determine the mediatypes present in all the 
            // streams of the file, we'll just request the two types we intend to 
            // handle. If one of them is not present in the file, no harm is done.
            System.Type [] typesToPlay = new System.Type [] 
                    { typeof(AudioMediaType), typeof(VideoMediaType) };  

            // Create a destination capable of displaying video and audio.
            Destination destination = new Destination(pictureBox1.Handle);

            // Open the file. This creates the topology.
            m_mediaEngine.Open(fileName, destination, typesToPlay); 
            engineIsOpenEvent.WaitOne(); 

            // Reset the cursor.
            Cursor.Current = Cursors.Default;

            // If it's a valid media file, get its duration and some controls.
            // If it's video, set the display to the right aspect ratio.
            if (m_validFile)
            {
                m_duration = GetDuration(m_mediaEngine);
                m_fileName = fileName;        // Global in case we need to reopen.
                GetVolumeControl();
                GetRateControl();
                GetVideoControl();
                EnableUI(true);    

                // Set screen dimensions.
                if (m_videoControl != null)
                {
                    SetScreen(m_videoControl, pictureBox1, m_maxScreenHeight, m_maxScreenWidth);
                }
                return true;
            }
            else 
            {       
                EnableUI(false);                   
                return false;
            }
        }

        /// <summary>
        /// Dispose of global objects.
        /// </summary>
        private void Cleanup()
        {
            if (m_volumeControl != null) m_volumeControl.Dispose();
            if (m_rateControl != null) m_rateControl.Dispose();
            if (m_videoControl != null) m_videoControl.Dispose();
            if(m_mediaEngine != null)
            {
                m_mediaEngine.Stop();
                m_mediaEngine.Close();
                m_mediaEngine.Dispose();
            }
        }            
        
        /// <summary>
        /// Set the playback rate.
        /// </summary>
        private bool SetRate(float rate)
        {
            try
            {
                if (m_rateControl != null) 
                {
                    if (m_rateControl.IsRateSupported(rate))
                    {
                        m_rateControl.Rate = rate;
                        return true;
                    }
                    else return false;
                }
                else return false;
            }
            catch // (Exception e)
            {
                // Optional diagnostics.
                //System.Diagnostics.Debug.WriteLine("Exception " + e.Message + " in SetRate");
                return false;
            }
        }
        
        /// <summary>
        /// Set the display window dimensions to the correct aspect ratio.
        /// </summary>
        private void SetScreen(VideoControl videoControl, PictureBox pictureBox, int maxHeight, int maxWidth)
        {
            VideoMediaType videoMediaType;
            float aspectRatio;
            float defaultAspectRatio;

			videoMediaType = videoControl.MediaType as VideoMediaType;
			aspectRatio = (float) videoMediaType.Width / videoMediaType.Height;
            defaultAspectRatio = (float) maxWidth / maxHeight;
            if (aspectRatio > defaultAspectRatio)
            {
                pictureBox.Height = (int) (maxWidth / aspectRatio);
                pictureBox.Width = maxWidth;
            }
            else 
            {
                pictureBox.Width = (int) (maxHeight * aspectRatio);
                pictureBox.Height = maxHeight;
            }
            videoControl.DestinationPosition = pictureBox.ClientRectangle;
        }

        /// <summary>
        /// Move the trackbar to show the current position within the presentation.
        /// </summary>
        private void AdjustTracker()
        {
            double scale;
            scale = m_mediaEngine.PresentationTime.TotalMilliseconds / m_duration * tbPosition.Maximum;
            if ((int)scale <= tbPosition.Maximum)
            {
                tbPosition.Value = (int) scale;
            }
        }
        
        /// <summary>
        /// Get volume and mute controls for the open media.
        /// </summary>
        private void GetVolumeControl()
        {
            try
            {
                m_volumeControl = m_mediaEngine.GetService(typeof(IAudioVolumeControl)) as IAudioVolumeControl;
            }
            catch
            {
                // Optional diagnostics.
                // System.Diagnostics.Debug.WriteLine("Could not get volume control");
                m_volumeControl = null;
            }
            try
            {
                m_audioMuteControl = m_mediaEngine.GetService(typeof(IAudioMuteControl)) as IAudioMuteControl;
            }
            catch
            {
                // Optional diagnostics.
                // System.Diagnostics.Debug.WriteLine("Could not get mute control");
                m_audioMuteControl = null;
            }
        }
 
       
        /// <summary>
        /// Get rate control for the open media.
        /// </summary>
        private void GetRateControl()
        {
            try
            {
                m_rateControl = RateControl.GetRateControl(m_mediaEngine);
            }
            catch
            {
                // Optional diagnostics.
                // System.Diagnostics.Debug.WriteLine("Could not get rate control");
                m_rateControl = null;
            }
        }
        /// <summary>
        /// Get video control for the open media.
        /// </summary>
        private void GetVideoControl()
        {
            try
            {
                m_videoControl = VideoControl.GetVideoControl(m_mediaEngine);
            }
            catch
            {
                // Optional diagnostics.
                // System.Diagnostics.Debug.WriteLine("Could not get video control");
                m_videoControl = null;
            }
        }

        /// <summary>
        /// Update the appearance of the pause button depending on whether
        /// the performance is paused.
        /// </summary>
        /// <param name="paused">True if the performance is paused.</param>
        private void SetPauseButtonColor(bool paused)
        {
            if (paused)
            {
                btnPause.BackColor = SystemColors.ControlDark;
            }
            else
            {
                btnPause.BackColor = SystemColors.Control;
            }
            btnPause.Refresh();
        }

        /// <summary>
        /// Update UI when media stopped. Controls must be handled by the main thread, 
        /// so this gets Invoked by the event handlers.
        /// </summary>
        private void UpdateUIOnStop()
        {
            btnPause.Enabled = false;
            SetPauseButtonColor(false);
            btnPlay.Text = "&Start";
            playerState = PlayerState.stopped;
            tbPosition.Value = tbPosition.Minimum; 
        }

        /// <summary>
        /// Update UI when media started. Controls must be handled by the main thread, 
        /// so this gets Invoked by the event handlers.
        /// </summary>
        private void UpdateUIOnStart()
        {
            btnPlay.Text = "&Stop";
            btnPause.Enabled = true;
            SetPauseButtonColor(false);
            playerState = PlayerState.playing;
        }
        

        #endregion


        #region Media event handlers

        /// <summary>
        /// Engine is open. This event is fired even if the engine could
        /// not open the file, and we need to check the arguments to 
        /// ensure we can play.
        /// </summary>
        private void MediaOpened(Object sender, MediaEventArgs args)
        {
            m_validFile = (args.Exception == null); 
            engineIsOpenEvent.Set();
            // Optional diagnostics.
            // System.Diagnostics.Debug.WriteLine("MediaOpened fired.");
        }


        /// <summary>
        /// Engine has started.
        /// </summary>
        private void MediaStarted(Object sender, MediaEventArgs args)
        {
			if (args.Exception != null)
			{
				System.Diagnostics.Debug.WriteLine("MediaStarted failed: " + args.Exception.Message);
			}

			// Update UI. This must be done on the main thread.
            MethodInvoker mi = new MethodInvoker(this.UpdateUIOnStart);
            this.BeginInvoke(mi);

            // We need to set a private event when restarting the timer after
            // moving the slider. This is to make sure the timer doesn't 
            // set the position thumb until playback has restarted at the
            // new position.
            if (null != engineHasStartedEvent)
            {
                engineHasStartedEvent.Set();
            }
            // Optional diagnostics.
            // System.Diagnostics.Debug.WriteLine("MediaStarted fired.");
        }


        /// <summary>
        /// Engine has stopped.
        /// </summary>
        private void MediaStopped(Object sender, MediaEventArgs args)
        {
            // Update UI. This has to be done on the main thread.
            MethodInvoker mi = new MethodInvoker(this.UpdateUIOnStop);
            this.BeginInvoke(mi);
            // Optional diagnostics.
            // System.Diagnostics.Debug.WriteLine("MediaStopped fired.");
        }


        /// <summary>
        /// End of file reached. 
        /// Note: No MediaStop event is fired in this case.
        /// </summary>
        private void MediaEnded(Object sender, MediaEventArgs args)
        {
            // Update UI. This has to be done on the main thread.
            MethodInvoker mi = new MethodInvoker(this.UpdateUIOnStop);
            this.BeginInvoke(mi);
            // Optional diagnostics.
            // System.Diagnostics.Debug.WriteLine("MediaEnded fired.");
        }

        
        /// <summary>
        /// Engine has closed. Clean up global objects, which will be recreated when
        /// we start again.
        /// </summary>
        private void MediaClosed(Object sender, MediaEventArgs args)
        {
            if (m_volumeControl != null) m_volumeControl.Dispose();
            if (m_rateControl != null) m_rateControl.Dispose();
            if (m_videoControl != null) m_videoControl.Dispose();
            // Optional diagnostics.
            // System.Diagnostics.Debug.WriteLine("MediaClosed fired.");
        }

        #endregion

        
        #region Member variables

        private MediaEngine                     m_mediaEngine = null;
        string                                  m_fileName;
        private System.Threading.AutoResetEvent engineIsOpenEvent = null;
        private System.Threading.AutoResetEvent engineHasStartedEvent = null;
        private IAudioVolumeControl             m_volumeControl = null;
        private IAudioMuteControl               m_audioMuteControl = null;
        private RateControl                     m_rateControl = null;
        private VideoControl                    m_videoControl = null;
        private bool                            m_validFile = false;
        private Int64                           m_duration;
        private int                             m_maxScreenHeight;
        private int                             m_maxScreenWidth;
        private enum                            PlayerState {playing, paused, stopped};
        private PlayerState                     playerState = PlayerState.stopped;

        private System.Windows.Forms.TrackBar tbPosition;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.StatusBar statusBar1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuFile;
        private System.Windows.Forms.MenuItem menuFileOpen;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TrackBar tbVolume;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown upDownRate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbMute;
        private System.Windows.Forms.MenuItem menuFileExit;

        #endregion
        
       
        #region Menu handlers

        /// <summary>
        /// Get a file name from the user and attempt to open it with the engine.
        /// </summary>
        private void menuFileOpen_Click(object sender, System.EventArgs e)
        {
            if (DialogResult.OK == openFileDialog1.ShowDialog())
            {
                if (false == OpenEngine(openFileDialog1.FileName))
                {
                    MessageBox.Show("Cannot play that file.");
                }

                // Persist the default directory.
                string directory = System.IO.Path.GetDirectoryName(openFileDialog1.FileName);
                if (directory != String.Empty) 
                {
                    openFileDialog1.InitialDirectory = directory;
                }
            }
        }

        
        /// <summary>
        /// Exit.
        /// </summary>
        private void menuFileExit_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        
        #region Form control handlers

        /// <summary>
        /// Play/Stop button.
        /// </summary>
        private void btnPlay_Click(object sender, System.EventArgs e)
        {
            if (playerState != PlayerState.stopped)   // Stop if playing or paused.
            {
                m_mediaEngine.Stop();
                btnPlay.Text = "&Play";
            }
            else                                      // Start if stopped.
            {
                // Set the current volume from the slider.
                tbVolume_Scroll(null, null);  

                // Ensure no trackbar adjustment till Start complete.
                playerState = PlayerState.stopped;

                // Start playing
                m_mediaEngine.Start(TimeSpan.Zero, TimeSpan.Zero);
                timer1.Enabled = true;                
            }
        }


        /// <summary>
        /// Pause button.
        /// </summary>
        private void btnPause_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (playerState == PlayerState.paused)
                {
                    m_mediaEngine.Start();
                    playerState = PlayerState.playing;
                    SetPauseButtonColor(false);
                }
                else
                {
                    m_mediaEngine.Pause();  
                    playerState = PlayerState.paused;
                    SetPauseButtonColor(true);
                }
            }
            catch 
            {
                // Optional diagnostics.
                // System.Diagnostics.Debug.WriteLine("Could not pause.");
            }
        }

        /// <summary>
        /// Click on time slider.
        /// Stop the timer so the thumb is released.
        /// Note: to keep things simple, we don't support keyboard input on this slider.
        /// </summary>
        private void tbPosition_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            timer1.Enabled = false;
        }


        /// <summary>
        /// Release click on time slider. 
        /// Start media from new position and restart timer.
        /// </summary>
        private void tbPosition_MouseUp(object sender, MouseEventArgs e)
        {
            TimeSpan startTime;
            double scale;

            scale = (double) tbPosition.Value / (double) tbPosition.Maximum;
            startTime =  TimeSpan.FromMilliseconds(scale * m_duration);

            // Pause is going to be canceled by Start, so reset the button.
            if (playerState == PlayerState.paused)
            {
                SetPauseButtonColor(false);
            }
 
            engineHasStartedEvent = new System.Threading.AutoResetEvent(false);
            m_mediaEngine.Start(startTime, TimeSpan.Zero);

            // Wait for Start to finish before enabling the timer, so thumb doesn't jump back.
            engineHasStartedEvent.WaitOne();
            engineHasStartedEvent.Close();
            engineHasStartedEvent = null;
            timer1.Enabled = true;
        }

        /// <summary>
        /// Timer tick. Adjust the timeline thumb.
        /// </summary>
        private void timer1_Tick(object sender, System.EventArgs e)
        {
            if (playerState == PlayerState.playing)
            {
                AdjustTracker();
            }
        }

        /// <summary>
        /// Mute/unmute.
        /// </summary>
        private void cbMute_CheckedChanged(object sender, System.EventArgs e)
        {
            if (null != m_audioMuteControl)
            {
                m_audioMuteControl.Mute = cbMute.Checked;
            }
        }

        /// <summary>
        /// Volume trackbar scroll.
        /// </summary>
        private void tbVolume_Scroll(object sender, System.EventArgs e)
        {
            if (null != m_volumeControl)
            {                
                AudioVolume volume = AudioVolume.FromAmplitudeScalar((float) tbVolume.Value / tbVolume.Maximum);
                m_volumeControl.Volume = volume; 
            }
        }

        /// <summary>
        /// Change the rate.
        /// The sample supports only forward rates. To run in reverse, you have
        /// to stop and restart the media engine.
        /// </summary>
        private void upDownRate_ValueChanged(object sender, System.EventArgs e)
        {
            if (null != m_rateControl)
            {
                float rate = (float) upDownRate.Value;
                SetRate(rate);
            }
        }

        #endregion

        /// <summary>
        /// Clean up on exit.
        /// </summary>
        private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Cleanup();
        }

        /// <summary>
        /// Initialize the UI.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, System.EventArgs e)
        {
            EnableUI(false);
            //openFileDialog1.InitialDirectory = System.Environment.ExpandEnvironmentVariables(@"%WINDIR%\Media");
        }

    }
}



