using System;
using System.Windows;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace WCPSample {
		// Create the collection of Order objects
	public class Orders: ArrayListDataCollection
	{
		public Order o1 = new Order(1001, 5682, "Blue Sweater", 44, "Yes", new DateTime(2003, 1, 23), new DateTime(2003, 2, 4));
		public Order o2 = new Order(1002, 2211, "Gray Jacket Long", 181, "No", new DateTime(2003, 2, 14));
		public Order o3 = new Order(1003, 5682, "Brown Pant W", 02, "Yes", new DateTime(2002, 12, 20), new DateTime(2003, 1, 13));
		public Order o4 = new Order(1004, 3143, "Orange T-shirt", 205, "No", new DateTime(2003, 1, 28));
			public Orders():base()
		{
			Add(o1);
			Add(o2);
			Add(o3);
			Add(o4);
		}
	}
	public class Order : IPropertyChange
	{
		private int _order;
		private int _customer;
		private string _name;
		private int _id;
		private string _filled;
		private DateTime _orderdate;
		private DateTime _datefilled;
		public int order
		{
			get{ return _order;}
			set{ _order=value; NotifyPropertyChanged("order");}
		}
		public int customer
		{
			get{ return _customer;}
			set{_customer=value; NotifyPropertyChanged("customer");}
		}
		public string name
		{
			get{ return _name;}
			set{_name=value; NotifyPropertyChanged("name");}
		}
		public int id
		{
			get{ return _id;}
			set{_id=value; NotifyPropertyChanged("id");}
		}
		public string filled
		{
			get{ return _filled;}
			set{ _filled=value; NotifyPropertyChanged("filled");}
		}
		public DateTime orderdate
		{
			get{ return _orderdate;}
			set{ _orderdate=value; NotifyPropertyChanged("orderdate");}
		}
		public DateTime datefilled
		{
			get{ return _datefilled;}
			set{ _datefilled=value; NotifyPropertyChanged("datefilled");}
		}
		public Order(int _order, int _customer, string _name, int _id, string _filled, DateTime _orderdate, DateTime _datefilled)
		{
			this.order = _order;
			this.customer = _customer;
			this.name  = _name;
			this.id = _id;
			this.filled = _filled;
			this.orderdate = _orderdate;
			this.datefilled = _datefilled;
		}
		public Order(int _order, int _customer, string _name, int _id, string _filled, DateTime _orderdate)
		{
			this.order = _order;
			this.customer = _customer;
			this.name  = _name;
			this.id = _id;
			this.filled = _filled;
			this.orderdate = _orderdate;
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(info));
		}
	}
}