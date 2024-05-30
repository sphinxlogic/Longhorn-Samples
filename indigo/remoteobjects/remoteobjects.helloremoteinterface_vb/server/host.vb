Imports System
Imports System.MessageBus
Imports System.MessageBus.Remoting
Namespace Microsoft.Samples.MessageBus.Quickstarts.RemoteObjects
    Public Class Host
        Public Shared Sub Main(ByVal args() As String)
            ' The remoting configuration starts with the 
            ' System.MessageBus.ServiceEnvironment class.
            Dim se As ServiceEnvironment = ServiceEnvironment.Load()

            Dim remManager As RemotingManager = CType(se(GetType(RemotingManager)), RemotingManager)
            Dim remObject As IRemoteInterface = New RemotableObject
            Dim pso As PublishedServerObject = New PublishedServerObject(remObject, New Uri("urn:CustomInterface"))
            remManager.PublishedServerObjects.Add(pso)

            ' Start the service environment.
            se.Open()

            Console.WriteLine("Listening for requests. Press Enter to exit...")
            Console.ReadLine()

            ' Close the environment.
            se.Close()
        End Sub
    End Class
End Namespace