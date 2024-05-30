namespace DebugHelp
{
    using System.Runtime.InteropServices;

    public class Trace
    {
        [DllImport("kernel32.dll", EntryPoint = "OutputDebugStringW", CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern void OutputDebugString(string msg);

        public static void Message(string msg)
        {
            OutputDebugString(msg + "\n");
        }
    }
}

namespace Surfin
{
    using System;
    using System.Threading;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Shapes;
    using System.Windows.Media;
    using System.Windows.Media.Animation;

    public partial class ChannelBrowser
    {
        private const int FAVORITES_PANEL_HEIGHT = 200;
        private const int FAVORITES_BUTTON_RIGHT = 50;
        private const int FAVORITES_BUTTON_BOTTOM = 0;
        private const int FAVORITES_BACKGROUND_WIDTH = 100;

        private const int LISTITEM_HEIGHT = 86;
        private const int PREVIEW_WIDTH = 160;
        private const int PREVIEW_HEIGHT = 120;

        private const int CCP_WIDTH = 350;
        private const int CCP_HEIGHT = 200;

        private bool _previewVisible = false;
        private bool _zoomingToMainScreen = false;
        private double _favoritesTitleX = FAVORITES_BUTTON_RIGHT;
        private double _favoritesTitleY = FAVORITES_BUTTON_BOTTOM;

        private Listings[] _favoritesListings = { null, null, null, null, null };

        private void PageLoaded(object sender, EventArgs args)
        {
            // set preview dimensions
            previewScreen.Width = new Length(PREVIEW_WIDTH);
            previewScreen.Height = new Length(PREVIEW_HEIGHT);

            // set event handlers for hotspots
            InitializeListItems(channelsListBox);

            // select initial channel
            channelsListBox.SelectedIndex = 0;  // Known issue: this should highlight selection
        }

        private UIElement FindParentOfListItems(UIElement el)
        {
            foreach (object o in ((IVisual)el).Children)
            {
                if (o is ListItem)
                    return el;
                else
                {
                    if (o is UIElement)
                    {
                        UIElement Parent = FindParentOfListItems(o as UIElement);

                        if (Parent != null)
                            return Parent;
                    }
                }
            }

            return null;
        }

        private Border FindHotSpot(UIElement el)
        {
            foreach (object o in ((IVisual)el).Children)
            {
                if ((o is Border) && (o as Border).ID.ToLower() == "hotspot")
                    return (o as Border);
                else
                {
                    if (o is UIElement)
                    {
                        Border hotspot = FindHotSpot(o as UIElement);

                        if (hotspot != null)
                            return hotspot;
                    }
                }
            }

            return null;
        }

        private object InitializeListItems(object lb)
        {
            // find the control within the listbox that is the direct parent of the ListItems
            UIElement parent = FindParentOfListItems(lb as ListBox);

            if (parent == null)
            {
                // this will only occur if the visual tree does not yet contain
                // the databound list items.  in that case, try again in half a second
                ThreadPool.QueueUserWorkItem(new WaitCallback(DelayInitializeListItems), lb);
                return null;
            }

            // enumerate through list items and set events on hotspots
            int index = 0;

            foreach (ListItem li in ((IVisual)parent).Children)
            {
                Border b = FindHotSpot(li);

                if (b == null)
                {
                    // this will only occur if the visual tree does not yet contain
                    // the hotspot border item.  in that case, try again in half a second
                    ThreadPool.QueueUserWorkItem(new WaitCallback(DelayInitializeListItems), lb);
                    return null;
                }

                // set IsMouseOverChanged event on border
                b.IsMouseOverChanged += 
                        new DependencyPropertyChangedEventHandler(MouseOverHotspotChanged);

                // rename border ID to include listitem index
                b.ID = "hotspot" + index.ToString();
                index++;
            }

            // set Tag property of all Channels to their respective index values
            for (index = 0; index < (lb as ListBox).Items.FlatView.Count; index++)
                ((lb as ListBox).Items.FlatView[index] as Channel).Tag = index;

            // load each user's favorites list
            InitializeFavorites();

            return null;
        }

        private void DelayInitializeListItems(object lb)
        {
            // temporary hack to initialize listitems
            //
            // since I don't know when they will be added to the tree, I call the
            // InitializeListItems every half second until the operation succeeds
            Thread.Sleep(500);
            (lb as ListBox).Context.BeginInvoke(new UIContextOperationDelegate(InitializeListItems), lb);
        }

        private object ResizeFavoriteItems(object o)
        {
            // find the control within the listbox that is the direct parent of the ListItems
            UIElement parent = FindParentOfListItems(favoritesListBox);

            if (parent == null)
            {
                // this will only occur if the visual tree does not yet contain
                // the databound list items.  in that case, try again shortly
                ThreadPool.QueueUserWorkItem(new WaitCallback(DelayResizeFavoriteItems), null);
                return null;
            }
            
            // enumerate through list items and set widths and docking
            foreach (ListItem li in ((IVisual)parent).Children)
            {
                li.Width = new Length(240);
                DockPanel.SetDock(li, Dock.Left);
            }

            // set initial selection to match channel currently playing
            for (int index = 0; index < favoritesListBox.Items.FlatView.Count; index++)
                if ((int) ((favoritesListBox.Items.FlatView[index] as Channel).Tag) == channelsListBox.SelectedIndex)
                    favoritesListBox.SelectedIndex = index;

            return null;
        }

        private void DelayResizeFavoriteItems(object o)
        {
            // temporary hack to resize listitems
            //
            // since I don't know when they will be added to the tree, I call the
            // ResizeFavoriteItems every few milliseconds until the operation succeeds
            Thread.Sleep(50);
            favoritesListBox.Context.BeginInvoke(new UIContextOperationDelegate(ResizeFavoriteItems), null);
        }

        private Listings NewFavoritesForUser(string name, int[] channels)
        {
            Listings favorites;
            favoritesComboBox.Items.Add(name);
            favorites = new Listings(name);
            for (int index = 0; index < channels.Length; index++)
                if (channels[index] < channelsListBox.Items.FlatView.Count)
                    favorites.Channels.Add(channelsListBox.Items.FlatView[channels[index]] as Channel);
            return favorites;
        }

        private void InitializeFavorites()
        {
            int[] channels0 = { 0, 2 };
            _favoritesListings[0] = NewFavoritesForUser("Jeff's", channels0);

            int[] channels1 = { 0, 4 };
            _favoritesListings[1] = NewFavoritesForUser("Jonathan's", channels1);

            int[] channels2 = { 3 };
            _favoritesListings[2] = NewFavoritesForUser("Kevin's", channels2);

            int[] channels3 = { 1, 3, 4 };
            _favoritesListings[3] = NewFavoritesForUser("Nathan's", channels3);
            
            int[] channels4 = { 0, 1, 2, 3 };
            _favoritesListings[4] = NewFavoritesForUser("Robert's", channels4);

            // Create fake channel
            Channel ppv = new Channel("Pay Per View", "PDC 2003 Keynote", "");
            ppv.Tag = -1;
            _favoritesListings[0].Channels.Add(ppv);
            
            favoritesComboBox.SelectedIndex = 0;
        }

        private void AnimateOpacity(UIElement el, double from, double to, int duration)
        {
            DoubleAnimation opacityAnim = new DoubleAnimation(from, to, duration, TimeFill.Auto);

            opacityAnim.Begin = Time.Immediately;

            el.AddAnimation(UIElement.OpacityProperty, opacityAnim);
            el.Opacity = to;
        }

        void SetMainVideoChannel(int index)
        {
            // set source on main video
            if (mainVideo.Source != (channelsListBox.Items.FlatView[index] as Channel).VideoData)
            {
                (channelsListBox.Items.FlatView[index] as Channel).VideoData.StatusOfNextUse = UseStatus.ChangeableReference;
                mainVideo.Source = (channelsListBox.Items.FlatView[index] as Channel).VideoData;
            }

            // show main screen fully opaque
            mainScreen.Opacity = 1;
        }

        void ChangeChannel(object sender, SelectionChangedEventArgs args)
        {
            if (contentControlPanel.Visibility == Visibility.Visible)
                ScaleContentControlPanel(1, 0.01, 250);
            
            // if the preview for this channel is showing, zoom it up to the main screen
            if (_previewVisible 
                    && previewVideo.Source == (channelsListBox.Items.FlatView[channelsListBox.SelectedIndex] as Channel).VideoData)
            {
                // hide main screen
                AnimateOpacity(mainScreen, 0.4F, 0.01F, 250);

                // animate flyout of preview; the animation ended event will change the channel
                ZoomPreviewToMainScreen();
            }
            else // else, just switch channels
            {
                // a preview may be showing for another channel, so turn it off
                if (_previewVisible)
                    TurnPreviewOff();

                // set the correct channel
                SetMainVideoChannel(channelsListBox.SelectedIndex);

                // if main video and preview video are set to the same source,
                // reset properties of preview video.  otherwise, properties of
                // main video (such as volume) will be applied to the hidden 
                // preview video.
                if (mainVideo.Source == previewVideo.Source)
                {
                    previewVideo.Source = null;
                    previewVideo.Volume = 0;
                }
            }
        }

        private void MouseOverHotspotChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            if ((bool)args.NewValue)
                ShowPreview(sender, null);
            else
                HidePreview(sender, null);
        }

        private void ShowPreview(object sender, MouseEventArgs args)
        {
            // if another preview is already showing, don't even try to show this one
            if (_previewVisible)
                return;

            // determine which item the mouse is over
            int index = int.Parse((sender as Border).ID.Substring(7));

            // show the preview (if its not the video showing on the main screen)
            if (channelsListBox.SelectedIndex != index)
            {
                TurnPreviewOn(index);

                // dim main screen
                AnimateOpacity(mainScreen, 1, 0.4F, 250);
            }
        }

        private void HidePreview(object sender, MouseEventArgs args)
        {
            if (_previewVisible && !_zoomingToMainScreen)
            {
                TurnPreviewOff();

                // brighten main screen
                AnimateOpacity(mainScreen, 0.4F, 1, 250);
            }
        }

        private void TurnPreviewOn(int index)
        {
            // set preview source
            VideoData videoData = (channelsListBox.Items.FlatView[index] as Channel).VideoData;

            // Video data may be null when MediaFoundation bits are out of whack.
            if (videoData != null)
            {
                videoData.StatusOfNextUse = UseStatus.ChangeableReference;
                previewVideo.Source = videoData;
            }

            // position preview screen
            Canvas.SetTop(previewScreen, new Length(index * LISTITEM_HEIGHT));
            Canvas.SetRight(previewScreen, new Length(channelsListBox.Width.Value));

            // start zoom up
            ScalePreview(0.01, 1, 250);
        }

        private void TurnPreviewOff()
        {
            // start zoom down
            ScalePreview(1, 0.01, 250);
        }

        private void ScalePreview(double from, double to, int duration)
        {
            // create new animation
            DoubleAnimation previewScaleAnim = new DoubleAnimation(from, to, duration, TimeFill.Hold);
            previewScaleAnim.Deceleration = 1;
            previewScaleAnim.Begin = Time.Immediately;
            if (from < to)
                previewScaleAnim.Begun += new EventHandler(ScalePreviewUpBegun);
            else
                previewScaleAnim.Ended += new EventHandler(ScalePreviewDownEnded);

            previewScreen.Transform = new ScaleTransform(1, previewScaleAnim, 1, previewScaleAnim, new Point((previewScreen.Width.Value / 2), (previewScreen.Height.Value / 2)), PointAnimationCollection.Empty);
        }

        private void ScalePreviewUpBegun(object sender, EventArgs e)
        {
            _previewVisible = true;
            previewScreen.Opacity = 0.75F;
        }

        private void ScalePreviewDownEnded(object sender, EventArgs e)
        {
            previewScreen.Opacity = 0.01F;
            _previewVisible = false;
        }

        private void ZoomPreviewToMainScreen()
        {
            _zoomingToMainScreen = true;

            // create new animations
            DoubleAnimation previewZoomAnimX = new DoubleAnimation(1, mainVideo.Width.Value / previewVideo.Width.Value, 500, TimeFill.Hold);

            previewZoomAnimX.Begin = Time.Immediately;

            DoubleAnimation previewZoomAnimY = new DoubleAnimation(1, mainVideo.Height.Value / previewVideo.Height.Value, 500, TimeFill.Hold);

            previewZoomAnimY.Begin = Time.Immediately;

            // set up ended event handler
            previewZoomAnimX.Ended += new EventHandler(ZoomPreviewToMainScreenEnded);

            // apply animations to preview screen
            previewScreen.Transform = new ScaleTransform(1, previewZoomAnimX, 1, previewZoomAnimY, new Point(previewVideo.Width.Value, (Canvas.GetTop(previewScreen).Value + (previewVideo.Height.Value * (Canvas.GetTop(previewScreen).Value / mainVideo.Height.Value))) * (previewVideo.Height.Value / mainVideo.Height.Value)), PointAnimationCollection.Empty);
        }

        private void ZoomPreviewToMainScreenEnded(object sender, EventArgs e)
        {
            // set the correct channel
            SetMainVideoChannel(channelsListBox.SelectedIndex);

            // hide preview
            previewScreen.Opacity = 0.01F;
            _previewVisible = false;
            _zoomingToMainScreen = false;
        }

        private void ToggleFavoritesPanel(object sender, ClickEventArgs e)
        {
            bool show = (favoritesPanel.Height.Value == 0);

            // set initial favorites selection
            if (show)
            {
                favoritesListBox.DataContext = _favoritesListings[favoritesComboBox.SelectedIndex];

                // Known Issue: the ListItem style properties are 
                // not being honored so I reset them dynamically
                ResizeFavoriteItems(null);
            }

            if (show)
                RelocateTitlePanel(null, e);

            LengthAnimation la = (LengthAnimation)((Timeline)(favoritesPanel.GetLocalAnimations(Canvas.HeightProperty))[0]).Children[0].Children[show ? 0 : 1];
            la.BeginIn(show ? 0 : 500);

            DoubleAnimation da = (DoubleAnimation)((Timeline)favoritesListBox.GetLocalAnimations(Canvas.OpacityProperty)[0]).Children[0].Children[show ? 0 : 1];
            da.BeginIn(show ? 500 : 0);

            da = (DoubleAnimation)((Timeline)favoritesBackgroundRect.GetLocalAnimations(Rectangle.OpacityProperty)[0]).Children[0].Children[show ? 0 : 1];
            da.BeginIn(show ? 0 : 500);
            
            da = (DoubleAnimation)((Timeline)favoritesForegroundRect.GetLocalAnimations(Rectangle.OpacityProperty)[0]).Children[0].Children[show ? 0 : 1];
            da.BeginIn(show ? 500 : 0);
            
            la = (LengthAnimation)((Timeline)favoritesTitlePanel.GetLocalAnimations(Canvas.WidthProperty)[0]).Children[0].Children[show ? 0 : 1];
            la.BeginIn(show ? 500 : 0);
        }

        private void RelocateTitlePanel(object sender, EventArgs e)
        {
            bool show = (_favoritesTitleY == FAVORITES_BUTTON_BOTTOM);

            if (show)
            {
                _favoritesTitleX = (mainCanvas.Width.Value / 2) - favoritesTitlePanelRotateDecorator.Width.Value + FAVORITES_BUTTON_RIGHT;
                _favoritesTitleY = FAVORITES_PANEL_HEIGHT + 10;
            }
            else
                favoritesListBox.DataContext = null;

            DoubleAnimation da = new DoubleAnimation(0, show ? -360 : 360, 500);

            da.Begin = Time.CurrentGlobalTime;
            favoritesTitlePanelRotateDecorator.Transform = new RotateTransform(0, da, new Point(favoritesTitlePanelRotateDecorator.Width.Value / 2, favoritesTitlePanelRotateDecorator.Height.Value / 2), PointAnimationCollection.Empty);

            DoubleAnimation daX;

            daX = new DoubleAnimation(0, _favoritesTitleX * (show ? -1 : 1), 500);
            daX.Ended += new EventHandler(TitlePanelRelocated);
            daX.Begin = Time.CurrentGlobalTime;

            DoubleAnimation daY;

            daY = new DoubleAnimation(0, _favoritesTitleY * (show ? -1 : 1), 500);
            daY.Begin = Time.CurrentGlobalTime;
            favoritesTitlePanelTranslateDecorator.Transform = new TranslateTransform(0, daX, 0, daY);
            if (!show)
            {
                _favoritesTitleX = FAVORITES_BUTTON_RIGHT;
                _favoritesTitleY = FAVORITES_BUTTON_BOTTOM;
            }
        }

        private void TitlePanelRelocated(object sender, EventArgs e)
        {
            bool show = (_favoritesTitleY != FAVORITES_BUTTON_BOTTOM);

            Canvas.SetRight(favoritesTitlePanelTranslateDecorator, new Length(_favoritesTitleX + (show ? Canvas.GetRight(favoritesTitlePanelTranslateDecorator).Value : 0)));
            Canvas.SetBottom(favoritesTitlePanelTranslateDecorator, new Length(_favoritesTitleY + (show ? Canvas.GetBottom(favoritesTitlePanelTranslateDecorator).Value : 0)));
        }

        private void FavoritesPanelResizing(object sender, EventArgs e)
        {
            favoritesPanel.Height = new Length(favoritesPanel.Height.Value == 0 ? FAVORITES_PANEL_HEIGHT : 0);
            DebugHelp.Trace.Message(favoritesPanel.Height.Value.ToString());
        }

        private void FavoritesTitlePanelResizing(object sender, EventArgs e)
        {
            favoritesTitlePanel.Width = new Length(favoritesTitlePanel.Width.Value == FAVORITES_BACKGROUND_WIDTH ? FAVORITES_BACKGROUND_WIDTH * 2 : FAVORITES_BACKGROUND_WIDTH);
        }

        private void FavoritesListBoxFading(object sender, EventArgs e)
        {
            favoritesListBox.Opacity = (favoritesListBox.Opacity == 0 ? 1 : 0);
        }

        private void FavoritesBackgroundRectFading(object sender, EventArgs e)
        {
            favoritesBackgroundRect.Opacity = (favoritesBackgroundRect.Opacity == 0 ? 1 : 0);
        }

        private void FavoritesForegroundRectFading(object sender, EventArgs e)
        {
            favoritesForegroundRect.Opacity = (favoritesForegroundRect.Opacity == 0 ? 1 : 0);
        }

        private void SelectFavoriteChannel(object sender, SelectionChangedEventArgs args)
        {
            if (favoritesListBox.SelectedIndex >= 0)
            {
                int channelIndex = (int)(favoritesListBox.Items.FlatView[favoritesListBox.SelectedIndex] as Channel).Tag;
                if (channelIndex >= 0)
                    channelsListBox.SelectedIndex = channelIndex;
                else
                {
                    ScaleContentControlPanel(0.01, 1, 500);
                    
                    bool found = false;
                    for (int index = 0; index < favoritesListBox.Items.FlatView.Count; index++)
                        if ((int)((favoritesListBox.Items.FlatView[index] as Channel).Tag) == channelsListBox.SelectedIndex)
                        {
                            favoritesListBox.SelectedIndex = index;
                            found = true;
                            break;
                        }
                    if (!found)
                        favoritesListBox.SelectedIndex = -1;
                }
            }
        }

        private void BeginChangeFavorites(object sender, SelectionChangedEventArgs args)
        {
            // if favorites panel is showing
            if (favoritesPanel.Height.Value > 0)
            {
                // animate fade out of favorites.  when the animation completes, a new
                // set of favorites will be selected
                DoubleAnimation da = (DoubleAnimation)((Timeline)favoritesListBox.GetLocalAnimations(ListBox.OpacityProperty)[0]).Children[0].Children[2];
                da.BeginIn(0);
            }
        }

        private void EndChangeFavorites(object sender, EventArgs e)
        {
            if (favoritesPanel.Height.Value > 0)
            {
                // change data context on favorites ListBox
                favoritesListBox.DataContext = _favoritesListings[favoritesComboBox.SelectedIndex];
                ResizeFavoriteItems(null);

                // animate fade in of favorites
                DoubleAnimation da = (DoubleAnimation)((Timeline)favoritesListBox.GetLocalAnimations(ListBox.OpacityProperty)[0]).Children[0].Children[3];
                da.BeginIn(0);
            }
        }

        private void ScaleContentControlPanel(double from, double to, int duration)
        {
            // create new animation
            DoubleAnimation da = new DoubleAnimation(from, to, duration, TimeFill.Hold);
            da.Deceleration = 1;
            da.Begin = Time.Immediately;
            if (from < to)
            {
                Canvas.SetTop(contentControlPanel, new Length((mainScreen.Height.Value - CCP_HEIGHT) / 2));
                Canvas.SetLeft(contentControlPanel, new Length((mainScreen.Width.Value - CCP_WIDTH) / 2));
                contentControlPanel.Visibility = Visibility.Hidden;
                da.Begun += new EventHandler(ScaleCCPUpBegun);
            }
            else
                da.Ended += new EventHandler(ScaleCCPDownEnded);

            contentControlPanel.Transform = new ScaleTransform(1, da, 1, da, 
                    new Point((CCP_WIDTH / 2), (CCP_HEIGHT / 2)), PointAnimationCollection.Empty);
        }

        private void ScaleCCPUpBegun(object sender, EventArgs e)
        {
            contentControlPanel.Visibility = Visibility.Visible;
        }

        private void ScaleCCPDownEnded(object sender, EventArgs e)
        {
            contentControlPanel.Visibility = Visibility.Hidden;
        }

        private void HideCCP(object sender, ClickEventArgs e)
        {
            ScaleContentControlPanel(1, 0.01, 250);
        }
    }
}