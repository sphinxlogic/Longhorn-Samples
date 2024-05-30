using System;
using System.Windows;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.ComponentModel;
using System.Windows.Data;

namespace WCPSample {
	public class myData: IPropertyChange
	{
		private DateTime thedate;
		public myData() {
			thedate = DateTime.Now;
		}

		public DateTime TheDate
		{
			get{return thedate;}
			set
			{
				thedate = value;
				NotifyPropertyChanged("TheDate");}
		}
		//Declare event
		public event PropertyChangedEventHandler PropertyChanged;
		//NotifyPropertyChanged event handler to update property value in binding
		private void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged !=null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}
	}
    }