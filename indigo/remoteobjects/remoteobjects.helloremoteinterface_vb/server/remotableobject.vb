Imports System
Namespace Microsoft.Samples.MessageBus.Quickstarts.RemoteObjects
    Public Class RemotableObject
        Implements IRemoteInterface

        Private clientCount As Integer = 0

        Public Sub New()
            Console.WriteLine("Object {0} has been created.", Me.GetHashCode().ToString())
        End Sub
        Public Function Hello(ByVal name As String) As String Implements IRemoteInterface.Hello
            clientCount = clientCount + 1
            Console.WriteLine("{0} has said Hello. This instance has been called {1} times.", name, clientCount.ToString())
            Return String.Format("Hello, {0}. This is RemotableObject instance ""{1}""", name, Me.GetHashCode().ToString())
        End Function
        Protected Overrides Sub Finalize()
            Console.WriteLine("Object {0} is being torn down.", Me.GetHashCode().ToString())
        End Sub
    End Class
End Namespace