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
Imports System.Collections
Imports System.Globalization
Imports System.Xml
Imports System.Xml.Serialization

' The following classes represent the RSS XML document.
' XmlSerializer parses the RSS document and creates
' an RssFeed object, creating the channel and items
' contained in the document. See the documentation
' for XmlSerializer for more information.

' RssFeed represents the rss element in an RSS XML document
Namespace Microsoft.Samples.Communications
    <XmlType(TypeName:="rss")> _
    Public Class RssFeed

        ' The RSS feed contains a channel
        <XmlElement(ElementName:="channel")> _
        Public Property Channel() As RssChannel
            Get
                Return channel_
            End Get
            Set(ByVal Value As RssChannel)
                channel_ = Value
            End Set
        End Property

        ' Loads the given RSS document and returns an RssFeed
        Public Shared Function Load(ByVal path As String) As RssFeed
            Dim feed As RssFeed = Nothing
            Dim reader As XmlTextReader = Nothing

            Try
                Dim serializer As XmlSerializer = New XmlSerializer(GetType(RssFeed))

                reader = New XmlTextReader(path)
                feed = CType(serializer.Deserialize(reader), RssFeed)
            Finally
                If Not reader Is Nothing Then
                    reader.Close()
                End If
            End Try
            Return feed
        End Function

        Private channel_ As RssChannel

    End Class

    ' RssChannel represents the channel element in an RSS XML document
    Public Class RssChannel

        ' The RSS channel consists of several items
        <XmlElement(ElementName:="item", Type:=GetType(RssItem))> _
        Public ReadOnly Property Items() As ArrayList
            Get
                Return items_
            End Get
        End Property

        Private items_ As ArrayList = New ArrayList()

    End Class

    ' RssItem represents the item element in an RSS XML document
    Public Class RssItem

        ' These public fields match the RSS XML document
        <XmlElement("pubDate")> _
        Public Property PubDate() As String
            Get
                Return pubDate_
            End Get
            Set(ByVal Value As String)
                pubDate_ = Value
            End Set
        End Property

        <XmlElement("author")> _
        Public Property Author() As String
            Get
                Return author_
            End Get
            Set(ByVal Value As String)
                author_ = Value
            End Set
        End Property

        <XmlElement("title")> _
        Public Property Title() As String
            Get
                Return title_
            End Get
            Set(ByVal Value As String)
                title_ = Value
            End Set
        End Property

        <XmlElement("description")> _
        Public Property Description() As String
            Get
                Return description_
            End Get
            Set(ByVal Value As String)
                description_ = Value
            End Set
        End Property

        <XmlElement("link")> _
        Public Property Link() As String
            Get
                Return link_
            End Get
            Set(ByVal Value As String)
                link_ = Value
            End Set
        End Property

        ' Simple method which parses the date string into a DateTime object
        Public Function ParseDate() As DateTime
            ' Parse the RFC1123 date
            Return DateTime.ParseExact(pubDate_.Trim(), "R", New CultureInfo("en-US"))
        End Function

        ' Returns the title if one is set, otherwise return "No Title"
        Public ReadOnly Property SafeTitle() As String
            Get
                Dim returnTitle As String
                If Not title_ Is Nothing Then
                    returnTitle = title_
                Else
                    returnTitle = "No Title"
                End If
                Return returnTitle
            End Get
        End Property

        Private pubDate_ As String = Nothing

        Private author_ As String = Nothing

        Private title_ As String = Nothing

        Private description_ As String = Nothing

        Private link_ As String = Nothing

    End Class
End Namespace
