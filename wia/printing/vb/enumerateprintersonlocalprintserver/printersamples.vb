Imports System
Imports System.Printing.Configuration
Imports System.Printing.PrintSubSystem
Imports Microsoft.Printing.DeviceCapabilities
Imports Microsoft.Printing.JobTicket
Imports System.Globalization

Module PrinterSamples

    Sub Main()
        Console.WriteLine("This sample enumerates local printers on the local server and " & _
                "prints some of the properties.")
        Console.ReadLine()
        EnumeratePrintQueuesOnLocalPrintServer()

    End Sub

    Sub EnumeratePrintQueuesOnLocalPrintServer()
        Try
            Console.WriteLine("Sample code for enumerating Print Queues on local Print Server " & ControlChars.CrLf)
            Dim localPrintServer As LocalPrintServer = New LocalPrintServer
            Dim printQueues As PrintQueueCollection = localPrintServer.GetPrintQueues()

            Dim enumerator As IEnumerator = printQueues.GetEnumerator()

            Dim printQueue As PrintQueue
            Dim name As String
            Dim port As Port
            Dim driver As Driver
            Dim queueLocation As String
            Dim queueComment As String
            Dim shareName As String

            Dim jt As JobTicket
            Dim devCap As DeviceCapabilities

            Dim option_enumerator As IEnumerator

            While enumerator.MoveNext()
                printQueue = CType(enumerator.Current, PrintQueue)
                name = printQueue.Name
                port = printQueue.QueuePort
                driver = printQueue.QueueDriver
                queueLocation = printQueue.Location
                queueComment = printQueue.Comment
                shareName = printQueue.ShareName
                '
                ' Print out the queried property values
                '
                Console.WriteLine(ControlChars.CrLf & "--- Printer '{0}' located at '{1}' ---", name, queueLocation)
                Console.WriteLine("Port:        {0}" & ControlChars.CrLf & _
                               "Driver:      {1}" & ControlChars.CrLf & _
                               "Comment:     {2}" & ControlChars.CrLf & _
                               "Shared as:   {3}" & ControlChars.CrLf, _
                               port.Name, _
                               driver.Name, _
                               queueComment, _
                               shareName)

                '
                ' Construct a JobTicket object using the printer's default job ticket.
                '

                jt = New JobTicket(printQueue.DefaultJobTicket)
                '
                ' Acquire the printer's device capabilities and construct a DeviceCapabilities object.
                '
                devCap = New DeviceCapabilities(printQueue.AcquireDeviceCapabilities(printQueue.DefaultJobTicket))
                '
                ' Check if the printer can print color.
                '
                Console.WriteLine(IIf(devCap.CanPrintColor, "Can print color documents.", _
                            "Cannot print color documents."))



                '
                ' Query for device capability of document duplexing.
                '
                Console.WriteLine("DocumentDuplex Capability:")
                If devCap.SupportsCapability(PrintSchema.Features.DocumentDuplex) Then
                    option_enumerator = devCap.DocumentDuplexCap.DuplexOptions.GetEnumerator()
                    While option_enumerator.MoveNext()
                        Dim duplex_option As DocumentDuplexCapability.DuplexOption = _
                            CType(option_enumerator.Current, DocumentDuplexCapability.DuplexOption)
                        Console.WriteLine("    {0} (constrained by: {1})", _
                            duplex_option.Value.ToString(), _
                            duplex_option.Constrained.ToString())
                    End While
                Else
                    Console.WriteLine("    not supported")
                End If

                ' Query for device capability of page resolution.
                Console.WriteLine("PageResolution Capability:")
                If devCap.SupportsCapability(PrintSchema.Features.PageResolution) Then
                    option_enumerator = devCap.PageResolutionCap.Resolutions.GetEnumerator()
                    While option_enumerator.MoveNext()
                        Dim option_resolution As PageResolutionCapability.Resolution = _
                            CType(option_enumerator.Current, PageResolutionCapability.Resolution)
                        Console.WriteLine("    {0}x{1} (constrained by: {2})", _
                            option_resolution.ResolutionX.ToString(CultureInfo.CurrentUICulture), _
                            option_resolution.ResolutionY.ToString(CultureInfo.CurrentUICulture), _
                            option_resolution.Constrained.ToString())
                    End While
                Else
                    Console.WriteLine("    not supported")
                End If


                ' Query for device capability of page canvas size.
                Console.WriteLine("PageCanvasSize Capability:")

                If devCap.SupportsCapability(PrintSchema.Features.PageCanvasSize) Then
                    ' Set the length unit type to Inch in order to get length values in inches.
                    devCap.LengthUnitType = PrintSchema.LengthUnitTypes.Inch

                    ' If the printer's device capability doesn't specify CanvasSizeX or CanvasSizeY,
                    ' then the value will be PrintSchema.UnspecifiedDecimalValue.
                    If devCap.PageCanvasSizeCap.CanvasSizeX <> PrintSchema.UnspecifiedDecimalValue Then
                        Console.WriteLine("    CanvasSizeX = {0}", _
                            devCap.PageCanvasSizeCap.CanvasSizeX.ToString(CultureInfo.CurrentUICulture))
                    Else
                        Console.WriteLine("    CanvasSizeX not specified")
                    End If

                    If devCap.PageCanvasSizeCap.CanvasSizeY <> PrintSchema.UnspecifiedDecimalValue Then
                        Console.WriteLine("    CanvasSizeY = {0}", _
                            devCap.PageCanvasSizeCap.CanvasSizeY.ToString(CultureInfo.CurrentUICulture))
                    Else
                        Console.WriteLine("    CanvasSizeY not specified")
                    End If
                Else
                    Console.WriteLine("    not supported")
                End If

                Console.WriteLine("Printer-default Job Ticket Settings:")
                Console.WriteLine("    PageMediaSize = {0}", _
                        jt.PageMediaSize.Value.ToString())
                Console.WriteLine("    PageOrientation = {0}", _
                        jt.PageOrientation.Value.ToString())
            End While

        Catch printException As PrintSystemException
            Console.WriteLine(printException.Message)
        End Try
    End Sub

End Module
