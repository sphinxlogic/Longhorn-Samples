//------------------------------------------------------------------------------
using System;
using System.Windows;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using WCPSample;

namespace WCPSample
{
	public partial class SortFilter
	{

		public ArrayListCollectionView MyALCollectionView;

		// Object o keeps the currency for the table
		public WCPSample.Order o;

		public void StartHere(Object sender, DependencyPropertyChangedEventArgs args)
		{
			MyALCollectionView = Binding.GetView(rootElem.DataContext) as ArrayListCollectionView;
		}

		public void ColClicked(Object sender, ClickEventArgs args)
		{
			Button srcB = sender as Button;
			//Sort the data based on the column selected
            SortDescription[] sdA = new SortDescription[1];
			switch(srcB.ID.ToString())
			{
				case "hdrOrder":
                    sdA[0] = new SortDescription("order",ListSortDirection.Ascending);
					MyALCollectionView.Sort = sdA;
					break;
				case "hdrCustomer":
                    sdA[0] = new SortDescription("customer", ListSortDirection.Ascending);
                	MyALCollectionView.Sort = sdA;
					break;
				case "hdrName":
                    sdA[0] = new SortDescription("name", ListSortDirection.Ascending);
                	MyALCollectionView.Sort = sdA;
					break;
				case "hdrID":
                    sdA[0] = new SortDescription("id", ListSortDirection.Ascending);
                	MyALCollectionView.Sort = sdA;
					break;
				case "hdrOF":
                    sdA[0] = new SortDescription("filled", ListSortDirection.Ascending);
                    MyALCollectionView.Sort = sdA;
					break;
			}
            MyALCollectionView.Refresh();
		}

		//OnButton is called whenever the Next or Previous buttons
		//are clicked to change the currency
		public void OnButton(Object sender, ClickEventArgs args)
		{
			Button b = sender as Button;
  		    switch(b.ID)
			{
				case "Previous":
					
					if(MyALCollectionView.CurrentItem.MovePrevious())
					{
						FeedbackText.TextContent = "";
						o =  MyALCollectionView.CurrentItem.Current as Order;
					}
					else
					{
						MyALCollectionView.CurrentItem.MoveFirst();
						FeedbackText.TextContent = "At first record";
					}
					break;
				case "Next":
					if (MyALCollectionView.CurrentItem.MoveNext())
					{
						FeedbackText.TextContent = "";
						o =  MyALCollectionView.CurrentItem.Current as Order;
					}
					else
					{
						MyALCollectionView.CurrentItem.MoveLast();
						FeedbackText.TextContent = "At last record";
					}
					break;
			}
		}
		//OnButton is called whenever the Next or Previous buttons
		//are clicked to change the currency
		public void OnFilter(Object sender, ClickEventArgs args)
		{
			Button b = sender as Button;
  		    switch(b.ID)
			{
				case "Filter":
					MyALCollectionView.CustomFilter = new MyFilter();
					MyALCollectionView.Refresh();
					break;
				case "Unfilter":
					MyALCollectionView.CustomFilter = null;
					MyALCollectionView.Refresh();
					break;
			}
		}
		public class MyFilter: IContains
		{
			public bool Contains(object de)
			{
				Order order = de as Order;
				//Return members whose Orders have not been filled
				return(order.filled== "No");
			}
		}
	}
}