//---------------------------------------------------------------------
// SpeechRecognition
//  This sample application demonstrates the basic speech recognition 
//  functionalities provided by System.Speech.
//---------------------------------------------------------------------
// This file is part of the Microsoft .NET Framework SDK Code Samples.
// 
// Copyright (C) Microsoft Corporation.  All rights reserved.
// 
// This source code is intended only as a supplement to Microsoft
// Development Tools and/or on-line documentation.  See these other
// materials for detailed information regarding Microsoft code samples.
// 
// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//---------------------------------------------------------------------

// Standard WinForm namespaces
using System;
using System.Drawing;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;

// Speech namespaces
using System.Speech.Recognition; // Basic speech recognition
using Srgs = System.Speech.Srgs; // W3C Speech Recognition 
                                 // Grammar Specification (SRGS)

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Speech Recognition")]
[assembly: AssemblyDescription("Speech Recognition Sample Application")]
[assembly: AssemblyCompany("Microsoft Corporation")]
[assembly: AssemblyProduct("Microsoft (R) Windows (R) Operating System")]
[assembly: AssemblyCopyright("Copyright (C) 2003 Microsoft Corporation.")]
[assembly: AssemblyCulture("")]
[assembly: AssemblyVersion("1.0.*")]
[assembly: CLSCompliant(true)]
[assembly: ComVisible(false)]
[assembly: EnvironmentPermission(SecurityAction.RequestMinimum)]

// SpeechRecognition is a WinForm application that demonstrates basic speech 
// recognition functionalities.
public class SpeechRecognition : Form
{
    // The main entry point for the application.
    [STAThread]
    static void Main()
    {
        Application.Run(new SpeechRecognition());
    }

    // Initializes SpeechRecognition form.
    public SpeechRecognition()
    {
        InitializeComponent();
    }

    // Disposes the resources used by the SpeechRecognition application.
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (components != null)
            {
                components.Dispose();
            }
        }

        base.Dispose(disposing);
    }

    // Initializes the SystemRecognizer and loads sample grammars on form load.
    private void SpeechRecognition_Load(object sender, EventArgs e)
    {
        // Initialize the SystemRecognizer and event handlers.
        recognizer = new SystemRecognizer();
        recognizer.RecognizeProgressChanged += 
            new RecognizeProgressChangedEventHandler(Recognizer_RecognizeProgressChanged);
        recognizer.Hypothesis += 
            new RecognitionEventHandler(Recognizer_Hypothesis);
        recognizer.NoRecognition += 
            new RecognitionEventHandler(Recognizer_NoRecognition);
        recognizer.RejectedRecognition += 
            new RecognitionEventHandler(Recognizer_RejectedRecognition);
        recognizer.Recognition += 
            new RecognitionEventHandler(Recognizer_Recognition);

        // Disable recognition of SystemRecognizer grammars.
        recognizer.IsActive = false;

        // Load Dictation grammar.
        DictationGrammar dictationGrammar = new DictationGrammar(recognizer);
        dictationGrammar.Name = "Dictation";
        dictationGrammar.IsActive = true;

        // Load ChangeColor grammar.
        // - Create grammar using System.Speech.Srgs object model.
        // - Add event handler to change background color on recognition.
        Srgs.SrgsGrammar changeColorGrammar = CreateChangeColorGrammar();
        changeColorGrammar.IsActive = true;
        changeColorGrammar.Recognition += 
            new RecognitionEventHandler(ChangeColorGrammar_Recognition);

        // Display loaded grammars/rules in grammarTreeView.
        AddToGrammarTreeView(dictationGrammar);
        AddToGrammarTreeView(changeColorGrammar);            
    }

    // Creates a Srgs grammar for changing the background color of the form
    // to any of the .NET defined color enumerations.
    private Srgs.SrgsGrammar CreateChangeColorGrammar()
    {
        // Build the following SRGS grammar with SemanticInterpretation using
        // System.Speech.Srgs object model.
        // - http://www.w3.org/TR/speech-grammar
        // - http://www.w3.org/TR/semantic-interpretation
        //
        // <grammar version="1.0" xml:lang="en-US" 
        //   xmlns="http://www.w3.org/2001/06/grammar" root="ChangeColor">
        //   <rule id="ChangeColor">Change color to
        //     <one-of>
        //       <item>ActiveBorder
        //         <tag>$.color="ActiveBorder";</tag>
        //       </item>
        //       <item>ActiveCaption
        //         <tag>$.color="ActiveCaption";</tag>
        //       </item>
        //       ...
        //     </one-of>
        //   </rule>
        // </grammar>
        //
        // Examples Phrases
        // - "Change color to Red"
        // - "Change color to Blue"

        // Create a SrgsGrammar object representing the ChangeColor grammar.
        Srgs.SrgsGrammar grammar = new Srgs.SrgsGrammar(recognizer);
        grammar.Name = "Change Color";
        
        // Create an OneOf list of system colors with semantics.
        Srgs.OneOf colorOneOf = new Srgs.OneOf();
        foreach (string colorName in System.Enum.GetNames(typeof(KnownColor)))
        {
            // Associate the string name of the color with each item.
            string tag = String.Format("$.color=\"{0}\";", colorName);
            colorOneOf.Elements.AddItem(colorName, tag);
        }

        // Build the ChangeColor rule: "Change color to <colorOneOf>"
        Srgs.Rule rule = new Srgs.Rule("ChangeColor");
        rule.Elements.Add("Change color to");
        rule.Elements.Add(colorOneOf);

        // Add the rule to the grammar and set it as the Root rule.
        grammar.Rules.Add(rule);
        grammar.Root = rule;
        
        return grammar;
    }

    // Starts/Stops the recognition of loaded grammars depending on the
    // current toggle state of the listenButton.
    private void ListenButton_Click(object sender, EventArgs e)
    {
        if (listenButton.Text == "Listen")  // Start listening.
        {
            // Update listenButton text.
            listenButton.Text = "Stop";

            // Display the audio level meter.
            audioLevelProgressBar.Value = 0; 
            audioLevelProgressBar.Visible = true; 

            // Clear the display of recognition result.
            DisplayResult("", Color.Black, null);

            // Enable recognition of SystemRecognizer grammars.
            recognizer.IsActive = true;
        }
        else                                // Stop listening.
        {
            // Update listenButton text.
            listenButton.Text = "Listen";

            // Disable recognition of SystemRecognizer grammars.
            recognizer.IsActive = false;

            // Hide the audio level meter.
            audioLevelProgressBar.Visible = false;
        }

    }

    // Emulates the recognition of emulateTextBox.
    private void EmulateButton_Click(object sender, EventArgs e)
    {
        if (emulateTextBox.Text.Length > 0)
        {
            recognizer.EmulateRecognize(emulateTextBox.Text);
        }
    }

    // Simulates the click of the emulateButton when Enter is pressed in 
    // the emulateTextBox.
    private void EmulateTextBox_KeyPress(object sender, KeyPressEventArgs e)
    {
        if ((Keys)e.KeyChar == Keys.Enter)
        {
            EmulateButton_Click(sender, e);
        }
    }

    // Updates the audio level meter when the recognition progress has been
    // updated.
    private void Recognizer_RecognizeProgressChanged(object sender, RecognizeProgressChangedEventArgs e)
    {
        audioLevelProgressBar.Value = e.AudioLevel;
    }

    // Handles the Hypothesis event and displays the hypothesized result.
    private void Recognizer_Hypothesis(object sender, RecognitionEventArgs e)
    {
        DisplayResult(e.Result.Text + " ...", Color.Black, null);
    }

    // Handles the NoRecognition event and displays the empty result.
    private void Recognizer_NoRecognition(object sender, 
                                          RecognitionEventArgs e)
    {
        DisplayResult("<none>", Color.Red, null);
    }

    // Handles the RejectedRecognition event and displays the rejected result.
    private void Recognizer_RejectedRecognition(object sender, 
                                                RecognitionEventArgs e)
    {
        DisplayResult(e.Result.Text, Color.OrangeRed, e.Result);
    }

    // Handles the Recognition event and displays the recognized result.
    private void Recognizer_Recognition(object sender, RecognitionEventArgs e)
    {
        DisplayResult(e.Result.Text, Color.Green, e.Result);
    }

    // Displays the semantics, rules, and alternates of the result.
    private void DisplayResult(string resultText, Color resultColor, 
                                RecognitionResult result)
    {
        // Set the result text and color.
        recognitionTextBox.Text = resultText;
        recognitionTextBox.ForeColor = resultColor;

        // Clear semantic XML, rule tree, and alternates.
        recognitionSmlTextBox.Text = "";
        recognitionRuleTreeView.Nodes.Clear();
        alternatesListBox.Items.Clear();

        if (result != null)
        {
            // Set the semantics XML.
            recognitionSmlTextBox.Text = result.Sml;

            // Update rule tree based on the result.
            if (result.Rule != null)
            {
                UpdateRuleTreeView(recognitionRuleTreeView.Nodes, 
                                                result.Rule);
            }
            recognitionRuleTreeView.ExpandAll();

            // Updates alternates list with alternates of the result.
            foreach (RecognitionPhraseAlternate alt in result.Alternates)
            {
                alternatesListBox.Items.Add(alt.AlternateText);
            }
        }
    }

    // Recursively adds the RecognitionRule to the recognitionRuleTreeView.
    private void UpdateRuleTreeView(TreeNodeCollection nodes, 
                                    RecognitionRule rule)
    {
        string ruleText = String.Format("{0} [{1}] ({2})", 
                                        rule.Name, rule.Text, 
                                        rule.RecognizerConfidence);
        TreeNode ruleNode = new TreeNode(ruleText);
        foreach (RecognitionRule childRule in rule.Rules)
        {
            UpdateRuleTreeView(ruleNode.Nodes, childRule);
        }
        nodes.Add(ruleNode);
    }

    // Handles the Recognition event for the ChangeColorGrammar and sets 
    // the background color of the WinForm to the specified color.
    private void ChangeColorGrammar_Recognition(object sender, 
                                                RecognitionEventArgs e)
    {
        // Retrieve the value of the semantic property.
        string colorName = (string)e.Result.Properties["color"].Value;
        BackColor = Color.FromName(colorName);
    }

    // Displays the OpenFileDialog and loads the specified grammar.
    private void AddGrammarButton_Click(object sender, EventArgs e)
    {
        // Create the Open Grammar dialog box.
        OpenFileDialog dialog = new OpenFileDialog(); 
        dialog.Filter = "SRGS files|*.*xml|CFG files|*.cfg|All files|*.*"; 
        dialog.Title = "Open Grammar"; 
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            // Create, load, and activate the specified grammar.
            Grammar grammar = new Grammar(recognizer);
            grammar.Name = dialog.FileName;
            try
            {
                grammar.Load(dialog.FileName);
            }
            catch
            {
                // Catch all exceptions while loading the grammar file.
                // For PDC, Grammar.Load() throws COMException.  This
                // behavior may change in the future.  
                MessageBox.Show(dialog.FileName + " failed to load.");
                return;
            }
            grammar.IsActive = true;

            // Add the new grammar to the grammarTreeView.
            AddToGrammarTreeView(grammar);
        }
    }

    // Unloads the grammar of the selected grammarTreeView node.
    private void RemoveGrammarButton_Click(object sender, EventArgs e)
    {
        if (grammarTreeView.SelectedNode != null)
        {
            // Get the node representing the grammar.
            TreeNode grammarNode;
            if (grammarTreeView.SelectedNode.Parent == null)
            {
                grammarNode = grammarTreeView.SelectedNode;
            }
            else
            {
                grammarNode = grammarTreeView.SelectedNode.Parent;
            }

            // Dispose the grammar object stored in grammarNode.Tag.
            // This stops future recognition against this grammar and
            // releases the grammar resources.
            ((Grammar)grammarNode.Tag).Dispose();

            // Remove the grammar node from the grammarTreeView.
            grammarNode.Remove();
        }
    }

    // Adds the specified grammar to grammarTreeView.
    private void AddToGrammarTreeView(Grammar grammar)
    {
        // The grammarTreeView represents all loaded grammars and their 
        // top-level rules in a hierarchy.  Each grammar/rule can be
        // individually activated by checking the corresponding node.

        // Create the subtree representing the new grammar and its rules.
        // - Set the Checked state to match the IsActive state of the grammar 
        //   and its rules.
        // - Associate the grammar node with the Grammar object.
        // - Associate each rule node with the corresponding Rule object.
        TreeNode grammarNode = new TreeNode(grammar.Name);
        grammarNode.Checked = grammar.IsActive;
        grammarNode.Tag = grammar;
        foreach (Rule rule in grammar.TopLevelRules)
        {
            TreeNode ruleNode = new TreeNode(rule.Name);
            ruleNode.Checked = rule.IsActive;
            ruleNode.Tag = rule;
            grammarNode.Nodes.Add(ruleNode);
        }

        // Add the subtree to grammarTreeView.
        grammarTreeView.Nodes.Add(grammarNode);
        grammarNode.ExpandAll();
    }

    // Updates the IsActive state of the grammar/rule after node is checked.
    private void GrammarTreeView_AfterCheck(object sender, TreeViewEventArgs e)
    {
        if (e.Node.Parent == null)
        {
            // Grammar node
            ((Grammar)e.Node.Tag).IsActive = e.Node.Checked;
        }
        else
        {
            // Rule node
            ((Rule)e.Node.Tag).IsActive = e.Node.Checked;
        }
    }

    // Private Speech fields
    private SystemRecognizer recognizer;

    // Private WinForm fields
    private IContainer components;
    private Button listenButton;
    private TextBox recognitionTextBox;
    private ProgressBar audioLevelProgressBar;
    private Button emulateButton;
    private TextBox emulateTextBox;
    private TabControl featureTabControl;
    private TabPage grammarTabPage;
    private TreeView grammarTreeView;
    private Button addGrammarButton;
    private Button removeGrammarButton;
    private TabPage resultTabPage;
    private TreeView recognitionRuleTreeView;
    private TextBox recognitionSmlTextBox;
    private TabPage alternatesTabPage;
    private ListBox alternatesListBox;

    #region Windows Form Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.components = new Container();
        this.listenButton = new Button();
        this.recognitionTextBox = new TextBox();
        this.audioLevelProgressBar = new ProgressBar();
        this.emulateTextBox = new TextBox();
        this.emulateButton = new Button();
        this.featureTabControl = new TabControl();
        this.grammarTabPage = new TabPage();
        this.grammarTreeView = new TreeView();
        this.addGrammarButton = new Button();
        this.removeGrammarButton = new Button();
        this.resultTabPage = new TabPage();
        this.recognitionRuleTreeView = new TreeView();
        this.recognitionSmlTextBox = new TextBox();
        this.alternatesTabPage = new TabPage();
        this.alternatesListBox = new ListBox();
        this.featureTabControl.SuspendLayout();
        this.grammarTabPage.SuspendLayout();
        this.resultTabPage.SuspendLayout();
        this.alternatesTabPage.SuspendLayout();
        this.SuspendLayout();

        // 
        // listenButton
        // 
        this.listenButton.Location = new Point(8, 8);
        this.listenButton.Name = "listenButton";
        this.listenButton.Size = new Size(96, 32);
        this.listenButton.Text = "Listen";
        this.listenButton.BackColor = SystemColors.Control;
        this.listenButton.Click += new EventHandler(this.ListenButton_Click);

        // 
        // recognitionTextBox
        // 
        this.recognitionTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left |
                                            AnchorStyles.Right;
        this.recognitionTextBox.Location = new Point(104, 8);
        this.recognitionTextBox.Name = "recognitionTextBox";
        this.recognitionTextBox.Size = new Size(520, 32);
        this.recognitionTextBox.WordWrap = false;

        // 
        // audioLevelProgressBar
        // 
        this.audioLevelProgressBar.Anchor = AnchorStyles.Top | 
                                            AnchorStyles.Left | 
                                            AnchorStyles.Right;
        this.audioLevelProgressBar.Location = new Point(8, 40);
        this.audioLevelProgressBar.Name = "audioLevelProgressBar";
        this.audioLevelProgressBar.Size = new Size(616, 8);
        this.audioLevelProgressBar.Step = 1;
        this.audioLevelProgressBar.Value = 0;
        this.audioLevelProgressBar.Visible = false;

        // 
        // emulateTextBox
        // 
        this.emulateTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | 
                                     AnchorStyles.Right;
        this.emulateTextBox.HideSelection = false;
        this.emulateTextBox.Location = new Point(104, 56);
        this.emulateTextBox.Name = "emulateTextBox";
        this.emulateTextBox.Size = new Size(520, 32);
        this.emulateTextBox.KeyPress += 
            new KeyPressEventHandler(this.EmulateTextBox_KeyPress);

        // 
        // emulateButton
        // 
        this.emulateButton.Location = new Point(8, 56);
        this.emulateButton.Name = "emulateButton";
        this.emulateButton.Size = new Size(96, 32);
        this.emulateButton.Text = "Emulate";
        this.emulateButton.BackColor = SystemColors.Control;
        this.emulateButton.Click += 
            new System.EventHandler(this.EmulateButton_Click);

        // 
        // featureTabControl
        // 
        this.featureTabControl.Alignment = TabAlignment.Bottom;
        this.featureTabControl.Anchor = AnchorStyles.Top | 
                                        AnchorStyles.Bottom |
                                        AnchorStyles.Left | AnchorStyles.Right;
        this.featureTabControl.Controls.Add(this.grammarTabPage);
        this.featureTabControl.Controls.Add(this.resultTabPage);
        this.featureTabControl.Controls.Add(this.alternatesTabPage);
        this.featureTabControl.ItemSize = new Size(109, 32);
        this.featureTabControl.Location = new Point(0, 96);
        this.featureTabControl.Name = "featureTabControl";
        this.featureTabControl.SelectedIndex = 0;
        this.featureTabControl.Size = new Size(632, 312);

        // 
        // grammarTabPage
        // 
        this.grammarTabPage.Controls.Add(this.grammarTreeView);
        this.grammarTabPage.Controls.Add(this.removeGrammarButton);
        this.grammarTabPage.Controls.Add(this.addGrammarButton);
        this.grammarTabPage.Location = new Point(4, 4);
        this.grammarTabPage.Name = "grammarTabPage";
        this.grammarTabPage.Size = new Size(624, 272);
        this.grammarTabPage.Text = "Grammar";

        // 
        // grammarTreeView
        // 
        this.grammarTreeView.CheckBoxes = true;
        this.grammarTreeView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom |
                                        AnchorStyles.Left | AnchorStyles.Right;
        this.grammarTreeView.HideSelection = false;
        this.grammarTreeView.Location = new Point(0, 0);
        this.grammarTreeView.Name = "grammarTreeView";
        this.grammarTreeView.Size = new Size(624, 224);
        this.grammarTreeView.AfterCheck += 
            new TreeViewEventHandler(this.GrammarTreeView_AfterCheck);

        // 
        // addGrammarButton
        // 
        this.addGrammarButton.Anchor = AnchorStyles.Bottom | 
                                       AnchorStyles.Right;
        this.addGrammarButton.Location = new Point(456, 232);
        this.addGrammarButton.Name = "addGrammarButton";
        this.addGrammarButton.Size = new Size(56, 32);
        this.addGrammarButton.Text = "Add";
        this.addGrammarButton.Click += 
            new System.EventHandler(this.AddGrammarButton_Click);

        // 
        // removeGrammarButton
        // 
        this.removeGrammarButton.Anchor = AnchorStyles.Bottom | 
                                          AnchorStyles.Right;
        this.removeGrammarButton.Location = new Point(520, 232);
        this.removeGrammarButton.Name = "removeGrammarButton";
        this.removeGrammarButton.Size = new Size(96, 32);
        this.removeGrammarButton.Text = "Remove";
        this.removeGrammarButton.Click += 
            new System.EventHandler(this.RemoveGrammarButton_Click);

        // 
        // resultTabPage
        // 
        this.resultTabPage.Controls.Add(this.recognitionRuleTreeView);
        this.resultTabPage.Controls.Add(this.recognitionSmlTextBox);
        this.resultTabPage.Location = new Point(4, 4);
        this.resultTabPage.Name = "resultTabPage";
        this.resultTabPage.Size = new Size(624, 272);
        this.resultTabPage.Text = "Result";

        // 
        // recognitionRuleTreeView
        // 
        this.recognitionRuleTreeView.Anchor = AnchorStyles.Top | 
                                              AnchorStyles.Left |
                                              AnchorStyles.Right;
        this.recognitionRuleTreeView.Location = new Point(0, 0);
        this.recognitionRuleTreeView.Name = "recognitionRuleTreeView";
        this.recognitionRuleTreeView.Size = new Size(624, 136);

        // 
        // recognitionSmlTextBox
        // 
        this.recognitionSmlTextBox.Anchor = AnchorStyles.Top | 
                                            AnchorStyles.Bottom |
                                            AnchorStyles.Left | 
                                            AnchorStyles.Right;
        this.recognitionSmlTextBox.Location = new Point(0, 136);
        this.recognitionSmlTextBox.Multiline = true;
        this.recognitionSmlTextBox.Name = "recognitionSmlTextBox";
        this.recognitionSmlTextBox.ReadOnly = true;
        this.recognitionSmlTextBox.ScrollBars = ScrollBars.Vertical;
        this.recognitionSmlTextBox.Size = new Size(624, 136);

        // 
        // alternatesTabPage
        // 
        this.alternatesTabPage.Controls.Add(this.alternatesListBox);
        this.alternatesTabPage.Location = new Point(4, 4);
        this.alternatesTabPage.Name = "alternatesTabPage";
        this.alternatesTabPage.Size = new Size(624, 272);
        this.alternatesTabPage.Text = "Alternates";

        // 
        // alternatesListBox
        // 
        this.alternatesListBox.Dock = DockStyle.Fill;
        this.alternatesListBox.HorizontalScrollbar = true;
        this.alternatesListBox.ItemHeight = 24;
        this.alternatesListBox.Location = new Point(0, 0);
        this.alternatesListBox.Name = "alternatesListBox";
        this.alternatesListBox.Size = new Size(624, 268);

        // 
        // SpeechRecognition
        // 
        this.AutoScaleBaseSize = new Size(10, 25);
        this.ClientSize = new Size(632, 406);
        this.Controls.Add(this.recognitionTextBox);
        this.Controls.Add(this.emulateTextBox);
        this.Controls.Add(this.emulateButton);
        this.Controls.Add(this.audioLevelProgressBar);
        this.Controls.Add(this.listenButton);
        this.Controls.Add(this.featureTabControl);
        this.Font = new Font("Arial", 15.75F, FontStyle.Regular, 
                                GraphicsUnit.Point, (byte)0);
        this.Name = "SpeechRecognition";
        this.Text = "Speech Recognition Demo";
        this.Load += new System.EventHandler(this.SpeechRecognition_Load);
        this.featureTabControl.ResumeLayout(false);
        this.grammarTabPage.ResumeLayout(false);
        this.resultTabPage.ResumeLayout(false);
        this.alternatesTabPage.ResumeLayout(false);
        this.ResumeLayout(false);
    }
    #endregion
}
