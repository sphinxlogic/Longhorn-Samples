using System;
using System.Windows;
using System.Threading;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.ComponentModel;
using System.Windows.Data;

namespace Form1
{

	public class Form1:Application
	{
		public Text mytext;
		public Button button;
		public Button button2;
		public myData myDataObject;
		public Bind myBindDef;
		public int CurrentFontSize = 20;
		public DockPanel dp;
		public FlowPanel fp;

		protected override void OnStartingUp(StartingUpCancelEventArgs e)
		{
			Window win = new Window();
			DockPanel root = new DockPanel();
            win.Content = root;
			root.Width = new Length(400);

			dp = new DockPanel();
            fp = new FlowPanel();
            DockPanel.SetDock(dp,Dock.Bottom);
			root.Children.Add(dp);
            root.Children.Add(fp);

			button = new Button();
			button.ID = "Clear";
            button.Content = "Clear";
			button.Width = new Length(200);
			button.Height = new Length(30);
			dp.Children.Add(button);

			button2 = new Button();
			button2.Content = "Change Binding";
			button2.ID = "Change";
			button2.Width = new Length(200);
            button2.Height = new Length(30);
			dp.Children.Add(button2);

			mytext = new Text();
            mytext.ID = "BoundControl";
            mytext.TextContent = "no binding yet..."; //this text will never appear, the binding is established immediately
			mytext.Width = new Length(200);
            mytext.Height = new Length(30);
            fp.Children.Add(mytext);

			CreateBinding(); //first binding established here
			win.Show();
		}

		public void CreateBinding()
		{
			myDataObject = new myData(System.DateTime.Now);
			myBindDef = new Bind("MyDataProperty", BindType.TwoWay, new ExplicitObjectRef(myDataObject));
			mytext.SetBinding(Text.TextContentProperty, myBindDef);
			ClickEventHandler ceh = new ClickEventHandler(OnClick);
			button.Click += ceh;
			button2.Click += ceh;
		}

		public void OnClick(Object obj, ClickEventArgs args)
		{
        	FrameworkElement fe = (FrameworkElement) obj;
			switch(fe.ID)
			{
				case "Clear":
					mytext.ClearAllBindings();
					break;
				case "Change":
					//make a new source
					myData myChangedDataObject = new myData(DateTime.Now);
					//recycle the previous Bind definition, but feed it a new source
					myBindDef.Source =  new ExplicitObjectRef(myChangedDataObject);
					if (mytext.GetBinding(Text.TextContentProperty)==null) {
                    	//binding has been cleared, remake it; else the old binding is still there
	                    mytext.SetBinding(Text.TextContentProperty, myBindDef);
                    }
                    break;
			}
		}
	}


		public class myData: IPropertyChange
		{
			private string _myDataProperty;

			public myData(DateTime adate)
			{
				_myDataProperty = "Last bound time was " + adate.ToLongTimeString();
			}


			public String MyDataProperty
			{
				get{return  _myDataProperty;}
				set
				{
					 _myDataProperty = value;
					NotifyPropertyChanged("MyDataProperty");}
			}

			//Declare event
			public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
			//NotifyPropertyChanged event handler to update property value in binding
			private void NotifyPropertyChanged(string info)
			{
				if (PropertyChanged !=null)
				{
					PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(info));
				}
			}
		}
	internal sealed class TestMain
	{
    	[System.STAThread()]
		public static void Main()
		{
			Thread.CurrentThread.ApartmentState = System.Threading.ApartmentState.STA;
			Form1 app = new Form1();
			app.Run();
		}
	}
}