'---------------------------------------------------------------------
' 
'  Copyright (C) Microsoft Corporation.  All rights reserved.
' 
' This source code is intended only as a supplement to Microsoft
' Development Tools and/or on-line documentation.  See these other
' materials for detailed information regarding Microsoft code samples.
' 
' THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
' KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
' IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
' PARTICULAR PURPOSE.
'---------------------------------------------------------------------

Imports System.Windows.Media.Core

' This sample application displays medadata for a media file.
Namespace MetaDataVB

    Public Class Form1
        Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

        Public Sub New()
            MyBase.New()

            'This call is required by the Windows Form Designer.
            InitializeComponent()

            'Add any initialization after the InitializeComponent() call

            ' Resize column headers to fit window.
            ListView1.Columns(0).Width = ListView1.ClientRectangle.Width / 3
            ListView1.Columns(1).Width = ListView1.ClientRectangle.Width / 3
            ListView1.Columns(2).Width = ListView1.ClientRectangle.Width / 3



        End Sub

        'Form overrides dispose to clean up the component list.
        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
        Friend WithEvents ListView1 As System.Windows.Forms.ListView
        Friend WithEvents rbAll As System.Windows.Forms.RadioButton
        Friend WithEvents rbNamed As System.Windows.Forms.RadioButton
        Friend WithEvents chProperty As System.Windows.Forms.ColumnHeader
        Friend WithEvents chType As System.Windows.Forms.ColumnHeader
        Friend WithEvents chValue As System.Windows.Forms.ColumnHeader
        Friend WithEvents miFile As System.Windows.Forms.MenuItem
        Friend WithEvents miOpen As System.Windows.Forms.MenuItem
        Friend WithEvents miExit As System.Windows.Forms.MenuItem
        Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
        Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
        Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container
            Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
            Me.miFile = New System.Windows.Forms.MenuItem
            Me.miOpen = New System.Windows.Forms.MenuItem
            Me.miExit = New System.Windows.Forms.MenuItem
            Me.MenuItem1 = New System.Windows.Forms.MenuItem
            Me.ListView1 = New System.Windows.Forms.ListView
            Me.chProperty = New System.Windows.Forms.ColumnHeader
            Me.chType = New System.Windows.Forms.ColumnHeader
            Me.chValue = New System.Windows.Forms.ColumnHeader
            Me.rbAll = New System.Windows.Forms.RadioButton
            Me.rbNamed = New System.Windows.Forms.RadioButton
            Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
            Me.StatusBar1 = New System.Windows.Forms.StatusBar
            Me.SuspendLayout()
            '
            'MainMenu1
            '
            Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miFile, Me.MenuItem1})
            Me.MainMenu1.Name = "MainMenu1"
            '
            'miFile
            '
            Me.miFile.Index = 0
            Me.miFile.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.miOpen, Me.miExit})
            Me.miFile.Name = "miFile"
            Me.miFile.Text = "&File"
            '
            'miOpen
            '
            Me.miOpen.Index = 0
            Me.miOpen.Name = "miOpen"
            Me.miOpen.Text = "&Open..."
            '
            'miExit
            '
            Me.miExit.Index = 1
            Me.miExit.Name = "miExit"
            Me.miExit.Text = "E&xit"
            '
            'MenuItem1
            '
            Me.MenuItem1.Index = 1
            Me.MenuItem1.Name = "MenuItem1"
            Me.MenuItem1.Text = ""
            '
            'ListView1
            '
            Me.ListView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chProperty, Me.chType, Me.chValue})
            Me.ListView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
            Me.ListView1.Location = New System.Drawing.Point(20, 16)
            Me.ListView1.MultiSelect = False
            Me.ListView1.Name = "ListView1"
            Me.ListView1.Size = New System.Drawing.Size(584, 272)
            Me.ListView1.TabIndex = 0
            Me.ListView1.View = System.Windows.Forms.View.Details
            '
            'chProperty
            '
            Me.chProperty.Text = "Property"
            '
            'chType
            '
            Me.chType.Text = "Type"
            '
            'chValue
            '
            Me.chValue.Text = "Value"
            '
            'rbAll
            '
            Me.rbAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.rbAll.Checked = True
            Me.rbAll.Location = New System.Drawing.Point(88, 304)
            Me.rbAll.Name = "rbAll"
            Me.rbAll.TabIndex = 1
            Me.rbAll.TabStop = True
            Me.rbAll.Text = "&All properties"
            '
            'rbNamed
            '
            Me.rbNamed.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.rbNamed.Location = New System.Drawing.Point(216, 304)
            Me.rbNamed.Name = "rbNamed"
            Me.rbNamed.TabIndex = 2
            Me.rbNamed.Text = "&Named only"
            '
            'StatusBar1
            '
            Me.StatusBar1.Location = New System.Drawing.Point(0, 340)
            Me.StatusBar1.Name = "StatusBar1"
            Me.StatusBar1.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.StatusBar1.Size = New System.Drawing.Size(624, 22)
            Me.StatusBar1.TabIndex = 3
            '
            'Form1
            '
            Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
            Me.ClientSize = New System.Drawing.Size(624, 362)
            Me.Controls.Add(Me.StatusBar1)
            Me.Controls.Add(Me.rbNamed)
            Me.Controls.Add(Me.rbAll)
            Me.Controls.Add(Me.ListView1)
            Me.Menu = Me.MainMenu1
            Me.Name = "Form1"
            Me.Text = "Media Foundation Metadata Viewer"
            Me.ResumeLayout(False)

        End Sub
        ' Global variables

        Dim m_fileName As String = String.Empty

#End Region

#Region " AppDefinedMethods "

        ' This is where we create a media source from the file, extract metadata,
        ' and display it in the ListView.

        Private Sub GetMetadata(ByVal fileName As String)
            Dim propertyStore As System.Windows.Explorer.PropertyStore
            Dim mediaSource As MediaSource
            Dim presDesc As PresentationDescriptor
            Dim propDesc As System.Windows.Explorer.PropertyDescription
            Dim displayName As String
            Dim keyCollection As System.Collections.ICollection
            Dim keys As System.Windows.Explorer.PropertyKey()

            ' Clear any previous data and put up an hourglass.
            ListView1.Items.Clear()
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            ' Try to get metadata.
            Try
                mediaSource = New MediaSource(fileName)
                presDesc = mediaSource.CreatePresentationDescriptor()
                propertyStore = presDesc.Properties
            Catch
                System.Windows.Forms.MessageBox.Show("No metadata found.")
                Return
            End Try

            ' Get the collection of keys and copy to a PropertyKey array.

            keyCollection = propertyStore.Keys
            ReDim keys(keyCollection.Count)
            keyCollection.CopyTo(keys, 0)

            ' Iterate through the keys to obtain property descriptions and values.
            Dim x As Integer = -1
            Dim k As System.Windows.Explorer.PropertyKey
            For Each k In keys
                x = x + 1
                Try
                    Try

                        propDesc = New System.Windows.Explorer.PropertyDescription(k)
                        displayName = propDesc.DisplayName
                    Catch
                        If (rbNamed.Checked = True) Then

                            ' Display only properties that have friendly names.
                            GoTo ENDFOR
                        Else
                            displayName = "n/a"
                        End If
                    End Try

                    ' Display the name of the property.
                    Dim lvItem As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(displayName)

                    ' Display the data type.
                    lvItem.SubItems.Add(propertyStore(x).GetType().Name)

                    ' Display the value.
                    lvItem.SubItems.Add(propertyStore.GetDisplayValue(k))
                    ListView1.Items.Add(lvItem)
                Catch
                    System.Diagnostics.Debug.WriteLine("Exception on retrieving item.")
                End Try
ENDFOR:
            Next

            ' Restore the cursor.
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        End Sub

#End Region

#Region " Control handlers "

        ' Refresh the metadata list when the user selects a radio button.

        Private Sub rbAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbAll.CheckedChanged
            If (m_fileName.Length > 0) Then
                GetMetadata(m_fileName)
            End If
        End Sub


        Private Sub miOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miOpen.Click
            Dim fileName As String

            If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
                fileName = OpenFileDialog1.FileName
                StatusBar1.Text = fileName
                GetMetadata(fileName)
                m_fileName = fileName
            End If

        End Sub

        Private Sub miExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles miExit.Click
            System.Windows.Forms.Application.Exit()
        End Sub

        ' Enable the user to refresh the list by clicking on it after resizing columns. Unfortunately,
        ' the only way to get events from the column headers is by overriding the window procedure, and 
        ' the list doesn't refresh automatically.
        Private Sub ListView1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ListView1.MouseDown
            ListView1.Refresh()
        End Sub

#End Region
    End Class

End Namespace

