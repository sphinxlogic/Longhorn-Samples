Imports System
Imports System.MessageBus
Imports System.MessageBus.Services
Namespace Microsoft.Samples.MessageBus.Quickstarts
    Public Class Host
        Public Shared Sub Main(ByVal args() As String)
		    'The service environment needs to be loaded before it can be used.
		    'The Load method loads whatever configuration is available in the configuration file.
            Dim se As ServiceEnvironment = ServiceEnvironment.Load()
		    'Only once the envrionment is open can connections be established.
            se.Open()
            Console.WriteLine("Press enter to stop the services...")
            Console.ReadLine()
		    'Closing the environment forces cleanup of server and should not be forgotten.
            se.Close()
        End Sub
    End Class
End Namespace