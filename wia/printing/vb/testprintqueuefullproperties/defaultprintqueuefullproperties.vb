Imports System
Imports System.Printing.Configuration
Imports System.Printing.PrintSubSystem
Imports Microsoft.Printing.DeviceCapabilities
Imports Microsoft.Printing.JobTicket

Module PrinterSamples

    Sub Main()
        Console.WriteLine("This sample shows how to get and set the properties of a print queue")
        LocalPrintQueueOperationsWithFullProperties()
    End Sub

    Sub LocalPrintQueueOperationsWithFullProperties()

        Dim localPrintQueue As PrintQueue

        Try
            Console.WriteLine("Sample code for Local Print Queue operations")
            Console.WriteLine("Enter the name of a local printer: ")
            Dim printQueueName As String = Console.ReadLine()

            '
            ' Instantiating an instance of the Local Print Queue with all possible
            ' properties populated. Changing some of the properties on the PrintQueue
            ' mendates instantiating with a PrinterFullAccess access. Also having this
            ' access would allow the sample to Pause / Resume Print Queues
            ' 
            localPrintQueue = New PrintQueue(New PrintServer, _
                                             printQueueName, _
                                             PrintSystemDesiredAccess.PrinterFullAccess)
            '
            ' Querying the name of the PrintQueue
            '
            Dim name As String = localPrintQueue.Name
            '
            ' Querying the Port, Driver and Print Processor of the Print Queue
            '
            Dim port As Port = localPrintQueue.QueuePort
            Dim driver As Driver = localPrintQueue.QueueDriver
            Dim printProcessor As PrintProcessor = localPrintQueue.QueuePrintProcessor
            '
            ' Querying the priority by which the jobs are scheduled on this print queue
            '
            Dim priority As Int32 = localPrintQueue.Priority
            '
            ' Querying the number of Jobs queued on the PrintQueue
            '
            Dim noJobs As Int32 = localPrintQueue.NumberOfJobs
            '
            ' Querying othe properties of the PrintQueue like Comment,Location,SpeFile and ShareName
            '
            Dim queueLocation As String = localPrintQueue.Location
            Dim queueComment As String = localPrintQueue.Comment
            Dim separatorFile As String = localPrintQueue.SepFile
            Dim share As String = localPrintQueue.ShareName
            Dim description As String = localPrintQueue.Description
            '
            ' Querying the driver settings of the PrintQueue for both PrintQueue and User
            '
            Dim defaultJobTicket As JobTicket = New JobTicket(localPrintQueue.DefaultJobTicket)
            Dim orientationValueD As PrintSchema.OrientationValues = defaultJobTicket.PageOrientation.Value
            Dim userJobTicket As JobTicket = New JobTicket(localPrintQueue.UserJobTicket)
            Dim orientationValueU As PrintSchema.OrientationValues = userJobTicket.PageOrientation.Value
            '
            ' Print out the queried values
            '
            Console.WriteLine("Local PrintQueue: {0} has the following properites:" & ControlChars.CrLf & _
                              "Port:           {1}" & ControlChars.CrLf & _
                              "Driver:         {2}" & ControlChars.CrLf & _
                              "PrintProcessor: {3}" & ControlChars.CrLf & _
                              "Location:       {4}" & ControlChars.CrLf & _
                              "Comment:        {5}" & ControlChars.CrLf & _
                              "Orientation:    {6}" & ControlChars.CrLf & _
                              "Description:    {7}" & ControlChars.CrLf & _
                              "Seperator_File: {8}" & ControlChars.CrLf & _
                              "Share_Name:     {9}" & ControlChars.CrLf & _
                              "Number Of Jobs  {10}" & ControlChars.CrLf & _
                              "Paused:         {11}" & ControlChars.CrLf, _
                              name, _
                              port.Name, driver.Name, printProcessor.Name, _
                              queueLocation, queueComment, _
                              orientationValueU.ToString(), _
                              description, separatorFile, share, _
                              noJobs, localPrintQueue.IsPaused.ToString())
            '
            ' Now lets try changing some of the Local Print Queue Properties 
            '
            Console.WriteLine("Enter a new name to set on the print queue: ")
            Dim newPrintQueueName As String = Console.ReadLine()
            Console.WriteLine("Enter a new port name to set on the print queue")
            Dim newPortName As String = Console.ReadLine()

            Dim newPort As Port = New Port(newPortName)
            localPrintQueue.QueuePort = newPort
            localPrintQueue.Comment = New String("SystemPrinting PrintQueue")
            localPrintQueue.ShareName = New String("SystemPrinting_Shared")
            localPrintQueue.Priority = 6
            localPrintQueue.Name = newPrintQueueName
            userJobTicket.PageOrientation.Value = PrintSchema.OrientationValues.Landscape
            localPrintQueue.UserJobTicket = userJobTicket.XmlStream
            defaultJobTicket.PageOrientation.Value = PrintSchema.OrientationValues.Landscape
            localPrintQueue.DefaultJobTicket = defaultJobTicket.XmlStream
            '
            ' Commit the changes to the PrintQueue
            '
            localPrintQueue.Commit()
            '
            ' If the PrintQueue is paused - resume else pause
            '
            If (localPrintQueue.IsPaused) Then
                Console.WriteLine("Resuming the Print Queue")
                localPrintQueue.Resume()
            Else
                Console.WriteLine("Pausing the Print Queue")
                localPrintQueue.Pause()
            End If

            '
            ' If required, the latest settings could be retrieved from the Print Server once
            ' more. This is a step to validate that what we commited is what is currently 
            ' reflected on the Print Queue
            '

            localPrintQueue.Refresh()
            name = localPrintQueue.Name
            port = localPrintQueue.QueuePort
            priority = localPrintQueue.Priority
            queueLocation = localPrintQueue.Location
            queueComment = localPrintQueue.Comment
            defaultJobTicket = New JobTicket(localPrintQueue.DefaultJobTicket)
            orientationValueD = defaultJobTicket.PageOrientation.Value
            userJobTicket = New JobTicket(localPrintQueue.UserJobTicket)
            orientationValueU = userJobTicket.PageOrientation.Value
            Console.WriteLine(ControlChars.CrLf & "Reading updated Print Queue information ...")
            Console.WriteLine("Local PrintQueue: {0} has the following properites:" & ControlChars.CrLf & _
                               "Port:           {1}" & ControlChars.CrLf & _
                               "Location:       {2}" & ControlChars.CrLf & _
                               "Comment:        {3}" & ControlChars.CrLf & _
                               "Orientation:    {4}" & ControlChars.CrLf & _
                               "Paused:         {5}" & ControlChars.CrLf, _
                               name, _
                               port.Name, _
                               queueLocation, queueComment, _
                               orientationValueU.ToString(), _
                               localPrintQueue.IsPaused.ToString())

            '
            ' Example of exceptions that can be thrown could be due to 
            ' access denied while trying to set the Print Queue properties.
            '
        Catch printQueueException As PrintQueueException
            Console.WriteLine("Error Message: {0}", printQueueException.Message)

        Catch commitException As PrintCommitAttributesException
            '
            ' Handle a PrintCommitAttributesException.
            '
            Console.WriteLine("Error Message: {0}", _
                          commitException.Message)
            Console.WriteLine("These attributes failed to commit...")

            Dim failedAttributesEnumerator As IEnumerator = commitException.FailToCommitAttributes.GetEnumerator()
            While failedAttributesEnumerator.MoveNext()
                Dim attributeName As String = failedAttributesEnumerator.Current.ToString()
                Console.Write("{0}  ", attributeName)
            End While
            Console.WriteLine()

            '
            ' To have the PrintQueue instance object in sync with the real Print Queue, 
            ' a refresh could be used. The instance properties that failed to commit will 
            ' be lost. The caller has the choice to not refresh the failed properties and
            ' try another Commit() later one.
            '
            localPrintQueue.Refresh()

        Catch printSystemException As PrintSystemException
            Console.WriteLine("Error Message: {0}", _
                           printSystemException.Message)

        Catch exception As Exception
            Console.WriteLine("Error Message {0}", _
                           exception.Message)
        End Try

        Console.ReadLine()

    End Sub

End Module
