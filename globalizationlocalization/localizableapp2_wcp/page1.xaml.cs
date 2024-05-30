namespace MySampleApp     // Namespace must be the same as what you set in project file
{
    using System;
    using System.Windows.Controls;
    using System.Windows;
    using System.Windows.Navigation;
    using System.Globalization;
    using System.Threading;
    using System.Resources;
    using System.Reflection;
    using System.Windows.Resources;

    public partial class MyPage
    {
        void OnClick(object sender, System.Windows.Controls.ClickEventArgs e)
        {
		ResourceManager rm = new ResourceManager ("stringtable", Assembly.GetExecutingAssembly());
		Text1.TextRange.Text =rm.GetString("Message");
        }
    }
}

