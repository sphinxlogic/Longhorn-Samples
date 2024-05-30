namespace System.Windows.Desktop
{
    using System;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;

    public sealed class Headlines : Canvas, IDisposable
    {
        #region IDisposable
        public void Dispose(bool disposing)
        {
            if(disposing)
            {
                timer.Stop();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion IDisposable

        private int storyOffset = 0; // how far around we are in rotating the headlines
        private DockPanel oldPanel, newPanel;

        private RSSFeed feed;
        public RSSFeed Feed
        {
            get
            {
                return feed;
            }
            set
            {
                feed = value;
                this.storyOffset = 0;
                Rotate(null);
            }
        }

        private UITimer timer;

        public object Rotate(object arg)
        {
            // Clear the animations from the previous pass through the function
            this.oldPanel.ClearLocalAnimations(UIElement.OpacityProperty);
            this.newPanel.ClearLocalAnimations(UIElement.OpacityProperty);

            this.Children.Remove(oldPanel);
            this.oldPanel = this.newPanel;
            this.oldPanel.IsEnabled = false;
            this.newPanel = new DockPanel();
            Canvas.SetLeft(this.newPanel, new Length(0));
            Canvas.SetTop(this.newPanel, new Length(0));

            // rotate the headlines
            for(int i = 0; i < this.feed.Stories.Count; i++)
            {
                int index = (i + this.storyOffset) % (this.feed.Stories.Count);
                HeadlinePanel headline = new HeadlinePanel(this.feed.Stories[index] as RSSStory);
                DockPanel.SetDock(headline, Dock.Bottom);
                this.newPanel.Children.Add(headline);
            }

            #region Cross-fade Animation
            
            DoubleAnimation fadeIn = new DoubleAnimation(0.0f, 1.0f, new Time(500), TimeFill.Freeze);
            DoubleAnimation fadeOut = new DoubleAnimation(1.0f, 0.0f, new Time(500), TimeFill.Freeze);

            fadeIn.Begin = Time.CurrentGlobalTime;
            fadeOut.Begin = fadeIn.Begin;

            // Add the new animations
            this.oldPanel.AddAnimation(UIElement.OpacityProperty, fadeOut);
            this.newPanel.AddAnimation(UIElement.OpacityProperty, fadeIn);
            
            this.Children.Add(this.newPanel);
            
            #endregion Cross-fade Animation

            // update our offset for next time
            this.storyOffset = (storyOffset + 1) % this.feed.Stories.Count;

            return null; // UIContextOperationDelegates must return an object
        }

        public void InvokeRotate(object sender, EventArgs args)
        {
            // we need the rotate to happen within the tile's UI context
            this.Context.BeginInvoke(new UIContextOperationDelegate(Rotate));
        }

        #region Event Handlers
        private void OnMouseEnter(object sender, MouseEventArgs args)
        {
            timer.Stop();
        }

        private void OnMouseLeave(object sender, MouseEventArgs args)
        {
            timer.Start();
        }
        #endregion Event Handlers

        public Headlines(RSSFeed feed)
        {
            this.feed = feed;

            this.oldPanel = new DockPanel();
            Canvas.SetLeft(this.oldPanel, new Length(0));
            Canvas.SetTop(this.oldPanel, new Length(0));
            this.Children.Add(this.oldPanel);
            this.newPanel = new DockPanel();
            Canvas.SetLeft(this.newPanel, new Length(0));
            Canvas.SetTop(this.newPanel, new Length(0));
            this.Children.Add(this.newPanel);

            Rotate(null);

            this.timer = new UITimer();
            this.timer.Tick += new EventHandler(InvokeRotate);
            this.timer.Interval = TimeSpan.FromMilliseconds(10000);
            this.timer.Start();

            this.MouseEnter += new MouseEventHandler(OnMouseEnter);
            this.MouseLeave += new MouseEventHandler(OnMouseLeave);
        }
    }
}
