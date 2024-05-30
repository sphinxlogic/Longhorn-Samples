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
Imports System.Globalization
Imports System.Text

'
' This file uses the following WinFS classes from the System.Storage namespace
'
Imports BinaryData = System.Storage.Messages.BinaryData
Imports Contact = System.Storage.Core.Contact
Imports Folder = System.Storage.Folder
Imports ItemContext = System.Storage.ItemContext
Imports ItemSearcher = System.Storage.ItemSearcher
Imports Message = System.Storage.Messages.Message
Imports MessageParticipants = System.Storage.Messages.MessageParticipants
Imports Participant = System.Storage.Messages.Participant
Imports SmtpEmailAddress = System.Storage.Contacts.SmtpEmailAddress
Imports UserDataFolder = System.Storage.Contacts.UserDataFolder

' RSSImporter downloads the RSS feed as an XmlDocument and
' creates messages which are visible in Communication History.
Namespace Microsoft.Samples.Communications
    Module RssImporter

        ' Imports the RSS feed into the Communication History.
        Sub Import(ByVal rssUrl As String)

            Console.Out.WriteLine("Downloading RSS feed from '{0}'", rssUrl)
            Dim context As ItemContext = Nothing
            Try
                Dim feed As RssFeed = RssFeed.Load(rssUrl)

                ' Parse the RSS feed. We do not attempt to parse every variation on RSS, since this
                ' is not an RSS parsing sample. This works for the MSDN feed and many other RSS 2.0
                ' feeds, but not all.  In particular, this example will not store messages that are
                ' missing both a date and a title.
                ' Open the ItemContext.
                context = ItemContext.Open()

                ' Get the folder to store the messages in
                Dim userFolder As Folder = UserDataFolder.FindMyUserDataFolder(context)

                ' For each <item> element, create the item in WinFS
                Dim item As RssItem
                For Each item In feed.Channel.Items
                    Dim title As String
                    title = item.SafeTitle
                    Console.Out.WriteLine("Creating message: {0}", title)
                    CreateMessageInStore(context, userFolder, item)
                Next

                context.Update()

            Catch e As System.InvalidOperationException
                Console.WriteLine("{0}An error occured while trying to download the RSS feed.{0}{1}", System.Environment.NewLine, e.Message)
            Finally
                If Not context Is Nothing Then
                    context.Close()
                End If
            End Try
        End Sub

        ' Creates the message in the given Folder using the given ItemContext.
        Sub CreateMessageInStore(ByVal context As ItemContext, ByVal userFolder As Folder, ByVal item As RssItem)
            ' Create the new Message item in WinFS
            Dim newMessage As Message = New Message()
            Dim name As String = newMessage.ItemId.ToString()

            ' Set the DisplayName Subject and TimeReceived
            newMessage.DisplayName = Nullable(Of String).FromObject(item.SafeTitle)
            newMessage.Subject = Nullable(Of String).FromObject(item.SafeTitle)
            If Not item.PubDate Is Nothing Then
                ' Parse the RFC1123 date
                newMessage.TimeReceived = Nullable(Of Date).FromObject(item.ParseDate())
            End If

            ' The following fields are constant as far as this example is concerned
            newMessage.Sensitivity = ""
            newMessage.Priority = 3
            newMessage.IsRead = False
            newMessage.IsSigned = Nullable(Of Boolean).FromObject(False)
            newMessage.IsEncrypted = Nullable(Of Boolean).FromObject(False)

            If MessageExistsInStore(context, item) = False Then

                ' Add message to folder
                userFolder.OutFolderMemberRelationships.AddItem(newMessage, name)

                ' Add the contents to the Message and to the Folder
                If Not item.Description Is Nothing Then
                    Dim data As BinaryData = AddContentsToMessage(newMessage, item)

                    userFolder.OutFolderMemberRelationships.AddItem(data, name + ".Contents")
                End If

                ' Add the author if one is specified
                If Not item.Author Is Nothing Then
                    AddAuthorToMessage(context, newMessage, item.Author)
                End If
            End If
        End Sub

        ' Queries the WinFS store to see if the message already exists.
        ' Currently only checks the title and the date of messages.
        Function MessageExistsInStore(ByVal context As ItemContext, ByVal item As RssItem) As Boolean
            Dim searcher As ItemSearcher = Message.GetSearcher(context)
            Dim query As String
            Dim result As Boolean = True

            ' Both the title and date are optional on the rss feed.
            If (Not item.Title Is Nothing) And (Not item.PubDate Is Nothing) Then
                query = "Subject = @subject && TimeReceived = @timeReceived"
                searcher.Filters.Add(query)

                ' Since the subject might contain characters that can confuse
                ' the query, the subject and timeReceived are added to the query
                ' as parameters of the query.
                searcher.Parameters.Add("subject", item.Title)
                searcher.Parameters.Add("timeReceived", item.ParseDate())

                Dim duplicateMessage As Message = CType(searcher.FindOne(), Message)

                result = Not duplicateMessage Is Nothing
            ElseIf Not item.Title Is Nothing Then
                query = "Subject = @subject"
                searcher.Filters.Add(query)
                searcher.Parameters.Add("subject", item.Title)

                Dim duplicateMessage As Message = CType(searcher.FindOne(), Message)

                result = Not duplicateMessage Is Nothing
            ElseIf Not item.PubDate Is Nothing Then
                query = "TimeReceived = @timeReceived"
                searcher.Filters.Add(query)
                searcher.Parameters.Add("timeReceived", item.ParseDate())

                Dim duplicateMessage As Message = CType(searcher.FindOne(), Message)

                result = Not duplicateMessage Is Nothing
            Else
                ' Message has neither a title nor a date, just say that the message exists
                ' This will cause the message to not be added if it's missing both a date
                ' and a title.
                result = True
            End If

            Return result
        End Function

        ' Creates a BinaryData object, associates it with the message, and returns it
        Function AddContentsToMessage(ByVal newMessage As Message, ByVal item As RssItem) As BinaryData
            ' Create the BinaryData object and a byte array to hold
            ' the contents.
            Dim data As BinaryData = New BinaryData()
            Dim encoder As New UnicodeEncoding()
            Dim byteArray As Byte() = encoder.GetBytes(item.Description)

            data.Data = Nullable(Of Byte()).FromObject(byteArray)

            ' Associate the contents with the message
            newMessage.OutMessageContentsRelationships.AddContents(data, Storage.RelationshipMode.Reference)

            Return data
        End Function

        ' Adds the given address as a participant of the message.  This method
        ' searhes Contacts to find a matching address.  If a match is found,
        ' the message is linked to the contact using a reference.
        Sub AddAuthorToMessage(ByVal context As ItemContext, ByVal newMessage As Message, ByVal address As String)
            Dim part As Participant = New Participant()

            part.Usage = Nullable(Of String).FromObject("From")

            Dim messPart As MessageParticipants = New MessageParticipants()

            messPart.Data = part

            ' Check for an existing contact
            Dim existingContact As Contact = FindContactByAddress(context, address)

            If Not existingContact Is Nothing Then
                ' if we found one, wrap our Participant around it
                messPart.TargetItemId = Nullable(Of System.Guid).FromObject(existingContact.ItemId)
                part.Address.Add(FindSmtpEmailAddress(existingContact, address))
            Else
                ' otherwise we have a participant for whom there is no contact
                ' create a "dangling" Participant for him
                part.Address.Add(CreateSmtpEmailAddress(address))
            End If

            newMessage.OutMessageParticipantsRelationships.Add(messPart)
        End Sub

        ' Searches the store for an existing contact.
        Function FindContactByAddress(ByVal context As ItemContext, ByVal address As String) As Contact
            Dim query As String = String.Format(CultureInfo.InvariantCulture, "EAddresses.Cast({0}).AccessPoint = @SmtpAddress", GetType(SmtpEmailAddress).FullName)
            Dim searcher As ItemSearcher = Contact.GetSearcher(context)

            searcher.Filters.Add(query)
            ' The address is added as a parameter to the query rather than concatenated into the query.
            ' This prevents special characters in the address from confusing the query.
            searcher.Parameters.Add("SmtpAddress", address)
            Return (CType(searcher.FindOne(), Contact))
        End Function

        ' Finds the SmtpEmailAddress matching the given address for a contact.
        Function FindSmtpEmailAddress(ByVal contact As Contact, ByVal address As String) As SmtpEmailAddress

            Dim newAddress As SmtpEmailAddress = Nothing
            Dim contactAddress As SmtpEmailAddress
            For Each contactAddress In contact.EAddresses.FilterByType(Of SmtpEmailAddress)()
                If (contactAddress.Address = address) Then
                    newAddress = CType(contactAddress.DeepCopy(), SmtpEmailAddress)

                    newAddress.DisplayName = contact.DisplayName
                    Exit For
                End If
            Next

            Return newAddress
        End Function

        ' Creates a new SmtpEmailAddress for the given address.
        Function CreateSmtpEmailAddress(ByVal address As String) As SmtpEmailAddress
            Dim newEmailAddress As SmtpEmailAddress

            newEmailAddress = New System.Storage.Contacts.SmtpEmailAddress()
            newEmailAddress.Address = address
            newEmailAddress.DisplayName = Nullable(Of String).FromObject(address)
            newEmailAddress.AccessPoint = address
            newEmailAddress.AccessPointType = New System.Storage.Core.Keyword("SMTP")
            Return newEmailAddress
        End Function
    End Module
End Namespace
