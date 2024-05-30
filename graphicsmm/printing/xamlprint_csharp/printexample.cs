using System;

namespace xamlprint_csharp
{
	/// <summary>
	/// Summary description for PrintExample.
	/// </summary>

	public class PrintExample
	{
		public static void PrintPaginatedVisualToSelectedPrinter()
		{
			// Let user select a printer queue using common dialog box
			System.Printing.PrintSubSystem.PrintQueue queue = System.Windows.Media.PrintContext.SelectPrintQueue();

			if (queue != null)
			{
				// Create System.Windows.Media.PrintContext if OK button was clicked
				System.Windows.Media.PrintContext pc = new System.Windows.Media.PrintContext(queue, "Printing Example");

				// Add a page of visual
				pc.AddPage(new VectorPage());

				// Print
				pc.Print();
			}
			else
			{
				
			}
		}

		public static void PrintPaginatedVisualToDefaultPrinter()
		{
			// create a print context for my default printer
			System.Windows.Media.PrintContext pc = new System.Windows.Media.PrintContext();

			// Add two pages to the default rendition
			pc.AddPage(new VectorPage());
			pc.AddPage(new ImagePage());

			//Print 
			pc.Print();
			
		}

		public static void QueryPaperSize()
		{
			// Let user select a printer queue using common dialog box
			System.Printing.PrintSubSystem.PrintQueue queue = System.Windows.Media.PrintContext.SelectPrintQueue();

			if (queue != null)
			{
				// Create System.Windows.Media.PrintContext if OK button was clicked
				System.Windows.Media.PrintContext pc = new System.Windows.Media.PrintContext(queue, "Vector Printing Example");
				double width, height;

				GetPaperWidthHeight(pc.JobTicket, queue, out width, out height);

				// Add a page of visual
				pc.AddPage(new DimensionPage(width, height));

				// Print
				pc.Print();
			}
		}

		public static void MixedOrientationPrint()
		{
			System.Printing.PrintSubSystem.LocalPrintServer ps = new System.Printing.PrintSubSystem.LocalPrintServer();
			System.Printing.PrintSubSystem.PrintQueue queue = ps.DefaultPrintQueue;
			System.Windows.Media.PrintContext pc = new System.Windows.Media.PrintContext(queue, "Landscape Example");
			Microsoft.Printing.JobTicket.JobTicket jt = pc.JobTicket;
			{
				jt.PageOrientation.Value = System.Printing.Configuration.PrintSchema.OrientationValues.Landscape;

				double width, height;

				GetPaperWidthHeight(pc.JobTicket, queue, out width, out height);

				// Add a page of visual
				pc.AddPage(new DimensionPage(height, width));
			}
			{
				jt.PageOrientation.Value = System.Printing.Configuration.PrintSchema.OrientationValues.Portrait;

				double width, height;

				GetPaperWidthHeight(pc.JobTicket, queue, out width, out height);

				// Add a page of visual
				pc.AddPage(new DimensionPage(width, height));
			}

			// Print
			pc.Print();
		}

		private static System.Windows.Media.Visual GeneratePage(object sender, int pageNo, System.Windows.Media.GetPageEventArgs ev)
		{
			if (pageNo == 0)
			{
				return new VectorPage();
			}
			else if (pageNo == 1)
			{
				return new ImagePage();
			}
			else
				return null;
		}

		public static void EventDriven()
		{
			System.Printing.PrintSubSystem.LocalPrintServer ps = new System.Printing.PrintSubSystem.LocalPrintServer();
			System.Printing.PrintSubSystem.PrintQueue queue = ps.DefaultPrintQueue;

			// Create System.Windows.Media.PrintContext if OK button was clicked
			System.Windows.Media.PrintContext pc = new System.Windows.Media.PrintContext(queue, "EventDriven(2 pages)");

			pc.GetPage += new System.Windows.Media.PrintContext.GetPageEventHandler(GeneratePage);

			// Print
			pc.Print();
		}

		public static System.Windows.UIElement LoadContent(string xamlFilePath)
		{
			System.IO.Stream xamlFileStream = System.IO.File.OpenRead(xamlFilePath);
			System.Windows.UIElement root = null;

			try
			{
				System.Windows.Serialization.ParserContext pc = new System.Windows.Serialization.ParserContext();
				System.Security.PermissionSet ps = System.Windows.TrustManagement.TrustManager.GetDefaultPermissions();
				System.Security.Permissions.FileIOPermission fiop = new System.Security.Permissions.FileIOPermission(System.Security.Permissions.FileIOPermissionAccess.AllAccess, System.IO.Path.GetFullPath(".\\"));

				ps.AddPermission(fiop);
				root = (System.Windows.UIElement)System.Windows.Serialization.Parser.LoadXml(xamlFileStream, null, pc, null, ps);
			}
			catch (Exception e)
			{
				Console.WriteLine("Load Failed:");
				Console.WriteLine(e);
			}
			finally
			{
				// done with the stream
				xamlFileStream.Close();
			}
			return root;
		}

		public static void PrintXamlFile(string xamlFilePath)
		{
			// Let user select a printer queue using common dialog box
			System.Printing.PrintSubSystem.PrintQueue queue = System.Windows.Media.PrintContext.SelectPrintQueue();

			if (queue != null)
			{
				//using (Context.Access()) {
				System.Windows.Media.PrintContext pc = new System.Windows.Media.PrintContext(queue, "Element printing test");
				System.Windows.UIElement page = LoadContent(xamlFilePath);

				//{
				System.Windows.LayoutManager lm = new System.Windows.LayoutManager(page);
				double width, height;

				GetPaperWidthHeight(pc.JobTicket, queue, out width, out height);
				lm.Size = new System.Windows.Size(width, height);
				lm.UpdateLayout();

				//}
				pc.AddPage(page);
				pc.Print();
				//}
			}
		}

		public static void PrintToFile()
		{
			System.Printing.PrintSubSystem.LocalPrintServer ps = new System.Printing.PrintSubSystem.LocalPrintServer();
			System.Printing.PrintSubSystem.PrintQueue queue = ps.DefaultPrintQueue;

			// Create PrintContext if OK button was clicked
			System.Windows.Media.PrintContext pc = new System.Windows.Media.PrintContext(queue, "PrintToFile");

			pc.Output = "test.prn";

			// Add a page of visual
			pc.AddPage(new VectorPage());

			// Print
			pc.Print();
		}


		#region Helper Methods
		
		internal static void GetPaperWidthHeight(Microsoft.Printing.JobTicket.JobTicket jt, System.Printing.PrintSubSystem.PrintQueue pq, out double width, out double height)
		{
			Microsoft.Printing.DeviceCapabilities.DeviceCapabilities devcap = new Microsoft.Printing.DeviceCapabilities.DeviceCapabilities(pq.AcquireDeviceCapabilities(null));

			devcap.LengthUnitType = System.Printing.Configuration.PrintSchema.LengthUnitTypes.Inch;
			width = (double)devcap.PageCanvasSizeCap.CanvasSizeX * 96;
			height = (double)devcap.PageCanvasSizeCap.CanvasSizeY * 96;
		}


		#endregion
	}
	#region VectorPage
	// A System.Windows.Media.DrawingVisual that is used in this sample to demonstrate System.Windows.Media.Visual printing.
	public class VectorPage : System.Windows.Media.DrawingVisual
	{
		public VectorPage() : base()
		{
			using (System.Windows.Media.DrawingContext ctx = RenderOpen())
			{
				Render(ctx);
			}
		}

		void Render(System.Windows.Media.DrawingContext ctx)
		{
			const float inch = 96.0f;

			if (null == ctx) return;

			System.Windows.Media.Color gray = System.Windows.Media.Color.FromScRGB(1.0f, 0.5f, 0.5f, 0.5f);

			ctx.DrawRectangle(new System.Windows.Media.SolidColorBrush(gray), null, new System.Windows.Rect(inch / 2, inch / 2, inch * 7.5f, inch * 10));

			System.Windows.Media.Color blue = System.Windows.Media.Color.FromScRGB(1.0f, 0.0f, 0.0f, 1.0f);
			System.Windows.Media.Color red = System.Windows.Media.Color.FromScRGB(1.0f, 1.0f, 0.0f, 0.0f);
			System.Windows.Media.Color yellow = System.Windows.Media.Color.FromScRGB(1.0f, 1.0f, 1.0f, 0.0f);
			System.Windows.Media.Brush colorBrush = new System.Windows.Media.SolidColorBrush(blue);
			System.Windows.Media.Brush horGradientBrush = new System.Windows.Media.LinearGradientBrush(red, yellow, 0);
			System.Windows.Media.Brush verGradientBrush = new System.Windows.Media.LinearGradientBrush(yellow, blue, 90);
			System.Windows.Media.Brush radGradientBrush = new System.Windows.Media.RadialGradientBrush(blue, yellow);
			float r = 1;

			ctx.DrawRoundedRectangle(colorBrush, null, new System.Windows.Rect(inch, inch, inch * 1.5, inch * 1.5), r * inch / 8, r * inch / 8);
			ctx.DrawRoundedRectangle(horGradientBrush, null, new System.Windows.Rect(inch, inch * 3, inch * 1.5, inch * 1.5), r * inch / 8, r * inch / 8);
			ctx.DrawRoundedRectangle(verGradientBrush, null, new System.Windows.Rect(inch, inch * 5, inch * 1.5, inch * 1.5), r * inch / 8, r * inch / 8);
			ctx.DrawRoundedRectangle(verGradientBrush, null, new System.Windows.Rect(inch * 3, inch, inch * 1.5, inch * 1.5), r * inch / 2, r * inch / 2);
			ctx.DrawRoundedRectangle(radGradientBrush, null, new System.Windows.Rect(inch * 3, inch * 3, inch * 1.5, inch * 1.5), r * inch / 2, r * inch / 2);
		}
	}
	#endregion

    #region DimensionPage
	public class DimensionPage : System.Windows.Media.DrawingVisual
	{
		public DimensionPage(double width, double height) : base()
		{
			const double inch = 96;

			using (System.Windows.Media.DrawingContext ctx = RenderOpen())
			{
				// half inch margin on all four sides
				ctx.DrawRectangle(null, new System.Windows.Media.Pen(System.Windows.Media.Brushes.Black, 0.5), new System.Windows.Rect(inch / 2, inch / 2, width - inch, height - inch));

				// 1 inch x 1 inch box 1 inch x 1 inch from top-left corner
				ctx.DrawRectangle(null, new System.Windows.Media.Pen(System.Windows.Media.Brushes.Black, 1), new System.Windows.Rect(inch, inch, inch, inch));
			}
		}
	}
		#endregion

		#region ImagePage
	public class ImagePage : System.Windows.Media.DrawingVisual
	{
		public ImagePage() : base()
		{
			using (System.Windows.Media.DrawingContext ctx = RenderOpen())
			{
				Render(ctx);
			}
		}

		void Render(System.Windows.Media.DrawingContext ctx)
		{
			System.Windows.Media.ImageData tulip = System.Windows.Media.ImageData.Create(@"data\tulip.jpg");

			ctx.DrawRectangle(new System.Windows.Media.ImageBrush(tulip), new System.Windows.Media.Pen(System.Windows.Media.Brushes.Orange, 2), new System.Windows.Rect(96, 96, 96, 96));
		}
	}
		#endregion

}
