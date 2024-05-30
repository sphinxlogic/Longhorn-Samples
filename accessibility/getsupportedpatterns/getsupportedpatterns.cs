/*************************************************************************************************\
* File: GetSupportedPatterns.cs
*
* Description:
*    This sample navigates the logical tree starting at the root, uses GetSupportedPatterns()  
*    to get an array of supported control patterns for each LogicalElement in the tree, and 
*    displays the names of the supported control patterns for each LogicalElement. 
*
* Programming Elements:
*    This sample demonstrates the following UI Automation programming elements from the 
*     System.Windows.Automation namespace:
*
*       LogicalElement class
*           RootElement property
*           FirstChild property
*           NextSibling property
*           GetPattern() method
*           GetSupportedPatterns() method
*       AutomationProperty class
*           ToString() method
*
* Copyright (C) 2003 by Microsoft Corporation.  All rights reserved.
\*************************************************************************************************/

using System;
using System.Windows.Forms;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Threading;

namespace SDKSample
{
    public class GetSupportedPatterns: System.Windows.Application
    {
      private Text _textElement;
      private DockPanel _panel;
      private Window _window;
      private static Form myForm;

        protected override void OnStartingUp(StartingUpCancelEventArgs e)
        {
            CreateWindow();

			// Add the form that we want to get the patterns for.
            myForm = new Form();
			myForm.Show();

			LogicalElement el = GetLogicalElementFromPoint();

            DisplayInfo(el);
        }

        // Display the control patterns supported by the logical element.
        private void DisplayInfo (LogicalElement element)
        {
            // For each LogicalElement in the tree, get an array of AutomationPatterns 
            // supported by the LogicalElement.
            AutomationPattern[] patterns = element.GetSupportedPatterns ();

            // For each desktop item, display the name of the supported control patterns.
            foreach (AutomationPattern pattern in patterns)
            {
               Output ("this element implements control pattern: " +
                    pattern.ToString () + "");
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
         _window = new Window();
         _panel = new DockPanel();
         _window.Content = _panel;
         _window.Show();
      }

      // The Output() method is used to output text to the application window.
      private void Output(string message)
      {
         _textElement = new Text();
         _textElement.TextRange.Text = message;
         DockPanel.SetDock(_textElement, Dock.Top);
         _panel.Children.Add(_textElement);
      }


      // Launch the sample application.
      internal sealed class TestMain
      {
         [System.STAThread()]
         static void Main()
         {
             Thread.CurrentThread.ApartmentState = System.Threading.ApartmentState.STA;

             // Create an instance of the sample class and call its Run() method to start it.
             GetSupportedPatterns app = new GetSupportedPatterns();
             app.Run();
         }
      }
    }


}



