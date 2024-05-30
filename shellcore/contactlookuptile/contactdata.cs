namespace MySidebarTiles
{
    using System;
    using System.Collections;
    using System.Globalization;
    using System.DirectoryServices;
    using SortOption = System.DirectoryServices.SortOption;
    using System.Storage;
    using System.Storage.Contacts;

    /// <summary>
    /// Enumeration for search type
    /// </summary>
    public enum SearchType
    {
        Normal = 0,
        FindManager = 1,
        FindDirects = 2,
        FindPeers = 3,
    }

    /// <summary>
    /// Encapsulates information about a contact
    /// </summary>
    public abstract class BaseContact : IComparable
    {
        #region IComparable implementation

        /// <summary>
        /// Compares this BaseContact to another (for ordering purposes)
        /// </summary>
        /// <param name="obj">BaseContact object to compare to.</param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            BaseContact data = obj as BaseContact;

            if (null == data)
            {
                throw new ArgumentException("A BaseContact object is required for comparison.");
            }

            // treat displayname as primary key
            return this.DisplayName.CompareTo(data.DisplayName);
        }

        #endregion

        #region Object overrides

        /// <summary>
        /// Returns true if this instance matches specified object.
        /// </summary>
        /// <param name="obj">Object to compare to.</param>
        /// <returns>True if this instance matches specified object; false otherwise.</returns>
        public override bool Equals(object obj)
        {
            return (0 == this.CompareTo(obj));
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>Hash code for this instance.</returns>
        public override int GetHashCode()
        {
            // treat display name as primary key
            return this.DisplayName.GetHashCode();
        }

        #endregion

        private string displayName;
        private string alias;
        private string title;
        private string department;
        private string office;
        private string phoneNumber;
        private string emailAddress;
        private bool hasManager;
        private bool hasDirectReports;
        private string managerSearchToken;
        private string directReportsSearchToken;

        #region Instance properties

        /// <summary>
        /// Gets the display name of this contact.
        /// </summary>
        public string DisplayName
        {
            get
            {
                return this.displayName;
            }
        }

        /// <summary>
        /// Gets the alias (mail nickname) of this contact.
        /// </summary>
        public string Alias
        {
            get
            {
                return this.alias;
            }
        }

        /// <summary>
        /// Gets the best email address to send mail to for this contact.
        /// </summary>
        public string BestEmailAddress
        {
            get
            {
                string address = this.alias;

                if (0 == address.Length)
                {
                    address = this.emailAddress;
                }

                return address;
            }
        }

        /// <summary>
        /// Gets the title of this contact.
        /// </summary>
        public string Title
        {
            get
            {
                return this.title;
            }
        }

        /// <summary>
        /// Gets the department for this contact.
        /// </summary>
        public string Department
        {
            get
            {
                return this.department;
            }
        }

        /// <summary>
        /// Gets the office of this contact.
        /// </summary>
        public string Office
        {
            get
            {
                return this.office;
            }
        }

        /// <summary>
        /// Gets the phone number of this contact.
        /// </summary>
        public string PhoneNumber
        {
            get
            {
                return this.phoneNumber;
            }
        }

        /// <summary>
        /// Gets the email address of this contact.
        /// </summary>
        public string EmailAddress
        {
            get
            {
                return this.emailAddress;
            }
        }
            
        /// <summary>
        /// True if this contact has a manager.
        /// </summary>
        public bool HasManager
        {
            get
            {
                return this.hasManager;
            }
        }

        /// <summary>
        /// True if this contact has direct report(s).
        /// </summary>
        public bool HasDirectReports
        {
            get
            {
                return this.hasDirectReports;
            }
        }

        /// <summary>
        /// Gets the search token used to find this contact's manager.
        /// </summary>
        public string ManagerSearchToken
        {
            get
            {
                return this.managerSearchToken;
            }
        }

        /// <summary>
        /// Gets the search token used to find this contact's direct reports.
        /// </summary>
        public string DirectReportsSearchToken
        {
            get
            {
                return this.directReportsSearchToken;
            }
        }

        #endregion

        /// <summary>
        /// Creates a BaseContact object.
        /// </summary>
        /// <param name="displayName">Display name of the contact.</param>
        /// <param name="aliasName">Alias (mail nickname) of the contact.</param>
        /// <param name="title">Title of the contact.</param>
        /// <param name="department">Department for the contact.</param>
        /// <param name="office">Office of the contact.</param>
        /// <param name="phoneNumber">Phone number of the contact.</param>
        /// <param name="emailAddress">Email address of the contact.</param>
        /// <param name="hasManager">True if this contact has a manager.</param>
        /// <param name="hasDirectReports">True if this contact has direct report(s).</param>
        /// <param name="managerSearchToken">Search token used to find this contact's manager.</param>
        /// <param name="directReportsSearchToken">Search token used to find this contact's direct reports.</param>
        protected BaseContact(string displayName, string aliasName, string title, string department, string office, string phoneNumber, string emailAddress, bool hasManager, bool hasDirectReports, string managerSearchToken, string directReportsSearchToken)
        {
            this.displayName = displayName;
            this.alias = aliasName;
            this.title = title;
            this.department = department;
            this.office = office;
            this.phoneNumber = phoneNumber;
            this.emailAddress = emailAddress;
            this.hasManager = hasManager;
            this.hasDirectReports = hasDirectReports;
            this.managerSearchToken = managerSearchToken;
            this.directReportsSearchToken = directReportsSearchToken;
        }
    }



    /// <summary>
    /// Encapsulates information about contacts found in My Contacts (WinFS).
    /// </summary>
    public sealed class WinFSContact : BaseContact
    {
        /// <summary>
        /// (Factory method) Retrives a list of WinFSContact's matching specified search criteria.
        /// </summary>
        /// <param name="type">Type of search to perform.</param>
        /// <param name="query">Search query.</param>
        /// <returns>An ArrayList of WinFSContact's matching specified search criteria.</returns>
        public static ArrayList FindContacts(SearchType type, string query)
        {
            ArrayList contacts = new ArrayList();

            try
            {
                if (0 != query.Length)
                {
                    using (ItemContext context = ItemContext.Open())
                    {
                        // get the My Contacts folder
                        WellKnownFolder myContactFolder = (WellKnownFolder) UserDataFolder.FindMyPersonalContactsFolder(context);

                        string filter = "";

                        switch (type)
                        {
                            case (SearchType.Normal):
                                char[] separator = new char[1] { ' ' };
                                // separate the query into words by spaces
                                string[] words = query.Split(separator);
                                
                                switch (words.Length)
                                {
                                    case (1):
                                        // assume that we have all or part of the first name
                                        filter = String.Format(CultureInfo.InvariantCulture, "PersonalNames[GivenName LIKE '{0}%']", words[0].Replace("'", "''"));
                                        break;
                                    default:
                                        // assume we have all of the first name, and part or all of the last night. ignore any words past the second one
                                        filter = String.Format(CultureInfo.InvariantCulture, "PersonalNames[GivenName = '{0}' AND Surname LIKE '{1}%']", words[0].Replace("'", "''"), words[1].Replace("'", "''"));
                                        break;
                                }
                                break;
                            default:
                                break;
                        }

                        // did we actually generate a search filter?
                        if (0 != filter.Length)
                        {
                            // perform the search
                            FindResult results = context.FindAll(typeof(Person), filter);

                            if (null != results)
                            {
                                // add to return value
                                foreach (Person person in results)
                                {
                                    contacts.Add(new WinFSContact(person));
                                }

                                // sort after the fact
                                contacts.Sort();
                            }
                        }
                    }
                }
            }
            catch (StorageException e)
            {
                throw new ApplicationException("An error occured when attempting to search the WinFS Contact Store", e);
            }

            return contacts;
        }


        /// <summary>
        /// Creates a WinFSContact object.
        /// </summary>
        /// <param name="entry">Person object containing contact information.</param>
        public WinFSContact(Person entry) : base(
            WinFSContact.GetFullName(entry),
            "",
            "",
            "",
            "",
            WinFSContact.GetPhoneNumber(entry),
            WinFSContact.GetEmailAddress(entry),
            false,
            false,
            "",
            ""
            )
        {

        }


        #region Static helper methods that extract information from a Person object

        /// <summary>
        /// Extracts the full name from a Person object.
        /// </summary>
        /// <param name="person">Person object to get name from.</param>
        /// <returns>Name of person (or empty string if none was found).</returns>
        private static string GetFullName(Person person)
        {
            string returnValue = "";

            NestedStoreObjectCollection<FullName> names = person.PersonalNames;

            if (null != names)
            {
                if (names.Count > 0)
                {
                    FullName name = (FullName) names[0];
                    returnValue = WinFSContact.ProtectNullValue(name.GivenName) + " " + WinFSContact.ProtectNullValue(name.Surname);
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Extracts a phone number from a Person object.
        /// </summary>
        /// <param name="person">Person object to get phone number from.</param>
        /// <returns>Phone number (or empty string if none was found).</returns>
        private static string GetPhoneNumber(Person person)
        {
            string returnValue = "";

            TelephoneNumber phoneNumber = person.BestTelephoneNumber;
    
            if (null != phoneNumber)
            {
                string countrycode = WinFSContact.ProtectNullValue(phoneNumber.CountryCode);
                string areacode = WinFSContact.ProtectNullValue(phoneNumber.AreaCode);
                string number = WinFSContact.ProtectNullValue(phoneNumber.Number);
                string extension = WinFSContact.ProtectNullValue(phoneNumber.Extension);

                if (0 != countrycode.Length)
                {
                    countrycode = "+" + countrycode + " ";
                }

                if (0 != areacode.Length)
                {
                    areacode = "(" + areacode + ") ";
                }

                if (0 != extension.Length)
                {
                    extension = " x" + extension;
                }

                returnValue = countrycode + areacode + number + extension;
            }

            return returnValue;
        }

        /// <summary>
        /// Extracts an email address from a Person object.
        /// </summary>
        /// <param name="person">Person object to get email address from.</param>
        /// <returns>Email address (or empty string if none was found).</returns>
        private static string GetEmailAddress(Person person)
        {
            string returnValue = "";

            SmtpEmailAddress emailAddress = person.BestEmailAddress;

            if (null != emailAddress)
            {
                returnValue = emailAddress.Address.ToString(CultureInfo.CurrentUICulture);
            }

            return returnValue;
        }

        /// <summary>
        /// Retrieves a string value from an Nullable string
        /// </summary>
        /// <param name="value">Nullable string to retrieve data from</param>
        /// <returns>String representation of data (or empty string if data was null)</returns>
        private static string ProtectNullValue(Nullable<String> value)
        {
            string returnValue = "";

            if (null != value)
            {
                returnValue = (string) value;
            }

            return returnValue;
        }

        #endregion
    }


    /// <summary>
    /// Encapsulates information about a contact found in Active Directory.
    /// </summary>
    public sealed class ADContact : BaseContact
    {
        /// <summary>
        /// (Factory method) Retrives a list of ADContact's matching specified search criteria.
        /// </summary>
        /// <param name="type">Type of search to perform.</param>
        /// <param name="query">Search query.</param>
        /// <param name="resultsLimit">Maximum number of results to return.</param>
        /// <returns>An ArrayList of ADContact's matching specified search criteria.</returns>
        public static ArrayList FindContacts(SearchType type, string query, int resultsLimit)
        {
            ArrayList contacts = new ArrayList();

            try
            {
                // get the result collection ready
                SearchResultCollection searchResults = null;

                if (0 != query.Length)
                {
                    string filter = "";

                    // prepare the search filter
                    switch (type)
                    {
                        case (SearchType.FindManager):
                            // search for distinguished name equals query
                            filter = "(&(objectCategory=person)(distinguishedName=" + query + "))";
                            break;
                        case (SearchType.FindDirects):
                        case (SearchType.FindPeers):
                            // search for manager equals query
                            filter = "(&(objectCategory=person)(manager=" + query + "))";
                            break;
                        case (SearchType.Normal):
                        default:
                            // search for "name begins with query", or "alias equals query"
                            filter = "(&(objectCategory=person)(|(displayName=" + query + "*)(mailNickname=" + query + ")))";
                            break;
                    }

                    DirectorySearcher searcher = new DirectorySearcher(filter);

                    // only search for the number of results we are willing to display
                    searcher.SizeLimit = resultsLimit;

                    // sort by display name ascending
                    searcher.Sort = new SortOption("displayName", SortDirection.Ascending);

                    // perform the search
                    searchResults = searcher.FindAll();
                }

                if (null != searchResults)
                {
                    foreach(SearchResult result in searchResults)
                    {
                        contacts.Add(new ADContact(result.GetDirectoryEntry()));
                    }
                }
            }
            catch (ArgumentException e)
            {
                throw new ApplicationException("An error occured when attempting to search Active Directory", e);
            }
            catch (System.Runtime.InteropServices.COMException e)
            {
                throw new ApplicationException("An error occured when attempting to search Active Directory", e);
            }

            return contacts;
        }


        /// <summary>
        /// Creates an ADContact object.
        /// </summary>
        /// <param name="entry">DirectoryEntry containing contact information.</param>
        public ADContact(DirectoryEntry entry) : base(
            ADContact.ProtectNullValue(entry, "displayName"),
            ADContact.ProtectNullValue(entry, "mailNickname"),
            ADContact.ProtectNullValue(entry, "title"),
            ADContact.ProtectNullValue(entry, "department"),
            ADContact.ProtectNullValue(entry, "physicalDeliveryOfficeName"),
            ADContact.ProtectNullValue(entry, "telephoneNumber"),
            ADContact.ProtectNullValue(entry, "mail"),
            ("" != ADContact.ProtectNullValue(entry, "manager")),
            ("" != ADContact.ProtectNullValue(entry, "directReports")),
            ADContact.ProtectNullValue(entry, "manager"),
            ADContact.ProtectNullValue(entry, "distinguishedName")
            )
        {

        }


        /// <summary>
        /// Static helper that extracts a value from a PropertyValueCollection
        /// </summary>
        /// <param name="entry">DirectoryEntry to extract property value from.</param>
        /// <param name="propertyName">Name of property to extract value from.</param>
        private static string ProtectNullValue(DirectoryEntry entry, string propertyName)
        {
            string data = "";

            // before extracting PropertyValueCollection, be sure that requested property actually exists in this DirectoryEntry
            // NOTE: If requested property does not exist, Contains method will throw (and handle) a first-chance exception 
            if (entry.Properties.Contains(propertyName))
            {
                PropertyValueCollection propertyValues = entry.Properties[propertyName];
                
                if (null != propertyValues)
                {
                    object propertyValue = propertyValues.Value;

                    if (null != propertyValue)
                    {
                        data = propertyValue.ToString();
                    }
                }
            }
            
            return data;
        }
    }
}
