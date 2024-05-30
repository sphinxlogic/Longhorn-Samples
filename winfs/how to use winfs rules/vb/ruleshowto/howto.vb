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
Imports System.Collections.Generic
Imports System.IO
Imports System.Storage
Imports System.Storage.Core
Imports System.Storage.Rules
Imports System.Runtime.InteropServices

<Assembly: ComVisible(False)> 
Module HowTo

    Sub Main()
        Dim itemContext As ItemContext
        Try
            itemContext = itemContext.Open()

            'Find out if the rules folder exists (i.e.: this has run before)
            'If not, create the folder
            Dim rulesFolderName As String = "HowTo-Rules"

            Console.WriteLine("Finding the {0} folder.", rulesFolderName)

            Dim rulesFolder As Folder = itemContext.FindByPath(GetType(Folder), rulesFolderName)

            'If the folder exists, delete holding relationships
            If Not (rulesFolder Is Nothing) Then
                'Clean up the folder from the previous run
                Dim list As List(Of FolderMember) = New List(Of FolderMember)

                'Save the folder members.  We can't use the original
                'collection in the loop that Removes items from that
                'collection.
                Dim folderMember As FolderMember
                For Each folderMember In rulesFolder.OutFolderMemberRelationships
                    list.Add(folderMember)
                Next

                'Remove every sub-folder
                For Each folderMember In list
                    rulesFolder.OutFolderMemberRelationships.Remove(folderMember)
                Next

                itemContext.Update()
            Else
                'Create the folder
                rulesFolder = New Folder()
                rulesFolder.DisplayName = System.Nullable(Of String).FromObject(rulesFolderName)

                Dim root As Folder = Folder.GetRootFolder(itemContext)

                root.OutFolderMemberRelationships.AddItem(rulesFolder, rulesFolderName)
            End If

            'Create a rule
            Console.WriteLine("Creating rule...")

            Dim rule As Rule = New Rule(GetType(Document))

            rule.DisplayName = System.Nullable(Of String).FromObject("MyRule")
            rule.Condition = New LeafCondition("=", New PropertyValue(GetType(Document), "Comments"), "Pre-Action Comment")

            Dim a As Action = New Action(New InstanceFunctionInfo("set_Comments"), "Post-Action Comment")
            rule.Actions.Add(a)

            'Create a rule set
            Console.WriteLine("Creating ruleset...")

            Dim rs As RuleSet = New RuleSet()
            rs.DisplayName = System.Nullable(Of String).FromObject("MyRuleSet")
            rulesFolder.OutFolderMemberRelationships.AddItem(rs, rs.DisplayName.ToString(System.Globalization.CultureInfo.CurrentUICulture))

            'Add our rule to the rule set
            rs.AddRule(rule)

            'Create a decision point
            Console.WriteLine("Creating decision point...")

            Dim dp As DecisionPoint = New DecisionPoint()
            dp.DisplayName = System.Nullable(Of String).FromObject("MyDecisionPoint")
            dp.ApplicationName = "SampleApplication"
            dp.ScopeRequired = False
            dp.Constraint = New RuleConstraintElement()
            'dp.Constraint.InputItemTypeId = System.Nullable(of System.Guid) > .default
            '		dp.Constraint.OutputItemTypeId = System.Nullable<System.Guid>.default;
            rulesFolder.OutFolderMemberRelationships.AddItem(dp, dp.DisplayName.ToString(System.Globalization.CultureInfo.CurrentUICulture))

            'Create a rule set attachment
            Console.WriteLine("Creating rule set attachment...")

            Dim rsa As RuleSetAttachment = New RuleSetAttachment()
            rsa.DisplayName = System.Nullable(Of String).FromObject("Attachment - " + dp.DisplayName.ToString(System.Globalization.CultureInfo.CurrentUICulture) + " to " + rs.DisplayName.ToString(System.Globalization.CultureInfo.CurrentUICulture))
            rulesFolder.OutFolderMemberRelationships.AddItem(rsa, rsa.DisplayName.ToString(System.Globalization.CultureInfo.CurrentUICulture))

            'Relate the RuleSetAttachment to the DecisionPoint
            dp.AddRuleSetAttachment(rsa)

            'Relate the RuleSetAttachment to the RuleSet
            rsa.AddRuleSet(rs)

            'Create a document and make comment what the rule is looking for
            Console.WriteLine("Creating document...")

            Dim rulesDoc As Document = New Document()
            rulesDoc.DisplayName = System.Nullable(Of String).FromObject("DocForRules")
            rulesDoc.Comments = System.Nullable(Of String).FromObject("Pre-Action Comment")
            rulesFolder.OutFolderMemberRelationships.AddItem(rulesDoc, rulesDoc.DisplayName.ToString(System.Globalization.CultureInfo.CurrentUICulture))
            Console.WriteLine("The document comment is now " + rulesDoc.Comments.ToString())

            'Save everything
            Console.WriteLine("Saving everything and closing item context.")
            itemContext.Update()
            itemContext.Close()

            'Open a new item context so the code is more real
            Console.WriteLine("")
            Console.WriteLine("Opening new item context...")
            itemContext = itemContext.Open()
            'Find the document to submit to the rules engine
            Console.WriteLine("Finding document to submit...")

            Dim docSearch As ItemSearcher = Document.GetSearcher(itemContext)
            docSearch.Filters.Add("DisplayName = 'DocForRules'")

            Dim doc As Document = docSearch.FindOne()

            'Find the decision point to use to submit the item
            Console.WriteLine("Finding decision point to use...")

            Dim dpSearch As ItemSearcher = DecisionPoint.GetSearcher(itemContext)
            dpSearch.Filters.Add("DisplayName = 'MyDecisionPoint'")

            Dim dp2 As DecisionPoint = dpSearch.FindOnly()

            'Create the rule input item for submitting the rule
            Console.WriteLine("Creating rule input and adding document...")
            Dim ruleInput As RuleInput = New RuleInput()
            ruleInput.OperationName = ""
            ruleInput.EventDataItemId = doc.ItemId
            ruleInput.EventClassTypeId = doc.GetTypeId()

            'Add the rule input to a collection to be submitted
            Dim ruleInputCollection As RuleInputCollection = New RuleInputCollection()
            ruleInputCollection.Add(ruleInput)

            'Establish a unique ID for the results returned from the engine
            Dim rseItemId As Guid = Guid.Empty

            'Submit the rule input and assign unique ID of the results
            Console.WriteLine("Submitting rule input collection to rules engine...")
            rseItemId = dp2.SubmitAndWait(ruleInputCollection)

            'Cast the results as a result set evaluation item
            Dim resultSet As RuleSetEvaluation = itemContext.FindItemById(rseItemId)

            'Report on the results
            Console.WriteLine("")
            Console.WriteLine("Results:")
            Console.WriteLine(resultSet.RuleResultElements.Count.ToString(System.Globalization.CultureInfo.CurrentUICulture) + " action(s) to be performed ...")

            'Execute the actions
            Console.WriteLine("")
            Console.WriteLine("Executing the actions ...")
            resultSet.Execute(itemContext)
            Console.WriteLine("The document comment is now " + doc.Comments.ToString())

            'Save stuff...
            itemContext.Update()
            itemContext.Close()
            Console.WriteLine("Done.")

            'Just used so console window doesn't go away
            Console.WriteLine("Press ENTER to exit...")
            Console.ReadLine()

        Catch ex As Exception
            Console.WriteLine("There is a problem with the sample and an exception was thrown...")
            Console.WriteLine("Exception info: {0}", ex.ToString())
        End Try
    End Sub

End Module
