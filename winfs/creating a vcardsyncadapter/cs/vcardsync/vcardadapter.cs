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
using System.Storage;
using System.Storage.Contacts;
using System.Storage.Synchronization;
using System.Storage.Core;

/* Sync Adapter Flow
 * -----------------
 * Sync
 *	ItemContext.Open()
 *	ItemContext.ReplicaSynchronizer
 *		Receive()
 *		Send()
 *	ItemContext.Close()
 *
 * Receive
 *	GetRemoteKnowledge
 *	Do Receive
 *	ItemContext.Update()
 *	SetRemoteKnowledge
 *
 * Send
 *	GetRemoteKnowledge
 *	Do Send
 *	SetRemoteKnowledge
 */
namespace Microsoft.Samples.WinFS
{
	[Adapter("http://schemas.microsoft.com/winfs/2003/07/01/sync/adapters/vcard", 
		SupportsSend = true, SupportsReceive = true, SupportsSendOverwriteAll = false,
		SupportsReceiveOverwriteAll = false)]
	[EndpointFormatAttribute(typeof(string))]
	public class VCardAdapter : SynchronizationAdapter
	{
		//Typically you would NOT hardcode this, as you can have multiple communities for one syncadapter.
		public static readonly string DEFAULTCOMMUNITY = "VCardCommunity";
		//Typically you would NOT hardcode this, as you can have more than one remote partner
		private string partnerIdRemote = "FC07F50B-E5C5-4b67-875E-BB8ABD9E2D2D";

		public override void Synchronize()
		{
			using (ItemContext ctx = ItemContext.Open(Profile.LocalEndpoint.WinfsSharePath, true))
			{
				ctx.ReplicaSynchronizer = new ReplicaSynchronizer(Profile.LocalEndpoint.ReplicaId, new Guid(partnerIdRemote));
				ctx.ReplicaSynchronizer.ConflictPolicy = Profile.ConflictPolicy;

				if (Profile.SynchronizationType == SynchronizationType.Receive || Profile.SynchronizationType == SynchronizationType.SendAndReceive)
					Receive(ctx);

				if (Profile.SynchronizationType == SynchronizationType.Send || Profile.SynchronizationType == SynchronizationType.SendAndReceive)
					Send(ctx);
			}
		}

		public override void Cancel()
		{
			throw new NotSupportedException("The VCardSyncAdapter does not support cancellations.");
		}

		#region "Receive"
		private void Receive(ItemContext ctx)
		{
			ctx.ReplicaSynchronizer.RemoteKnowledge = GetRemoteKnowledge(ctx);

			Folder localFolder = (Folder)ctx.FindByPath(typeof(Folder), @"\");

			// load existing or create new db
			VCardDB db = new VCardDB(Profile.RemoteEndpoint.ToString());
			foreach(VCard vcf in db)	//For each card
			{
				if (vcf.IsChanged)	//If changed, update winfs
					UpdateLocalStore(db, vcf, ctx, localFolder);

				if (vcf.IsDeleted)	//If deleted, we need to remove the tombstone
					db.Remove(vcf);
			}

			//Check for NEW VCF files
			string[] files = System.IO.Directory.GetFiles(Profile.RemoteEndpoint.ToString(), "*.vcf");
			foreach (string file in files)
			{
				if(!db.Contains(System.IO.Path.GetFileNameWithoutExtension(file)))
				{
					UpdateLocalStore(db, new VCard(file), ctx, localFolder);
				}
			}

			db.Save();
			ctx.Update();

			SetRemoteKnowledge(ctx.ReplicaSynchronizer.RemoteKnowledge, ctx);
		}

		private void UpdateLocalStore(VCardDB db, VCard vcf, ItemContext ctx, Folder localFolder)
		{
			Person localItem = ctx.FindItemById(vcf.UserId) as Person;

			if (localItem != null)
			{
				if (vcf.IsDeleted)	//The VCard was deleted, remove the WinFS Item
				{
					ItemSearcher fmSearcher = FolderMember.GetSearcherGivenItem(localItem);

					fmSearcher.Filters.Add("SourceItemId = @itemId");
					fmSearcher.Parameters.Add("itemId", localFolder.ItemId);

					FolderMember deletedFm = fmSearcher.FindOne() as FolderMember;
					localFolder.OutFolderMemberRelationships.RemoveItem(localItem);
					localItem.SynchronizationProperties.IsSyncChange = true;
					deletedFm.SynchronizationProperties.IsSyncChange = true;
				}
				else
				{
					VCardToWinFS(vcf, localItem);	//Just an edit, refresh values
				}
			}
			else
			{
				//New contact, create and refresh values
				Person newPerson = new Person();
				VCardToWinFS(vcf, newPerson);

				FolderMember fm=localFolder.OutFolderMemberRelationships.AddItem(newPerson, newPerson.ItemId.ToString());
				fm.SynchronizationProperties.IsSyncChange = true;

				vcf.UserId = newPerson.ItemId;
				vcf.Rename(System.IO.Path.Combine(Profile.RemoteEndpoint.ToString(), vcf.UserId.ToString() + ".vcf"));
				vcf.Save();
				db.Add(vcf);
			}
		}

		private void VCardToWinFS(VCard updatedCard, Person targetPerson)
		{
			targetPerson.SynchronizationProperties.IsSyncChange = true;
			targetPerson.DisplayName = updatedCard.DisplayName;
			if (updatedCard.EMail.Length > 0)
			{
				if (targetPerson.PrimaryEmailAddress == null)
				{
					SmtpEmailAddress email = new SmtpEmailAddress();

					email.Keywords.Add(new Keyword(StandardKeywords.Primary));
					email.AccessPointType = new Keyword("email");
					targetPerson.EAddresses.Add(email);
				}

				targetPerson.PrimaryEmailAddress.Address = updatedCard.EMail;
				targetPerson.PrimaryEmailAddress.AccessPoint = targetPerson.PrimaryEmailAddress.Address;
			}
		}
		#endregion

		#region "Send methods"
		private void Send(ItemContext ctx)
		{
			ChangeReader reader = null;

			try
			{
				ctx.ReplicaSynchronizer.RemoteKnowledge = GetRemoteKnowledge(ctx);

				// get the change reader
				reader = ctx.ReplicaSynchronizer.GetChangeReader();

				while (reader.Read())
				{
					if (reader.CurrentChange is CompoundItemChange)
					{
						CompoundItemChange itemChange = (CompoundItemChange)reader.CurrentChange;

						// write Changes in the Remote Store
						ChangeStatusCollection status=UpdateRemoteStore(ctx, itemChange);
						reader.AcknowledgeChange(status);
					}
				}

				SetRemoteKnowledge(ctx.ReplicaSynchronizer.RemoteKnowledge, ctx);
			}
			finally
			{
				if(reader!=null)
					reader.Close();
			}
		}

		private ChangeStatusCollection UpdateRemoteStore(ItemContext ctx, CompoundItemChange currentChange)
		{
			VCardDB db = new VCardDB(Profile.RemoteEndpoint.ToString());
			ChangeStatusCollection status=currentChange.GetStatusTemplate(ChangeResult.Success);

			foreach (RelationshipChange relChange in currentChange.HoldingRelationshipChanges)
			{
				System.Diagnostics.Debug.WriteLine(relChange);
				if (relChange.IsDeleted == true)	//If this is a delete, remove the vcard info.
				{
					string filePath= System.IO.Path.Combine(Profile.RemoteEndpoint.ToString(), relChange.TargetItemId.ToString(System.Globalization.CultureInfo.CurrentUICulture) + ".vcf");
					if(System.IO.File.Exists(filePath))
					{
						System.IO.File.Delete(filePath);
						db.Remove(relChange.TargetItemId.ToString());
						db.Save();
					}
				}
			}

			if (currentChange.ItemChange != null)
			{
				Item item = currentChange.ItemChange.Item;

				if (item is Person)	//If this new item is a person, then create a new VCard (or load existing) and update
				{
					string filePath = System.IO.Path.Combine(Profile.RemoteEndpoint.ToString(), item.ItemId.ToString() + ".vcf");
					Person changedPerson = item as Person;
					VCard updatedVCard = db.GetVCard(changedPerson.ItemId);

					//Update
					WinFSToVCard(changedPerson, updatedVCard);
					db.Add(updatedVCard);
				}
			}

			db.Save();
			return status;
		}

		private void WinFSToVCard(Person winfsPerson, VCard vcardPerson)
		{
			vcardPerson.UserId = winfsPerson.ItemId;
			vcardPerson.DisplayName = winfsPerson.DisplayName.ToString(System.Globalization.CultureInfo.CurrentUICulture);
			if (winfsPerson.PrimaryEmailAddress != null)
				vcardPerson.EMail = winfsPerson.PrimaryEmailAddress.Address.ToString(System.Globalization.CultureInfo.CurrentUICulture);
			else
				vcardPerson.EMail = "";

			vcardPerson.Save();
		}
		#endregion

		#region "Knowledge"
		private ReplicaKnowledge GetRemoteKnowledge(ItemContext ctx)
		{
			ReplicaKnowledgeItem rki = ctx.FindItemById(ctx.ReplicaSynchronizer.RemotePartnerId) as ReplicaKnowledgeItem;

			if (rki != null)
				return rki.ReplicaKnowledge;
			else
			{
				ReplicaKnowledge rk = new ReplicaKnowledge();

				rk.Knowledge = new Knowledge();
				return rk;
			}
		}

		private void SetRemoteKnowledge(ReplicaKnowledge remoteKnowledge, ItemContext ctx)
		{
			ReplicaKnowledgeItem rki = ctx.FindItemById(ctx.ReplicaSynchronizer.RemotePartnerId) as ReplicaKnowledgeItem;

			if (rki == null)
			{
				Replica replica = (Replica)ctx.FindItemById(ctx.ReplicaSynchronizer.ReplicaItemId);

				rki = new ReplicaKnowledgeItem();
				rki.ItemId = ctx.ReplicaSynchronizer.RemotePartnerId;
				replica.OutgoingRelationships.Add(new ItemReplicaKnowledgeItem(rki));
			}

			rki.ReplicaKnowledge = remoteKnowledge;
			ctx.Update();
		}
		#endregion
	}
}
