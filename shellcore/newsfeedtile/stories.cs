namespace System.Windows.Desktop
{
    using System;
    using System.Windows.Controls;
    using System.Windows;
    using System.Windows.Media;
    using System.Threading;
    using System.Windows.Explorer;

    public sealed class Stories : DockPanel
    {

        #region Fonts
        private static readonly string fontFamily = "Tahoma";
        #endregion Fonts

        private void OnClick(object sender, ClickEventArgs args)
        {
            // only allow HTTP links
            //if((sender as HyperLink).NavigateUri.Scheme == Uri.UriSchemeHttp)
            //{
            //    (new Item((sender as HyperLink).NavigateUri.ToString())).Verbs.Default.Invoke();
            //}
        }
        
        public Stories(RSSFeed feed)
        {
            this.Background = Brushes.White;

            #region Title
            Text titleText = new Text();
            titleText.TextWrap = TextWrap.Wrap;
            titleText.TextContent = feed.Title;

            HyperLink titleLink = new HyperLink();
            titleLink.Content = titleText;
            titleLink.FontFamily = fontFamily;
            titleLink.FontSize = new FontSize(12, FontSizeType.Point);
            titleLink.Foreground = new SolidColorBrush(Colors.White);
            titleLink.TextDecorations = null;
            titleLink.Cursor = System.Windows.Input.Cursor.Hand;
            titleLink.NavigateUri = new Uri(feed.Link);
            titleLink.Click += new ClickEventHandler(OnClick);

/*
            Text toolTip = new Text();
            toolTip.TextWrap = TextWrap.Wrap;
            toolTip.Width = new Length(300);
            toolTip.TextContent = feed.Description;
            titleLink.ToolTip = toolTip;
*/

            Border gradientBorder = new Border();
            gradientBorder.BorderThickness = new Thickness(new Length(24), new Length(18), new Length(24), new Length(14));
            gradientBorder.Child = titleLink;

            LinearGradientBrush titleGradient = new LinearGradientBrush(Color.FromRGB(0x5a, 0x6b, 0x7d), Color.FromRGB(0x87, 0x99, 0xb1), new Point(0f, 0f), new Point(0.5f, 0f));
            titleGradient.SpreadMethod = GradientSpreadMethod.Reflect;

            DockPanel titlePanel = new DockPanel();
            titlePanel.Children.Add(gradientBorder);
            titlePanel.Background = titleGradient;

            SetDock(titlePanel, Dock.Top);
            this.Children.Add(titlePanel);
            #endregion Title

            DockPanel scrollContents = new DockPanel();
            foreach(RSSStory story in feed.Stories)
            {
                Border storyBorder = new Border();
                storyBorder.BorderThickness = new Thickness(new Length(0), new Length(0), new Length(0), new Length(14));
                storyBorder.Child = new StoryPanel(story);
                SetDock(storyBorder, Dock.Top);
                scrollContents.Children.Add(storyBorder);
            }
            #region ScrollViewer
            Border scrollBorder = new Border();
            scrollBorder.BorderThickness = new Thickness(new Length(24), new Length(24), new Length(24), new Length(0));
            scrollBorder.Child = scrollContents;

            ScrollViewer scrollViewer = new ScrollViewer();
            scrollViewer.ScrollerVisibilityY = ScrollerVisibility.Auto;
            scrollViewer.ScrollerVisibilityX = ScrollerVisibility.Hidden;
            scrollViewer.Content = scrollBorder;
            this.Children.Add(scrollViewer);
            #endregion ScrollViewer
        }
    }
}
