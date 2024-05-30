 '***********************************************************************************************\
'* File: UsingUIAForClients.cs
'*
'* Description: 
'*    This sample provides sample code that shows how User Interface (UI) Automation clients
'*    can use UI Automation to do the following:
'*        1. Find a LogicalElement
'*        2. Get Supported Control Patterns
'*        3. Register for an Event
'*        4. Get Property Values
'*
'* Programming Elements:
'*    This sample demonstrates the following UI Automation programming elements from the 
'*     System.Windows.Automation namespace:
'*        Automation class
'*            FindLogicalElement() method
'*            AddAutomationEventHandler() method
'*            RemoveAutomationEventHandler() method
'*        LogicalElement class
'*            RootElement property
'*            NameProperty automation property
'*            AcceleratorKeyProperty automation property
'*            AccessKeyProperty automation property
'*            GetPattern()
'*            GetPropertyValue()
'*        InvokePattern class
'*            Invoke()
'*       
'* Copyright (C) 2003 by Microsoft Corporation.  All rights reserved.
'*
'\***********************************************************************************************

Imports System
Imports System.Windows.Forms
Imports System.Windows
Imports System.Windows.Documents
Imports System.Windows.Automation
Imports System.Windows.Controls
Imports System.Threading


Namespace SDKSample
   
    Public Class UsingUIAForClients
        Inherits System.Windows.Application
        Private handler As AutomationEventHandler
        Private element As LogicalElement

        Protected Overrides Sub OnStartingUp(ByVal e As StartingUpCancelEventArgs)
            CreateWindow()

            Dim myForm = New frmButtonHolder()
            ' Show the form that properties will be retrieved for.
            myForm.Show()

            Output("Looking for Button1 and listening for invoke commands when found.")

            ' 1. Find a LogicalElement
            '
            'The following code shows how to find a specific instance of a LogicalElement.
            'First, create a MatchCondition array.  You are looking
            'for the logical element property, NameProperty 
            'and a property value, "Button1".
            Dim conds() As MatchCondition = {New MatchCondition(LogicalElement.NameProperty, "Button1")}

            ' Start the search from the root...
            Dim root As LogicalElement = LogicalElement.RootElement

            ' Find functions can also be used in a loop to keep getting the next match, 
            'if more than one exists. For this example, just find the first one.
            element = System.Windows.Automation.Automation.FindLogicalElement(root, conds)

            ' Check the element returned
            If element Is Nothing Then
                ' Didn't find it
                Return
            End If

            ' 2. Get Supported Control Patterns 
            '
            ' Once you have an instance of a LogicalElement, you may want 
            ' to check to see which control patterns it supports.  You can 
            ' get all the control patterns that an element supports 
            ' by calling GetSupportedPatterns(), but in this case, just
            ' check if it supports the InvokePattern pattern by calling GetPatter().
            Dim invokePattern As InvokePattern = element.GetPattern(invokePattern.Pattern) '

            If invokePattern Is Nothing Then
                ' Doesn't support it
                Return
            End If

            ' The InvokePattern allows you to programmatically
            ' invoke a button. It also exposes an event to 
            ' let you know when the button has been invoked. The following
            ' code shows an example of listening for the Invoked event on the
            ' button and then invoking the button to receive the event.
            ' Alternatively, you can use the Input.MoveToAndClick(element) 
            ' to programmatically click the button.
            ' 3. Register for an Event
            ' 
            ' In order to intercept the event when the button is invoked,
            ' you can define a method as an AutomationEventHandler delegate.  
            ' This example also shows how to specify ScopeFlags for the event 
            ' of ineterest.
            handler = AddressOf OnInvoked

            ' Use AddAutomationEventHandler() to add an event handler to the event handling chain.
            System.Windows.Automation.Automation.AddAutomationEventHandler(invokePattern.InvokedEvent, element, ScopeFlags.Element, handler)

            ' Now invoke the button using the InvokePattern object returned
            ' above.  This will cause the HandleInvokeEvent.OnInvoked method to be called.
            invokePattern.Invoke()
        End Sub 'OnStartingUp

        Public Shared Sub OnInvoked(ByVal [source] As Object, ByVal e As AutomationEventArgs)
            ' OK to cast since we're listening on a LogicalElement 
            Dim srcEl As LogicalElement = CType([source], LogicalElement)

            ' 4. Get Property Values
            '
            ' The button with NameProperty, Button1, has been invoked.  
            '
            Dim name As String = srcEl.GetPropertyValue(LogicalElement.NameProperty) '
            Dim acceleratorKey As String = srcEl.GetPropertyValue(LogicalElement.AcceleratorKeyProperty) '
            Dim accessKey As String = srcEl.GetPropertyValue(LogicalElement.AccessKeyProperty) '
        End Sub 'OnInvoked

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
        Private _textElement As System.Windows.Controls.Text
        Private _panel As DockPanel
        Private _window As Window
        ' Launch the sample application.

        Friend NotInheritable Class TestMain

            <System.STAThread()> _
            Shared Sub Main()
                Thread.CurrentThread.ApartmentState = System.Threading.ApartmentState.STA

                ' Create an instance of the sample class and call its Run() method to start it.
                Dim app As New UsingUIAForClients()

                app.Run()
            End Sub 'Main
        End Class 'TestMain

        ' Window shut down event handler..
        Protected Overrides Sub OnShuttingDown(ByVal e As ShuttingDownEventArgs)
            ' To remove all event listeners you can use the RemoveAllEventHandlers method. 
            ' However, here we just remove this event handler.
            Try
                If Not (element Is Nothing) Then
                    System.Windows.Automation.Automation.RemoveAutomationEventHandler(InvokePattern.InvokedEvent, element, handler)
                End If
            Catch [error] As Exception
                ' If ourForm was closed before closing the main output window 
                ' the button element no longer exists and an exception occurs.
            End Try

            MyBase.OnShuttingDown(e)
        End Sub 'OnShuttingDown
    End Class 'UsingUIAForClients
End Namespace 'SDKSample