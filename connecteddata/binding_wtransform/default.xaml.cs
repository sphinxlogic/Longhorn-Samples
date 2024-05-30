//------------------------------------------------------------------------------
using System;
using System.Windows;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Media;

namespace WCPSample
{
	public partial class Binding_wTransform
	{
		public myData FormatDate;
		public Bind myBindDef;
        public Boolean bHasValidBinding = false;

		public void CreateBinding()
		{
			FormatDate = new myData();

			//Set transformer later
			myBindDef = new Bind("TheDate", BindType.OneWay, new ExplicitObjectRef(FormatDate));
			myText.SetBinding(Text.TextContentProperty, myBindDef);
			Binding.BindStatus status = myText.GetBinding(Text.TextContentProperty).Status;
		}

		public void ChangeTheCulture(string passedCulture)
		{
			myBindDef = myText.GetBinding(Text.TextContentProperty).ParentBind;
			//Begin defer cycle
			myBindDef.BeginDefer();

			myBindDef.Transformer = new MyTransformer();
			myBindDef.Culture = new System.Globalization.CultureInfo(passedCulture);

			//End defer cycle and update the Binding
			myBindDef.EndDefer();
		}

		public void OnClick(Object sender, ClickEventArgs args)
		{
			FrameworkElement fe = (FrameworkElement) sender;
			switch(fe.ID)
			{
				case "ChangeCulture":
                	if (bHasValidBinding) {
                      ListItem sLI = (ListItem) dropdown.SelectedItem;
					  string passedCulture = sLI.Content.ToString();
					  ChangeTheCulture(passedCulture);
                    } else {
					  System.Windows.Forms.MessageBox.Show("Click 'Create a Binding' first to create the default US culture binding");
                    }
                    break;
				case "Clear":
					myText.ClearAllBindings();
                    bHasValidBinding = false;
                    dropdown.SelectedIndex = -1;
                    myText.TextContent = "(no binding): Click 'Create a Binding' first to create the default US culture binding";
					break;
				case "Change":
					//make a new source, to grab a new timestamp
					myData myChangedData = new myData();
					//make a totally new Bind definition, this time with culture to en-us always and transformer from the start
					Bind myNewBindDef = new Bind(
						"TheDate",
						BindType.OneWay,
						new ExplicitObjectRef(myChangedData),
						UpdateType.Immediate,
						new MyTransformer(),
						new System.Globalization.CultureInfo("en-US")
						);
					myText.SetBinding(Text.TextContentProperty, myNewBindDef);
					myText.SetBinding(Text.ForegroundProperty, myNewBindDef);
					dropdown.SelectedIndex = 0;
                    bHasValidBinding = true;
					break;
			}
		}

		public class MyTransformer : IDataTransformer
		{
			public object Transform(object o, DependencyProperty dp, System.Globalization.CultureInfo culture)
			{
				DateTime date = (DateTime)o;
				switch(culture.TwoLetterISOLanguageName)
				{
					case "en":
                   		switch(dp.Name) {
                    	case "TextContent":
							return "Today is " + date.ToString("F",culture);
                    	case "Foreground":
							return Brushes.Black;
                        }
						break;
					case "es":
                   		switch(dp.Name) {
                    	case "TextContent":
                    	 	return "Hoy es " +  date.ToString("F",culture);
                    	case "Foreground":
							return Brushes.Orange;
                        }
						break;
					case "de":
                   		switch(dp.Name) {
                    	case "TextContent":
							return "Heute ist " +  date.ToString("F",culture);
                    	case "Foreground":
							return Brushes.Red;
                        }
						break;
				}
                return null;
			}

			public object InverseTransform(object o, System.Reflection.PropertyInfo info, System.Globalization.CultureInfo culture)
			{
				return null;
			}
		}
	}
}