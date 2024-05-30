namespace System.Windows.Desktop
{
    using System;
    using System.Windows.Controls;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Explorer;

    public sealed class StoryPanel : DockPanel
    {
        private RSSStory story;
        private HyperLink titleLink, storyLink;

        #region Colors
        private readonly Brush storyForeground = new SolidColorBrush(Color.FromRGB(0x68, 0x68, 0x67));
        private readonly Brush storyHighlight = new SolidColorBrush(Color.FromRGB(0x49, 0x7e, 0xc1));
        private readonly Brush titleForeground = new SolidColorBrush(Color.FromRGB(0x38, 0x53, 0x76));
        private readonly Brush titleHighlight = new SolidColorBrush(Color.FromRGB(0x2c, 0x73, 0xcf));
        private readonly Brush publishedForeground = new SolidColorBrush(Color.FromRGB(0xa8, 0xa8, 0xa8));
        #endregion Colors

        #region Fonts
        private static readonly string fontFamily = "Tahoma";
        private static readonly int fontSize = 8;
        #endregion Fonts

        #region Event Handlers
        private void OnClick(object sender, ClickEventArgs args)
        {
            // only allow HTTP links
            //if((sender as HyperLink).NavigateUri.Scheme == Uri.UriSchemeHttp)
            //{
            //    (new Item((sender as HyperLink).NavigateUri.ToString())).Verbs.Default.Invoke();
            //}
        }

        private void OnEnter(object sender, MouseEventArgs args)
        {
            this.titleLink.Foreground = titleHighlight;
            this.storyLink.Foreground = storyHighlight;
        }

        private void OnLeave(object sender, MouseEventArgs args)
        {
            this.titleLink.Foreground = titleForeground;
            this.storyLink.Foreground = storyForeground;
        }
        #endregion EventHandlers
        private static string Truncate(string text)
        {
            if(text.Length > 200)
            {
                int index = text.LastIndexOf(' ');
                // if there's a space at some reasonable length
                if(index > 100)
                {
                    text = text.Substring(0, index) + "...";
                }
                    // otherwise, just chop off in the middle of a word
                else
                {
                    text = text.Substring(0, 197) + "...";
                }
            }
            return text;
        }

        public StoryPanel(RSSStory story)
        {
            this.story = story;

            Text titleText = new Text();
            titleText.TextWrap = TextWrap.Wrap;
            titleText.TextContent = StoryPanel.Truncate(story.Title);
            
            this.titleLink = new HyperLink();
            this.titleLink.Content = titleText;
            this.titleLink.TextDecorations = null;
            this.titleLink.NavigateUri = new Uri(story.Link);
            this.titleLink.Foreground = titleForeground;
            this.titleLink.FontFamily = fontFamily;
            this.titleLink.FontSize = new FontSize(fontSize, FontSizeType.Point);
            this.titleLink.FontWeight = FontWeight.Bold;
            this.titleLink.Click += new ClickEventHandler(OnClick);
            this.titleLink.MouseEnter += new MouseEventHandler(OnEnter);
            this.titleLink.MouseLeave += new MouseEventHandler(OnLeave);
            this.titleLink.Cursor = System.Windows.Input.Cursor.Hand;
            SetDock(titleLink, Dock.Top);
            this.Children.Add(this.titleLink);

            Text storyText = new Text();
            storyText.TextContent = StoryPanel.Truncate(story.Description);
            storyText.TextWrap = TextWrap.Wrap;
            
            this.storyLink = new HyperLink();
            this.storyLink.Content = storyText;
            this.storyLink.TextDecorations = null;
            this.storyLink.NavigateUri = new Uri(story.Link);
            this.storyLink.Foreground = storyForeground;
            this.storyLink.FontFamily = fontFamily;
            this.storyLink.FontSize = new FontSize(fontSize, FontSizeType.Point);
            this.storyLink.Click += new ClickEventHandler(OnClick);
            this.storyLink.MouseEnter += new MouseEventHandler(OnEnter);
            this.storyLink.MouseLeave += new MouseEventHandler(OnLeave);
            this.storyLink.Cursor = System.Windows.Input.Cursor.Hand;
            SetDock(this.storyLink, Dock.Top);
            this.Children.Add(this.storyLink);

            if(story.PubDate != DateTime.MinValue)
            {
                Text publishedText = new Text();
                publishedText.TextWrap = TextWrap.Wrap;
                publishedText.TextContent = string.Format("Published: {0}", story.PubDate.ToString());
                publishedText.Foreground = publishedForeground;
                publishedText.FontFamily = fontFamily;
                publishedText.FontSize = new FontSize(fontSize, FontSizeType.Point);
                SetDock(publishedText, Dock.Top);
                this.Children.Add(publishedText);
            }
        }
    }
}
