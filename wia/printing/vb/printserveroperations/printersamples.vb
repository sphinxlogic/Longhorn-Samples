Imports System.Printing.PrintSubSystem
Imports Microsoft.Printing.JobTicket

Module PrinterSamples

    Sub Main()
        Console.WriteLine("This sample gets\sets properties of PrintServer")
        Console.WriteLine("Enter PrintServerName: ")
        Dim printServerName As String = Console.ReadLine()
        PrintServerOperations(printServerName)
    End Sub

    Sub PrintServerOperations( _
        ByVal printServerName As String _
    )
        Dim printServer As PrintServer = Nothing
        Try
            Dim printServerProperties() As PrintServerProperty = {PrintServerProperty.BeepEnabled, _
                                                    PrintServerProperty.DefaultSpoolDirectory}
            '
            ' The object is created and only the two properties above are initialized.
            ' This is a way to optimize in the time required to load the object if only
            ' a limited number of properties are required from the object
            '
            printServer = New PrintServer( _
                    printServerName, _
                    printServerProperties)
            printServer.BeepEnabled = True
            printServer.PortThreadPriority = Threading.ThreadPriority.Highest

            '
            ' Read print server properties. These properties are already initialized.
            ' This operations doesn't require a call to the Print Spooler service.
            '
            Dim spoolDirectory As String = printServer.DefaultSpoolDirectory
            Dim isBeepEnabled As Boolean = printServer.BeepEnabled

            '
            ' Read a property that wasn't initialized when the object was constructed.
            ' This operation requires a call to to the Print Spooler service. The data 
            ' will be cached in the printServer object instance.
            '
            Dim portThreadPriority As System.Threading.ThreadPriority = printServer.PortThreadPriority

            '
            ' A subsequent call to read this property will use the cached data. 
            ' This data can change independently by this application. To synchronize 
            ' printServer properties with the Print Spooler service, the caller must call Refresh().
            '
            Console.WriteLine("Print Server {0} has the following properties:" & ControlChars.CrLf & _
                           "Spool Directory:     {1}" & ControlChars.CrLf & _
                           "Beep Enabled:        {2}" & ControlChars.CrLf & _
                           "Thread Priority:     {3}" & ControlChars.CrLf, _
                           printServer.Name, _
                           spoolDirectory, _
                           isBeepEnabled.ToString(), _
                           portThreadPriority.ToString())

        Catch serverException As PrintServerException
            '
            ' Handle a PrintServerException.
            '
            Console.Write("Server name is {0}. {1} " & ControlChars.CrLf, _
                       serverException.ServerName, _
                       serverException.Message)

        End Try

        Console.ReadLine()

    End Sub



End Module
