/*****************************************************************************************\
* File: LaunchNotepad.cs
*
* Description: 
*    This sample starts Notepad and sends keyboard input to it.
*
* Programming Elements:
*    This sample demonstrates the following UI Automation programming elements from the 
*    System.Windows.Automation namespace:
*
*       Input class
*           SendKeyboardInput() method
*
* Copyright (C) 2003 by Microsoft Corporation.  All rights reserved.
*
\******************************************************************************************/
using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Diagnostics;
using System.Threading;

namespace SDKSample
{
    public class LaunchNotepad: Application
    {
        private Text        _textElement;
        private DockPanel   _panel;
        private Window      _window;

        protected override void OnStartingUp(StartingUpCancelEventArgs e)
        {
            CreateWindow();
            Output("This application launches Notepad.exe using the Automation Input ");
            Output("methods to bring up the Run Dialog with WinKey+R and then ");
            Output("sends keyboard input to the Run edit box to start Notepad.exe.");
            Output("Please wait .......\n");
            // Trap any exceptions.
            try
            {
                // Simulate keyboard input for WinKey+R. Simulating keyboard input
                // requires calling SendKeyboardInput twice: once with the
                // press parameter set to true (key press), once with the 
                // press parameter set to false (key release).
                Input.SendKeyboardInput(System.Windows.Input.Key.LWin, true);
                Input.SendKeyboardInput(System.Windows.Input.Key.R, true);
                Input.SendKeyboardInput(System.Windows.Input.Key.R, false);
                Input.SendKeyboardInput(System.Windows.Input.Key.LWin, false);

                // When a click causes UI to appear and you want to interact with it, then
                // you might need to wait until it is ready for user input. Another way to
                // do this is to listen for UI Automation events on the expected UI.
                Thread.Sleep(1000);

                // We know the input focus is on the "Open" EditBox so simulate keyboard
                // input for "NOTEPAD".
                Input.SendKeyboardInput(System.Windows.Input.Key.N, true);
                Input.SendKeyboardInput(System.Windows.Input.Key.N, false);
                Input.SendKeyboardInput(System.Windows.Input.Key.O, true);
                Input.SendKeyboardInput(System.Windows.Input.Key.O, false);
                Input.SendKeyboardInput(System.Windows.Input.Key.T, true);
                Input.SendKeyboardInput(System.Windows.Input.Key.T, false);
                Input.SendKeyboardInput(System.Windows.Input.Key.E, true);
                Input.SendKeyboardInput(System.Windows.Input.Key.E, false);
                Input.SendKeyboardInput(System.Windows.Input.Key.P, true);
                Input.SendKeyboardInput(System.Windows.Input.Key.P, false);
                Input.SendKeyboardInput(System.Windows.Input.Key.A, true);
                Input.SendKeyboardInput(System.Windows.Input.Key.A, false);
                Input.SendKeyboardInput(System.Windows.Input.Key.D, true);
                Input.SendKeyboardInput(System.Windows.Input.Key.D, false);

                // Simulate pressing the enter key for the OK button in the Run Dialog.
                Input.SendKeyboardInput(System.Windows.Input.Key.Enter, true);
                Input.SendKeyboardInput(System.Windows.Input.Key.Enter, false);
            }
            catch (Exception error)
            {
                Output("Error: " + error.Message);
            }
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

        // Window shut down event handler
        protected override void OnShuttingDown(ShuttingDownEventArgs e)
        {
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
                LaunchNotepad app = new LaunchNotepad();
                app.Run();
            }
        }
    }
}


