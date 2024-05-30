namespace ExpenseItApp
{
	using System;
	using System.Windows;
	using System.Windows.Navigation;
    using System.Windows.Controls;

	public partial class Page3
	{
		private void Navigate (object sender, ClickEventArgs args)
		{
    		((AppModelDemo) System.Windows.Application.Current).Navigate(sender);
		}
	}
}

