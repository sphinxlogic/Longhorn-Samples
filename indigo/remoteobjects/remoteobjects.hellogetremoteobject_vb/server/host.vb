Imports System
Imports System.MessageBus
Imports System.MessageBus.Remoting
Namespace Microsoft.Samples.MessageBus.Quickstarts.RemoteObjects
    Public Class Host
        Public Shared Sub Main(ByVal args() As String)
            Dim name As String = "Client"
            If (args.Length > 0) Then
                name = args(0)
            End If
            ' The remoting configuration starts with the 
            ' System.MessageBus.ServiceEnvironment class.
            Dim se As ServiceEnvironment = ServiceEnvironment.Load()

            ' Retrieve the ServiceManager from the default environment
            Dim remManager As RemotingManager = CType(se(GetType(RemotingManager)), RemotingManager)
            If (remManager Is Nothing) Then
                Throw New ApplicationException("The service environment does not have a RemotingManager.")
            End If
            ' Start the service environment.
            se.Open()

            '  Register an instance of a RemotableType for 
            '  remote connection hosted in this service environment.
            Dim remotableInstance As New RemotableObject

            '  The client can create a proxy to this remoted instance using 
            '  RemotingManager.GetObject.
            Dim serverObject As PublishedServerObject = New PublishedServerObject(remotableInstance, New Uri("urn:CustomInstance"))
            remManager.PublishedServerObjects.Add(serverObject)

            Console.WriteLine("Listening for requests. Press Enter to exit...")
            Console.ReadLine()

            '  Rescind the publication of the instance.
            remManager.PublishedServerObjects.Remove(serverObject)

            '  Close the environment.
            se.Close()
        End Sub
    End Class

End Namespace