Imports System
Imports System.MessageBus
Imports System.MessageBus.Services
Namespace Microsoft.Samples.MessageBus.Quickstarts
<DialogPortType(Name:="Hello", Namespace:="http://www.tempuri.org/quickstarts")> _
            Public Class Hello
    ' ServiceMethod attribute exposes methods for access on the service
    <ServiceMethodAttribute()> _
    Public Function Greeting(ByVal senderGreeting As HelloSalutation) As HelloResponse
        Console.WriteLine("Called by a client with the name {0}", senderGreeting.Name)
        Dim response As HelloResponse = New HelloResponse(senderGreeting.Name)
        response.Response = String.Format("Hello, {0}!", senderGreeting.Name)
        Return response
    End Function
    Protected Overrides Sub Finalize()
        Console.WriteLine("Hello service instance {0} is being recycled.", Me.GetHashCode())
    End Sub
End Class

' The HelloSalutation class is a custom class inheriting from the TypedMessage class
' to allow the developer to create custom classes that can be used for input and output
' parameters to methods of the service.  This is the default way to send custom types
' over the Service Framework.
<MessageAttribute(Namespace:="http://www.tempuri.org/quickstarts")> _
Public Class HelloSalutation
    Inherits TypedMessage
    Private internalSalutation As String

    ' Required
    Public Sub New()
        MyBase.New()
    End Sub
    Public Sub New(ByVal name As String)
        Me.internalSalutation = name
    End Sub

    <MessageBodyAttribute()> _
    Public Property Name() As String
        Get
            Return Me.internalSalutation
        End Get
        Set(ByVal Value As String)
            Me.internalSalutation = value
        End Set
    End Property
End Class

' The HelloResponse class is a custom class inheriting from the TypedMessage class
' to allow the developer to create custom classes that can be used for input and output
' parameters to methods of the service.  This is the default way to send custom types
' over the Service Framework.
<MessageAttribute(Namespace:="http://www.tempuri.org/quickstarts")> _
Public Class HelloResponse
    Inherits TypedMessage
    Private internalResponse As String
    Public Sub New()
        MyBase.New()
    End Sub
    Public Sub New(ByVal response As String)
        Me.internalResponse = response
    End Sub

    <MessageBodyAttribute()> _
    Public Property Response() As String
        Get
            Return Me.internalResponse
        End Get
        Set(ByVal Value As String)
            Me.internalResponse = value
        End Set
    End Property
End Class
End Namespace