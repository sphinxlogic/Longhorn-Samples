namespace ExpenseItApp
{
    using System;
    using System.Windows;
    using System.Windows.Navigation;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.ComponentModel;
    using System.Windows.Controls.Primitives;
    using System.Xml;

    public partial class Page1
    {
        XmlDataSource   employeesDataSrc;
        XmlDataSource   costCentersDataSrc;
		NavigationApplication myApp;
		NavigationWindow mainWindow;

        private void CreateReport(object sender, ClickEventArgs args)
        {
			mainWindow = (NavigationWindow)myApp.MainWindow;
            mainWindow.Navigate(new Uri ("page2.xaml", false, true));
        }

        private void Navigate(object sender, ClickEventArgs args)
        {
            Application.Navigate(sender);
        }

        private void OnEmployeeTypeChanged(object sender, SelectionChangedEventArgs args)
        {
            if (args.SelectedItems.Count > 0)
            {
                RadioButton rb = args.SelectedItems[0] as RadioButton;
                string query = string.Format("/Employees/Employee[@Type='{0}']", rb.ID);
                employeesDataSrc.XPath = query;
                employeesDataSrc.Refresh();
            }
        }

        #region Initialize
        
        private void Init(object sender, System.EventArgs args)
        {
			myApp = (NavigationApplication)System.Windows.Application.Current;

            costCentersDataSrc = (XmlDataSource)root.FindResource("CostCenters");
            // when data source has changed, will need to reevaluate the cost center's ComboBox.SelectedIndex
            ((IDataSource)costCentersDataSrc).DataChanged += new EventHandler(CostCentersDataChanged);
            
            employeesDataSrc = (XmlDataSource)root.FindResource("Employees");
            employeeTypeOptions.SelectedIndex = 0;
        }
        
#endregion

        private void CostCentersDataChanged(object sender, EventArgs args)
        {
            // use a transformer to map between cost center number and combobox's SelectedIndex
            costCenter.SetBinding(Selector.SelectedIndexProperty, "CostCenter", BindType.TwoWay, null, UpdateType. default, new CostCenter2Index(costCenter));
        }
    }

    // transform between a CostCenter and the SelectedIndex of the corresponding data item in the combobox
    // ideally, Selector controls will have a notion of SelectedValue properties where similar logic 
    // would become an internal part of the platform -> M7
    internal class CostCenter2Index : IDataTransformer
    {
        public CostCenter2Index(ComboBox cbo)
        {
            _cbo = cbo;
        }

        public object Transform(object o, DependencyProperty dp, System.Globalization.CultureInfo culture)
        {
            // o holds string with cost center, return index of this cost center in combobox
            string costCenter = (string)o;
            for (int i = 0; i < _cbo.Items.FlatView.Count; i++)
            {
                XmlNode node = _cbo.Items.FlatView[i] as XmlNode;
                XmlAttribute attr = node.Attributes["Number"];

                if (attr.Value == costCenter)
                {
                    return i;
                }
            }

            return null;
        }

        public object InverseTransform(object o, System.Reflection.PropertyInfo info, System.Globalization.CultureInfo culture)
        {
            // o holds index of item in cbo, return string with cost center
            int index = (int)o;
            if (index >= 0)
            {
                XmlNode node = _cbo.Items.FlatView[(int)o] as XmlNode;
                XmlAttribute attr = node.Attributes["Number"];

                return attr.Value;
            }
            return null;
        }

        private ComboBox    _cbo;
    }

}

