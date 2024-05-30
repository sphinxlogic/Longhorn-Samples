namespace MySidebarTiles
{
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System;
    using System.IO;
    using System.Collections;
    using System.Globalization;
    using System.Threading;
    using System.Windows.Desktop;

    /// <summary>
    /// Class for Contact Lookup tile
    /// </summary>
    public sealed class ContactLookupTile : BaseTile
    {
        #region Instance variables

        // UI elements  - tile body
        private QueryTextBox queryTextBox = null;

        // UI elements - tile flyout
        private DockPanel flyoutPanel = null;
        private QueryTextBox flyoutQueryTextBox = null;
        private ContactListView flyoutListView = null;
        private DetailsPane flyoutDetailsPane = null;

        // UI elements - properties dialog
        private TileProperties properties = null;

        // tile properties
        private ContactStore dataStore;
        private int resultsLimit;

        // query data
        private SearchType searchType;
        private string query;

        // thread that performs searches
        private Thread workerThread = null;

        // search results
        private ArrayList entries = null;

        // UI state variables
        private bool executeNewSearch;      // true if a search has been initiated
        private bool searchInProgress;      // true if search is in progress
        private bool flyoutOpen;            // true if flyout is currently open
        private bool searchCompleted;       // true if search is complete and results are displayed

        // used by PerformSearch to report errors
        private bool searchErrorOccured;
        private ApplicationException searchErrorException = null;

        // resources
        private ImageData genericContactImageData = null;
        private ImageData progressIndicatorImageData = null;
        private ImageData searchIconImageData = null;           
        private ImageData searchIconDisabledImageData = null;           

        #endregion

        #region Instance properties

        /// <summary>
        /// Gets or sets the contact store (to search)
        /// </summary>
        public ContactStore DataStore
        {
            get
            {
                return this.dataStore;
            }
            set
            {
                this.dataStore = value;
            }
        }

        /// <summary>
        /// Gets or sets the results limit count.
        /// </summary>
        public int ResultsLimit
        {
            get
            {
                return this.resultsLimit;
            }
            set
            {
                this.resultsLimit = value;
            }
        }

        #endregion

        #region Enumerations

        /// <summary>
        /// Enumeration for contact stores.
        /// </summary>
        [Serializable]
        public enum ContactStore
        {
            /// <summary>
            /// Search My Contacts (WinFS store)
            /// </summary>
            MyContacts = 0,
            /// <summary>
            /// Search Active Directory
            /// </summary>
            ActiveDirectory = 1
        }

        #endregion

        #region Constants

        internal const string textFontFamily = "Tahoma";

        internal const int flyoutWidthPixels = 260;
        internal const int iconPanelWidthPixels = 62;

        private const int defaultResultsLimit = 10;

        #endregion

        /// <summary>
        /// Default constructor for ContactLookupTile.
        /// We use a constructor to set defaults for tile settings. That way, I don't end up
        /// overwriting the data that may get loaded via tile serialization.
        /// </summary>
        public ContactLookupTile()
        {
            // initialize store to use
            this.dataStore = ContactStore.ActiveDirectory;

            // initalize max results count
            this.resultsLimit = ContactLookupTile.defaultResultsLimit;
        }

        /// <summary>
        /// Cleans up on dispose (BaseTile override)
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (null != this.workerThread)
                {
                    if (this.workerThread.IsAlive)
                    {
                        this.workerThread.Abort();
                    }
                }
            }
        }

        /// <summary>
        /// Initializes the tile (BaseTile override)
        /// </summary>
        public override void Initialize()
        {
            HasProperties = true;
            TooltipText = "Contact Lookup";

            string imageFolderPath = GetImageFolderPath();

            if (null != imageFolderPath)
            {
                try
                {
                    // attempt to load image resources
                    this.genericContactImageData = new ImageData(new FileStream(imageFolderPath + @"GenericContact.png", FileMode.Open, FileAccess.Read));
                    this.progressIndicatorImageData = new ImageData(new FileStream(imageFolderPath + @"ProgressIndicator.png", FileMode.Open, FileAccess.Read));
                    this.searchIconImageData = new ImageData(new FileStream(imageFolderPath + @"SearchIcon.png", FileMode.Open, FileAccess.Read));
                    this.searchIconDisabledImageData = new ImageData(new FileStream(imageFolderPath + @"SearchIconDisabled.png", FileMode.Open, FileAccess.Read));
                }
                catch (FileNotFoundException)
                {
                    // failed to load a resource - let's keep moving
                }
            }

            // initialize internal state
            this.executeNewSearch = false;
            this.searchErrorOccured = false;
            this.flyoutOpen = false;
            this.searchCompleted = false;

            // connect event handler
            Sidebar.FlyoutClosingEvent += new FlyoutClosingEventHandler(OnSidebarFlyoutClosing);

            // Create foreground.
            // WORKAROUND: We use GridPanel rather than DockPanel so that the TextBox
            //   gets a reasonable height.
            GridPanel tileElement = new GridPanel();
            
            QueryTextBox queryTextBox = new TileBodyQueryTextBox();
            // WORKAROUND: We give textbox a fixed width since it will not size correctly (horizontally)
            //   on its own inside the tile body.
            queryTextBox.Width = new Length(120);

            queryTextBox.LostFocus += new FocusChangedEventHandler(OnQueryTextBoxLostFocus);
            queryTextBox.KeyDown += new KeyEventHandler(OnQueryTextBoxKeyDown);

            tileElement.Children.Add(queryTextBox);
            this.queryTextBox = queryTextBox;

            Foreground = tileElement;
        }

        /// <summary>
        /// This event handler is especially important since it flags the flyout as closed 
        /// when the user closes the "progress" flyout prematurely. This way, CreateFlyout
        /// knows that it needs to create a new flyout to avoid an Avalon reparenting crash.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnSidebarFlyoutClosing(object sender, EventArgs args)
        {
            this.flyoutOpen = false;
        }

        private void OnQueryTextBoxLostFocus(object sender, FocusChangedEventArgs e)
        {
            TextBox queryTextBox = (TextBox) sender;

            if (this.queryTextBox.ShowHelpText)
            {
                // clear existing results set
                this.entries = null;

                // clear search state
                this.executeNewSearch = false;
            }
        }


        private void OnQueryTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            QueryTextBox queryTextBox = (QueryTextBox) sender;

            bool isFlyoutQueryTextBox = queryTextBox is FlyoutQueryTextBox;

            if (isFlyoutQueryTextBox)
            {
                // synchronize the two text boxes - update query text box
                this.queryTextBox.ShowHelpText = queryTextBox.ShowHelpText;
                this.queryTextBox.Text = queryTextBox.Text;
            }

            // if Enter was pressed, we need to begin the search
            if (Key.Return == e.TextInputKey)
            {
                e.Handled = true;

                Update(SearchType.Normal, queryTextBox.Text, isFlyoutQueryTextBox);
            }
        }

        /// <summary>
        /// Begins the search; builds the flyout (and displays a progress indicator), and then creates a thread
        /// to perform the search.
        /// </summary>
        /// <param name="searchType">Type of search to perform.</param>
        /// <param name="query">String query to use when searching directory</param>
        /// <param name="flyoutAlreadyOpen">True if flyout is already open.</param>
        private void Update(SearchType searchType, string query, bool flyoutAlreadyOpen)
        {
            if (0 != query.Length)
            {
                // if query is not empty, go search

                // signal to CreateFlyout that we are actually performing a search
                this.executeNewSearch = true;

                // signal to CreateFlyout that we need the activity indicator
                this.searchInProgress = true;

                // signal to CreateFlyout the current status of flyout
                this.flyoutOpen = flyoutAlreadyOpen;

                // signal to CreateFlyout that search is not complete
                this.searchCompleted = false;

                if (!flyoutAlreadyOpen)
                {
                    // we need a flyout

                    // create and show the flyout
                    Sidebar.ShowFlyout(null);

                    // we now have a flyout open
                    this.flyoutOpen = true;
                }
                else
                {
                    // update the flyout to show the progress indicator
                    CreateFlyout();
                }

                // initialize class members
                this.searchType = searchType;
                this.query = query;

                // launch worker thread to perform search
                this.workerThread = new Thread(new ThreadStart(PerformSearch));
                this.workerThread.Start();
            }
        }

        /// <summary>
        /// Performs search (on worker thread)
        /// </summary>
        private void PerformSearch()
        {
            try
            {
                switch (this.dataStore)
                {
                    case (ContactStore.ActiveDirectory):
                        this.entries = ADContact.FindContacts(this.searchType, this.query, this.resultsLimit);
                        break;
                    case (ContactStore.MyContacts):
                        this.entries = WinFSContact.FindContacts(this.searchType, this.query);
                        break;
                }

                this.searchErrorOccured = false;
            }
            catch (ApplicationException e)
            {
                // flag an error
                this.searchErrorOccured = true;

                this.searchErrorException = e;
            }
            
            // wake up the main thread
            Foreground.Context.BeginInvoke(new UIContextOperationDelegate(OnDispatch));
        }

        /// <summary>
        /// Helper function called when worker thread has completed its search
        /// </summary>
        private object OnDispatch(object target)
        {
            this.workerThread = null;

            // signal to CreateFlyout that we don't want the activity indicator since our search is complete
            this.searchInProgress = false;

            // UPDATE the flyout element with search results
            CreateFlyout();

            // signal to (future invocations of) CreateFlyout that search is complete
            this.searchCompleted = true;

            return null;
        }

        /// <summary>
        /// Returns flyout element. (BaseTile override)
        /// 
        /// Note that CreateFlyout can also be used to update the contents of the flyout (such as 
        /// when we go from the "progress indicator" flyout to the results flyout).
        /// 
        /// Also note that we go to great lengths to determine whether flyout is already open.
        /// If it is, we reuse that element rather than always construct a new element.
        /// This way, if a particular element in the flyout already has focus, we keep it there.
        /// 
        /// If it is not open, however, we must be sure to create a new flyout element to avoid 
        /// an Avalon reparenting crash.
        /// 
        /// IN "parameters" - CreateFlyout checks the state of the following instance members:
        /// this.executeNewSearch,
        /// this.searchInProgress,
        /// this.searchCompleted,
        /// this.flyoutOpen, and
        /// this.entries.
        /// </summary>
        /// <returns>Flyout element</returns>
        public override FrameworkElement CreateFlyout()
        {
            DockPanel flyoutPanel = null;
            QueryTextBox flyoutQueryTextBox = null;
            ContactListView flyoutListView = null;
            DetailsPane flyoutDetailsPane = null;

            // First, we have to determine whether we need to create a new flyout, or whether
            //   a flyout is already open (and available for use).
            // A new flyout needs to be created if either:
            //   (0) We are not performing a search (i.e. flyout was invoked when the text box is in help mode)
            //   (1) The flyout is not already open.
            //   (2) A search is already completed (meaning that someone closed the results flyout earlier and
            //       just invoked the flyout manually. In this case, we must be sure to create a new flyout
            //       to avoid an Avalon reparenting crash.
            if ((!this.executeNewSearch) || (!this.flyoutOpen) || (this.searchCompleted))
            {
                // we need to create a new flyout
                
                // create base flyoutPanel
                flyoutPanel = new DockPanel();
                flyoutPanel.Width = new Length(ContactLookupTile.flyoutWidthPixels, UnitType.Pixel);
                DockPanel.SetDock(flyoutPanel, Dock.Top);

                // create flyout query textbox
                flyoutQueryTextBox = new FlyoutQueryTextBox();
                flyoutQueryTextBox.ShowHelpText = this.queryTextBox.ShowHelpText;   // sync the textboxes
                flyoutQueryTextBox.Text = this.queryTextBox.Text;                   // sync the text
                
                flyoutQueryTextBox.LostFocus += new FocusChangedEventHandler(OnQueryTextBoxLostFocus);
                flyoutQueryTextBox.KeyDown += new KeyEventHandler(OnQueryTextBoxKeyDown);

                // create flyout listview (along with its border)
                flyoutListView = new ContactListView();
                Border flyoutListViewBorder = new Border();
                DockPanel.SetDock(flyoutListViewBorder, Dock.Top);
                flyoutListViewBorder.BorderBrush = Brushes.Black;
                flyoutListViewBorder.BorderThickness = new Thickness(new Length(1), new Length(0), new Length(1), new Length(1));
                flyoutListViewBorder.Child = flyoutListView;
                
                // create flyout details pane
                flyoutDetailsPane = new DetailsPane();
                Border flyoutDetailsPaneBorder = new Border();
                DockPanel.SetDock(flyoutDetailsPaneBorder, Dock.Top);
                flyoutDetailsPaneBorder.BorderBrush = Brushes.Black;
                flyoutDetailsPaneBorder.BorderThickness = new Thickness(new Length(1), new Length(0), new Length(1), new Length(1));
                flyoutDetailsPaneBorder.Child = flyoutDetailsPane;

                // add the flyout panel's children to the flyout panel
                flyoutPanel.Children.Add(flyoutQueryTextBox);
                flyoutPanel.Children.Add(flyoutListViewBorder);
                flyoutPanel.Children.Add(flyoutDetailsPaneBorder);
            }
            else
            {
                // reuse existing flyout, but clear the contents first
                flyoutPanel = this.flyoutPanel;

                flyoutQueryTextBox = this.flyoutQueryTextBox;

                flyoutListView = this.flyoutListView;
                flyoutListView.Children.Clear();

                flyoutDetailsPane = this.flyoutDetailsPane;
                flyoutDetailsPane.Children.Clear();
            }

            // check to see if we are actually performing a search
            if (!this.executeNewSearch)
            {
                // not performing a search, so show a help message in the preview pane
                string message = "";

                switch (this.dataStore)
                {
                    case (ContactStore.ActiveDirectory):
                        message += "There are two ways to perform a search:\n";
                        message += "- Enter an alias (mail nickname) to find.\n";
                        message += "- Enter part (or all) of a display name to find.\n";
                        break;
                    case (ContactStore.MyContacts):
                        message += "Enter part (or all) of a name to find.\n";
                        break;
                }

                // put temporary information into the preview pane
                Text infoText = CreateSimpleText(message);
                DockPanel.SetDock(infoText, Dock.Fill);
                infoText.Foreground = Brushes.White;
                infoText.Margin = new Thickness(new Length(24), new Length(16), new Length(4), new Length(18));
                flyoutDetailsPane.Children.Add(infoText);
            }
            else
            {
                if (this.searchInProgress)
                {
                    // show progress indicator
                    flyoutDetailsPane.Children.Add(new ProgressIndicator(this.progressIndicatorImageData));
                }
                else
                {
                    if (this.searchErrorOccured)
                    {
                        // someone flagged a search error; show error message
                        flyoutDetailsPane.Children.Add(CreateSimpleText("An error occured when searching. (" + this.searchErrorException.Message + " (" + this.searchErrorException.InnerException.Message + "))"));

                        this.searchErrorException = null;
                    }
                    else
                    {
                        switch (this.entries.Count)
                        {
                            case (0):
                                // no results found
                                flyoutDetailsPane.Children.Add(CreateSimpleText("No results were found."));
                                break;
                            case (1):
                                // only a single result was found, so go ahead and show it in "expanded" form
                                flyoutDetailsPane.Children.Add(CreateContactDetailsItem(0));
                                break;
                            default:
                                // more than one result was found, so show lots of list item entries
                                int i = 0;

                                // we need both parts of this conditional since we don't actually enforce
                                //   the results limit when performing a search of My Contacts (WinFS)
                                while ((i < this.resultsLimit) && (i < this.entries.Count))
                                {
                                    ContactListItem item = new ContactListItem(flyoutListView, i, (BaseContact) this.entries[i]);
                                    item.Click += new ClickEventHandler(OnContactListItemClick);

                                    flyoutListView.Children.Add(item);
                                    i++;
                                }

                                string message = "";

                                if (this.resultsLimit != i)
                                {
                                    message += i + " contacts found";
                                }
                                else
                                {
                                    message += "More than " + this.resultsLimit + " contacts found. ";
                                    message += "Only the first " + this.resultsLimit + " are displayed. ";
                                    message += "(To display more results, go to tile properties.)";
                                }

                                // put temporary information into the preview pane
                                Text infoText = CreateSimpleText(message);
                                DockPanel.SetDock(infoText, Dock.Fill);
                                infoText.Foreground = Brushes.White;
                                infoText.Margin = new Thickness(new Length(24), new Length(16), new Length(4), new Length(18));
                                flyoutDetailsPane.Children.Add(infoText);

                                break;
                        }
                    }
                }
            }

            // set class members
            this.flyoutPanel = flyoutPanel;
            this.flyoutQueryTextBox = flyoutQueryTextBox;
            this.flyoutListView = flyoutListView;
            this.flyoutDetailsPane = flyoutDetailsPane;

            return flyoutPanel;
        }

        private void OnContactListItemClick(object sender, ClickEventArgs e)
        {
            // get the details
            GetDetails(((ContactListItem) sender).ItemIndex);
        }

        /// <summary>
        /// Gets detailed contact information based on whatever small list item was clicked in the flyout
        /// </summary>
        /// <param name="index">Index of list item that was clicked.</param>
        private void GetDetails(int index)
        {
            this.flyoutDetailsPane.Children.Clear();
            this.flyoutDetailsPane.Children.Add(CreateContactDetailsItem(index));
        }

        /// <summary>
        /// Creates a contact details item.
        /// </summary>
        /// <param name="index">Index of BaseContact in entries list to create ContactDetailsItem for.</param>
        /// <returns>ContactDetailsItem corresponding to specified BaseContact.</returns>
        private ContactDetailsItem CreateContactDetailsItem(int index)
        {
            return new ContactDetailsItem((BaseContact) entries[index], index, new ClickEventHandler(OnContactDetailsItemButtonClick), this.genericContactImageData, this.searchIconImageData, this.searchIconDisabledImageData);
        }

        private void OnContactDetailsItemButtonClick(object sender, ClickEventArgs e)
        {
            // get the menuitem that caused this
            FrameworkElement menuItem = (FrameworkElement) sender;

            int entryIndex = this.flyoutDetailsPane.Child.ItemIndex;

            BaseContact entry = (BaseContact) this.entries[entryIndex];

            switch ((ContactOptionButtonValue) Convert.ToInt32(menuItem.ID, CultureInfo.InvariantCulture))
            {
                case (ContactOptionButtonValue.FindManager):
                    Update(SearchType.FindManager, entry.ManagerSearchToken, true);
                    break;
                case (ContactOptionButtonValue.FindDirects):
                    Update(SearchType.FindDirects, entry.DirectReportsSearchToken, true);
                    break;
                case (ContactOptionButtonValue.FindPeers):
                    Update(SearchType.FindPeers, entry.ManagerSearchToken, true);
                    break;
                default:
                    break;
            }
        }

        #region Properties window

        /// <summary>
        /// Returns properties element (BaseTile override)
        /// </summary>
        /// <returns>Properties element</returns>
        public override FrameworkElement CreateProperties()
        {
            TileProperties properties = new TileProperties(this.dataStore, this.resultsLimit);
            properties.OKButton.Click += new ClickEventHandler(OnPropertiesOKButtonClick);
            properties.CancelButton.Click += new ClickEventHandler(OnPropertiesCancelButtonClick);

            this.properties = properties;

            return properties;
        }

        private void OnPropertiesCancelButtonClick(object sender, ClickEventArgs e)
        {
            // close the window
            this.properties = null;
            Sidebar.CloseProperties();
        }

        private void OnPropertiesOKButtonClick(object sender, ClickEventArgs e)
        {
            // set contact store
            this.dataStore = (ContactStore) this.properties.DataStoreListBox.SelectedIndex;

            // get specified max results count
            string resultsLimitString = this.properties.ResultsLimitTextBox.Text;

            if (0 != resultsLimitString.Length)
            {
                try
                {
                    int resultsLimit = Convert.ToInt32(resultsLimitString, CultureInfo.InvariantCulture);

                    if (resultsLimit > 0)
                    {
                        this.resultsLimit = resultsLimit;
                    }
                }
                catch (FormatException)
                {
                    // integer wasn't supplied, ignore the value
                }
                catch (OverflowException)
                {
                    // huge number was supplied, ignore the value
                }
            }

            // close the properties window
            this.properties = null;
            Sidebar.CloseProperties();

            if (null != this.entries)
            {
                // there are results to go re-render, let's do it!
                Update(SearchType.Normal, this.queryTextBox.Text, false);
            }
        }



        #endregion

        #region Text factories

        private static Text CreateMessageText(string textString)
        {
            return CreateSimpleText(textString, Brushes.DarkGray);
        }

        private static Text CreateSimpleText(string textString)
        {
            return CreateSimpleText(textString, Brushes.White);
        }           

        private static Text CreateSimpleText(string textString, Brush foreground)
        {
            Text text = new Text();
            DockPanel.SetDock(text, Dock.Top);
            text.FontSize = new FontSize(8, FontSizeType.Point);
            text.Foreground = foreground;
            text.FontFamily = ContactLookupTile.textFontFamily;
            text.Margin = new Thickness(new Length(4), new Length(2), new Length(4), new Length(2));
            text.TextWrap = TextWrap.Wrap;
            text.TextContent = textString;
            
            return text;
        }

        #endregion

        #region Resource helpers

        /// <summary>
        /// Gets the path of the folder that contains image resource files; returns null if path could not be found.
        /// </summary>
        /// <returns>Path of the folder that contains image resource files, or null if path could not be found.</returns>
        private static string GetImageFolderPath()
        {
            string path = null;

            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"Software\ContactLookupTile");

            if (null != key)
            {
                path = key.GetValue("ImagePath") as string;
            }

            return path;
        }

        #endregion
    }
}
