using System;
using System.Globalization;
using System.Reflection;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Common;

namespace Colors
{
    public partial class MainWindow : Window
    {
        //----------------  Response to user actions   --------------------//

        // Event handler for the NewColor button
        void OnNewColorClicked(object sender, ClickEventArgs args)
        {
            Button button = (Button)sender;
            ColorItemList colorList = (ColorItemList)button.DataContext;
            ICurrentItem ici = Binding.GetView(colorList).CurrentItem;

            // add a new color based on the current one, then select the new one
            ColorItem newItem = new ColorItem((ColorItem)ici.Current);
            colorList.Add(newItem);
            ici.MoveTo(newItem);
        }

        // Event handler for the SortBy radio button list
        void OnSortByChanged(object sender, SelectionChangedEventArgs args)
        {
            RadioButtonList buttonList = (RadioButtonList)sender;
            int index = buttonList.SelectedIndex;
            string propertyName = (0 <= index && index < PropertyNames.Length)
                        ? PropertyNames[index] : null;

            // sort by the user's chosen property
            CollectionView cv = Binding.GetView(buttonList.DataContext);
            cv.Sort = (propertyName == null) ? null :
                        new SortDescription[] { new SortDescription(propertyName, System.ComponentModel.ListSortDirection.Descending) };
            cv.Refresh();
        }

        // this table must be in sync with the SortBy radio button list
        static string[] PropertyNames = { "Name", "Luminance", "Red", "Green", "Blue",
                                            "Hue", "Saturation", "Value" };


        //----------------  Data Transformers   --------------------//

        // Two-way conversion between byte and double (TypeConverter for double should provide this)

        public class ByteToDoubleTransformer : IDataTransformer
        {
            object IDataTransformer.Transform(object o, DependencyProperty dp, CultureInfo culture)
            {
                return Convert.ChangeType(o, typeof(double));
            }

            object IDataTransformer.InverseTransform(object o, PropertyInfo info, CultureInfo culture)
            {
                double d = (double)o;
                return (d < 0.0) ? (byte)0 : (d > 255.0) ? (byte)255 :
                    Convert.ChangeType(o, typeof(byte));
            }
        }

        // One-way conversion between double and string (adding formatting)

        public class DoubleToStringTransformer : IDataTransformer
        {
            object IDataTransformer.Transform(object o, DependencyProperty dp, CultureInfo culture)
            {
                return String.Format("{0:f2}", o);
            }

            object IDataTransformer.InverseTransform(object o, PropertyInfo info, CultureInfo culture)
            {
                return null;
            }
        }

        // One-way conversion between ColorItem.Source and bool -
        // used to disable editing for builtin colors

        public class SourceToBoolTransformer : IDataTransformer
        {
            object IDataTransformer.Transform(object o, DependencyProperty dp, CultureInfo culture)
            {
                ColorItem.Sources source = (ColorItem.Sources)o;
                return (source != ColorItem.Sources.BuiltIn);
            }

            object IDataTransformer.InverseTransform(object o, PropertyInfo info, CultureInfo culture)
            {
                return null;
            }
        }

        // One-way conversion between Luminance and Brush - used to draw text in
        // a color tile using a color that contrasts with the background

        public class LuminanceToBrushTransformer : IDataTransformer
        {
            object IDataTransformer.Transform(object o, DependencyProperty dp, CultureInfo culture)
            {
                double luminance = (double)o;
                return (luminance < 0.5) ? Brushes.White : Brushes.Black;
            }

            object IDataTransformer.InverseTransform(object o, PropertyInfo info, CultureInfo culture)
            {
                return null;
            }
        }
    }
}
