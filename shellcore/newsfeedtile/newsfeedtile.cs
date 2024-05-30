namespace MySidebarTiles
{
    using System.Windows.Controls;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Desktop;
    using System.Threading;
    using System;
    
    using System.Diagnostics;

    public class NewsFeedTile : BaseTile
    {
        private string feedUrl;
        public string FeedUrl
        {
            get
            {
                return feedUrl;
            }
            set
            {
                feedUrl = value;
            }
        }

        private RSSFeed feed;
        private Headlines headlines;
        private Stories stories;
        private Thread workerThread;
        private UITimer timer;
        private TextBox urlBox;

        #region IDisposable
        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                timer.Stop();
                if(headlines != null)
                {
                    headlines.Dispose();
                }
                if(workerThread != null && workerThread.IsAlive)
                {
                    workerThread.Abort();
                }
            }
        }
        #endregion IDisposable

        #region Event Handlers
        private void OnRefresh(object sender, ClickEventArgs args)
        {
            AsyncDownload(null, null);
        }

        private void OnContextMenu(object sender, ContextMenuEventArgs args)
        {
            MenuItem refresh = new MenuItem();
            args.ContextMenu.Items.Add(refresh);
            refresh.Header = "Refresh";
            refresh.Click += new ClickEventHandler(OnRefresh);
        }

        private void OnOKClick(object sender, ClickEventArgs args)
        {
            if(this.feedUrl != this.urlBox.Text)
            {
                this.Sidebar.CloseProperties();
                this.feedUrl = this.urlBox.Text;
                AsyncDownload(null, null);
            }
        }

        private void OnUrlFocus(object sender, FocusChangedEventArgs args)
        {
            // if we're still in invitation mode
            if(this.urlBox.Foreground == Brushes.Gray)
            {
                this.urlBox.Foreground = Brushes.Black;
                this.urlBox.Text = "";
            }
            else
            {
                this.urlBox.SelectAll();
            }
        }

        private void OnUrlKeyDown(object sender, KeyEventArgs args)
        {
            if(args.TextInputKey == Key.Return)
            {
                OnOKClick(null, null);
            }
        }
        #endregion Event Handlers

        #region Feed Download
        private object UpdateFeed(object o)
        {
            this.stories = new Stories(feed);
            this.stories.Width = new Length(370);
            this.stories.Height = new Length(700);

            if(this.headlines == null)
            {
                this.headlines = new Headlines(feed);
                ((Border)this.Foreground).Child = this.headlines;
            }
            else
            {
                this.headlines.Feed = feed;
            }
            this.headlines.Height = new Length(72);

            return null;
        }

        private void DownloadFeed()
        {
            this.feed = new RSSFeed(this.feedUrl);
            Foreground.Context.BeginInvoke(new UIContextOperationDelegate(UpdateFeed));
        }

        private void AsyncDownload(object sender, EventArgs args)
        {
            this.workerThread = new Thread(new ThreadStart(DownloadFeed));
            this.workerThread.Start();
        }
        #endregion Feed Download

        public override FrameworkElement CreateFlyout()
        {
            return stories;
        }

        
        public override FrameworkElement CreateProperties()
        {
            #region GUI
            Border border = new Border();
            border.BorderThickness = new Thickness(new Length(10), new Length(10), new Length(20), new Length(10));

            DockPanel properties = new DockPanel();
            properties.Width = new Length(400);
            properties.Height = new Length(55);

            border.Child = properties;

            // OK button
            Button okButton = new Button();
            okButton.Height = new Length(25);
            okButton.Content = "OK";
            okButton.Click += new ClickEventHandler(OnOKClick);
            DockPanel.SetDock(okButton, Dock.Right);
            properties.Children.Add(okButton);

            // text box to enter feed URL
            this.urlBox = new TextBox();
            this.urlBox.Height = new Length(25);
            this.urlBox.GotFocus += new FocusChangedEventHandler(OnUrlFocus);
            this.urlBox.KeyDown += new KeyEventHandler(OnUrlKeyDown);
            DockPanel.SetDock(this.urlBox, Dock.Left);
            properties.Children.Add(urlBox);
            #endregion GUI
            if(this.feedUrl == null)
            {
                this.urlBox.Foreground = Brushes.Gray;
                this.urlBox.Text = "Enter an RSS feed URL.";
            }
            else
            {
                this.urlBox.Foreground = Brushes.Black;
                this.urlBox.Text = this.feedUrl;
            }

            return border;
        }

        public override void Initialize()
        {
            if(this.feedUrl == null)
            {
                this.feedUrl = @"http://msdn.microsoft.com/netframework/rss.xml";
            }

            this.feed = null;
            this.TooltipText = "News Feed Tile";
            this.HasProperties = true;
 
            this.Sidebar.ContextMenuEvent += new ContextMenuEventHandler(OnContextMenu);

            Border tileBorder = new Border();
            tileBorder.BorderThickness = new Thickness(new Length(0), new Length(10), new Length(12), new Length(10));
            this.Foreground = tileBorder;

            AsyncDownload(null, null);

            this.timer = new UITimer();
            this.timer.Tick += new EventHandler(AsyncDownload);
            this.timer.Interval = TimeSpan.FromMinutes(35);
            this.timer.Start();
        }
    }
}
