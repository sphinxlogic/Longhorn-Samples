using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Data;

namespace WCPSample {
	public class myDataCollection: ArrayListDataCollection
	{
    	public myDataCollection _changeThis;
        public myData item1 = new myData("Ichiro Bobblehead",(decimal)24.95);
        public myData item2 = new myData("Edgar Toy Duck",(decimal) 16.05);
        public myData item3 = new myData("Jeff Cirillo Golden Sombero", (decimal) 0.99);
		public myDataCollection():base()
		{
			Add(item1);
			Add(item2);
			Add(item3);
            CreateTimer();
		}
		private void CreateTimer()
		{
   			System.Timers.Timer Timer1 = new System.Timers.Timer();
   			Timer1.Enabled = true;
            Timer1.Interval = 10000;
   			Timer1.Elapsed += new System.Timers.ElapsedEventHandler(Timer1_Elapsed);
		}

		private void Timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
            item1.BidItemPrice += (decimal) 1.10;
            item2.BidItemPrice += (decimal) 0.40;
		}
	}
	public class myData: IPropertyChange
	{
		private string _biditemname = "Unset";
        private decimal _biditemprice = (decimal) 0;

        public myData(string NewBidItemName, decimal NewBidItemPrice) {
			_biditemname = NewBidItemName;
			_biditemprice = NewBidItemPrice;
        }

		public string BidItemName
		{
			get
			{
				return _biditemname;
			}
			set
			{
				if(_biditemname.Equals(value) == false)
				{
					_biditemname = value;
					//Call Notify PropertyChanged whenever the property is updated
					NotifyPropertyChanged("BidItemName");
				}
			}
		}
        public decimal BidItemPrice
		{
			get
			{
				return _biditemprice;
			}
			set
			{
				if(_biditemprice.Equals(value) == false)
				{
					_biditemprice = value;
					//Call Notify PropertyChanged whenever the property is updated
					NotifyPropertyChanged("BidItemPrice");
				}
			}
		}
		//Declare event
		public event PropertyChangedEventHandler PropertyChanged;
		//NotifyPropertyChanged event handler to update property value in binding
		private void NotifyPropertyChanged(string propName)
		{
			if (PropertyChanged !=null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propName));
			}
		}
	}
}