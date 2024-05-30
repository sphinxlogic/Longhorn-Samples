Imports System
Imports System.Collections
Imports System.Printing
Imports System.Printing.PrintSubSystem
Imports System.Printing.Configuration
Imports Microsoft.Printing.DeviceCapabilities
Imports Microsoft.Printing.JobTicket


Module PrinterSamples

    Sub Main()
        Console.WriteLine("This sample shows how to read the default printer properties.")
        Console.ReadLine()
        DefaultPrintQueueOperations()
    End Sub

    Sub DefaultPrintQueueOperations()
        Try
            Console.WriteLine("Sample code for Default Print Queue operations")

            Dim lclPrintServer As LocalPrintServer = New LocalPrintServer
            Dim defaultPrintQueue As PrintQueue = lclPrintServer.DefaultPrintQueue
            Dim hostingPrintServerName As String = defaultPrintQueue.HostingPrintServer.ToString()
            Dim printQueueFullName As String = defaultPrintQueue.FullName
            Dim name As String = defaultPrintQueue.Name
            Dim port As Port = defaultPrintQueue.QueuePort
            Dim driver As Driver = defaultPrintQueue.QueueDriver
            Dim printProcessor As PrintProcessor = defaultPrintQueue.QueuePrintProcessor
            Dim priority As Int32 = defaultPrintQueue.Priority
            '
            ' Querying the number of Jobs queued on the PrintQueue
            '
            Dim noJobs As Int32 = defaultPrintQueue.NumberOfJobs

            '
            ' Querying othe properties of the PrintQueue like Comment,Location,SepFile and ShareName
            '
            Dim queueLocation As String = defaultPrintQueue.Location
            Dim queueComment As String = defaultPrintQueue.Comment
            Dim separatorFile As String = defaultPrintQueue.SepFile
            Dim description As String = defaultPrintQueue.Description
            Dim share As String = defaultPrintQueue.ShareName


            '
            ' Querying the driver settings of the PrintQueue for both Printer and User
            '
            Dim defaultJobTicket As JobTicket = New JobTicket(defaultPrintQueue.DefaultJobTicket)
            Dim orientationValueD As PrintSchema.OrientationValues = defaultJobTicket.PageOrientation.Value
            Dim userJObTicket As JobTicket = New JobTicket(defaultPrintQueue.UserJobTicket)
            Dim OrientationValueU As PrintSchema.OrientationValues = userJObTicket.PageOrientation.Value

            '
            ' Print out the queried values
            '

            Console.WriteLine("Default PrintQueue: {0} on Print Server: {1}" & _
                    "has the following properties:" & ControlChars.CrLf & _
                    "Port:           {2}" & ControlChars.CrLf & "Driver:         {3}" & _
                    ControlChars.CrLf & "PrintProcessor: {4}" & ControlChars.CrLf & _
                    "Location:       {5}" & ControlChars.CrLf & "Comment:        {6}" & _
                    ControlChars.CrLf & "Orientation:    {7}" & ControlChars.CrLf & _
                    "Description:    {8}" & ControlChars.CrLf & "Seperator_File: {9}" & _
                    ControlChars.CrLf & "Share_Name:     {10}" & ControlChars.CrLf & _
                    "Number Of Jobs:  {11}" & ControlChars.CrLf & "Paused:         {12}" & _
                    ControlChars.CrLf, name, hostingPrintServerName, port.Name, driver.Name, _
                    printProcessor.Name, queueLocation, queueComment, _
                    OrientationValueU.ToString(), description, separatorFile, _
                    share, noJobs, defaultPrintQueue.IsPaused.ToString())

            '
            ' If required, the latest settings could be retrieved from the Print Server once
            ' more
            '
            defaultPrintQueue.Refresh()

            '
            ' Example of exceptions that can be thrown could be due to 
            ' access denied on trying to pause the Print Queue if you are
            ' not an administrator on that queue
            '


        Catch printSystemException As PrintQueueException
            Console.WriteLine("Error Message {0}", printSystemException.Message)


        Catch printSystemException As PrintSystemException
            Console.WriteLine("Error Message {0}", printSystemException.Message)
        End Try

        Console.ReadLine()

    End Sub
End Module
