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
Imports System.Storage.Core
Imports System.Storage.Contacts
Imports System.Runtime.InteropServices

<Assembly: ComVisible(False)> 
<Assembly: CLSCompliant(True)> 
Module SearchByType
    Private ReadOnly SAMPLEDATAFOLDERNAME As String = "WinFS Beta1 Sample Data Folder"
    Private context As ItemContext

    Sub Main()
        Try
            context = ItemContext.Open()
            If ValidateSampleDataExists() = False Then
                Console.WriteLine("This sample requires the sample data to be populated using the 'PopulateContacts' sample also shipped with the SDK.")
                Console.WriteLine("Please run this tool before executing this sample.")
                Return
            End If

            'Run the query to limit the results to a specific city
            FindContacts("Renton")
        Finally
            If Not (context Is Nothing) Then context.Close()
        End Try
    End Sub

    Private Sub FindContacts(ByVal city As String)
        Console.WriteLine("==============City: {0}===============", city)

        Dim personSearcher As ItemSearcher = Person.GetSearcher(context)

        personSearcher.Filters.Add("OutContactLocationsRelationships.Locations.LocationElements.Cast(System.Storage.Locations.Address).PrimaryCity = @city")
        personSearcher.Parameters.Add("city", city)

        Dim result As FindResult = personSearcher.FindAll(New SortOption("DisplayName", SortOrder.Descending))
        'For each contact returned, display its information
        Dim thePerson As Person
        For Each thePerson In result
            Console.WriteLine("Display Name : {0}", thePerson.DisplayName)
        Next
        Console.WriteLine()
    End Sub


    Private Function ValidateSampleDataExists() As Boolean
        Dim folderSearcher As ItemSearcher = Folder.GetSearcher(context)

        folderSearcher.Filters.Add("DisplayName = @displayName")
        folderSearcher.Parameters.Add("displayName", SAMPLEDATAFOLDERNAME)
        Dim sampleDataFolder As Folder = folderSearcher.FindOne(CType(Nothing, SortOption()))
        If sampleDataFolder Is Nothing Then
            Return False
        Else
            Return True
        End If
    End Function
End Module
