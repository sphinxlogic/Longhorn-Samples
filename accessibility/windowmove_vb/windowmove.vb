 '************************************************************************************************\
'*
'* File: WindowMove.cs
'*
'* Description:  
'*       Moves a form window at a specified location to the top left corner  
'*       of the desktop. This is done by getting a logical element (lefp) based 
'*    corresponding to the form based on a point on the form (by calling  
'*       the local function GetLogicalElementFromPoint), and then getting a window
'*       pattern object from this logical element (using LogicalElement.GetPattern()).
'*       Then, it navigates the logical tree until reaching a top-level window that 
'*       contains a logical element. Once this is done it checks to see if the 
'*       WindowPattern of the top-level window parent is moveable.
'*       Focus is set to the top-level window containing the logical element
'*       and a rect for both the logical element parent window and the 
'*       desktop for screen location calculations is created.
'*       The top and left coordinates needed to move the parent window is set
'*       and window is moved to the new coordinates if these coordinates
'*       are not out of range.
'* 
'* Programming Elements:
'*    This sample demonstrates the following UI Automation programming elements from the 
'*     System.Windows.Automation namespace:
'*
'*       LogicalElement class
'*           RootElement property
'*           FromPoint() method
'*           GetPattern() method
'*           Parent property
'*           SetFocus() method
'*           GetPropertyValue() method
'*           BoundingRectangleProperty property
'*       WindowPattern class
'*           Moveable property
'*           MoveTo() method
'*       AutomationProperty class
'*           ToString() method
'*
'* Copyright (C) 2003 by Microsoft Corporation.  All rights reserved.
'*
'\************************************************************************************************
Imports System
Imports System.Windows
Imports System.Windows.Forms
Imports System.Windows.Documents
Imports System.Windows.Automation
Imports System.Windows.Controls
Imports System.Threading


Namespace SDKSample
   
   Public Class WindowMove
      Inherits System.Windows.Application
      Private _textElement As System.Windows.Controls.Text
      Private _panel As DockPanel
      Private _window As Window
      Private Shared myForm As Form
      
      
      Protected Overrides Sub OnStartingUp(e As StartingUpCancelEventArgs)
         ' Create the informational window and the form that will be moved
         CreateWindow()
         
         
         ' Root logical element for current desktop - children include the top-level windows.
         Dim root As LogicalElement = LogicalElement.RootElement
         
         Output("Find the logical element for the form based on a point on the form. ")
         
         ' Get a logical element based based on a point on the form.
         Dim lefp As LogicalElement = GetLogicalElementFromPoint()
         
         ' Does the logical element exist?
         If lefp Is Nothing Then
            Return
         End If 
         'Create a window pattern object from the logical element.
         Dim windowPattern As WindowPattern = lefp.GetPattern(WindowPattern.Pattern) '
          
         ' Look up the tree for the logical elment's parents until reaching 
         ' a top level window parent.
         While windowPattern Is Nothing
            If lefp.Parent Is Nothing Then
               Output("Top level reached in search for window.")
               Return
            Else
               lefp = lefp.Parent
            End If
            
            windowPattern = lefp.GetPattern(WindowPattern.Pattern) '
         End While
         
         ' Is the WindowPattern object moveable?
         If windowPattern.Moveable Then
            ' Set focus to the top level window containing this logical element.
            lefp.SetFocus()
            
            ' Create a rect for both the logical element parent window and the desktop
            ' for screen location calculations.
            Dim rle As Rect = CType(lefp.GetPropertyValue(LogicalElement.BoundingRectangleProperty), Rect)
            Dim rroot As Rect = CType(root.GetPropertyValue(LogicalElement.BoundingRectangleProperty), Rect)
            
            ' Get the top and left coordinates to move the parent window 
            Output(("Attempting move of Top, Left corner of the window from " + rle.Top.ToString() + "," + rle.Left.ToString() + " to 0,0"))
            
            Dim top As Integer = 0
            Dim left As Integer = 0
            
            Thread.Sleep(4000)
            Output("Moving the form to left top corner...")
            
            ' Move window if coordinates not out of range.
            ' By default, MoveTo doesn't allow a window to be moved completely off-screen.
            If top <= rroot.Bottom -(rle.Bottom - rle.Top) AndAlso left <= rroot.Right -(rle.Right - rle.Left) Then
               windowPattern.MoveTo(left, top)
               Output("Window moved successfully.")
            Else
               Output("Window is too wide, part of window will appear off-screen. Run again please.")
            End If
         Else
            Output("Wndow is not moveable. Run again please.")
         End If
      End Sub 'OnStartingUp
      
      
      
      ' Get a logical element based on point on the form.
      Public Shared Function GetLogicalElementFromPoint() As LogicalElement
         Dim pt As New System.Windows.Point()
         
         Thread.Sleep(4000)
         pt.X = CInt(myForm.Location.X) + 10
         pt.Y = CInt(myForm.Location.Y) + 10
         Return LogicalElement.FromPoint(pt)
      End Function 'GetLogicalElementFromPoint
      
      ' The CreateWindow() method creates a simple window to which output may be written.
      Private Sub CreateWindow()
         ' Create the informational window.
         _window = New Window()
         _panel = New DockPanel()
         _window.Content = _panel
         _window.Show()
         
         ' Create the movable form.
         myForm = New Form()
         myForm.Text = "Movable Window"
         myForm.Show()
      End Sub 'CreateWindow
       
      
      ' The Output() method is used to output text to the application window.
      Private Sub Output(message As String)
         _textElement = New System.Windows.Controls.Text()
         _textElement.TextRange.Text = message
         DockPanel.SetDock(_textElement, Dock.Top)
         _panel.Children.Add(_textElement)
      End Sub 'Output
      
      
      ' Window shut down event handler..
      Protected Overrides Sub OnShuttingDown(e As ShuttingDownEventArgs)
         ' Remove the event handler. You can also use RemoveAllEventHandlers() to 
         ' remove all event handlers. 
         MyBase.OnShuttingDown(e)
      End Sub 'OnShuttingDown
      
      ' Launch the sample application.
      
      NotInheritable Friend Class TestMain
         
         <System.STAThread()>  _
         Shared Sub Main()
            Thread.CurrentThread.ApartmentState = System.Threading.ApartmentState.STA
            ' Create an instance of the sample class and call its Run() method to start it.
            Dim app As New WindowMove()
            app.Run()
         End Sub 'Main
      End Class 'TestMain
   End Class 'WindowMove
End Namespace 'SDKSample