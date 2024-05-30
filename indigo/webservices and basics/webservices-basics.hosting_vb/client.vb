'  CustomTransportSample.cs
Imports System
Imports System.Globalization
Imports System.IO
Imports System.MessageBus
Imports System.MessageBus.Transports
Imports System.Xml
Namespace Microsoft.Samples.MessageBus.Quickstarts.Hosting
    ' Client class used for the hosting sample. The sample is a very simple
    ' service implemented using Indigo. The client is sending to the service 
    ' a string, the service is replying back to the client and the client is 
    ' finally priting the outcome to the screen.
    Public Class Client
        ' the port that the client will use to communicate with the service
        Dim port As Port
        ' a request reply manager that will be used to receive the server's reply
        Dim requestReplyManager As RequestReplyManager

        Public Sub New()
            Me.port = New Port
            Me.requestReplyManager = New RequestReplyManager(port)
            Try
                Me.port.Open()
            Catch Ex As PortIOException
                Console.WriteLine("Error: Could not open the port")
                Environment.Exit(-1)
            End Try
        End Sub


        ' This method implements all the logic.
        ' <param name="name">The string to pass to the server</param>
        ' <param name="serverUri">The server's Uri</param>
        Public Sub Run(ByVal name As String, ByVal serverUri As Uri)
            ' validate the arguments
            If (name Is Nothing) Then
                Throw New ArgumentNullException("name")
            End If

            If (serverUri Is Nothing) Then
                Throw New ArgumentNullException("serverUri")
            End If

            ' Create a message that has in its content the string that was passed by the user
            Dim message As Message = New Message(New Uri("http://www.tempuri.org/quickstarts/hosting/client"), name)

            ' Create a channel to send the request and receive the reply using the 
            ' RequestReply manager object
            Dim channel As SendRequestChannel = requestReplyManager.CreateSendRequestChannel(serverUri)

            ' Notify the user, send the request syncronously, receive the reply. notice that
            ' we do not need to close the message that we are sending because the channel
            ' is going to do that automatically for us.
            Console.WriteLine("Sending request-reply. Client says {0}", name)
            Dim reply As Message = channel.SendRequest(message)

            ' Open the content of the reply and read the string that the 
            ' client has sent, notify the user. Notice that we should close the 
            ' reply message becuase we extracted its content. In fact when we are 
            ' closing the message we are in fact closing the underlying stream that was
            ' used to read the content
            Dim replyMessage As String
            Try

                replyMessage = CType(reply.Content.GetObject(GetType(String)), String)

            Catch Ex As InvalidCastException
                Console.WriteLine("Error: The server did not reply back as expected")
                ' check to see if something bad has happened and try to 
                ' figure out what it was

                If (reply.Content.IsException) Then

                    ' get the exception content and print out all the info that you can
                    Dim content As ExceptionContent = CType(reply.Content, ExceptionContent)
                    If (content.IsException AndAlso Not (content.IsEmpty)) Then
                        Dim exception As Exception = CType(content.GetObject(GetType(Exception)), Exception)
                        Throw New ApplicationException("Server threw an exception: ", exception)
                    End If
                End If
            Finally
                reply.Close()
            End Try
            Console.WriteLine("Request-reply completed. Server said {0}", replyMessage)
        End Sub

        ' <summary>
        ' Closes the port before terminating the application
        ' </summary>
        Public Sub Close()
            If (Not (port Is Nothing) AndAlso (port.IsOpen)) Then
                port.Close()
                port = Nothing
            End If
        End Sub

        ' <summary>
        ' Prints out the help and outputs error messages if applicable
        ' </summary>
        ' <param name="err"></param>
        Public Shared Sub PrintHelp(ByVal err As String)

            If Not (err Is Nothing) Then
                Console.WriteLine("Error:  {0}", err)
                Console.WriteLine()
            End If
            Console.WriteLine("Usage: ")
            Console.WriteLine()
            Console.WriteLine("\tHostingClient.exe -server <serverUrl> <name>")
            Console.WriteLine()
            Console.WriteLine("Example:")
            Console.WriteLine()
            Console.WriteLine("\tHostingClient.exe -server http://localhost/mb/hello.msgx MicrosoftUser")
            Console.WriteLine()
        End Sub

        Public Shared Sub Main(ByVal args() As String)

            Dim client As New Client
            Dim serverUri As Uri

            Dim argIndex As Integer = 0
            Do While (argIndex < args.Length AndAlso (args(argIndex)(0) = "-" OrElse args(argIndex)(0) = "/"))

                Dim modifier As String = args(argIndex).Substring(1)

                Select Case (modifier)
                    Case "server"
                        argIndex = argIndex + 1
                        serverUri = New Uri(args(argIndex))
                    Case "?"
                        PrintHelp(Nothing)
                    Case Else
                        PrintHelp("unrecognized modifier: " + modifier)
                End Select
                argIndex = argIndex + 1
            Loop

            If (argIndex < args.Length) Then
                client.Run(args(argIndex), serverUri)
            End If

            client.Close()
        End Sub
    End Class
End Namespace 