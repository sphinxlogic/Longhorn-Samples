 '************************************************************************************************\
'* File: GetSupportedPatterns.cs
'*
'* Description:
'*    This sample navigates the logical tree starting at the root, uses GetSupportedPatterns()  
'*    to get an array of supported control patterns for each LogicalElement in the tree, and 
'*    displays the names of the supported control patterns for each LogicalElement. 
'*
'* Programming Elements:
'*    This sample demonstrates the following UI Automation programming elements from the 
'*     System.Windows.Automation namespace:
'*
'*       LogicalElement class
'*           RootElement property
'*           FirstChild property
'*           NextSibling property
'*           GetPattern() method
'*           GetSupportedPatterns() method
'*       AutomationProperty class
'*           ToString() method
'*
'* Copyright (C) 2003 by Microsoft Corporation.  All rights reserved.
'\************************************************************************************************

Imports System
Imports System.Windows.Forms
Imports System.Windows
Imports System.Windows.Documents
Imports System.Windows.Automation
Imports System.Windows.Controls
Imports System.Threading


Namespace SDKSample
   
   Public Class GetSupportedPatterns
      Inherits System.Windows.Application
      Private _textElement As System.Windows.Controls.Text
      Private _panel As DockPanel
      Private _window As Window
      Private Shared myForm As Form
      
      
      Protected Overrides Sub OnStartingUp(e As StartingUpCancelEventArgs)
         CreateWindow()
         
         ' Add the form that we want to get the patterns for.
         myForm = New Form()
         myForm.Show()
         
         Dim el As LogicalElement = GetLogicalElementFromPoint()
         
         DisplayInfo(el)
      End Sub 'OnStartingUp
      
      
      ' Display the control patterns supported by the logical element.
      Private Sub DisplayInfo(element As LogicalElement)
         ' For each LogicalElement in the tree, get an array of AutomationPatterns 
         ' supported by the LogicalElement.
         Dim patterns As AutomationPattern() = element.GetSupportedPatterns()
         
         ' For each desktop item, display the name of the supported control patterns.
         Dim pattern As AutomationPattern
         For Each pattern In  patterns
            Output(("this element implements control pattern: " + pattern.ToString() + ""))
         Next pattern
      End Sub 'DisplayInfo
      
      
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
         _window = New Window()
         _panel = New DockPanel()
         _window.Content = _panel
         _window.Show()
      End Sub 'CreateWindow
      
      
      ' The Output() method is used to output text to the application window.
      Private Sub Output(message As String)
         _textElement = New System.Windows.Controls.Text()
         _textElement.TextRange.Text = message
         DockPanel.SetDock(_textElement, Dock.Top)
         _panel.Children.Add(_textElement)
      End Sub 'Output
      
      
      ' Launch the sample application.
      
      NotInheritable Friend Class TestMain
         
         <System.STAThread()>  _
         Shared Sub Main()
            Thread.CurrentThread.ApartmentState = System.Threading.ApartmentState.STA
            
            ' Create an instance of the sample class and call its Run() method to start it.
            Dim app As New GetSupportedPatterns()
            app.Run()
         End Sub 'Main
      End Class 'TestMain
   End Class 'GetSupportedPatterns
End Namespace 'SDKSample 



