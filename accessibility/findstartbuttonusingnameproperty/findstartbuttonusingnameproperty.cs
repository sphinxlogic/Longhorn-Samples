/*************************************************************************************************\
*
* File: FindStartButtonUsingNameProperty.cs
*
* Description: 
*    This sample demonstrates how to use User Interface Automation to find the "start" button
*    using the "Name" property, invoke it and handle the invoke event. Specifically:
*        1. Find the LogicalElement for the "start" button
*        2. Get the InvokePattern pattern
*        3. Register for the Invoke event
*        4. Use the InvokePattern control pattern to invoke the "start" button
*        5. Handle the invoke event
*
* Programming Elements:
*    This sample demonstrates the following UI Automation programming elements from the 
*    System.Windows.Automation namespace:
*
*       LogicalElement class
*            GetPropertyValue() method
*            GetPattern() method
*       Automation class
*			LogicalElementSearcher object
*           AddAutomationFocusChangedEventHandler() method
*           RemoveAutomationFocusChangedEventHandler() method
*
* Copyright (C) 2003 by Microsoft Corporation.  All rights reserved.
*
\*************************************************************************************************/
using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Text;
using System.Threading;
using System.Windows.Automation.Searcher;

namespace SDKSample
{
	class FindStartButtonUsingNameProperty : Application
	{
		private Text _textElement;
		private DockPanel _panel;
		private Window _window;
		private LogicalElement _element;
		private StringBuilder _sb;

		AutomationEventHandler handler;

		protected override void OnStartingUp(StartingUpCancelEventArgs e)
		{
			//create our window
			CreateWindow();

			// First, create a Searcher object. 
			LogicalElementSearcher les = new LogicalElementSearcher();

			// Get the root element as a starting place to look for the Start menu.  For
			// performance reasons, it's not a good idea to start searching for UI from
			// the root unless the UI you are looking for is very near the root, which 
			// the "start" button is.
			LogicalElement root = LogicalElement.RootElement;
			les.Root = root;

            //
			// 1. Find the LogicalElement for the "start" button
			//    We're looking for the logical element with the NameProperty property "start".
			//    ie, the "start" button with the Sidebar disabled (no name property available if Sidebar enabled). 
			//    This property is release dependent.
			//
            // 2. Get the InvokePattern pattern
			//    We're also using the Condition class to specify that the logical element must 
			//    also support the Invoke pattern.
			//
			les.Condition = new AndCondition(new PropertyCondition(LogicalElement.NameProperty, "start"), 
				new PatternPresentCondition(InvokePattern.Pattern));

			// Only have to find first since our root element is the desktop and the "start" button is a child.	
			LogicalElement _element = les.FindFirst();

			if (_element != null)
			{
			    //
                // 3. Register for the Invoke event
				//
				InvokePattern invokePattern = _element.GetPattern(InvokePattern.Pattern) as InvokePattern;
				handler = new AutomationEventHandler(OnInvoke);
				Automation.AddAutomationEventHandler(InvokePattern.InvokedEvent, _element, ScopeFlags.Element, handler);

				// Get the bounding rectangle for the currently focused element and display it's coordinates
				Rect rle = (Rect)_element.GetPropertyValue(LogicalElement.BoundingRectangleProperty);

				_sb = new StringBuilder();
				_sb.Append("\nObject found at:");
				_sb.Append("\nTop left corner: " + rle.TopLeft.ToString());
				_sb.Append("\nTop right corner: " + rle.TopRight.ToString());
				_sb.Append("\nBottom left corner: " + rle.BottomLeft.ToString());
				_sb.Append("\nBottom right corner: " + rle.BottomRight.ToString());
				Output(_sb.ToString());
				Output("\nInvoking object....");
				//
                // 4. Use the InvokePattern control pattern to invoke the "start" button
				//
				invokePattern.Invoke();
			}
			else
				Output("\nFailed to find the control.");
		}

        // 5. Handle the invoke event
		// Invoke event handler
		public void OnInvoke(object src, AutomationEventArgs e)
		{
			MessageBox.Show("Start button has been invoked.\n");
		}

		// The CreateWindow() method creates a simple window to which output may be written.
		private void CreateWindow()
		{
				_window = new Window();
				_panel = new DockPanel();
				_panel = new DockPanel();
				_window.Content = _panel;
				_window.Text = "FindStartButtonUsingNameProperty Sample";
				_window.Show();
				Output("Finding start button...");
			}

		// The Output() method is used to output text to the application window.
		private void Output(string message)
		{
			_textElement = new Text();
			_textElement.TextRange.Text = message;
			DockPanel.SetDock(_textElement, Dock.Top);
			_panel.Children.Add(_textElement);
		}

		// Window shut down event handler..
		protected override void OnShuttingDown(ShuttingDownEventArgs e)
		{
			// To remove all event listeners you can use the RemoveAllEventHandlers method. 
			// However, here we just remove the Invoke event handler.
			try
			{
				if (_element != null)
					Automation.RemoveAutomationEventHandler(InvokePattern.InvokedEvent, _element, handler);
			}
			catch (Exception error)
			{
				// If ourForm was closed before closing the main output window 
				// the button element no longer exists and an exception occurs.
				MessageBox.Show(error.ToString());
			}
			base.OnShuttingDown(e);
		}

		// Launch the sample application.
		internal sealed class TestMain
		{
			[System.STAThread()]
			static void Main()
			{
				Thread.CurrentThread.ApartmentState = System.Threading.ApartmentState.STA;
				// Create an instance of the sample class and call its Run() method to start it.
				FindStartButtonUsingNameProperty app = new FindStartButtonUsingNameProperty();
				app.Run();
			}
		}
	}
}
