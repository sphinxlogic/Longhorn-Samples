using System;
using System.Windows;
using System.ComponentModel;
using System.Windows;
using System.ComponentModel;
using System.Windows.Data;

namespace WCPSample {
	public class UserProfile: IPropertyChange
	{

    	public UserProfile() {}
		private string _firstname = "";
		private string _lastname = "";
		private string _address1 = "";
		private string _address2 = "";
		private string _city = "";
		private string _state = "";
		private string _zipcode = "";
		private string _phonenumber = "";
		private string _email = "";

		public string firstname
		{
			get{return _firstname;}
            set{ _firstname=value; NotifyPropertyChanged("firstname");}
		}

		public string lastname
		{
			get{return _lastname;}
            set{_lastname=value; NotifyPropertyChanged("lastname");}
		}

		public string address1
		{
			get{return _address1;}
            set{_address1=value; NotifyPropertyChanged("address1");}
		}

		public string address2
		{
            get{return _address2;}
			set{_address2=value; NotifyPropertyChanged("address2");}
		}

		public string city
		{
			get{return _city;}
            set{_city=value; NotifyPropertyChanged("city");}
		}

		public string state
		{
			get{return _state;}
            set{_state=value; NotifyPropertyChanged("state");}
		}

		public string zipcode
		{
			get{return _zipcode;}
            set{_zipcode=value; NotifyPropertyChanged("zipcode");}
		}

		public string phonenumber
		{
			get{return _phonenumber;}
            set{_phonenumber=value; NotifyPropertyChanged("phonenumber");}
		}

		public string email
		{
			get{return _email;}
            set{_email=value; NotifyPropertyChanged("email");}
		}

		//Declare event
		public event PropertyChangedEventHandler PropertyChanged;
		//NotifyPropertyChanged event handler to update property value in binding
		private void NotifyPropertyChanged(string info)
		{
			if (PropertyChanged !=null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}
    }
}