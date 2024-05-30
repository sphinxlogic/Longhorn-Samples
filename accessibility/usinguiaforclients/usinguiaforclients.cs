/************************************************************************************************\
* File: UsingUIAForClients.cs
*
* Description: 
*    This sample provides sample code that shows how User Interface (UI) Automation clients
*    can use UI Automation to do the following:
*        1. Find a LogicalElement
*        2. Get Supported Control Patterns
*        3. Register for an Event
*        4. Get Property Values
*
* Programming Elements:
*    This sample demonstrates the following UI Automation programming elements from the 
*     System.Windows.Automation namespace:
*        Automation class
*            FindLogicalElement() method
*            AddAutomationEventHandler() method
*            RemoveAutomationEventHandler() method
*        LogicalElement class
*            RootElement property
*            NameProperty automation property
*            AcceleratorKeyProperty automation property
*            AccessKeyProperty automation property
*            GetPattern()
*            GetPropertyValue()
*        InvokePattern class
*            Invoke()
*       
* Copyright (C) 2003 by Microsoft Corporation.  All rights reserved.
*
\************************************************************************************************/

using System;
using System.Windows.Forms;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Threading;

namespace SDKSample
{
    public class UsingUIAForClients: System.Windows.Application
    {
        AutomationEventHandler handler;
        LogicalElement element;
		
        protected override void OnStartingUp(StartingUpCancelEventArgs e)
        {
            CreateWindow();

			Form myForm = new frmButtonHolder();
			// Show the form that properties will be retrieved for.
			myForm.Show();

			Output("Looking for Button1 and listening for invoke commands when found.");

            // 1. Find a LogicalElement
            //
            //The following code shows how to find a specific instance of a LogicalElement.
            //First, create a MatchCondition array.  You are looking
            //for the logical element property, NameProperty 
            //and a property value, "Button1".
            MatchCondition[] conds = new MatchCondition[] {
                new MatchCondition(LogicalElement.NameProperty, "Button1")
            };

            // Start the search from the root...
            LogicalElement root = LogicalElement.RootElement;

            // Find functions can also be used in a loop to keep getting the next match, 
            //if more than one exists. For this example, just find the first one.
            element = Automation.FindLogicalElement(root, conds);

            // Check the element returned
            if (element == null)
            {
                // Didn't find it
                return;
            }

            // 2. Get Supported Control Patterns 
            //
            // Once you have an instance of a LogicalElement, you may want 
            // to check to see which control patterns it supports.  You can 
            // get all the control patterns that an element supports 
            // by calling GetSupportedPatterns(), but in this case, just
            // check if it supports the InvokePattern pattern by calling GetPatter().
            InvokePattern invokePattern = element.GetPattern(InvokePattern.Pattern) as InvokePattern;

            if (invokePattern == null)
            {
                // Doesn't support it
                return;
            }

            // The InvokePattern allows you to programmatically
            // invoke a button. It also exposes an event to 
            // let you know when the button has been invoked. The following
            // code shows an example of listening for the Invoked event on the
            // button and then invoking the button to receive the event.
            // Alternatively, you can use the Input.MoveToAndClick(element) 
            // to programmatically click the button.
            // 3. Register for an Event
            // 
            // In order to intercept the event when the button is invoked,
            // you can define a method as an AutomationEventHandler delegate.  
            // This example also shows how to specify ScopeFlags for the event 
            // of ineterest.
            handler = new AutomationEventHandler(OnInvoked);

            // Use AddAutomationEventHandler() to add an event handler to the event handling chain.
            Automation.AddAutomationEventHandler(InvokePattern.InvokedEvent, element, ScopeFlags.Element, handler);

            // Now invoke the button using the InvokePattern object returned
            // above.  This will cause the HandleInvokeEvent.OnInvoked method to be called.
            invokePattern.Invoke();
		}
        private static void OnInvoked(object source, AutomationEventArgs e)
        {
			// OK to cast since we're listening on a LogicalElement 
            LogicalElement srcEl = (LogicalElement)source;

            // 4. Get Property Values
            //
            // The button with NameProperty, Button1, has been invoked.  
            //
            string name = srcEl.GetPropertyValue(LogicalElement.NameProperty) as string;
			string acceleratorKey = srcEl.GetPropertyValue(LogicalElement.AcceleratorKeyProperty) as string;
            string accessKey = srcEl.GetPropertyValue(LogicalElement.AccessKeyProperty) as string;
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
        public void Output(string message)
        {
            _textElement = new Text();
            _textElement.TextRange.Text = message;
            DockPanel.SetDock(_textElement, Dock.Top);
            _panel.Children.Add(_textElement);
        }
        private Text _textElement;
        private DockPanel _panel;
        private Window _window;
        // Launch the sample application.
        internal sealed class TestMain
        {
            [System.STAThread()]
            static void Main()
            {
                Thread.CurrentThread.ApartmentState = System.Threading.ApartmentState.STA;

                // Create an instance of the sample class and call its Run() method to start it.
                UsingUIAForClients app = new UsingUIAForClients();

                app.Run();
            }
        }
        // Window shut down event handler..
        protected override void OnShuttingDown(ShuttingDownEventArgs e)
        {
            // To remove all event listeners you can use the RemoveAllEventHandlers method. 
            // However, here we just remove this event handler.
            try
            {
               if(element != null)
                  Automation.RemoveAutomationEventHandler(InvokePattern.InvokedEvent, element, handler);
            }
            catch (Exception error)
            {
               // If ourForm was closed before closing the main output window 
               // the button element no longer exists and an exception occurs.
            }
	        base.OnShuttingDown(e);
        }
    }
}
