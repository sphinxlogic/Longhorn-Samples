//****************************************************************************
//	FILE:			ImageNotification.cs
//
//	PURPOSE:		Demonstrate an application sending a Notification and 
//					handling the user click event on that Notification.
//
//	NOTES:			The actual Notification, once sent, resides on another 
//					thread. For this reason, it is important to keep the  
//					sending application alive so its notification delegate 
//					is still around to handle the event args returned when 
//					the user clicks an item on the notification. This sample 
//					uses an AutoResetEvent to let us know the user has clicked 
//					the notification. It also uses a timer to tell the 
//					application to shut down after there has been no user  
//					activity with the Notification UI. 
//****************************************************************************

namespace Sender
{
	using System;
	using System.Drawing;
	using System.Windows.Desktop;
	using System.ComponentModel;
	using System.Windows.Forms;
	using System.Windows.Media;
	using System.Runtime.InteropServices;
	using System.IO;
	using System.Threading;


	public class ShowNotification : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btn_Exit;

		//AutoResetEvent discussed in the notes above.
		private AutoResetEvent userClickedTheNotificationEvent = new AutoResetEvent(false);

		//Timer discussed in the notes above.
		private static System.Windows.Forms.Timer appTimer = new System.Windows.Forms.Timer();
		
		// A public flag used to notify the application that the user opted to view the image.
		private static bool ShowImageFlag = false;



		internal class MyNotification : Notification
		{
			public MyNotification(AutoResetEvent notificationClickedEvent)
			{
				notificationClicked = notificationClickedEvent;
				CreateImageNotification();
			}

			private AutoResetEvent notificationClicked;
			//	SendImageNotification
			//	Purpose:	Create and send the new notification. For the sample,
			//				the notification will have the following components.
			//				HeaderIcon
			//				HeaderText
			//				BodyText(Clickable)
			//				Yes button
			//				No Button
			private void CreateImageNotification()
			{
				// Set the notification title.
				string MessageHeaderText = "Message from my image editor";

				HeaderText = MessageHeaderText;

				// Create the main message text for the notification.
				string MessageBodyText = "The image is ready to be viewed full screen.  Would you like to view the image?";

				BodyText = MessageBodyText;

				// The message text can be set to clickable and the event ID can 
				// be trapped just like a button. The event ID is always 0 for the
				// Body Text.  
				IsBodyClickable = true;

				////Event handler for when the user clicks the Notification message body
				BodyClick += new NotificationClickedEventHandler(ImageReadyNotification_BodyClick);

				// Create a "Yes" button.
				NotificationButton btn_NotificationYes = new NotificationButton();

				btn_NotificationYes.Text = "Yes";

				//Event handler for the Yes button on the Notification
				btn_NotificationYes.Click += new NotificationClickedEventHandler(btn_NotificationYes_Click);

				AddButton(btn_NotificationYes);

				// Create a "No" button.
				NotificationButton btn_NotificationNo = new NotificationButton();

				btn_NotificationNo.Text = "No";

				//Event handler for the No button on the Notification
				btn_NotificationNo.Click += new NotificationClickedEventHandler(btn_NotificationNo_Click);

				AddButton(btn_NotificationNo);

				// Set the image you would like to be shown on the notification.
				ImageData NotificationImage = new ImageData(new FileStream(@"LH.ico", FileMode.Open, FileAccess.Read));

				HeaderIcon = NotificationImage;
			}

			// btn_MessageBody_Click event delegate
			// Purpose: Handel the case where the user clicks the Message body 
			private void ImageReadyNotification_BodyClick(object sender, NotificationClickedEventArgs e)
			{
				// Set the flag to show the user wishes to see the image
				ShowImageFlag = true;

				// Set the AutoResetEvent.  Lets us know that we are done waiting
				// for the users input to the notification.
				notificationClicked.Set();
			}

			// btn_Yes_Click event delegate
			// Purpose: Handel the case where the user clicks the yes button 
			private void btn_NotificationYes_Click(object sender, NotificationClickedEventArgs e)
			{
				// Set the flag to show the user wishes to see the image
				ShowImageFlag = true;

				// Set the AutoResetEvent.  Lets us know that we are done waiting
				// for the users input to the notification.
				notificationClicked.Set();
			}

			// btn_No_Click event delegate
			// Purpose: Handel the case where the user clicks the no button
			private void btn_NotificationNo_Click(object sender, NotificationClickedEventArgs e)
			{
				// Set the AutoResetEvent.  Lets us know that we are done waiting
				// for the users input to the notification.  
				notificationClicked.Set();
			}
		}


		// The notification sample main entry point.
		[STAThread]
		static void Main()
		{
			// Start the new application.
			System.Windows.Forms.Application.Run(new ShowNotification());

			// We are done. Exit the application.
			System.Windows.Forms.Application.Exit();
		}


		// Required designer variable.
		private System.ComponentModel.Container components = null;


		public ShowNotification()
		{
			// Setup the viewer.
			InitializeComponent();

			// Set up timer and event delegate.
			appTimer.Interval = 180000;  //3 min * 60 Sec * 1000ms

			appTimer.Enabled = true;

			appTimer.Tick += new EventHandler(appTimerDelegate);

			//Create a new instance of the notification to the user.
			Notification UserNotification = new MyNotification(userClickedTheNotificationEvent);

			//Send the user notification
			UserNotification.Send();

			// Listen for the notification to be clicked using the wait.
            userClickedTheNotificationEvent.WaitOne();

			// If the user clicked yes on the notification, show it Full screen image here.
			if(ShowImageFlag == true)
			{
				this.TopMost = true;

				this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

				this.Visible = true;

				this.Refresh();
			}
			else
			{
				// Shut down the application.
				System.Windows.Forms.Application.Exit();
			}
		}


		// Clean up any resources being used.
		protected override void Dispose(bool disposing)
		{
			if(disposing)
			{
				if(components != null)
				{
					components.Dispose();
				}
			}

			base.Dispose(disposing);
		}


		#region Windows Form Designer generated code
		public void InitializeComponent ()
		{
			this.btn_Exit = new System.Windows.Forms.Button ();

			this.SuspendLayout ();

			// 
			// btn_Exit
			// 
			this.btn_Exit.BackgroundImage = Image.FromFile("btnCover_Exit.JPG");

			//this.btn_Exit.BackgroundImage = new ImageData(new FileStream(@"btnCover_Exit.JPG", FileMode.Open, FileAccess.Read));

			this.btn_Exit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;

			this.btn_Exit.Location = new System.Drawing.Point (8, 8);

			this.btn_Exit.Name = "btn_Exit";

			this.btn_Exit.Size = new System.Drawing.Size (32, 32);

			this.btn_Exit.TabIndex = 0;

			this.btn_Exit.Click += new System.EventHandler (this.btn_Exit_Click);

			// 
			// The Image window  ShowNotification
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size (5, 13);

			this.BackgroundImage = Image.FromFile ("TaraAndNevarre.JPG");

			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;

			this.ClientSize = new System.Drawing.Size (696, 456);

			this.Controls.Add (this.btn_Exit);

			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

			this.MaximizeBox = false;

			this.MinimizeBox = false;

			this.Name = "ShowNotification";

			this.ShowInTaskbar = false;

			this.TopMost = false;

			this.Visible = false;

			this.WindowState = System.Windows.Forms.FormWindowState.Minimized;

			this.ResumeLayout (false);
		}
		#endregion


		//	btn_Exit_Click event delegate
		//	Purpose: The viewer is done looking at the full screen image
		private void btn_Exit_Click(object sender, System.EventArgs e)
		{
			// Let our viewer loop know it can quit.
			ShowImageFlag = false;

			// Remove the application from the view.  This looks better than waiting on the Dispose
			this.Visible = false;

			this.Refresh();

			this.WindowState = System.Windows.Forms.FormWindowState.Minimized;

			this.Refresh();

			// Shut down the application
			System.Windows.Forms.Application.Exit();
		}


		//	appTimerDelegate event delegate
		//	Purpose:	A certain amount of time has passed and the user has not interacted 
		//				with the Notification
		private static void appTimerDelegate(Object ThisObject, EventArgs eventArgs)
		{
			// Shut down the application
			System.Windows.Forms.Application.Exit();
		}
	}
}
