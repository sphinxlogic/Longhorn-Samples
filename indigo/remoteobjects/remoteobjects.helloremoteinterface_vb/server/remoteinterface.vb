Imports System
Imports System.MessageBus.Remoting
Namespace Microsoft.Samples.MessageBus.Quickstarts.RemoteObjects
    <RemotableAttribute()> _
    Public Interface IRemoteInterface
        Function Hello(ByVal name As String) As String
    End Interface
End Namespace
