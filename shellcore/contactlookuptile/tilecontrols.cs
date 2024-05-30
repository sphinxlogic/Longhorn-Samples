namespace MySidebarTiles
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Globalization;

    /// <summary>
    /// Enumeration for option button values
    /// </summary>
    internal enum ContactOptionButtonValue
    {
        FindManager = 0,
        FindDirects = 1,
        FindPeers = 2,
    }
        
    #region QueryTextBox controls

    /// <summary>
    /// Base class for TextBoxes used by the Contact Lookup tile
    /// </summary>
    internal abstract class QueryTextBox : TextBox
    {
        private bool showHelpText;

        private const string textFontFamily = ContactLookupTile.textFontFamily;

        /// <summary>
        /// Gets or sets the value used to indicate whether the textbox should display help text.
        /// </summary>
        public bool ShowHelpText
        {
            get
            {
                return this.showHelpText;
            }
            set
            {
                // does new value match existing value?
                if (value != this.showHelpText)
                {
                    // update textbox state
                    this.Foreground = value ? Brushes.Gray : Brushes.Black;
                    this.Text = value ? "Type a contact to find" : "";

                    this.showHelpText = value;
                }
            }
        }

        /// <summary>
        /// QueryTextBox constructor
        /// </summary>
        /// <param name="showHelpText">True if the text box should initially display help text.</param>
        public QueryTextBox(bool showHelpText) : base()
        {
            DockPanel.SetDock(this, Dock.Top);
            this.BorderThickness = new Thickness(new Length(1));
            this.BorderBrush = Brushes.Black;
            this.FontFamily = QueryTextBox.textFontFamily;
            this.FontSize = new FontSize(8, FontSizeType.Point);

            this.ShowHelpText = showHelpText;
        }

        protected override void OnGotFocus(FocusChangedEventArgs e)
        {
            base.OnGotFocus(e);

            if (this.ShowHelpText)
            {
                // if we were showing help text, stop
                this.ShowHelpText = false;
            }
            else
            {
                // select the text already in the textbox
                this.SelectAll();
            }
        }

        protected override void OnLostFocus(FocusChangedEventArgs e)
        {
            base.OnLostFocus(e);

            if (0 == this.Text.Length)
            {
                // if text box was empty, put it back into "help" mode

                this.ShowHelpText = true;
            }
        }
    }


    /// <summary>
    /// QueryTextBox to be used in the flyout of the Contact Lookup tile
    /// </summary>
    internal sealed class FlyoutQueryTextBox : QueryTextBox
    {
        /// <summary>
        /// FlyoutQueryTextBox constructor
        /// </summary>
        public FlyoutQueryTextBox() : base(false)
        {

        }
    }


    /// <summary>
    /// QueryTextBox to be used in the tile body of the Contact Lookup tile
    /// </summary>
    internal sealed class TileBodyQueryTextBox : QueryTextBox
    {
        /// <summary>
        /// TileBodyQueryTextBox constructor
        /// </summary>
        public TileBodyQueryTextBox() : base(true)
        {

        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            // was a space entered? if so, execute workaround code that allows 
            //   the TextBox to accept spaces
            if (Key.Space == e.TextInputKey)
            {
                // Keystrokes with Unicode values are routed first as KeyDownEvents.
                // If the keystroke is unhandled, the system raises a TextInputEvent
                // with the appropriate Unicode value.
                //
                // The TextBox we're listening to here is the child of a Button.
                // When Key.Space gets routed, the Button will handle the initial
                // KeyDownEvent before it can be translated to a TextInputEvent,
                // and the TextBox will never receive the Unicode text matching Key.Space.
                //
                // As a workaround, we listen to the KeyDownEvent here, and raise our
                // own TextInputEvent when appropriate before the parent Button control
                // has a chance to handle the event.
                TextCompositionEventArgs textArgs = new TextCompositionEventArgs(e.Device, new TextComposition(InputManager.Current, " "));
                textArgs.SetRoutedEventID(TextCompositionManager.TextInputEventID);

                UIElement element = e.Source as UIElement;

                if (null != element)
                {
                    element.RaiseEvent(textArgs);
                }

                e.Handled = textArgs.Handled;
            }
        }
    }

    #endregion

    #region Flyout "list view" and "list item"

    /// <summary>
    /// List View component of the Contact Lookup tile's flyout.
    /// </summary>
    internal sealed class ContactListView : DockPanel
    {
        private static readonly Brush backgroundBrush = Brushes.White;

        private static readonly Color listItemHighlightColor = Color.FromARGB(0xFF, 0xFF, 0xEB, 0xCD);

        /// <summary>
        /// ContactListView constructor
        /// </summary>
        public ContactListView() : base()
        {
            DockPanel.SetDock(this, Dock.Top);

            this.Background = ContactListView.backgroundBrush;
        }

        /// <summary>
        /// Highlights the specified ContactListItem
        /// </summary>
        /// <param name="target">ContactListItem (inside this list view) to highlight.</param>
        public void HighlightItem(ContactListItem target)
        {
            foreach(UIElement element in this.Children)
            {
                // let's make sure we're dealing with a ContactListItem
                ContactListItem item = element as ContactListItem;

                if (null != item)
                {
                    item.ChildPanel.Background = ContactListView.backgroundBrush;
                }
            }

            // now paint the target
            target.ChildPanel.Background = new SolidColorBrush(ContactListView.listItemHighlightColor);            
        }
    }


    /// <summary>
    /// List item to be hosted in a ContactListView. 
    /// This is data-bound to a BaseContact object at initialization.
    /// </summary>
    internal sealed class ContactListItem : Button
    {
        private DockPanel childPanel = null;            // inner content for this button
        private ContactListView listViewParent = null;  // parent of this list item
        private int index;                              // index of this item in parent

        /// <summary>
        /// Gets the inner content for this button.
        /// </summary>
        public DockPanel ChildPanel
        {
            get
            {
                return this.childPanel;
            }
        }

        /// <summary>
        /// Gets the index of this list item in its parent ContactListView.
        /// </summary>
        public int ItemIndex
        {
            get
            {
                return index;
            }
        }

        private const string textFontFamily = ContactLookupTile.textFontFamily;

        /// <summary>
        /// ContactListItem constructor
        /// </summary>
        /// <param name="parent">Parent of this list item.</param>
        /// <param name="index">Index of this list item in its parent.</param>
        /// <param name="entry">BaseContact with contact information to bind to this list item.</param>
        public ContactListItem(ContactListView parent, int index, BaseContact entry) : base()
        {
            DockPanel.SetDock(this, Dock.Top);

            // initialize instance members
            this.listViewParent = parent;
            this.index = index;

            // create the text
            Text text = new Text();
            DockPanel.SetDock(text, Dock.Fill);
            text.Margin = new Thickness(new Length(12), new Length(2), new Length(2), new Length(2));
            text.FontFamily = ContactListItem.textFontFamily;
            text.FontSize = new FontSize(8, FontSizeType.Point);
            text.TextContent = entry.DisplayName;
            text.TextWrap = TextWrap.NoWrap;

            // create the child panel
            DockPanel childPanel = new DockPanel();
            DockPanel.SetDock(childPanel, Dock.Top);
            this.childPanel = childPanel;

            // add the text
            childPanel.Children.Add(text);


            // finally, squish the child panel into the button
            this.Style = null;
            ((IVisual) this).Children.Add(childPanel);
        }

        protected override void OnClick()
        {
            base.OnClick();

            this.listViewParent.HighlightItem(this);
        }
    }

    #endregion

    #region Flyout: DetailsPane and ContactDetailsItem

    /// <summary>
    /// Details pane used in the flyout of the Contact Lookup tile
    /// </summary>
    internal sealed class DetailsPane : DockPanel
    {
        /// <summary>
        /// Gets the child ContactDetailsItem of this DetailsPane. (Returns null if child is not a ContactDetailsItem.)
        /// </summary>
        public ContactDetailsItem Child
        {
            get
            {
                ContactDetailsItem item = null;

                if (this.Children.Count > 0)
                {
                    item = this.Children[0] as ContactDetailsItem;
                }

                return item;
            }
        }

        private static readonly Color gradientOuterColor = Color.FromARGB(0xFF, 0xD2, 0x64, 0x2A);
        private static readonly Color gradientInnerColor = Color.FromARGB(0xFF, 0xEB, 0x8B, 0x58);

        /// <summary>
        /// DetailsPane constructor
        /// </summary>
        public DetailsPane() : base()
        {
            DockPanel.SetDock(this, Dock.Top);

            // create gradientbrush for preview pane
            GradientStopCollection gradientStops = new GradientStopCollection();
            gradientStops.Add(new GradientStop(DetailsPane.gradientOuterColor, 0.0F));
            gradientStops.Add(new GradientStop(DetailsPane.gradientInnerColor, 0.5F));
            gradientStops.Add(new GradientStop(DetailsPane.gradientOuterColor, 1.0F));

            LinearGradientBrush gradientBrush = new LinearGradientBrush();
            gradientBrush.StartPoint = new Point(0, 0);
            gradientBrush.EndPoint = new Point(1, 0);
            gradientBrush.GradientStops = gradientStops;

            // set background
            this.Background = gradientBrush;
        }
    }

    /// <summary>
    /// Progress indicator control to be used in the flyout DetailsPane
    /// </summary>
    internal sealed class ProgressIndicator : DockPanel
    {
        private const int iconPanelWidthPixels = ContactLookupTile.iconPanelWidthPixels;
        private const string textFontFamily = ContactLookupTile.textFontFamily;

        /// <summary>
        /// Creates a ProgressIndicator control.
        /// </summary>
        /// <param name="progressIndicatorImageData">ImageData for icon's image resource.</param>
        public ProgressIndicator(ImageData progressIndicatorImageData) : base()
        {
            DockPanel.SetDock(this, Dock.Top);

            // prepare icon panel
            DockPanel iconPanel = new DockPanel();
            DockPanel.SetDock(iconPanel, Dock.Left);
            iconPanel.Width = new Length(ProgressIndicator.iconPanelWidthPixels);

            // get the image ready
            Image iconImage = new Image();
            DockPanel.SetDock(iconImage, Dock.Top);
            iconImage.Margin = new Thickness(new Length(33), new Length(16), new Length(6), new Length(16));
            iconImage.Source = progressIndicatorImageData;

            // prepare animation
            DoubleAnimationCollection collection = new DoubleAnimationCollection();
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 0;
            animation.To = 360;
            animation.Duration = new Time(2000);
            animation.RepeatDuration = Time.Indefinite;
            collection.Add(animation);

            // prepare transform
            RotateTransform transform = new RotateTransform();
            transform.AngleAnimations = collection;
            transform.Center = new Point(11, 11);

            // create decorator, and load image into decorator
            TransformDecorator decorator = new TransformDecorator();
            decorator.Transform = transform;
            decorator.Child = iconImage;

            // load icon (i.e. its decorator) into icon panel
            iconPanel.Children.Add(decorator);


            // prepare info panel
            DockPanel infoPanel = new DockPanel();
            DockPanel.SetDock(infoPanel, Dock.Fill);
                    
            Text text = new Text();
            DockPanel.SetDock(text, Dock.Top);
            text.FontFamily = ProgressIndicator.textFontFamily;
            text.FontSize = new FontSize(12, FontSizeType.Point);
            text.Foreground = Brushes.White;
            text.Margin = new Thickness(new Length(0), new Length(16), new Length(0), new Length(16));
            text.TextContent = "Searching ...";

            infoPanel.Children.Add(text);

            // finish master panel
            this.Children.Add(iconPanel);
            this.Children.Add(infoPanel);
        }
    }

    /// <summary>
    /// "Data" item to be hosted in a DetailsPane. 
    /// This is data-bound to a BaseContact object at initialization.
    /// </summary>
    internal sealed class ContactDetailsItem : DockPanel
    {
        private int index;
        private ImageData searchIconResource = null;
        private ImageData searchIconDisabledResource = null;
        
        /// <summary>
        /// Gets the index of the item (in the results list).
        /// </summary>
        public int ItemIndex
        {
            get
            {
                return this.index;
            }
        }

        private const string textFontFamily = ContactLookupTile.textFontFamily;
        private const int itemWidthPixels = ContactLookupTile.flyoutWidthPixels;
        private const int iconPanelWidthPixels = ContactLookupTile.iconPanelWidthPixels;
        
        private static readonly Color hyperLinkNormalColor = Colors.White;
        private static readonly Color hyperLinkDisabledColor = Color.FromARGB(0xFF, 0xFF, 0xC9, 0xAC);
        private static readonly Color hyperLinkHoverColor = Color.FromARGB(0xFF, 0xFF, 0xF1, 0x7D);

        /// <summary>
        /// Constructs a ContactDetailsItem
        /// </summary>
        /// <param name="entry">BaseContact object containing contact data to bind to this item.</param>
        /// <param name="index">Index of specified BaseContact object (in entries list).</param>
        /// <param name="onContactDetailsItemMenu">Event handler called when links are clicked.</param>
        /// <param name="contactImageResource">Image resource for the contact's icon/picture.</param>
        /// <param name="searchIconResource">Image resource for search icon.</param>
        /// <param name="searchIconDisabledResource">Image resource for disabled search icon.</param>
        public ContactDetailsItem(BaseContact entry, int index, ClickEventHandler onContactDetailsItemMenu, ImageData contactImageResource, ImageData searchIconResource, ImageData searchIconDisabledResource) : base()
        {
            this.index = index;
            this.searchIconResource = searchIconResource;
            this.searchIconDisabledResource = searchIconDisabledResource;
            
            DockPanel.SetDock(this, Dock.Top);

            // create the text for the name/alias
            Text nameText = new Text();
            DockPanel.SetDock(nameText, Dock.Top);
            nameText.FontFamily = ContactDetailsItem.textFontFamily;
            nameText.FontSize = new FontSize(12, FontSizeType.Point);
            nameText.Foreground = Brushes.White;
            nameText.Margin = new Thickness(new Length(0), new Length(16), new Length(2), new Length(0));
            nameText.TextWrap = TextWrap.NoWrap;
            
            if (0 != entry.Alias.Length)
            {
                nameText.TextContent = entry.DisplayName + " (" + entry.Alias + ")";
            }
            else
            {
                nameText.TextContent = entry.DisplayName;
            }


            // create the main text
            Text text = new Text();
            DockPanel.SetDock(text, Dock.Top);
            text.FontFamily = ContactDetailsItem.textFontFamily;
            text.FontSize = new FontSize(8, FontSizeType.Point);
            text.Foreground = Brushes.White;
            text.Margin = new Thickness(new Length(0), new Length(18), new Length(2), new Length(18));
            text.TextContent = "";
            text.TextWrap = TextWrap.NoWrap;
                                    
            if (0 != entry.Title.Length)
            {
                text.TextContent += entry.Title + "\n";
            }

            if (0 != entry.Department.Length)
            {
                text.TextContent += entry.Department + "\n";
            }

            text.TextContent += "\n";

            text.TextContent += entry.EmailAddress + "\n";
            text.TextContent += entry.PhoneNumber + "\n";

            if (0 != entry.Office.Length)
            {
                text.TextContent += entry.Office;
            }


            // now prepare final layout dockpanels

            // left sub panel (Icon Panel)
            DockPanel iconPanel = new DockPanel();
            DockPanel.SetDock(iconPanel, Dock.Left);
            iconPanel.Width = new Length(ContactDetailsItem.iconPanelWidthPixels);
            
            if (null != contactImageResource)
            {
                Image contactImage = new Image();
                contactImage.Source = contactImageResource;
                contactImage.Margin = new Thickness(new Length(24), new Length(16), new Length(0), new Length(0));
                iconPanel.Children.Add(contactImage);
            }
            
            // right sub panel (Info Panel)
            DockPanel infoPanel = new DockPanel();
            DockPanel.SetDock(infoPanel, Dock.Fill);
            infoPanel.Width = new Length(ContactDetailsItem.itemWidthPixels - ContactDetailsItem.iconPanelWidthPixels);
            // add the two text elements
            infoPanel.Children.Add(nameText);
            infoPanel.Children.Add(text);

            // parent panel
            DockPanel mainPanel = new DockPanel();
            DockPanel.SetDock(mainPanel, Dock.Top);
            mainPanel.Children.Add(iconPanel);
            mainPanel.Children.Add(infoPanel);

            this.Children.Add(mainPanel);

            if (entry.HasManager || entry.HasDirectReports)
            {
                // add the option buttons
                DockPanel menuPanel = new DockPanel();
                DockPanel.SetDock(menuPanel, Dock.Top);
                menuPanel.Margin = new Thickness(new Length(0), new Length(0), new Length(0), new Length(18));

                menuPanel.Children.Add(CreateSearchLink("Find Manager", entry.HasManager, (int) ContactOptionButtonValue.FindManager, onContactDetailsItemMenu));
                menuPanel.Children.Add(CreateSearchLink("Find Directs", entry.HasDirectReports, (int) ContactOptionButtonValue.FindDirects, onContactDetailsItemMenu));
                menuPanel.Children.Add(CreateSearchLink("Find Peers", entry.HasManager, (int) ContactOptionButtonValue.FindPeers, onContactDetailsItemMenu));

                this.Children.Add(menuPanel);
            }
        }

        /// <summary>
        /// Creates a search link (option button) to be used in this ContactDetailsItem
        /// </summary>
        /// <param name="label">String label of link.</param>
        /// <param name="enabled">True if hyperlink should be enabled.</param>
        /// <param name="id">ID of link.</param>
        /// <param name="onClick">Event handler called when link is clicked.</param>
        /// <returns>Search link matching requested criteria.</returns>
        private DockPanel CreateSearchLink(string label, bool enabled, int id, ClickEventHandler onClick)
        {
            DockPanel searchPanel = new DockPanel();
            DockPanel.SetDock(searchPanel, Dock.Top);
            searchPanel.Margin = new Thickness(new Length(40), new Length(2), new Length(2), new Length(2));

            if ((null != this.searchIconResource) && (null != this.searchIconDisabledResource))
            {
                Image searchIcon = new Image();
                DockPanel.SetDock(searchIcon, Dock.Left);
                searchIcon.Margin = new Thickness(new Length(0), new Length(0), new Length(6), new Length(0));
                searchIcon.Source = enabled ? this.searchIconResource : this.searchIconDisabledResource;

                searchPanel.Children.Add(searchIcon);
            }

            searchPanel.Children.Add(CreateHyperLink(label, enabled, id, onClick));
            
            return searchPanel;          
        }

        /// <summary>
        /// (Factory method) Creates a HyperLink.
        /// </summary>
        /// <param name="label">String label of link.</param>
        /// <param name="enabled">True if hyperlink should be enabled.</param>
        /// <param name="id">ID of link.</param>
        /// <param name="onClick">Event handler called when link is clicked.</param>
        /// <returns>HyperLink with requested criteria.</returns>
        private static HyperLink CreateHyperLink(string label, bool enabled, int id, ClickEventHandler onClick)
        {
            Text text = new Text();
            DockPanel.SetDock(text, Dock.Top);
            text.FontFamily = ContactDetailsItem.textFontFamily;
            text.FontSize = new FontSize(8, FontSizeType.Point);
            text.Foreground = new SolidColorBrush(enabled ? ContactDetailsItem.hyperLinkNormalColor: ContactDetailsItem.hyperLinkDisabledColor);
            text.TextContent = label;

            HyperLink link = new HyperLink();
            DockPanel.SetDock(link, Dock.Left);
            link.Content = text;
            link.ID = id.ToString(CultureInfo.InvariantCulture);
            link.IsEnabled = enabled;

            link.Click += new ClickEventHandler(onClick);
            link.MouseEnter += new MouseEventHandler(OnLinkEnter);
            link.MouseLeave += new MouseEventHandler(OnLinkLeave);

            return link;
        }

        private static void OnLinkEnter(object sender, MouseEventArgs e)
        {
            Text text = (Text) ((HyperLink) sender).Content;
            text.Foreground = new SolidColorBrush(ContactDetailsItem.hyperLinkHoverColor);
        }

        private static void OnLinkLeave(object sender, MouseEventArgs e)
        {
            HyperLink link = ((HyperLink) sender);
            Text text = (Text) link.Content;
            text.Foreground = new SolidColorBrush(link.IsEnabled ? ContactDetailsItem.hyperLinkNormalColor : ContactDetailsItem.hyperLinkDisabledColor);
        }
    }

    #endregion

    #region Tile Properties 

    /// <summary>
    /// Tile properties element
    /// </summary>
    internal sealed class TileProperties : DockPanel
    {
        private ListBox dataStoreListBox = null;
        private TextBox resultsLimitTextBox = null;

        private Button okButton = null;
        private Button cancelButton = null;

        private const string textFontFamily = ContactLookupTile.textFontFamily;

        /// <summary>
        /// Gets the data store list box in this properties element.
        /// </summary>
        public ListBox DataStoreListBox
        {
            get
            {
                return this.dataStoreListBox;
            }
        }


        /// <summary>
        /// Gets the results limit text box in this properties element.
        /// </summary>
        public TextBox ResultsLimitTextBox
        {
            get
            {
                return this.resultsLimitTextBox;
            }
        }

        /// <summary>
        /// Gets the OK button in this properties element
        /// </summary>
        public Button OKButton
        {
            get
            {
                return this.okButton;
            }
        }

        /// <summary>
        /// Gets the Cancel button in this properties element
        /// </summary>
        public Button CancelButton
        {
            get
            {
                return this.cancelButton;
            }
        }

        /// <summary>
        /// Creates a TileProperties element.
        /// </summary>
        /// <param name="dataStore">Startup value for data store.</param>
        /// <param name="resultsLimit">Startup value for results limit.</param>
        public TileProperties(ContactLookupTile.ContactStore dataStore, int resultsLimit) : base()
        {
            this.Height = new Length(220);
            this.Width = new Length(250);
         
            this.Children.Add(CreatePropertiesText("Contact store to use: "));

            // contact store listbox control
            ListBox dataStoreListBox = new ListBox();
            DockPanel.SetDock(dataStoreListBox, Dock.Top);
            dataStoreListBox.ID = "DataStoreListBox";
            
            // set the instance member
            this.dataStoreListBox = dataStoreListBox;
            
            ListItem myContactsListItem = new ListItem();
            myContactsListItem.Content = "My Contacts (WinFS)";
            dataStoreListBox.Items.Add(myContactsListItem);

            ListItem activeDirectoryListItem = new ListItem();
            activeDirectoryListItem.Content = "Active Directory";
            dataStoreListBox.Items.Add(activeDirectoryListItem);

            // now select the right one
            dataStoreListBox.SelectedIndex = (int) dataStore;

            // add list box to properties element
            this.Children.Add(dataStoreListBox);



            Text maxResultsHelpText = CreatePropertiesText("Maximum number of search results to display:");
            maxResultsHelpText.Margin = new Thickness(new Length(4), new Length(20), new Length(4), new Length(2));
            this.Children.Add(maxResultsHelpText);
            
            // "max results count" control
            TextBox maxResultsTextBox = new TextBox();
            DockPanel.SetDock(maxResultsTextBox, Dock.Top);
            maxResultsTextBox.ID = "ResultsLimitTextBox";
            maxResultsTextBox.Text = resultsLimit.ToString(CultureInfo.InvariantCulture);
            maxResultsTextBox.Width = new Length(40);
            
            // set the instance member
            this.resultsLimitTextBox = maxResultsTextBox;

            // add textbox to properties element
            // WORKAROUND: We nest the TextBox inside a GridPanel so that it has a finite height.
            GridPanel textBoxPanel = new GridPanel();
            DockPanel.SetDock(textBoxPanel, Dock.Top);
            textBoxPanel.Children.Add(maxResultsTextBox);
            this.Children.Add(textBoxPanel);



            // create dockpanel for the buttons
            DockPanel buttonPanel = new DockPanel();
            DockPanel.SetDock(buttonPanel, Dock.Top);
            buttonPanel.Margin = new Thickness(new Length(0), new Length(20), new Length(0), new Length(0));
            
            // cancel button
            Button cancelButton = new Button();
            DockPanel.SetDock(cancelButton, Dock.Right);
            cancelButton.Content = "Cancel";
            buttonPanel.Children.Add(cancelButton);         
            this.cancelButton = cancelButton;

            // ok button
            Button okButton = new Button();
            DockPanel.SetDock(okButton, Dock.Right);
            okButton.Content = "OK";
            buttonPanel.Children.Add(okButton);
            this.okButton = okButton;

            this.Children.Add(buttonPanel);
        }

        /// <summary>
        /// (Factory method) Creates a Text to use in this properties element.
        /// </summary>
        /// <param name="textString">String to use for this Text.</param>
        /// <returns>Text with supplied string.</returns>
        private static Text CreatePropertiesText(string textString)
        {
            Text text = new Text();
            DockPanel.SetDock(text, Dock.Top);
            text.FontSize = new FontSize(8, FontSizeType.Point);
            text.Foreground = Brushes.Black;
            text.FontFamily = TileProperties.textFontFamily;
            text.Margin = new Thickness(new Length(4), new Length(2), new Length(4), new Length(2));
            text.TextWrap = TextWrap.Wrap;
            text.TextContent = textString;
            
            return text;
        }           
    }

    #endregion
}
