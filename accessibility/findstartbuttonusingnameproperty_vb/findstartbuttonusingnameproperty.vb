'************************************************************************************************\
'*
'* File: FindStartButtonUsingNameProperty_vb.vb
'*
'* Description: 
'*    This sample demonstrates how to use User Interface Automation to find the "start" button
'*    using the "Name" property, invoke it and handle the invoke event. Specifically:
'*        1. Find the LogicalElement for the "start" button
'*        2. Get the InvokePattern pattern
'*        3. Register for the Invoke event
'*        4. Use the InvokePattern control pattern to invoke the "start" button
'*        5. Handle the invoke event
'*
'* Programming Elements:
'*    This sample demonstrates the following UI Automation programming elements from the 
'*    System.Windows.Automation namespace:
'*
'*       LogicalElement class
'*          GetPropertyValue() method
'*          GetPattern() method
'*       Automation class
'*			LogicalElementSearcher object
'*          AddAutomationFocusChangedEventHandler() method
'*          RemoveAutomationFocusChangedEventHandler() method
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
Imports System.Windows.Automation.Searcher
Imports Microsoft.VisualBasic


Namespace SDKSample

    Class FindStartButtonUsingNameProperty
        Inherits Application
        Private _textElement As System.Windows.Controls.Text
        Private _panel As DockPanel
        Private _window As Window
        Private _element As LogicalElement
        Private _sb As StringBuilder

        Private handler As AutomationEventHandler


        Protected Overrides Sub OnStartingUp(ByVal e As StartingUpCancelEventArgs)
            'create our window
            CreateWindow()

            ' First, create a Searcher object. 
            Dim les As New LogicalElementSearcher()

            ' Get the root element as a starting place to look for the Start menu.  For
            ' performance reasons, it's not a good idea to start searching for UI from
            ' the root unless the UI you are looking for is very near the root, which 
            ' the "start" button is.
            Dim root As LogicalElement = LogicalElement.RootElement
            les.Root = root

            '
            ' 1. Find the LogicalElement for the "start" button
            '    We're looking for the logical element with the NameProperty property "start".
            '    ie, the "start" button with the Sidebar disabled (no name property available if Sidebar enabled). 
            '    This property is release dependent.
            '
            ' 2. Get the InvokePattern pattern
            '    We're also using the Condition class to specify that the logical element must 
            '    also support the Invoke pattern.
            '
            les.Condition = New AndCondition(New PropertyCondition(LogicalElement.NameProperty, "start"), New PatternPresentCondition(InvokePattern.Pattern))

            ' Only have to find first since our root element is the desktop and the "start" button is a child.	
            Dim _element As LogicalElement = les.FindFirst()

            If Not (_element Is Nothing) Then
                '
                ' 3. Register for the Invoke event
                '
                Dim invokePattern As InvokePattern = _element.GetPattern(invokePattern.Pattern)
                handler = New AutomationEventHandler(AddressOf OnInvoke)
                System.Windows.Automation.Automation.AddAutomationEventHandler(invokePattern.InvokedEvent, _element, ScopeFlags.Element, handler)

                ' Get the bounding rectangle for the currently focused element and display it's coordinates
                Dim rle As Rect = CType(_element.GetPropertyValue(LogicalElement.BoundingRectangleProperty), Rect)

                _sb = New StringBuilder()
                _sb.Append(ControlChars.Lf + "Object found at:")
                _sb.Append((ControlChars.Lf + "Top left corner: " + rle.TopLeft.ToString()))
                _sb.Append((ControlChars.Lf + "Top right corner: " + rle.TopRight.ToString()))
                _sb.Append((ControlChars.Lf + "Bottom left corner: " + rle.BottomLeft.ToString()))
                _sb.Append((ControlChars.Lf + "Bottom right corner: " + rle.BottomRight.ToString()))
                Output(_sb.ToString())
                Output(ControlChars.Lf + "Invoking object....")
                '
                ' 4. Use the InvokePattern control pattern to invoke the "start" button
                '
                invokePattern.Invoke()
            Else
                Output(ControlChars.Lf + "Failed to find the control.")
            End If
        End Sub 'OnStartingUp

        ' 5. Handle the invoke event
        ' Invoke event handler
        Public Sub OnInvoke(ByVal src As Object, ByVal e As AutomationEventArgs)
            MessageBox.Show("Start button has been invoked." + ControlChars.Lf)
        End Sub 'OnInvoke


        ' The CreateWindow() method creates a simple window to which output may be written.
        Private Sub CreateWindow()
            _window = New Window()
            _panel = New DockPanel()
            _panel = New DockPanel()
            _window.Content = _panel
            _window.Text = "FindStartButtonUsingNameProperty Sample"
            _window.Show()
            Output("Finding start button...")
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
            ' To remove all event listeners you can use the RemoveAllEventHandlers method. 
            ' However, here we just remove the Invoke event handler.
            Try
                If Not (_element Is Nothing) Then
                    System.Windows.Automation.Automation.RemoveAutomationEventHandler(InvokePattern.InvokedEvent, _element, handler)
                End If
            Catch [error] As Exception
                ' If ourForm was closed before closing the main output window 
                ' the button element no longer exists and an exception occurs.
                MessageBox.Show([error].ToString())
            End Try
            MyBase.OnShuttingDown(e)
        End Sub 'OnShuttingDown

        ' Launch the sample application.

        Friend NotInheritable Class TestMain

            <System.STAThread()> _
            Shared Sub Main()
                Thread.CurrentThread.ApartmentState = System.Threading.ApartmentState.STA
                ' Create an instance of the sample class and call its Run() method to start it.
                Dim app As New FindStartButtonUsingNameProperty()
                app.Run()
            End Sub 'Main
        End Class 'TestMain
    End Class 'FindStartButtonUsingNameProperty
End Namespace 'SDKSample