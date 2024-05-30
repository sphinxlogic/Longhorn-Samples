namespace System.Windows.Desktop
{
    using System.Net;
    using System;
    using System.Collections;
    using System.IO;
    using System.Windows.Controls;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Explorer;
    using System.Windows.Desktop;

    public sealed class HeadlinePanel : DockPanel
    {
        private HyperLink storyLink;
        private System.Windows.Controls.Image bullet;

        #region Fonts
        private static readonly string fontFamily = "Tahoma";
        private static readonly int fontSize = 8;
        #endregion Fonts

        #region Bullet Caching
        private static Hashtable bulletCache = new Hashtable();
        // A favicon is a web site's custom icon.  This shows up in the
        //   Internet Explorer address bar as well as the favorites menu.
        public static Image GetBullet(Uri uri)
        {
            Image img = new Image();
            img.Source = NativeResourceHelper.LoadImageData("imageres.dll", 6100);
            img.Width = new Length(16);
            img.Height = new Length(16);
            return img;

/*
            // http://host/favicon.ico
            string faviconpath = string.Format("{0}://{1}/favicon.ico", uri.Scheme, uri.Host);
            img.Source = HeadlinePanel.bulletCache[faviconpath] as ImageData;
            if(img.Source == null)
            {
                try
                {
                    Stream webStream = WebRequest.Create(faviconpath).GetResponse().GetResponseStream();
                    img.Source = new ImageData(webStream);
                    webStream.Close();
                }
                catch
                {
                    img.Source = NativeResourceHelper.LoadImageData("imageres.dll", 6100); // IDB_SB_NEWSFEED_BULLET
                }
                HeadlinePanel.bulletCache[faviconpath] = img.Source;
            }
            img.Width = new Length(16);
            img.Height = new Length(16);
            return img;
*/
        }
        #endregion Bullet Caching

        #region Event Handlers
        private void OnClick(object sender, ClickEventArgs args)
        {
            // only allow HTTP links
            //if((sender as HyperLink).NavigateUri.Scheme == Uri.UriSchemeHttp)
            //{
                // create a new shell item for the URL
            //    Item linkItem = new Item(((HyperLink)sender).NavigateUri.ToString());
                // invoke the default verb of the shell item (should be "open")
                //   to browse to the web page
            //    linkItem.Verbs.Default.Invoke();
            //}
        }

        private void OnLinkEnter(object sender, MouseEventArgs args)
        {
            // stop the rotate timer
            //(this.Parent as Headlines).Timer.Stop();
            (sender as HyperLink).Foreground = new SolidColorBrush(Color.FromRGB(0xff, 0xba, 0x0));
        }

        private void OnLinkLeave(object sender, MouseEventArgs args)
        {
            // restart the rotate timer
            //(this.Parent as Headlines).Timer.Start();
            (sender as HyperLink).Foreground = new SolidColorBrush(Colors.White);
        }
        #endregion Event Handlers

        public HeadlinePanel(RSSStory story)
        {
            #region Link
            Text content = new Text();
            content.TextWrap = TextWrap.Wrap;
            content.TextContent = story.Title + "\n\n";
            content.Opacity = 1.0f;

            this.storyLink = new HyperLink();
            this.storyLink.Opacity = 1.0f;
            this.storyLink.Content = content;
            this.storyLink.NavigateUri = new Uri(story.Link);
            this.storyLink.Foreground = new SolidColorBrush(Colors.White);
            this.storyLink.FontFamily = fontFamily;
            this.storyLink.FontSize = new FontSize(fontSize, FontSizeType.Point);
            this.storyLink.TextDecorations = null;
            this.storyLink.Click += new ClickEventHandler(OnClick);
            this.storyLink.MouseEnter += new MouseEventHandler(OnLinkEnter);
            this.storyLink.MouseLeave += new MouseEventHandler(OnLinkLeave);
            this.storyLink.Cursor = System.Windows.Input.Cursor.Hand;

            Border linkBorder = new Border();
            linkBorder.BorderThickness = new Thickness(new Length(5), new Length(0), new Length(0), new Length(0));
            linkBorder.Child = storyLink;
            #endregion Link

            #region Bullet
            this.bullet = GetBullet(this.storyLink.NavigateUri);
            SetDock(this.bullet, Dock.Left);
            Children.Add(this.bullet);
            SetDock(linkBorder, Dock.Left);
            Children.Add(linkBorder);
            #endregion Bullet

            #region Headline
            Text headlineText = new Text();
            headlineText.TextWrap = TextWrap.Wrap;
            headlineText.Foreground = Brushes.Black;
            headlineText.Width = new Length(300);
            headlineText.TextContent = string.Format("{0}\n\n", story.Title);
            headlineText.FontSize = new FontSize(fontSize, FontSizeType.Point);
            headlineText.FontWeight = FontWeight.Bold;
            headlineText.FontFamily = fontFamily;
            #endregion Headline

            #region Body
            Text bodyText = new Text();
            bodyText.TextWrap = TextWrap.Wrap;
            bodyText.Foreground = Brushes.Black;
            bodyText.Width = new Length(300);
            bodyText.FontSize = new FontSize(fontSize, FontSizeType.Point);
            bodyText.FontFamily = fontFamily;
            bodyText.TextContent = story.Description;
            #endregion Body

/*
            #region Tooltip
            DockPanel toolTip = new DockPanel();
            SetDock(headlineText, Dock.Top);
            toolTip.Children.Add(headlineText);
            SetDock(bodyText, Dock.Top);
            toolTip.Children.Add(bodyText);
            this.storyLink.ToolTip = toolTip;
            #endregion Tooltip
*/
        }
    }
}
