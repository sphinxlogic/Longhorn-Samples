using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Input;
using System.Threading;
using System.Windows.Serialization;
using System.Windows.Commands;
using System.ComponentModel;

using System.Collections;


[assembly: System.Security.AllowPartiallyTrustedCallers()]
namespace ExpenseItApp
{
    #region Chart
    public class Chart : Border
    {

        #region Constructor
        public Chart ()
        {
            // init private variables
            _sChartType = ""; 

            // BUGBUG: set this in the xaml file

            //
            // GradientStop Color Animations
            //
            LinearGradientBrush lgBrush;

            lgBrush = new LinearGradientBrush ();
            
            lgBrush.StartPoint = new Point (0.0, 0.0);
            lgBrush.EndPoint = new Point (1.0, 0.0);

            lgBrush.MappingMode = BrushMappingMode.RelativeToBoundingBox;

            lgBrush.GradientStops.Add (new GradientStop ( Color.FromRGB (0x4E,0x87,0xD4) , 0.0));
            //lgBrush.GradientStops.Add (new GradientStop (Colors.White, 0.5));
            lgBrush.GradientStops.Add (new GradientStop (Color.FromRGB (0x73,0xB2,0xF5) , 1.0));
            
            this.Background = lgBrush;
            
            //this.Background           = Brushes.SeaShell;
            this.BorderBrush        = Brushes.SeaGreen;
            this.BorderThickness    = new Thickness (new Length (2.0));
            this.Width              = new Length (400.0);
            this.Height             = new Length (325.0);

            // create a DockPanel to place the visuals
            _dp = new DockPanel ();
            this.Child = _dp;
            
            // Add the visuals to the dock panel
            UIElement v = new UIBar (); // this class uses the OnRender call for drawing


            // add the UI Element to the dock panel
            DockPanel.SetDock (v, Dock.Top);
            ((IAddChild)_dp).AddChild (v);

        }
        #endregion Constructor

        #region Public Properties
        public string ChartType
        {
            get {return _sChartType;}
            set {_sChartType = value;}
        }
        #endregion Public Properties

        #region Local variables

        private string      _sChartType;    // type of chart to draw
        private DockPanel   _dp;            // root Dock Panel

        #endregion Local variables
    }
    #endregion Chart

    #region UIBar
    // BUGBUG: each bar should be an element, but for now just draw something.
    public class UIBar : UIElement
    {
        protected override void OnRender (DrawingContext ctx)
        {
            FormattedText text = new FormattedText ("ExpenseIt Chart", new Typeface ("Arial"), 16, Brushes.Black);
            ctx.DrawText (text, new Point (20, 20));

            LinearGradientBrush lgBrush;

            lgBrush = new LinearGradientBrush ();
            lgBrush.StartPoint = new Point (0.0, 0.0);
            lgBrush.EndPoint = new Point (1.0, 0.0);
            lgBrush.MappingMode = BrushMappingMode.RelativeToBoundingBox;
            lgBrush.GradientStops.Add (new GradientStop (Colors.Red, 0.0));
            lgBrush.GradientStops.Add (new GradientStop (Colors.White, 0.5));
            lgBrush.GradientStops.Add (new GradientStop (Colors.Red, 1.0));

            ctx.DrawRectangle (lgBrush, null, new Rect ( 40,  50, 50, 250));
            ctx.DrawRectangle (lgBrush, null, new Rect ( 130, 150, 50, 150));
            ctx.DrawRectangle (lgBrush, null, new Rect (220, 250, 50,  50));
            ctx.DrawRectangle (lgBrush, null, new Rect (310, 200, 50,  100));
        }
    }
    #endregion

    #region Expense Classes
    public class Expense : IPropertyChange
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Expense()
        {
            _lineItems = new LineItemCollection();
            ((ICollectionChange)_lineItems).CollectionChanged += new CollectionChangeEventHandler(OnLineItemsChanged);
        }

        public string Alias
        {
            get { return _alias; }
            set
            {
                _alias = value;
                NotifyPropertyChanged("Alias");
            }
        }
                
        public string CostCenter
        {
            get { return _costCenter; }
            set
            {
                _costCenter = value;
                NotifyPropertyChanged("CostCenter");
            }
        }
                
        public string EmployeeNumber
        {
            get { return _employeeNumber; }
            set
            {
                _employeeNumber = value;
                NotifyPropertyChanged("EmployeeNumber");
            }
        }

        public int TotalExpenses
        {
            // calculated property, no setter
            get { return _totalExpenses; }
        }

        public LineItemCollection LineItems
        {
            get { return _lineItems; }
        }

        private void OnLineItemsChanged(object sender, CollectionChangeEventArgs e)
        {
            RecalculateTotalExpense();
        }

        private void RecalculateTotalExpense()
        {
            _totalExpenses = 0;
            foreach (LineItem item in LineItems)
            {
                _totalExpenses += item.Cost;
            }
            NotifyPropertyChanged("TotalExpenses");
        }

        private void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        private string _alias;
        private string _costCenter;
        private string _employeeNumber;
        private LineItemCollection _lineItems;
        private int _totalExpenses;
    }

    public class LineItem : IPropertyChange
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Type
        {
            get { return _type; }
            set
            {
                _type = value;
                NotifyPropertyChanged("Type");
            }
        }
                
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                NotifyPropertyChanged("Description");
            }
        }
                
        public int Cost
        {
            get { return _cost; }
            set
            { 
                _cost = value;
                NotifyPropertyChanged("Cost");
            }
        }

        
        private void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        private string  _type           = "(Expense type)";
        private string  _description    = "(Description)";
        private int     _cost           = 0;
    }

    public class LineItemCollection : System.Windows.Data.ArrayListDataCollection, ICollectionChange    
    {
        public LineItemCollection()
        {
            base.CollectionChanged += new CollectionChangeEventHandler(ForwardBaseCollectionChanged);
        }

        public LineItemCollection(int capacity) : base(capacity)
        {
            base.CollectionChanged += new CollectionChangeEventHandler(ForwardBaseCollectionChanged);
        }

        public override int Add(object value)
        {
            IPropertyChange item = value as IPropertyChange;

            if (item != null)
            {
                item.PropertyChanged += new PropertyChangedEventHandler(LineItemPropertyChanged);
            }
            return base.Add(value);
        }
        
        private void LineItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Cost")
            {
                RaiseCollectionChanged(this, new CollectionChangeEventArgs(CollectionChangeAction.Refresh, this));
            }
        }

        public new event CollectionChangeEventHandler CollectionChanged;
        
        private void ForwardBaseCollectionChanged(object sender, CollectionChangeEventArgs args)
        {
            RaiseCollectionChanged(sender, args);
        }
        
        void RaiseCollectionChanged(object sender, CollectionChangeEventArgs args)
        {
            if (CollectionChanged != null)
            {
                CollectionChanged(sender, args);
            }
        }
    }

    #endregion    
}



