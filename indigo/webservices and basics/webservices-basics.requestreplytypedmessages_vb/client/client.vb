Imports System
Imports System.MessageBus
Imports System.MessageBus.Services
' the namespace of the imported service interface
Imports www_tempuri_org.quickstarts
Namespace Microsoft.Samples.MessageBus.Quickstarts
    Public Class Client
        Public Shared Sub Main(ByVal args() As String)

            Dim name As String = "Client"
            If (args.Length > 0) Then
                name = args(0)
            End If

            ' Load the default service environment, called "main".
            Dim se As ServiceEnvironment = ServiceEnvironment.Load()

            ' Retrieve the ServiceManager from the default environment
            Dim manager As ServiceManager = CType(se(GetType(ServiceManager)), ServiceManager)
            If (manager Is Nothing) Then
                Throw New ApplicationException("The ServiceManager is not available in the service se for some reason.")
            End If
            ' Start the service environment.
            se.Open()

            ' Create a proxy channel that points to the service to call.
            Dim serverUri As New Uri("soap.tcp://localhost:46000/HelloService/")
            Dim channel As IHelloChannel = CType(manager.CreateChannel(GetType(IHelloChannel), serverUri), IHelloChannel)
            Dim greeting As HelloSalutation = New HelloSalutation(name)
            Try
                Dim response As HelloResponse = channel.Greeting(greeting)
                Console.WriteLine(response.Response)
            Catch ex As Exception
                Console.WriteLine(Ex)
            Finally
                se.Close()
            End Try
        End Sub

    End Class
End Namespace