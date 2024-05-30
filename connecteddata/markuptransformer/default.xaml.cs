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
	public partial class MarkupTransformer
	{
		public class MyTransformer : IDataTransformer
		{
			public object Transform(object o, DependencyProperty dp, System.Globalization.CultureInfo culture)
			{
				DateTime date = (DateTime)o;
                switch (dp.Name) {
                case "TextContent":
  				    return "Heute ist " +  date.ToString("F", new System.Globalization.CultureInfo("de-DE"));
                case "Foreground":
					return Brushes.Red;
                default:
                    return o;
                }
			}

			public object InverseTransform(object o, System.Reflection.PropertyInfo info, System.Globalization.CultureInfo culture)
			{
				return null;
			}

		}
	}
}