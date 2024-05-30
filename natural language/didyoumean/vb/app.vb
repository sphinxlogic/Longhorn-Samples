'---------------------------------------------------------------------
'  This file is part of the Microsoft .NET Framework SDK Code Samples.
' 
'  Copyright (C) Microsoft Corporation.  All rights reserved.
' 
'This source code is intended only as a supplement to Microsoft
'Development Tools and/or on-line documentation.  See these other
'materials for detailed information regarding Microsoft code samples.
' 
'THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
'KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
'IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
'PARTICULAR PURPOSE.
'---------------------------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data


Namespace Microsoft.Samples.NaturalLanguage
    '/ <summary>
    '/ Summary description for Form1.
    '/ </summary>
    Public Class App
        Inherits System.Windows.Forms.Form

        Private WithEvents standardMainMenu As System.Windows.Forms.MainMenu
        Private WithEvents fileMenuItem As System.Windows.Forms.MenuItem '
        Private WithEvents exitMenuItem As System.Windows.Forms.MenuItem
        Private WithEvents helpMenuItem As System.Windows.Forms.MenuItem
        Private WithEvents contentsMenuItem As System.Windows.Forms.MenuItem
        Private WithEvents indexMenuItem As System.Windows.Forms.MenuItem
        Private separatorMenuItem5 As System.Windows.Forms.MenuItem
        Private WithEvents aboutMenuItem As System.Windows.Forms.MenuItem
        Private imageList1 As System.Windows.Forms.ImageList
        Private splitContainer1 As System.Windows.Forms.SplitContainer
        Private WithEvents btnGo As System.Windows.Forms.Button
        Private WithEvents textSearch As System.Windows.Forms.RichTextBox
        Private WithEvents labelDidYouMean As System.Windows.Forms.LinkLabel
        Private WithEvents tabControl1 As System.Windows.Forms.TabControl
        Private WithEvents tabPage1 As System.Windows.Forms.TabPage
        Private WithEvents tabPage2 As System.Windows.Forms.TabPage
        Private WithEvents tabPage3 As System.Windows.Forms.TabPage
        Private WithEvents webBrowser1 As System.Windows.Forms.WebBrowser
        Private webBrowser2 As System.Windows.Forms.WebBrowser
        Private webBrowser3 As System.Windows.Forms.WebBrowser
        Private components As System.ComponentModel.IContainer

        Private bshowDYM As Boolean
        Private searchTuner As SearchTuner
        Private itemHighLighted As MenuItem

        '/ <summary>
        '/ Required designer variable.
        '/ </summary>
        Public Sub New()
            InitializeComponent()
        End Sub 'New


        '/ <summary>
        '/ Clean up any resources being used.
        '/ </summary>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
            End If

            MyBase.Dispose(disposing)
        End Sub 'Dispose


        '/ Required method for Designer support - do not modify
        '/ the contents of this method with the code editor.
        '/ </summary>
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Me.standardMainMenu = New System.Windows.Forms.MainMenu(Me.components)
            Me.fileMenuItem = New System.Windows.Forms.MenuItem()
            Me.exitMenuItem = New System.Windows.Forms.MenuItem()
            Me.helpMenuItem = New System.Windows.Forms.MenuItem()
            Me.contentsMenuItem = New System.Windows.Forms.MenuItem()
            Me.indexMenuItem = New System.Windows.Forms.MenuItem()
            Me.separatorMenuItem5 = New System.Windows.Forms.MenuItem()
            Me.aboutMenuItem = New System.Windows.Forms.MenuItem()
            Me.imageList1 = New System.Windows.Forms.ImageList(Me.components)
            Me.splitContainer1 = New System.Windows.Forms.SplitContainer()
            Me.btnGo = New System.Windows.Forms.Button()
            Me.textSearch = New System.Windows.Forms.RichTextBox()
            Me.labelDidYouMean = New System.Windows.Forms.LinkLabel()
            Me.tabControl1 = New System.Windows.Forms.TabControl()
            Me.tabPage1 = New System.Windows.Forms.TabPage()
            Me.webBrowser1 = New System.Windows.Forms.WebBrowser()
            Me.tabPage2 = New System.Windows.Forms.TabPage()
            Me.webBrowser2 = New System.Windows.Forms.WebBrowser()
            Me.tabPage3 = New System.Windows.Forms.TabPage()
            Me.webBrowser3 = New System.Windows.Forms.WebBrowser()
            Me.splitContainer1.SuspendLayout()
            Me.tabControl1.SuspendLayout()
            Me.tabPage1.SuspendLayout()
            Me.tabPage2.SuspendLayout()
            Me.tabPage3.SuspendLayout()
            Me.SuspendLayout()

            ' 
            ' standardMainMenu
            ' 
            Me.standardMainMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.fileMenuItem, Me.helpMenuItem})
            Me.standardMainMenu.Name = "standardMainMenu"

            ' 
            ' fileMenuItem
            ' 
            Me.fileMenuItem.Index = 0
            Me.fileMenuItem.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.exitMenuItem})
            Me.fileMenuItem.Name = "fileMenuItem"
            Me.fileMenuItem.Text = "&File"

            ' 
            ' exitMenuItem
            ' 
            Me.exitMenuItem.Index = 0
            Me.exitMenuItem.Name = "exitMenuItem"
            Me.exitMenuItem.Text = "E&xit"

            ' 
            ' helpMenuItem
            ' 
            Me.helpMenuItem.Index = 1
            Me.helpMenuItem.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.contentsMenuItem, Me.indexMenuItem, Me.separatorMenuItem5, Me.aboutMenuItem})
            Me.helpMenuItem.Name = "helpMenuItem"
            Me.helpMenuItem.Text = "&Help"

            ' 
            ' contentsMenuItem
            ' 
            Me.contentsMenuItem.Index = 0
            Me.contentsMenuItem.Name = "contentsMenuItem"
            Me.contentsMenuItem.Text = "&Contents"

            ' 
            ' indexMenuItem
            ' 
            Me.indexMenuItem.Index = 1
            Me.indexMenuItem.Name = "indexMenuItem"
            Me.indexMenuItem.Text = "&Index"

            ' 
            ' separatorMenuItem5
            ' 
            Me.separatorMenuItem5.Index = 2
            Me.separatorMenuItem5.Name = "separatorMenuItem5"
            Me.separatorMenuItem5.Text = "-"

            ' 
            ' aboutMenuItem
            ' 
            Me.aboutMenuItem.Index = 3
            Me.aboutMenuItem.Name = "aboutMenuItem"
            Me.aboutMenuItem.Text = "&About..."

            ' 
            ' imageList1
            ' 
            Me.imageList1.ImageSize = New System.Drawing.Size(16, 16)
            Me.imageList1.TransparentColor = System.Drawing.Color.Transparent

            ' 
            ' splitContainer1
            ' 
            Me.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
            Me.splitContainer1.Location = New System.Drawing.Point(0, 0)
            Me.splitContainer1.Name = "splitContainer1"
            Me.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
            Me.splitContainer1.Size = New System.Drawing.Size(809, 605)
            Me.splitContainer1.SplitterDistance = 58
            Me.splitContainer1.SplitterWidth = 6
            Me.splitContainer1.TabIndex = 1
            Me.splitContainer1.Text = "splitContainer1"

            ' 
            ' splitterPanel1
            ' 
            Me.splitContainer1.Panel1.Controls.Add(Me.btnGo)
            Me.splitContainer1.Panel1.Controls.Add(Me.textSearch)

            ' 
            ' splitterPanel2
            ' 
            Me.splitContainer1.Panel2.Controls.Add(Me.labelDidYouMean)
            Me.splitContainer1.Panel2.Controls.Add(Me.tabControl1)

            ' 
            ' btnGo
            ' 
            Me.btnGo.Anchor = CType(System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right, System.Windows.Forms.AnchorStyles)
            Me.btnGo.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.btnGo.Location = New System.Drawing.Point(731, 14)
            Me.btnGo.Name = "btnGo"
            Me.btnGo.Size = New System.Drawing.Size(48, 32)
            Me.btnGo.TabIndex = 1
            Me.btnGo.Text = "Go"

            ' 
            ' textSearch
            ' 
            Me.textSearch.Anchor = CType(System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right, System.Windows.Forms.AnchorStyles)
            Me.textSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
            Me.textSearch.Location = New System.Drawing.Point(23, 14)
            Me.textSearch.Multiline = False
            Me.textSearch.Name = "textSearch"
            Me.textSearch.Size = New System.Drawing.Size(682, 32)
            Me.textSearch.TabIndex = 0
            Me.textSearch.Text = ""

            ' 
            ' labelDidYouMean
            ' 
            Me.labelDidYouMean.BackColor = System.Drawing.SystemColors.MenuBar
            Me.labelDidYouMean.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0F, CType(System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic, System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CByte(0))
            Me.labelDidYouMean.Links.Add(New System.Windows.Forms.LinkLabel.Link(0, 0))
            Me.labelDidYouMean.Location = New System.Drawing.Point(160, -1)
            Me.labelDidYouMean.Name = "labelDidYouMean"
            Me.labelDidYouMean.Size = New System.Drawing.Size(619, 22)
            Me.labelDidYouMean.TabIndex = 1
            Me.labelDidYouMean.TextAlign = System.Drawing.ContentAlignment.MiddleCenter

            ' 
            ' tabControl1
            ' 
            Me.tabControl1.Controls.Add(Me.tabPage1)
            Me.tabControl1.Controls.Add(Me.tabPage2)
            Me.tabControl1.Controls.Add(Me.tabPage3)
            Me.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tabControl1.Location = New System.Drawing.Point(0, 0)
            Me.tabControl1.Name = "tabControl1"
            Me.tabControl1.SelectedIndex = 0
            Me.tabControl1.ShowToolTips = True
            Me.tabControl1.Size = New System.Drawing.Size(809, 541)
            Me.tabControl1.TabIndex = 2

            ' 
            ' tabPage1
            ' 
            Me.tabPage1.Controls.Add(Me.webBrowser1)
            Me.tabPage1.Location = New System.Drawing.Point(4, 22)
            Me.tabPage1.Name = "tabPage1"
            Me.tabPage1.Padding = New System.Windows.Forms.Padding(3)
            Me.tabPage1.Size = New System.Drawing.Size(801, 536)
            Me.tabPage1.TabIndex = 0
            Me.tabPage1.Text = "MSN"

            ' 
            ' webBrowser1
            ' 
            Me.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.webBrowser1.Location = New System.Drawing.Point(3, 3)
            Me.webBrowser1.Name = "webBrowser1"
            Me.webBrowser1.Size = New System.Drawing.Size(795, 530)
            Me.webBrowser1.TabIndex = 0
            Me.webBrowser1.Tag = "http://search.msn.com/results.asp?RS=CHECKED&FORM=MSNH&v=1&q={0}"

            ' 
            ' tabPage2
            ' 
            Me.tabPage2.Controls.Add(Me.webBrowser2)
            Me.tabPage2.Location = New System.Drawing.Point(4, 22)
            Me.tabPage2.Name = "tabPage2"
            Me.tabPage2.Padding = New System.Windows.Forms.Padding(3)
            Me.tabPage2.Size = New System.Drawing.Size(801, 536)
            Me.tabPage2.TabIndex = 1
            Me.tabPage2.Text = "MSDN"

            ' 
            ' webBrowser2
            ' 
            Me.webBrowser2.Dock = System.Windows.Forms.DockStyle.Fill
            Me.webBrowser2.Location = New System.Drawing.Point(3, 3)
            Me.webBrowser2.Name = "webBrowser2"
            Me.webBrowser2.Size = New System.Drawing.Size(795, 530)
            Me.webBrowser2.TabIndex = 1
            Me.webBrowser2.Tag = "http://search.microsoft.com/search/results.aspx?View=msdn&st=a&qu={0}&c=0&s=1"

            ' 
            ' tabPage3
            ' 
            Me.tabPage3.Controls.Add(Me.webBrowser3)
            Me.tabPage3.Location = New System.Drawing.Point(4, 22)
            Me.tabPage3.Name = "tabPage3"
            Me.tabPage3.Padding = New System.Windows.Forms.Padding(3)
            Me.tabPage3.Size = New System.Drawing.Size(801, 515)
            Me.tabPage3.TabIndex = 2
            Me.tabPage3.Text = "Microsoft"

            ' 
            ' webBrowser3
            ' 
            Me.webBrowser3.Dock = System.Windows.Forms.DockStyle.Fill
            Me.webBrowser3.Location = New System.Drawing.Point(3, 3)
            Me.webBrowser3.Name = "webBrowser3"
            Me.webBrowser3.Size = New System.Drawing.Size(795, 509)
            Me.webBrowser3.TabIndex = 1
            Me.webBrowser3.Tag = "http://search.microsoft.com/search/results.aspx?st=b&qu={0}&view=en-us"

            ' 
            ' App
            ' 
            Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
            Me.ClientSize = New System.Drawing.Size(809, 605)
            Me.Controls.Add(splitContainer1)
            Me.Menu = Me.standardMainMenu
            Me.Name = "App"
            Me.Text = "Did You Mean"
            Me.splitContainer1.ResumeLayout(False)
            Me.tabControl1.ResumeLayout(False)
            Me.tabPage1.ResumeLayout(False)
            Me.tabPage2.ResumeLayout(False)
            Me.tabPage3.ResumeLayout(False)
            Me.ResumeLayout(False)
        End Sub 'InitializeComponent

        '/ <summary>
        '/ The main entry point for the application.
        '/ </summary>
        <STAThread()> Public Overloads Shared Sub Main(ByVal args() As String)
            Application.Run(New App())
        End Sub 'Main


        Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
            SearchAll()
        End Sub 'btnGo_Click


        Private Sub labelDidYouMean_LinkClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles labelDidYouMean.LinkClicked
            Me.textSearch.Text = CStr(e.Link.LinkData)
            labelDidYouMean.Visible = False
            SearchAll()
        End Sub 'labelDidYouMean_LinkClicked


        Private Sub App_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Search = New SearchTuner()
            labelDidYouMean.Visible = False
            btnGo.NotifyDefault(True)
        End Sub 'App_Load


        Private Sub DocumentCompleted(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles webBrowser1.DocumentCompleted
            labelDidYouMean.Visible = ShowDidYouMean
        End Sub 'DocumentCompleted


        Private Sub exitMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles exitMenuItem.Click
            Close()
        End Sub 'exitMenuItem_Click


        Private Sub Tab_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabPage1.Click, tabPage2.Click, tabPage3.Click
            Dim button As TabPage = CType(sender, TabPage)

            labelDidYouMean.Visible = False
            If Not (textSearch.Text Is Nothing) And textSearch.Text.Length = 0 Then
                btnGo.PerformClick()
            End If
            ' start the search
            Dim [text] As String = Me.textSearch.Text
            Dim revised As String = Search.SpellCheck([text])

            If Not (revised Is Nothing) And revised <> [text] Then
                labelDidYouMean.Text = [String].Format(System.Globalization.CultureInfo.CurrentUICulture, "Did you mean ""{0}""?", revised)
                labelDidYouMean.Links.Clear()
                labelDidYouMean.Links.Add(labelDidYouMean.Text.IndexOf(ControlChars.Quote) + 1, revised.Length, revised)
                labelDidYouMean.Visible = False
                ShowDidYouMean = True
            Else
                ShowDidYouMean = False
            End If

            SearchAll()
        End Sub 'Tab_Click


        Public Property ShowDidYouMean() As Boolean
            Get
                Return bshowDYM
            End Get
            Set(ByVal Value As Boolean)
                bshowDYM = Value
            End Set
        End Property


        Public Property Search() As SearchTuner
            Get
                Return searchTuner
            End Get
            Set(ByVal Value As SearchTuner)
                searchTuner = Value
            End Set
        End Property


        Public Property SearchItemHighlighted() As MenuItem
            Get
                Return itemHighLighted
            End Get
            Set(ByVal Value As MenuItem)
                itemHighLighted = Value
            End Set
        End Property


        Public Sub SearchAll()
            Dim [text] As String = Me.textSearch.Text
            Dim revised As String = Search.SpellCheck([text])

            If Not (revised Is Nothing) And revised <> [text] Then
                labelDidYouMean.Text = [String].Format(System.Globalization.CultureInfo.CurrentUICulture, "Did you mean ""{0}""?", revised)
                labelDidYouMean.Links.Clear()
                labelDidYouMean.Links.Add(labelDidYouMean.Text.IndexOf(ControlChars.Quote) + 1, revised.Length, revised)
                labelDidYouMean.Visible = False
                ShowDidYouMean = True
            Else
                ShowDidYouMean = False
            End If

            Dim i As Integer
            For i = 0 To (Me.tabControl1.Controls.Count) - 1
                Dim thisBrowser As WebBrowser = CType(Me.tabControl1.Controls(i).Controls(0), WebBrowser)

                Search.LinkFormat = CStr(thisBrowser.Tag)
                labelDidYouMean.Visible = False
                thisBrowser.Url = Search.Link(Me.textSearch.Text)
            Next i
        End Sub 'SearchAll


        Private Sub textSearch_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles textSearch.KeyDown
            If e.KeyCode = System.Windows.Forms.Keys.Return Then
                SearchAll()
            End If
        End Sub 'textSearch_KeyDown
    End Class 'App
End Namespace 'Microsoft.Samples.NaturalLanguage
