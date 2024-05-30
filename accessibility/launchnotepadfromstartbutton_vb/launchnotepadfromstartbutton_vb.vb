'************************************************************************************************\
'* File: LaunchNotepad.cs
'*
'* Description: 
'*    This sample starts Notepad and sends keyboard input to it.
'*
'* Programming Elements:
'*    This sample demonstrates the following UI Automation programming elements from the 
'*    System.Windows.Automation namespace:
'*
'*       Input class
'*           SendKeyboardInput() method
'*
'* Copyright (C) 2003 by Microsoft Corporation.  All rights reserved.
'*
'\************************************************************************************************
Imports System
Imports System.Windows
Imports System.Windows.Documents
Imports System.Windows.Automation
Imports System.Windows.Controls
Imports System.Windows.Media
Imports System.Text
Imports System.Threading
Imports Microsoft.VisualBasic

Namespace SDKSample

    Public Class LaunchNotepad
        Inherits Application

        Private _textElement As System.Windows.Controls.Text
        Private _panel As DockPanel
        Private _window As Window

        Protected Overrides Sub OnStartingUp(ByVal e As StartingUpCancelEventArgs)
            CreateWindow()
            Output("This application launches Notepad.exe using the Automation Input ")
            Output("methods to bring up the Run Dialog with WinKey+R and then ")
            Output("sends keyboard input to the Run edit box to start Notepad.exe.")
            Output("Please wait ......." + ControlChars.Lf)
            ' Trap any exceptions.
            Try
                ' Simulate keyboard input for WinKey+R. Simulating keyboard input
                ' requires calling SendKeyboardInput twice: once with the
                ' press parameter set to true (key press), once with the 
                ' press parameter set to false (key release).
                System.Windows.Automation.Input.SendKeyboardInput(System.Windows.Input.Key.LWin, True)
                System.Windows.Automation.Input.SendKeyboardInput(System.Windows.Input.Key.R, True)
                System.Windows.Automation.Input.SendKeyboardInput(System.Windows.Input.Key.R, False)
                System.Windows.Automation.Input.SendKeyboardInput(System.Windows.Input.Key.LWin, False)

                ' When a click causes UI to appear and you want to interact with it, then
                ' you might need to wait until it is ready for user input. Another way to
                ' do this is to listen for UI Automation events on the expected UI.
                Thread.Sleep(1000)

                ' We know the input focus is on the "Open" EditBox so simulate keyboard
                ' input for "NOTEPAD".
                System.Windows.Automation.Input.SendKeyboardInput(System.Windows.Input.Key.N, True)
                System.Windows.Automation.Input.SendKeyboardInput(System.Windows.Input.Key.N, False)
                System.Windows.Automation.Input.SendKeyboardInput(System.Windows.Input.Key.O, True)
                System.Windows.Automation.Input.SendKeyboardInput(System.Windows.Input.Key.O, False)
                System.Windows.Automation.Input.SendKeyboardInput(System.Windows.Input.Key.T, True)
                System.Windows.Automation.Input.SendKeyboardInput(System.Windows.Input.Key.T, False)
                System.Windows.Automation.Input.SendKeyboardInput(System.Windows.Input.Key.E, True)
                System.Windows.Automation.Input.SendKeyboardInput(System.Windows.Input.Key.E, False)
                System.Windows.Automation.Input.SendKeyboardInput(System.Windows.Input.Key.P, True)
                System.Windows.Automation.Input.SendKeyboardInput(System.Windows.Input.Key.P, False)
                System.Windows.Automation.Input.SendKeyboardInput(System.Windows.Input.Key.A, True)
                System.Windows.Automation.Input.SendKeyboardInput(System.Windows.Input.Key.A, False)
                System.Windows.Automation.Input.SendKeyboardInput(System.Windows.Input.Key.D, True)
                System.Windows.Automation.Input.SendKeyboardInput(System.Windows.Input.Key.D, False)

                ' Simulate pressing the enter key for the OK button in the Run Dialog.
                System.Windows.Automation.Input.SendKeyboardInput(System.Windows.Input.Key.Enter, True)
                System.Windows.Automation.Input.SendKeyboardInput(System.Windows.Input.Key.Enter, False)
            Catch [error] As Exception
                Output(("Error: " + [error].Message))
            End Try
        End Sub 'OnStartingUp

        ' The CreateWindow() method creates a simple window to which output may be written.
        Private Sub CreateWindow()
            _window = New Window()
            _panel = New DockPanel()
            _window.Content = _panel
            _window.Show()
        End Sub 'CreateWindow

        ' The Output() method is used to output text to the application window.
        Private Sub Output(ByVal message As String)
            _textElement = New System.Windows.Controls.Text()
            _textElement.TextRange.Text = message
            DockPanel.SetDock(_textElement, Dock.Top)
            _panel.Children.Add(_textElement)
        End Sub 'Output

        ' Window shut down event handler..
        Protected Overrides Sub OnShuttingDown(ByVal e As ShuttingDownEventArgs)
            MyBase.OnShuttingDown(e)
        End Sub 'OnShuttingDown
        ' Launch the sample application.

        Friend NotInheritable Class TestMain

            <System.STAThread()> _
            Shared Sub Main()
                Thread.CurrentThread.ApartmentState = System.Threading.ApartmentState.STA
                ' Create an instance of the sample class and call its Run() method to start it.
                Dim app As New LaunchNotepad()
                app.Run()
            End Sub 'Main
        End Class 'TestMain
    End Class 'LaunchNotepad
End Namespace 'SDKSample

