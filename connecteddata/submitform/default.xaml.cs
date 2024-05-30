//------------------------------------------------------------------------------
using System;
using System.Windows;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Events;
using System.Windows.Documents;

namespace WCPSample
{
	public partial class SubmitForm {
		public void OnSubmit(object sender, ClickEventArgs args)
		{
			foreach (FrameworkElement fe in rootElem.Children) {
				if (fe.DependencyObjectType.Name == "TextBox") {
					Binding b = fe.GetBinding(TextBox.TextProperty);
                    b.Update();
                }
            }

			userdata.Opacity=1;
			Finish.Opacity=1;
		}

		public void OnFinish(object sender, ClickEventArgs args)
		{

			UserProfile temp = new UserProfile();
			temp = rootElem.DataContext as UserProfile;
			MessageBox.Show("Thank you for creating an account with us, " + temp.firstname + " " + temp.lastname);
		}
	}
}
