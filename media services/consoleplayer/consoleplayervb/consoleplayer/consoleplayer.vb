' '---------------------------------------------------------------------
' 
'  Copyright (C) Microsoft Corporation.  All rights reserved.
' 
' This source code is intended only as a supplement to Microsoft
' Development Tools and/or on-line documentation.  See these other
' materials for detailed information regarding Microsoft code samples.
' 
' THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
' KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
' IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
' PARTICULAR PURPOSE.
'---------------------------------------------------------------------

Imports System
Imports System.Windows.Media.Core
Imports System.Windows.Media.Types


' This console application plays the sound file specified on the command line.

Class MediaEngineDemo

    Private m_mediaEngine As MediaEngine
    Private engineCanBeDisposedEvent As System.Threading.AutoResetEvent
    Private engineOpenCompleteEvent As System.Threading.AutoResetEvent
    Private m_validFile As Boolean = True

    Shared Sub Main(ByVal args() As String)
        If (0 < args.Length) Then
            Dim player As MediaEngineDemo
            player = New MediaEngineDemo(args(0))
        Else
            Console.WriteLine("No media file specified.")
        End If
    End Sub

    Private Sub New(ByVal mediaFilePath As String)
        ' Create MediaEngine.
        m_mediaEngine = New MediaEngine

        ' Add event handlers.
        AddHandler m_mediaEngine.MediaEnded, AddressOf MediaEnded
        AddHandler m_mediaEngine.MediaOpened, AddressOf MediaOpened

        ' Create private events to be signaled by media event handlers.
        engineCanBeDisposedEvent = New System.Threading.AutoResetEvent(False)
        engineOpenCompleteEvent = New System.Threading.AutoResetEvent(False)

        ' Open and play the file. 
        ' Because media engine methods are asynchronous, they always return "success" 
        ' and any errors must be caught in the event handlers. In this case, if the file
        ' is not valid, a global variable is set.
        m_mediaEngine.Open(mediaFilePath, Nothing, GetType(AudioMediaType))
        engineOpenCompleteEvent.WaitOne()
        If (m_validFile = True) Then

            ' No exception, so start playing.
            m_mediaEngine.Start()

            ' Wait till playback is finished before exiting.
            engineCanBeDisposedEvent.WaitOne()
        Else
            Console.WriteLine("Could not open file.")
        End If
        engineCanBeDisposedEvent.Close()
        engineOpenCompleteEvent.Close()
    End Sub

    Private Sub MediaEnded(ByVal sender As Object, ByVal args As MediaEventArgs)
        ' Set a private event to notify the main thread that it is okay to exit.
        engineCanBeDisposedEvent.Set()
    End Sub

    Private Sub MediaOpened(ByVal sender As Object, ByVal args As MediaEventArgs)
        If (Not args.Exception Is Nothing) Then
            m_validFile = False
        End If
        engineOpenCompleteEvent.Set()
    End Sub

End Class


