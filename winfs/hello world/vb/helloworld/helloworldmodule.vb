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
Imports System.Storage
Imports System.Storage.Contacts
Imports System.Storage.Core
Imports System.Runtime.InteropServices

<Assembly: ComVisible(False)> 
Module HelloWorld
    Private itemContext As ItemContext

    Sub Main()
	Try
	        itemContext = itemContext.Open()

	        Console.WriteLine("Contacts BEFORE addition:")
        	ListContacts()
	        Console.WriteLine("Press enter to continue!")
        	Console.ReadLine()

	        AddContact("Shane DeSeranno")
	        Console.WriteLine("")
        	Console.WriteLine("Contacts AFTER addition:")
	        ListContacts()
        	Console.WriteLine("Press enter to continue!")
	        Console.ReadLine()

	        RenameContact("Shane DeSeranno", "Dylan Miller")
        	Console.WriteLine("")
	        Console.WriteLine("Contacts AFTER modification:")
        	ListContacts()
	        Console.WriteLine("Press enter to continue!")
        	Console.ReadLine()

	        DeleteContact("Dylan Miller")
        	Console.WriteLine("Contacts AFTER deletion:")
	        ListContacts()
	Finally
		If Not(itemContext Is Nothing) Then itemContext.Close()
	End Try

    End Sub

    Private Sub DeleteContact(ByVal displayName As String)
        Dim personalContactsFolder As WellKnownFolder = UserDataFolder.CreateMyPersonalContactsFolder(itemContext)

        Dim personSearcher As ItemSearcher = Person.GetSearcher(itemContext)
        personSearcher.Filters.Add("DisplayName = @DisplayName")
        personSearcher.Parameters.Add("DisplayName", displayName)

        Dim deletedPerson As Person = personSearcher.FindOne(CType(Nothing, SortOption()))
        If deletedPerson Is Nothing Then
            Console.WriteLine("Error: There were problems finding the requested contact: " & displayName)
        Else
            personalContactsFolder.OutgoingRelationships.RemoveTarget(deletedPerson)
        End If

        itemContext.Update()
    End Sub

    Private Sub RenameContact(ByVal fromName As String, ByVal toName As String)
        Dim personalContactsFolder As WellKnownFolder = UserDataFolder.CreateMyPersonalContactsFolder(itemContext)
        Dim personSearcher As ItemSearcher = Person.GetSearcher(itemContext)

        personSearcher.Filters.Add("DisplayName = @DisplayName")
        personSearcher.Parameters.Add("DisplayName", fromName)

        Dim modifiedPerson As Person = personSearcher.FindOne(CType(Nothing, SortOption()))
        If modifiedPerson Is Nothing Then
            Console.WriteLine("Error: There were problems finding the requested contact: " & fromName)
        Else
            modifiedPerson.DisplayName = System.Nullable(Of String).FromObject(toName)
        End If

        itemContext.Update()
    End Sub

    Private Sub AddContact(ByVal name As String)
        Dim newPerson As Person = New Person

        newPerson.DisplayName = System.Nullable(Of String).FromObject(name)

        Dim email As SmtpEmailAddress = New SmtpEmailAddress("somecontact@litware.com")

        email.Keywords.Add(New Keyword(StandardKeywords.Primary))
        email.AccessPointType = New Keyword("email")
        email.AccessPoint = email.Address
        newPerson.EAddresses.Add(email)

        Dim phone As TelephoneNumber = New TelephoneNumber("1", "425", "555-1234", "")

        phone.Keywords.Add(New Keyword(StandardKeywords.Home))
        phone.AccessPointType = New Keyword(StandardKeywords.Home)
        phone.AccessPoint = phone.Number.ToString(System.Globalization.CultureInfo.CurrentUICulture)
        newPerson.EAddresses.Add(phone)

        Dim personalContactsFolder As WellKnownFolder = UserDataFolder.FindMyPersonalContactsFolder(itemContext)

        personalContactsFolder.OutFolderMemberRelationships.AddItem(newPerson, newPerson.ItemId.ToString())
        itemContext.Update()
    End Sub

    Private Sub ListContacts()
        System.Console.WriteLine("Displaying contacts...")
        Dim fr As FindResult = itemContext.FindAll(GetType(Person))

        Dim foundContacts As Boolean = False
        Dim p As Person
        For Each p In fr
            foundContacts = True
            Console.WriteLine("  " & p.DisplayName.ToString(System.Globalization.CultureInfo.CurrentUICulture))
        Next
        If foundContacts = False Then
            Console.WriteLine("  No contacts found.")
        End If
    End Sub

End Module