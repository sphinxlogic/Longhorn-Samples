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
	public partial class CollectionViewSample
	{

        public CollectionView MyCollectionView;
		// Object o keeps the currency for the table
		public Order o;

		public void StartHere(Object sender, DependencyPropertyChangedEventArgs args)
		{
            MyCollectionView = Binding.GetView(rootElem.DataContext);
		}

		//OnButton is called whenever the Next or Previous buttons
		//are clicked to change the currency
		public void OnButton(Object sender, ClickEventArgs args)
		{
			Button b = sender as Button;

			switch (b.ID)
			{
				case "Previous":
					if (MyCollectionView.CurrentItem.MovePrevious())
					{
						FeedbackText.TextContent = "";
						o = MyCollectionView.CurrentItem.Current as Order;
					}
					else
					{
						MyCollectionView.CurrentItem.MoveFirst();
						FeedbackText.TextContent = "At first record";
					}

					break;

				case "Next":
					if (MyCollectionView.CurrentItem.MoveNext())
					{
						FeedbackText.TextContent = "";
						o = MyCollectionView.CurrentItem.Current as Order;
					}
					else
					{
						MyCollectionView.CurrentItem.MoveLast();
						FeedbackText.TextContent = "At last record";
					}

					break;
			}
		}
	}
}