namespace SimpleApp     // Namespace must be the same as what you set in project file
{
	using System;
	using System.Windows;
	using System.Windows.Controls;

    public partial class MyPage
    {
        void OnClick(object sender, System.Windows.Controls.ClickEventArgs e)
        {
		Text1.TextRange.Text = "Button is Clicked";

        }
    }
}

