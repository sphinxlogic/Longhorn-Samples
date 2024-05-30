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
            Dim serverPortUri As Uri = New Uri("soap.tcp://localhost:46460/RemotableObject")

            ' obtain a proxy to a currently running object. 
            ' The first parameter to GetObject represents the 
            ' server's Port Uri. The second parameter is the 
            ' name of the object you want to connect to.
            Dim remotedInstance As IRemoteInterface = _
                CType(remManager.GetObject( _
                     serverPortUri, _
                    New Uri("urn:CustomInterface")), IRemoteInterface)


            If Not (remotedInstance Is Nothing) Then
                Console.WriteLine(remotedInstance.Hello(name))
            Else
                Console.WriteLine("The remote object is not available.")
            End If

            Console.WriteLine("Press Enter to exit...")
            Console.ReadLine()
            ' close the service 
            se.Close()
        End Sub
    End Class
End Namespace