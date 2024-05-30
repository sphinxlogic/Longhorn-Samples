using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.ComponentModel;

namespace WCPSample {
	public class NetIncome : IPropertyChange
	{
		private int totalincome = 5000;
		private int rent = 2000;
		private int food = 0;
		private int misc = 0;
		private int savings = 0;
		public NetIncome()
		{
			savings = totalincome - (rent+food+misc);
		}

		public int TotalIncome
		{
			get
			{
				return totalincome;
			}
			set
			{
				if( TotalIncome != value)
				{
					totalincome = value;
					NotifyPropertyChanged("TotalIncome");
				}
			}
		}
		public int Rent
		{
			get
			{
				return rent;
			}
			set
			{
               	value = (int) value;
				if( Rent != value)
				{
					rent = value;
					NotifyPropertyChanged("Rent");
					UpdateSavings();
				}
			}
		}
		public int Food
		{
			get
			{
				return food;
			}
			set
			{
                value = (int) value;
				if( Food != value)
				{
					food = value;
					NotifyPropertyChanged("Food");
					UpdateSavings();
				}
			}
		}
		public int Misc
		{
			get
			{
				return misc;
			}
			set
			{
            	value = (int) value;
				if( Misc != value)
				{
					misc = value;
					NotifyPropertyChanged("Misc");
					UpdateSavings();
				}
			}
		}
		public int Savings
		{
			get
			{
				return savings;
			}
			set
			{
				if( Savings != value)
				{
					savings = value;
					NotifyPropertyChanged("Savings");
					UpdateSavings();
				}
			}
		}

		private void UpdateSavings()
		{
			Savings = TotalIncome - (Rent+Misc+Food);
			if(Savings < 0)
			{}
			else if(Savings >= 0)
			{}
		}
		public event PropertyChangedEventHandler PropertyChanged;
		private void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged !=null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}
	}
}