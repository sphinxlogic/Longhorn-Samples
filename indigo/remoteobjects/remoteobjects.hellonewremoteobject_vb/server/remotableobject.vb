Imports System
Namespace Microsoft.Samples.MessageBus.Quickstarts.RemoteObjects
    Public Class RemotableObject
        Inherits MarshalByRefObject

        Public Sub New()
            Console.WriteLine("Object {0} has been created.", Me.GetHashCode().ToString())
        End Sub
        Public Function Hello(ByVal name As String) As String
            Return String.Format("Hello, {0}. This is RemotableObject instance ""{1}""", name, Me.GetHashCode().ToString())
        End Function
        Protected Overrides Sub Finalize()
            Console.WriteLine("Object {0} is being torn down.", Me.GetHashCode().ToString())
        End Sub
    End Class
End Namespace