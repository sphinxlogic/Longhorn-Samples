' ---------------------------------------------------------------------
'  This file is part of the Microsoft .NET Framework SDK Code Samples.
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
' ---------------------------------------------------------------------

Option Strict On
Option Explicit On 

' Standard WinForm namespaces
Imports System
Imports System.Drawing
Imports System.ComponentModel
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Security.Permissions
Imports System.Windows.Forms

' Speech namespaces
Imports System.Speech.Recognition ' Basic speech recognition
Imports Srgs = System.Speech.Srgs ' W3C Speech Recognition Grammar Specification (SRGS)

' General Information about an assembly is controlled through the following
' set of attributes. Change these attribute values to modify the information
' associated with an assembly.
<Assembly: AssemblyTitle("Speech Recognition")> 
<Assembly: AssemblyDescription("Speech Recognition Sample Application")> 
<Assembly: AssemblyCompany("Microsoft Corporation")> 
<Assembly: AssemblyProduct("Microsoft (R) Windows (R) Operating System")> 
<Assembly: AssemblyCopyright("Copyright (C) 2003 Microsoft Corporation.")> 
<Assembly: AssemblyCulture("")> 
<Assembly: AssemblyVersion("1.0.*")> 
<Assembly: CLSCompliant(True)> 
<Assembly: ComVisible(False)> 
<Assembly: EnvironmentPermission(SecurityAction.RequestMinimum)> 

' SpeechRecognition is a WinForm application that demonstrates basic speech 
' recognition functionalities.
Public Class SpeechRecognition
    Inherits System.Windows.Forms.Form

    ' The main entry point for the application.
    Public Shared Sub Main()
        Application.Run(New SpeechRecognition())
    End Sub

    ' Initializes the SystemRecognizer and loads sample grammars on form load.
    Private Sub SpeechRecognition_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        ' Initialize the SystemRecognizer
        recognizer = New SystemRecognizer()

        ' Disable recognition of SystemRecognizer grammars.
        recognizer.IsActive = False

        ' Load Dictation grammar.
        Dim dictationGrammar As New DictationGrammar(recognizer)
        dictationGrammar.Name = "Dictation"
        dictationGrammar.IsActive = True

        ' Load ChangeColor grammar.
        ' - Create grammar using System.Speech.Srgs object model.
        changeColorGrammar = CreateChangeColorGrammar()
        changeColorGrammar.IsActive = True

        ' Display loaded grammars/rules in grammarTreeView.
        AddToGrammarTreeView(dictationGrammar)
        AddToGrammarTreeView(changeColorGrammar)
    End Sub

    ' Creates a Srgs grammar for changing the background color of the form
    ' to any of the .NET defined color enumerations.
    Private Function CreateChangeColorGrammar() As Srgs.SrgsGrammar
        ' Build the following SRGS grammar with SemanticInterpretation using
        ' System.Speech.Srgs object model.
        ' - http:'www.w3.org/TR/speech-grammar
        ' - http:'www.w3.org/TR/semantic-interpretation
        '
        ' <grammar version="1.0" xml:lang="en-US" 
        '   xmlns="http:'www.w3.org/2001/06/grammar" root="ChangeColor">
        '   <rule id="ChangeColor">Change color to
        '     <one-of>
        '       <item>ActiveBorder
        '         <tag>$.color="ActiveBorder"</tag>
        '       </item>
        '       <item>ActiveCaption
        '         <tag>$.color="ActiveCaption"</tag>
        '       </item>
        '       ...
        '     </one-of>
        '   </rule>
        ' </grammar>
        '
        ' Example Phrases
        ' - "Change color to Red"
        ' - "Change color to Blue"

        ' Create a SrgsGrammar object representing the ChangeColor grammar.
        Dim grammar As New Srgs.SrgsGrammar(recognizer)
        grammar.Name = "Change Color"

        ' Create an OneOf list of system colors with semantics.
        Dim colorOneOf As New Srgs.OneOf()
        Dim colorName As String
        Dim kc As KnownColor
        For Each colorName In System.Enum.GetNames(kc.GetType())
            ' Associate the string name of the color with each item.
            Dim tag As String = String.Format("$.color=""{0}"";", colorName)
            colorOneOf.Elements.AddItem(colorName, tag)
        Next

        ' Build the ChangeColor rule: "Change color to <colorOneOf>"
        Dim rule As New Srgs.Rule("ChangeColor")
        rule.Elements.Add("Change color to")
        rule.Elements.Add(colorOneOf)

        ' Add the rule to the grammar and set it as the Root rule.
        grammar.Rules.Add(rule)
        grammar.Root = rule

        CreateChangeColorGrammar = grammar
    End Function

    ' Starts/Stops the recognition of loaded grammars depending on the
    ' current toggle state of the listenButton.
    Private Sub ListenButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles listenButton.Click
        If listenButton.Text = "Listen" Then ' Start listening.
            ' Update listenButton text.
            listenButton.Text = "Stop"

            ' Display the audio level meter.
            audioLevelProgressBar.Value = 0
            audioLevelProgressBar.Visible = True

            ' Clear the display of recognition result.
            DisplayResult("", Color.Black, Nothing)

            ' Enable recognition of SystemRecognizer grammars.
            recognizer.IsActive = True
        Else ' Stop listening.

            ' Update listenButton text.
            listenButton.Text = "Listen"

            ' Disable recognition of SystemRecognizer grammars.
            recognizer.IsActive = False

            ' Hide the audio level meter.
            audioLevelProgressBar.Visible = False
        End If
    End Sub

    ' Emulates the recognition of emulateTextBox.
    Private Sub EmulateButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles emulateButton.Click
        If emulateTextBox.Text.Length > 0 Then
            recognizer.EmulateRecognize(emulateTextBox.Text)
        End If
    End Sub

    ' Simulates the click of the emulateButton when Enter is pressed in 
    ' the emulateTextBox.
    Private Sub EmulateTextBox_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles emulateTextBox.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Enter) Then
            EmulateButton_Click(sender, e)
        End If
    End Sub

    ' Updates the audio level meter when the recognition progress has been updated.
    Private Sub Recognizer_RecognizeProgressChanged(ByVal sender As Object, ByVal e As RecognizeProgressChangedEventArgs) Handles recognizer.RecognizeProgressChanged
        audioLevelProgressBar.Value = e.AudioLevel
    End Sub

    ' Handles the Hypothesis event and displays the hypothesized result.
    Private Sub Recognizer_Hypothesis(ByVal sender As Object, ByVal e As RecognitionEventArgs) Handles recognizer.Hypothesis
        DisplayResult(e.Result.Text + " ...", Color.Black, Nothing)
    End Sub

    ' Handles the NoRecognition event and displays the empty result.
    Private Sub Recognizer_NoRecognition(ByVal sender As Object, ByVal e As RecognitionEventArgs) Handles recognizer.NoRecognition
        DisplayResult("<none>", Color.Red, Nothing)
    End Sub

    ' Handles the RejectedRecognition event and displays the rejected result.
    Private Sub Recognizer_RejectedRecognition(ByVal sender As Object, ByVal e As RecognitionEventArgs) Handles recognizer.RejectedRecognition
        DisplayResult(e.Result.Text, Color.OrangeRed, e.Result)
    End Sub

    ' Handles the Recognition event and displays the recognized result.
    Private Sub Recognizer_Recognition(ByVal sender As Object, ByVal e As RecognitionEventArgs) Handles recognizer.Recognition
        DisplayResult(e.Result.Text, Color.Green, e.Result)
    End Sub

    ' Displays the semantics, rules, and alternates of the result.
    Private Sub DisplayResult(ByVal resultText As String, ByVal resultColor As Color, ByVal result As RecognitionResult)
        ' Set the result text and color.
        recognitionTextBox.Text = resultText
        recognitionTextBox.ForeColor = resultColor

        ' Clear semantic XML, rule tree, and alternates.
        recognitionSmlTextBox.Text = ""
        recognitionRuleTreeView.Nodes.Clear()
        alternatesListBox.Items.Clear()

        If Not result Is Nothing Then
            ' Set the semantics XML.
            recognitionSmlTextBox.Text = result.Sml

            ' Update rule tree based on the result.
            If Not result.Rule Is Nothing Then
                UpdateRuleTreeView(recognitionRuleTreeView.Nodes, result.Rule)
            End If
            recognitionRuleTreeView.ExpandAll()

            ' Updates alternates list with alternates of the result.
            Dim alt As RecognitionPhraseAlternate
            For Each alt In result.Alternates
                alternatesListBox.Items.Add(alt.AlternateText)
            Next
        End If
    End Sub

    ' Recursively adds the RecognitionRule to the recognitionRuleTreeView.
    Private Sub UpdateRuleTreeView(ByVal nodes As TreeNodeCollection, ByVal rule As RecognitionRule)
        Dim ruleText As String = String.Format("{0} [{1}] ({2})", rule.Name, rule.Text, rule.RecognizerConfidence)
        Dim ruleNode As New TreeNode(ruleText)
        Dim childRule As RecognitionRule
        For Each childRule In rule.Rules
            UpdateRuleTreeView(ruleNode.Nodes, childRule)
        Next
        nodes.Add(ruleNode)
    End Sub

    ' Handles the Recognition event for the ChangeColorGrammar and sets 
    ' the background color of the WinForm to the specified color.
    Private Sub ChangeColorGrammar_Recognition(ByVal sender As Object, ByVal e As RecognitionEventArgs) Handles changeColorGrammar.Recognition
        ' Retrieve the value of the semantic property.
        Dim colorName As String = CType(e.Result.Properties("color").Value, String)
        BackColor = Color.FromName(colorName)
    End Sub

    ' Displays the OpenFileDialog and loads the specified grammar.
    Private Sub AddGrammarButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles addGrammarButton.Click
        ' Create the Open Grammar dialog box.
        Dim dialog As New OpenFileDialog()
        dialog.Filter = "SRGS files|*.*xml|CFG files|*.cfg|All files|*.*"
        dialog.Title = "Open Grammar"
        If dialog.ShowDialog() = DialogResult.OK Then
            ' Create, load, and activate the specified grammar.
            Dim grammar As New Grammar(recognizer)
            grammar.Name = dialog.FileName
            Try
                grammar.Load(dialog.FileName)
            Catch
                ' Catch all exceptions while loading the grammar file.
                ' For PDC, Grammar.Load() throws COMException.  This
                ' behavior may change in the future.  
                MessageBox.Show(dialog.FileName + " failed to load.")
                Return
            End Try
            grammar.IsActive = True

            ' Add the new grammar to the grammarTreeView.
            AddToGrammarTreeView(grammar)
        End If
    End Sub

    ' Unloads the grammar of the selected grammarTreeView node.
    Private Sub RemoveGrammarButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles removeGrammarButton.Click
        If Not grammarTreeView.SelectedNode Is Nothing Then
            ' Get the node representing the grammar.
            Dim grammarNode As TreeNode
            If grammarTreeView.SelectedNode.Parent Is Nothing Then
                grammarNode = grammarTreeView.SelectedNode
            Else
                grammarNode = grammarTreeView.SelectedNode.Parent
            End If

            ' Dispose the grammar object stored in grammarNode.Tag.
            ' This stops future recognition against this grammar and
            ' releases the grammar resources.
            CType(grammarNode.Tag, Grammar).Dispose()

            ' Remove the grammar node from the grammarTreeView.
            grammarNode.Remove()
        End If
    End Sub

    ' Adds the specified grammar to grammarTreeView.
    Private Sub AddToGrammarTreeView(ByVal grammar As Grammar)
        ' The grammarTreeView represents all loaded grammars and their 
        ' top-level rules in a hierarchy.  Each grammar/rule can be
        ' individually activated by checking the corresponding node.

        ' Create the subtree representing the new grammar and its rules.
        ' - Set the Checked state to match the IsActive state of the grammar 
        '   and its rules.
        ' - Associate the grammar node with the Grammar object.
        ' - Associate each rule node with the corresponding Rule object.
        Dim grammarNode As New TreeNode(grammar.Name)
        grammarNode.Checked = grammar.IsActive
        grammarNode.Tag = grammar
        Dim rule As Rule
        For Each rule In grammar.TopLevelRules
            Dim ruleNode As New TreeNode(rule.Name)
            ruleNode.Checked = rule.IsActive
            ruleNode.Tag = rule
            grammarNode.Nodes.Add(ruleNode)
        Next

        ' Add the subtree to grammarTreeView.
        grammarTreeView.Nodes.Add(grammarNode)
        grammarNode.ExpandAll()
    End Sub

    ' Updates the IsActive state of the grammar/rule after the node is checked.
    Private Sub GrammarTreeView_AfterCheck(ByVal sender As Object, ByVal e As TreeViewEventArgs) Handles grammarTreeView.AfterCheck
        If e.Node.Parent Is Nothing Then
            ' Grammar node
            CType(e.Node.Tag, Grammar).IsActive = e.Node.Checked
        Else
            ' Rule node
            CType(e.Node.Tag, Rule).IsActive = e.Node.Checked
        End If
    End Sub

    ' Private Speech fields
    Private WithEvents recognizer As SystemRecognizer
    Private WithEvents changeColorGrammar As Srgs.SrgsGrammar

    ' Private WinForm fields
    Private WithEvents listenButton As Button
    Private WithEvents recognitionTextBox As TextBox
    Private WithEvents audioLevelProgressBar As ProgressBar
    Private WithEvents emulateButton As Button
    Private WithEvents emulateTextBox As TextBox
    Private WithEvents featureTabControl As TabControl
    Private WithEvents grammarTabPage As TabPage
    Private WithEvents grammarTreeView As TreeView
    Private WithEvents addGrammarButton As Button
    Private WithEvents removeGrammarButton As Button
    Private WithEvents resultTabPage As TabPage
    Private WithEvents recognitionRuleTreeView As TreeView
    Private WithEvents recognitionSmlTextBox As TextBox
    Private WithEvents alternatesTabPage As TabPage
    Private WithEvents alternatesListBox As ListBox

#Region " Windows Form Designer generated code "
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

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
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New Container()
        Me.listenButton = New Button()
        Me.recognitionTextBox = New TextBox()
        Me.audioLevelProgressBar = New ProgressBar()
        Me.emulateTextBox = New TextBox()
        Me.emulateButton = New Button()
        Me.featureTabControl = New TabControl()
        Me.grammarTabPage = New TabPage()
        Me.grammarTreeView = New TreeView()
        Me.addGrammarButton = New Button()
        Me.removeGrammarButton = New Button()
        Me.resultTabPage = New TabPage()
        Me.recognitionRuleTreeView = New TreeView()
        Me.recognitionSmlTextBox = New TextBox()
        Me.alternatesTabPage = New TabPage()
        Me.alternatesListBox = New ListBox()
        Me.featureTabControl.SuspendLayout()
        Me.grammarTabPage.SuspendLayout()
        Me.resultTabPage.SuspendLayout()
        Me.alternatesTabPage.SuspendLayout()
        Me.SuspendLayout()

        ' 
        ' listenButton
        ' 
        Me.listenButton.Location = New Point(8, 8)
        Me.listenButton.Name = "listenButton"
        Me.listenButton.Size = New Size(96, 32)
        Me.listenButton.Text = "Listen"
        Me.listenButton.BackColor = SystemColors.Control

        ' 
        ' recognitionTextBox
        ' 
        Me.recognitionTextBox.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        Me.recognitionTextBox.Location = New Point(104, 8)
        Me.recognitionTextBox.Name = "recognitionTextBox"
        Me.recognitionTextBox.Size = New Size(520, 32)
        Me.recognitionTextBox.WordWrap = False

        ' 
        ' audioLevelProgressBar
        ' 
        Me.audioLevelProgressBar.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        Me.audioLevelProgressBar.Location = New Point(8, 40)
        Me.audioLevelProgressBar.Name = "audioLevelProgressBar"
        Me.audioLevelProgressBar.Size = New Size(616, 8)
        Me.audioLevelProgressBar.Step = 1
        Me.audioLevelProgressBar.Value = 0
        Me.audioLevelProgressBar.Visible = False

        ' 
        ' emulateTextBox
        ' 
        Me.emulateTextBox.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        Me.emulateTextBox.HideSelection = False
        Me.emulateTextBox.Location = New Point(104, 56)
        Me.emulateTextBox.Name = "emulateTextBox"
        Me.emulateTextBox.Size = New Size(520, 32)

        ' 
        ' emulateButton
        ' 
        Me.emulateButton.Location = New Point(8, 56)
        Me.emulateButton.Name = "emulateButton"
        Me.emulateButton.Size = New Size(96, 32)
        Me.emulateButton.Text = "Emulate"
        Me.emulateButton.BackColor = SystemColors.Control

        ' 
        ' featureTabControl
        ' 
        Me.featureTabControl.Alignment = TabAlignment.Bottom
        Me.featureTabControl.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Me.featureTabControl.Controls.Add(Me.grammarTabPage)
        Me.featureTabControl.Controls.Add(Me.resultTabPage)
        Me.featureTabControl.Controls.Add(Me.alternatesTabPage)
        Me.featureTabControl.ItemSize = New Size(109, 32)
        Me.featureTabControl.Location = New Point(0, 96)
        Me.featureTabControl.Name = "featureTabControl"
        Me.featureTabControl.SelectedIndex = 0
        Me.featureTabControl.Size = New Size(632, 312)

        ' 
        ' grammarTabPage
        ' 
        Me.grammarTabPage.Controls.Add(Me.grammarTreeView)
        Me.grammarTabPage.Controls.Add(Me.removeGrammarButton)
        Me.grammarTabPage.Controls.Add(Me.addGrammarButton)
        Me.grammarTabPage.Location = New Point(4, 4)
        Me.grammarTabPage.Name = "grammarTabPage"
        Me.grammarTabPage.Size = New Size(624, 272)
        Me.grammarTabPage.Text = "Grammar"

        ' 
        ' grammarTreeView
        ' 
        Me.grammarTreeView.CheckBoxes = True
        Me.grammarTreeView.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Me.grammarTreeView.HideSelection = False
        Me.grammarTreeView.Location = New Point(0, 0)
        Me.grammarTreeView.Name = "grammarTreeView"
        Me.grammarTreeView.Size = New Size(624, 224)

        ' 
        ' addGrammarButton
        ' 
        Me.addGrammarButton.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Me.addGrammarButton.Location = New Point(456, 232)
        Me.addGrammarButton.Name = "addGrammarButton"
        Me.addGrammarButton.Size = New Size(56, 32)
        Me.addGrammarButton.Text = "Add"

        ' 
        ' removeGrammarButton
        ' 
        Me.removeGrammarButton.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Me.removeGrammarButton.Location = New Point(520, 232)
        Me.removeGrammarButton.Name = "removeGrammarButton"
        Me.removeGrammarButton.Size = New Size(96, 32)
        Me.removeGrammarButton.Text = "Remove"

        ' 
        ' resultTabPage
        ' 
        Me.resultTabPage.Controls.Add(Me.recognitionRuleTreeView)
        Me.resultTabPage.Controls.Add(Me.recognitionSmlTextBox)
        Me.resultTabPage.Location = New Point(4, 4)
        Me.resultTabPage.Name = "resultTabPage"
        Me.resultTabPage.Size = New Size(624, 272)
        Me.resultTabPage.Text = "Result"

        ' 
        ' recognitionRuleTreeView
        ' 
        Me.recognitionRuleTreeView.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        Me.recognitionRuleTreeView.Location = New Point(0, 0)
        Me.recognitionRuleTreeView.Name = "recognitionRuleTreeView"
        Me.recognitionRuleTreeView.Size = New Size(624, 136)

        ' 
        ' recognitionSmlTextBox
        ' 
        Me.recognitionSmlTextBox.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Me.recognitionSmlTextBox.Location = New Point(0, 136)
        Me.recognitionSmlTextBox.Multiline = True
        Me.recognitionSmlTextBox.Name = "recognitionSmlTextBox"
        Me.recognitionSmlTextBox.ReadOnly = True
        Me.recognitionSmlTextBox.ScrollBars = ScrollBars.Vertical
        Me.recognitionSmlTextBox.Size = New Size(624, 136)

        ' 
        ' alternatesTabPage
        ' 
        Me.alternatesTabPage.Controls.Add(Me.alternatesListBox)
        Me.alternatesTabPage.Location = New Point(4, 4)
        Me.alternatesTabPage.Name = "alternatesTabPage"
        Me.alternatesTabPage.Size = New Size(624, 272)
        Me.alternatesTabPage.Text = "Alternates"

        ' 
        ' alternatesListBox
        ' 
        Me.alternatesListBox.Dock = DockStyle.Fill
        Me.alternatesListBox.HorizontalScrollbar = True
        Me.alternatesListBox.ItemHeight = 24
        Me.alternatesListBox.Location = New Point(0, 0)
        Me.alternatesListBox.Name = "alternatesListBox"
        Me.alternatesListBox.Size = New Size(624, 268)

        ' 
        ' SpeechRecognition
        ' 
        Me.AutoScaleBaseSize = New Size(10, 25)
        Me.ClientSize = New Size(632, 406)
        Me.Controls.Add(Me.recognitionTextBox)
        Me.Controls.Add(Me.emulateTextBox)
        Me.Controls.Add(Me.emulateButton)
        Me.Controls.Add(Me.audioLevelProgressBar)
        Me.Controls.Add(Me.listenButton)
        Me.Controls.Add(Me.featureTabControl)
        Me.Font = New Font("Arial", 15.75F, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "SpeechRecognition"
        Me.Text = "Speech Recognition Demo"
        Me.featureTabControl.ResumeLayout(False)
        Me.grammarTabPage.ResumeLayout(False)
        Me.resultTabPage.ResumeLayout(False)
        Me.alternatesTabPage.ResumeLayout(False)
        Me.ResumeLayout(False)
    End Sub
#End Region
End Class