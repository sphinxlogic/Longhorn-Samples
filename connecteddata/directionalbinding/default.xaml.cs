//------------------------------------------------------------------------------
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.ComponentModel;
using System.Windows.Media;

namespace WCPSample
{
	public partial class DirectionalBinding {
		public void OnRentRaise(Object sender, ClickEventArgs args)
		{
			// Update bills
			System.Random random = new System.Random();
			double i = random.Next(10);
            Binding theBind = SavingsText.GetBinding(Text.TextContentProperty);
            WCPSample.NetIncome srcData = (WCPSample.NetIncome) theBind.DataItem;
            srcData.Rent = (int)((1+i/100)*(double) srcData.Rent);
		}

		public void OnDataTransfer(Object sender, DataTransferEventArgs args)
		{
        	FrameworkElement fe = sender as FrameworkElement;
			InfoText.TextContent = "";
			InfoText.TextContent += args.Property.Name + " property of a " + args.Property.OwnerType.Name;
			InfoText.TextContent += " element (";
			InfoText.TextContent += fe.ID;
			InfoText.TextContent += ") updated...";
			InfoText.TextContent += (String)System.DateTime.Now.ToLongDateString();
			InfoText.TextContent += " at ";
			InfoText.TextContent += (String)System.DateTime.Now.ToLongTimeString();
		}
		public void StartHere(Object sender, EventArgs args) {
			RentText.AddHandler(Binding.DataTransferEventID,(DataTransferEventHandler) new DataTransferEventHandler(OnDataTransfer));
        }
	}
}