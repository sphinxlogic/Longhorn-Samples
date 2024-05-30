using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Threading;

namespace Grid_Demo
{
	public class MyApp : System.Windows.Application
	{

		System.Windows.Controls.Grid grid1;
		System.Windows.Controls.ColumnDefinition colDef1;
		System.Windows.Controls.ColumnDefinition colDef2;
		System.Windows.Controls.ColumnDefinition colDef3;
		System.Windows.Controls.RowDefinition rowDef1;
		System.Windows.Controls.RowDefinition rowDef2;
		System.Windows.Controls.RowDefinition rowDef3;
		System.Windows.Controls.Text txt1;
		System.Windows.Controls.Text txt2;
		System.Windows.Controls.Text txt3;
		System.Windows.Controls.Text txt4;
		System.Windows.Controls.Text txt5;
		System.Windows.Controls.Text txt6;
		System.Windows.Controls.Text txt7;
		System.Windows.Window mainWindow;

		protected override void OnStartingUp (StartingUpCancelEventArgs e)
		{
			base.OnStartingUp (e);
			CreateAndShowMainWindow ();
		}

		private void CreateAndShowMainWindow ()
		{
			// Create the application's main window
			mainWindow = new System.Windows.Window ();

			// Create the Grid
			grid1 = new Grid ();
			grid1.Width = new Length (250);
			grid1.Height = new Length (100);
			grid1.ShowGridLines = true;

			// Define the Columns
			colDef1 = new ColumnDefinition();
			colDef2 = new ColumnDefinition();
			colDef3 = new ColumnDefinition();
			grid1.ColumnDefinitions.Add(colDef1);
			grid1.ColumnDefinitions.Add(colDef2);
			grid1.ColumnDefinitions.Add(colDef3);

			// Define the Rows
			rowDef1 = new RowDefinition();
			rowDef2 = new RowDefinition();
			rowDef3 = new RowDefinition();
			grid1.RowDefinitions.Add(rowDef1);
			grid1.RowDefinitions.Add(rowDef2);
			grid1.RowDefinitions.Add(rowDef3);

			// Add the first text cell to the Grid
			txt1 = new System.Windows.Controls.Text ();
			txt1.TextContent = "2004 Products Shipped";
			txt1.FontSize = new System.Windows.FontSize(20,System.Windows.FontSizeType.Pixel); 
			txt1.FontWeight = System.Windows.FontWeight.Bold;
			Grid.SetColumnSpan(txt1, 3);
			Grid.SetRow(txt1, 0);
			Grid.SetColumn(txt1, 0);

			// Add the second text cell to the Grid
			txt2 = new System.Windows.Controls.Text();
			txt2.TextContent = "Quarter 1";
			txt2.FontSize = new System.Windows.FontSize(12, System.Windows.FontSizeType.Pixel);
			txt2.FontWeight = System.Windows.FontWeight.Bold;
			Grid.SetRow(txt2, 1);
			Grid.SetColumn(txt2, 0);

			// Add the third text cell to the Grid
			txt3 = new System.Windows.Controls.Text();
			txt3.TextContent = "Quarter 2";
			txt3.FontSize = new System.Windows.FontSize(12, System.Windows.FontSizeType.Pixel);
			txt3.FontWeight = System.Windows.FontWeight.Bold;
			Grid.SetRow(txt3, 1);
			Grid.SetColumn(txt3, 1);

			// Add the fourth text cell to the Grid
			txt4 = new System.Windows.Controls.Text();
			txt4.TextContent = "Quarter 3";
			txt4.FontSize = new System.Windows.FontSize(12, System.Windows.FontSizeType.Pixel);
			txt4.FontWeight = System.Windows.FontWeight.Bold;
			Grid.SetRow(txt4, 1);
			Grid.SetColumn(txt4, 2);

			// Add the sixth text cell to the Grid
			txt5 = new System.Windows.Controls.Text();
			txt5.TextContent = "50,000";
			txt5.FontSize = new System.Windows.FontSize(12, System.Windows.FontSizeType.Pixel);
			txt5.FontWeight = System.Windows.FontWeight.Normal;
			Grid.SetRow(txt5, 2);
			Grid.SetColumn(txt5, 0);

			// Add the seventh text cell to the Grid
			txt6 = new System.Windows.Controls.Text();
			txt6.TextContent = "100,000";
			txt6.FontSize = new System.Windows.FontSize(12, System.Windows.FontSizeType.Pixel);
			txt6.FontWeight = System.Windows.FontWeight.Normal;
			Grid.SetRow(txt6, 2);
			Grid.SetColumn(txt6, 1);

			// Add the final text cell to the Grid
			txt7 = new System.Windows.Controls.Text();
			txt7.TextContent = "150,000";
			txt7.FontSize = new System.Windows.FontSize(12, System.Windows.FontSizeType.Pixel);
			txt7.FontWeight = System.Windows.FontWeight.Normal;
			Grid.SetRow(txt7, 2);
			Grid.SetColumn(txt7, 2);
			
			grid1.Children.Add(txt1);
			grid1.Children.Add(txt2);
			grid1.Children.Add(txt3);
			grid1.Children.Add(txt4);
			grid1.Children.Add(txt5);
			grid1.Children.Add(txt6);
			grid1.Children.Add(txt7);
			mainWindow.Content = grid1;
			mainWindow.Show ();

		}
	}

	internal sealed class EntryClass
	{
		[System.STAThread()]
		private static void Main ()
		{
			Thread.CurrentThread.ApartmentState = System.Threading.ApartmentState.STA;
			MyApp app = new MyApp ();
			app.Run ();
		}
	}
}
