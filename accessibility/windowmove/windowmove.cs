/*************************************************************************************************\
*
* File: WindowMove.cs
*
* Description:  
*       Moves a form window at a specified location to the top left corner  
*       of the desktop. This is done by getting a logical element (lefp) based 
*		corresponding to the form based on a point on the form (by calling  
*       the local function GetLogicalElementFromPoint), and then getting a window
*       pattern object from this logical element (using LogicalElement.GetPattern()).
*       Then, it navigates the logical tree until reaching a top-level window that 
*       contains a logical element. Once this is done it checks to see if the 
*       WindowPattern of the top-level window parent is moveable.
*       Focus is set to the top-level window containing the logical element
*       and a rect for both the logical element parent window and the 
*       desktop for screen location calculations is created.
*       The top and left coordinates needed to move the parent window is set
*       and window is moved to the new coordinates if these coordinates
*       are not out of range.
* 
* Programming Elements:
*    This sample demonstrates the following UI Automation programming elements from the 
*     System.Windows.Automation namespace:
*
*       LogicalElement class
*           RootElement property
*           FromPoint() method
*           GetPattern() method
*           Parent property
*           SetFocus() method
*           GetPropertyValue() method
*           BoundingRectangleProperty property
*       WindowPattern class
*           Moveable property
*           MoveTo() method
*       AutomationProperty class
*           ToString() method
*
* Copyright (C) 2003 by Microsoft Corporation.  All rights reserved.
*
\*************************************************************************************************/
using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Documents;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Threading;

namespace SDKSample
{
    public class WindowMove:  System.Windows.Application
    {
		private Text _textElement;
		private DockPanel _panel;
		private Window _window;
		private static Form myForm; 

		protected override void OnStartingUp(StartingUpCancelEventArgs e)
        {
            // Create the informational window and the form that will be moved
			CreateWindow();
			

            // Root logical element for current desktop - children include the top-level windows.
            LogicalElement root = LogicalElement.RootElement;

			Output("Find the logical element for the form based on a point on the form. ");

			// Get a logical element based based on a point on the form.
			LogicalElement lefp = GetLogicalElementFromPoint();

            // Does the logical element exist?
            if (lefp == null)
                return;

            //Create a window pattern object from the logical element.
            WindowPattern windowPattern = lefp.GetPattern(WindowPattern.Pattern) as WindowPattern;

            // Look up the tree for the logical elment's parents until reaching 
            // a top level window parent.
            while (windowPattern == null)
            {
				if (lefp.Parent == null)
				{
					Output("Top level reached in search for window.");
					return;
				}
				else
				{
					lefp = lefp.Parent;
				}

				windowPattern = lefp.GetPattern(WindowPattern.Pattern) as WindowPattern;
            }

            // Is the WindowPattern object moveable?
            if (windowPattern.Moveable)
            {
                // Set focus to the top level window containing this logical element.
                lefp.SetFocus();

                // Create a rect for both the logical element parent window and the desktop
                // for screen location calculations.
                Rect rle = (Rect)lefp.GetPropertyValue(LogicalElement.BoundingRectangleProperty);
                Rect rroot = (Rect)root.GetPropertyValue(LogicalElement.BoundingRectangleProperty);

                // Get the top and left coordinates to move the parent window 
                Output("Attempting move of Top, Left corner of the window from " + rle.Top.ToString() + "," + rle.Left.ToString() + " to 0,0");

                int top = 0;
                int left = 0;

                Thread.Sleep(4000);
				Output("Moving the form to left top corner...");

				// Move window if coordinates not out of range.
                // By default, MoveTo doesn't allow a window to be moved completely off-screen.
                if ((top <= (rroot.Bottom - (rle.Bottom - rle.Top))) && (left <= (rroot.Right - (rle.Right - rle.Left))))
                {
                    windowPattern.MoveTo(left, top);
                    Output("Window moved successfully.");
                }
                else
                {
                    Output("Window is too wide, part of window will appear off-screen. Run again please.");
                }
            }
            else
            {
                Output("Wndow is not moveable. Run again please.");
            }
        }
        
		
		// Get a logical element based on point on the form.
        public static LogicalElement GetLogicalElementFromPoint()
        {
            System.Windows.Point pt = new System.Windows.Point();

			Thread.Sleep(4000);
			pt.X = (int)myForm.Location.X + 10;
			pt.Y = (int)myForm.Location.Y + 10;
			return LogicalElement.FromPoint(pt);
        }
        // The CreateWindow() method creates a simple window to which output may be written.
        private void CreateWindow()
        {
			// Create the informational window.
            _window = new Window();
            _panel = new DockPanel();
            _window.Content = _panel;
            _window.Show();

			// Create the movable form.
			myForm = new Form();
			myForm.Text = "Movable Window";
			myForm.Show();

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
            // Remove the event handler. You can also use RemoveAllEventHandlers() to 
            // remove all event handlers. 
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
                WindowMove app = new WindowMove();
                app.Run();
            }
        }
    }
}
