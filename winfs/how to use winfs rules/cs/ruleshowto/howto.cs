//---------------------------------------------------------------------
//  This file is part of the Microsoft .NET Framework SDK Code Samples.
// 
//  Copyright (C) Microsoft Corporation.  All rights reserved.
// 
//This source code is intended only as a supplement to Microsoft
//Development Tools and/or on-line documentation.  See these other
//materials for detailed information regarding Microsoft code samples.
// 
//THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
//KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//PARTICULAR PURPOSE.
//---------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using System.Storage;
using System.Storage.Core;
using System.Storage.Rules;
using System.Runtime.InteropServices;

[assembly: ComVisible(false)]
[assembly: CLSCompliant(true)]
namespace Microsoft.Samples.WinFS
{
	// Contains code used in How To Use WinFS Rules
	public sealed class WinFSRulesHowTo
	{
		private WinFSRulesHowTo() { } 
		static void Main(string[] args)
		{
			try
			{
				using (ItemContext itemContext = ItemContext.Open())
				{
					// Find out if the rules folder exists (i.e.: this has run before)
					// If not, create the folder
					String rulesFolderName = "HowTo-Rules";

					Console.WriteLine("Finding the {0} folder.", rulesFolderName);

					Folder rulesFolder = itemContext.FindByPath(typeof(Folder), rulesFolderName) as Folder;

					// If the folder exists, delete holding relationships
					if (null != rulesFolder)
					{
						// Clean up the folder from the previous run
						List<FolderMember> list = new List<FolderMember>();

						// Save the folder members.  We can't use the original
						// collection in the loop that Removes items from that
						// collection.
						foreach (FolderMember folderMember in rulesFolder.OutFolderMemberRelationships)
						{
							list.Add(folderMember);
						}

						// Remove every sub-folder
						foreach (FolderMember folderMember in list)
						{
							rulesFolder.OutFolderMemberRelationships.Remove(folderMember);
						}

						itemContext.Update();
					}
					else
					{
						// Create the folder
						rulesFolder = new Folder();
						rulesFolder.DisplayName = rulesFolderName;

						Folder root = Folder.GetRootFolder(itemContext);

						root.OutFolderMemberRelationships.AddItem(rulesFolder, rulesFolderName);
					}

					// Create a rule
					Console.WriteLine("Creating rule...");

					Rule rule = new Rule(typeof(Document));

					rule.DisplayName = "MyRule";
					rule.Condition = new LeafCondition("=", new PropertyValue(typeof(Document), "Comments"), "Pre-Action Comment");

					Action a = new Action(new InstanceFunctionInfo("set_Comments"), "Post-Action Comment");

					rule.Actions.Add(a);

					// Create a rule set
					Console.WriteLine("Creating ruleset...");

					RuleSet rs = new RuleSet();

					rs.DisplayName = "MyRuleSet";
					rulesFolder.OutFolderMemberRelationships.AddItem(rs, rs.DisplayName.ToString(System.Globalization.CultureInfo.CurrentUICulture));

					// Add our rule to the rule set
					rs.AddRule(rule);

					// Create a decision point
					Console.WriteLine("Creating decision point...");

					DecisionPoint dp = new DecisionPoint();

					dp.DisplayName = "MyDecisionPoint";
					dp.ApplicationName = "SampleApplication";
					dp.ScopeRequired = false;
					dp.Constraint = new RuleConstraintElement();
					dp.Constraint.InputItemTypeId = System.Nullable<System.Guid>.default;
					dp.Constraint.OutputItemTypeId = System.Nullable<System.Guid>.default;
					rulesFolder.OutFolderMemberRelationships.AddItem(dp, dp.DisplayName.ToString(System.Globalization.CultureInfo.CurrentUICulture));

					//Create a rule set attachment
					Console.WriteLine("Creating rule set attachment...");

					RuleSetAttachment rsa = new RuleSetAttachment();

					rsa.DisplayName = "Attachment - " + dp.DisplayName + " to " + rs.DisplayName;

					rulesFolder.OutFolderMemberRelationships.AddItem(rsa, rsa.DisplayName.ToString(System.Globalization.CultureInfo.CurrentUICulture));

					// Relate the RuleSetAttachment to the DecisionPoint
					dp.AddRuleSetAttachment(rsa);

					// Relate the RuleSetAttachment to the RuleSet
					rsa.AddRuleSet(rs);

					// Create a document and make comment what the rule is looking for
					Console.WriteLine("Creating document...");

					Document rulesDoc = new Document();

					rulesDoc.DisplayName = "DocForRules";
					rulesDoc.Comments = "Pre-Action Comment";
					rulesFolder.OutFolderMemberRelationships.AddItem(rulesDoc, rulesDoc.DisplayName.ToString(System.Globalization.CultureInfo.CurrentUICulture));
					Console.WriteLine("The document comment is now " + rulesDoc.Comments);

					// Save everything
					Console.WriteLine("Saving everything and closing item context.");
					itemContext.Update();
				}

				// Open a new item context so the code is more real
				Console.WriteLine("\nOpening new item context...");
				using (ItemContext itemContext = ItemContext.Open())
				{
					// Find the document to submit to the rules engine
					Console.WriteLine("Finding document to submit...");

					ItemSearcher docSearch = Document.GetSearcher(itemContext);

					docSearch.Filters.Add("DisplayName = 'DocForRules'");

					Document doc = (Document)docSearch.FindOne();

					// Find the decision point to use to submit the item
					Console.WriteLine("Finding decision point to use...");

					ItemSearcher dpSearch = DecisionPoint.GetSearcher(itemContext);

					dpSearch.Filters.Add("DisplayName = 'MyDecisionPoint'");

					DecisionPoint dp2 = (DecisionPoint)dpSearch.FindOnly();

					// Create the rule input item for submitting the rule
					Console.WriteLine("Creating rule input and adding document...");

					RuleInput ruleInput = new RuleInput();

					ruleInput.OperationName = "";
					ruleInput.EventDataItemId = doc.ItemId;
					ruleInput.EventClassTypeId = doc.GetTypeId();

					// Add the rule input to a collection to be submitted
					RuleInputCollection ruleInputCollection = new RuleInputCollection();

					ruleInputCollection.Add(ruleInput);

					// Establish a unique ID for the results returned from the engine
					Guid rseItemId = Guid.Empty;

					// Submit the rule input and assign unique ID of the results
					Console.WriteLine("Submitting rule input collection to rules engine...");
					rseItemId = dp2.SubmitAndWait(ruleInputCollection);

					// Cast the results as a result set evaluation item
					RuleSetEvaluation resultSet = (RuleSetEvaluation)itemContext.FindItemById(rseItemId);

					// Report on the results
					Console.WriteLine("\nResults:\n" + resultSet.RuleResultElements.Count.ToString(System.Globalization.CultureInfo.CurrentUICulture) + " action(s) to be performed ...");

					// Execute the actions
					Console.WriteLine("\nExecuting the actions ...");
					resultSet.Execute(itemContext);
					Console.WriteLine("The document comment is now " + doc.Comments);

					// Save stuff...
					itemContext.Update();
					Console.WriteLine("Done.");

					// Just used so console window doesn't go away
					Console.WriteLine("Press ENTER to exit...");
					Console.ReadLine();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("There is a problem with the sample and an exception was thrown...");
				Console.WriteLine("Exception info: {0}", ex.ToString());
				if (null != ex.InnerException)
				{
					Console.WriteLine("Inner exception:");
					Console.WriteLine(ex.InnerException.ToString());
				}
			}
		}
	}
}

