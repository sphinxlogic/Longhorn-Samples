Imports System
Imports System.MessageBus
Imports System.MessageBus.Services
Namespace Microsoft.Samples.MessageBus.Quickstarts
    'DatagramPortType attribute indicates a sessionless service named Hello
    <DatagramPortType(Name:="Hello", Namespace:="http://www.tempuri.org/quickstarts")> _
    Public Class Hello
        ' ServiceMethod attribute exposes methods for access on the service
        <ServiceMethodAttribute()> _
        Public Function Greeting(ByVal senderGreeting As HelloSalutation) As HelloResponse
            Console.WriteLine("Called by a client with the name {0}", senderGreeting.Name)
            Dim response As HelloResponse = New HelloResponse(senderGreeting.Name)
            response.Response = String.Format("Hello, {0}!", senderGreeting.Name)
            Return response
        End Function
    End Class

    ' The HelloSalutation class is a custom class inheriting from the TypedMessage class
    ' to allow the developer to create custom classes that can be used for input and output
    ' parameters to methods of the service.  This is the default way to send custom types
    ' over the Service Framework.
    <MessageAttribute(Namespace := "http://www.tempuri.org/quickstarts")> _ 
    public class HelloSalutation 
        Inherits TypedMessage
	    private  internalSalutation as string

        Public Sub New
            MyBase.New()
        End Sub
	    public sub New (name as string)
		    Me.internalSalutation = name
	    end sub

	    <MessageBodyAttribute> _ 
	    public property Name as string
        Get
            return Me.internalSalutation
        End Get
        Set
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