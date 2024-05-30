Imports System
Imports System.MessageBus
Imports System.MessageBus.Remoting
Namespace Microsoft.Samples.MessageBus.Quickstarts.RemoteObjects
    Public Class Client
        Public Shared Sub Main(ByVal args() As String)

            Dim name As String = "Test Client"
            If (args.Length > 0 AndAlso Not (args(0).Equals(String.Empty))) Then
                name = args(0)
            End If
            ' The remoting configuration starts with the 
            ' System.MessageBus.ServiceEnvironment class
            Dim se As ServiceEnvironment = ServiceEnvironment.Load()

            ' Acquire the RemotingManager to do remote work.
            Dim remManager As RemotingManager = CType(se(GetType(RemotingManager)), RemotingManager)
            If (remManager Is Nothing) Then
                Console.WriteLine("The configuration is incorrect.")
                Return
            End If

            ' Start the ServiceEnvironment. 
            se.Open()

            ' create a new remote object and obtain a proxy to it
            Dim newRemotableObject As RemotableObject = _
                CType(remManager.CreateInstance(GetType(RemotableObject), Nothing), RemotableObject)

            Console.WriteLine(newRemotableObject.Hello(name))
            Console.WriteLine("Press Enter to exit...")
            Console.ReadLine()
            ' close the service 
            se.Close()
        End Sub
    End Class
End Namespace