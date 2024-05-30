'---------------------------------------------------------------------
'  This file is part of the Microsoft .NET Framework SDK Code Samples.
' 
'  Copyright (C) Microsoft Corporation.  All rights reserved.
' 
'This source code is intended only as a supplement to Microsoft
'Development Tools and/or on-line documentation.  See these other
'materials for detailed information regarding Microsoft code samples.
' 
'THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
'KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
'IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
'PARTICULAR PURPOSE.
'---------------------------------------------------------------------

Imports System
Imports System.Globalization
Imports System.Text
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Management.Instrumentation

Namespace Microsoft.LogicalDrive

    ' The instrumented class implements a logical drive, exposing information such as the amount of free space on the disk
    ' Use the Folder attribute at the type level to indicate that the type is instrumented and where in the management namespace it should be placed
    <Folder(Uri:="#System/Drive")> _
    Public Class Drive

        ' Private fields
        Private driveVB As String
        Private sectorsPerClusterVB As Integer
        Private bytesPerSectorVB As Integer
        Private freeClustersVB As Integer
        Private totalClustersVB As Integer
        Private volumeLabelVB As String

        ' Use the Probe attribute at the member level to expose the member for management purposes
        ' Empty set code required to include this property in the return XSD of the URI to this class due to limitations of current implementation

        ' Volume label
        Public Property VolumeLabel() As String
            <Probe()> Get
                Return volumeLabelVB
            End Get
            Set(ByVal value As String)
            End Set
        End Property

        ' Number of sectors per cluster
        Public Property SectorsPerCluster() As Integer
            <Probe()> Get
                Return sectorsPerClusterVB
            End Get
            Set(ByVal value As Integer)
            End Set
        End Property

        ' Number of bytes per sector
        Public Property BytesPerSector() As Integer
            <Probe()> Get
                Return bytesPerSectorVB
            End Get
            Set(ByVal value As Integer)
            End Set
        End Property

        ' Number of free clusters
        Public Property FreeClusters() As Integer
            <Probe()> Get
                Return freeClustersVB
            End Get
            Set(ByVal value As Integer)
            End Set
        End Property

        ' Total number of clusters
        Public Property TotalClusters() As Integer
            <Probe()> Get
                Return totalClustersVB
            End Get
            Set(ByVal value As Integer)
            End Set
        End Property

        ' Percentage of free clusters
        Public Property PercentFree() As Integer
            <Probe()> Get
                If TotalClusters = 0 Then
                    Return 0
                End If
                Dim free As Double = (CDbl(FreeClusters) / CDbl(TotalClusters)) * 100
                Return (CInt(free))
            End Get
            Set(ByVal value As Integer)
            End Set
        End Property

        ' Name of the logical drive
        ' Use the Key attribute to identify this member as the means of identifying instances of the class within a collection.
        <Key()> _
        Public Property DriveName() As String
            <Probe()> Get
                Return driveVB
            End Get
            Set(ByVal value As String)
            End Set
        End Property

        ' Retrieves the Drive object, given the name of the logical drive on this computer in the form "Drive_Letter:\".
        <Probe(Uri:="Name=_")> _
        Public Shared Function GetLogicalDrive(ByVal name As String) As Drive
            Dim retVal As Drive = Nothing
            NativeMethods.SetErrorMode(1)
            Dim driveStrs() As String = Directory.GetLogicalDrives()
            For Each driveString As String In driveStrs
                If (String.Compare(name, driveString, True, CultureInfo.InvariantCulture) = 0) Then
                    Dim tmp As Integer = 0, tmp2 As Integer = 0, tmp3 As Integer = 0
                    Dim volName As StringBuilder = New StringBuilder(255)

                    ' retrieves volume whose drive name is specified, for example the C drive as "C:\".
                    NativeMethods.GetVolumeInformation(driveString, volName, 255, IntPtr.Zero, tmp, tmp2, IntPtr.Zero, tmp3)
                    Dim driveVB As Drive = New Drive
                    driveVB.volumeLabelVB = volName.ToString()
                    driveVB.driveVB = driveString

                    ' retrieves information about the specified disk, including the amount of free space on the disk.
                    NativeMethods.GetDiskFreeSpace(driveVB.DriveName, driveVB.sectorsPerClusterVB, driveVB.bytesPerSectorVB, driveVB.freeClustersVB, driveVB.totalClustersVB)
                    retVal = driveVB
                    Exit For
                End If
            Next
            Return retVal
        End Function

        ' Retrieves all the Drive objects corresponding to the logical drives on this computer
        <Probe(Uri:="GetAll", ResultType:=GetType(Drive))> _
        Public Shared Function GetLogicalDrives() As Drive()
            NativeMethods.SetErrorMode(1)
            Dim driveStrs() As String = Directory.GetLogicalDrives()
            Dim retVal(driveStrs.Length - 1) As Drive
            Dim index As Integer = 0
            For Each driveString As String In driveStrs
                Dim tmp As Integer = 0, tmp2 As Integer = 0, tmp3 As Integer = 0
                Dim volName As StringBuilder = New StringBuilder(255)

                ' retrieves volume whose drive name is specified, for example the C drive as "C:\".
                NativeMethods.GetVolumeInformation(driveString, volName, 255, IntPtr.Zero, tmp, tmp2, IntPtr.Zero, tmp3)
                Dim driveVB As Drive = New Drive
                driveVB.volumeLabelVB = volName.ToString()
                driveVB.driveVB = driveString

                ' retrieves information about the specified disk, including the amount of free space on the disk.
                NativeMethods.GetDiskFreeSpace(driveVB.DriveName, driveVB.sectorsPerClusterVB, driveVB.bytesPerSectorVB, driveVB.freeClustersVB, driveVB.totalClustersVB)
                retVal(index) = driveVB
                index = index + 1
            Next
            Return retVal
        End Function

        ' The SetVolumeLabel function sets the label of a file system volume.
        <Probe(Uri:="VolumeLabel=_")> _
        Public Function SetVolumeLabel(ByVal label As String) As Boolean
            Return NativeMethods.SetVolumeLabel(driveVB, label)
        End Function

        ' Deletes all the file in the specified directory.
        <Probe(Uri:="Cleanup")> _
        Public Function Cleanup() As Boolean
            Dim success As Boolean = False
            While success = False
                Dim searchPath As String = DriveName + "temp"
                Try
                    For Each fileName As String In Directory.GetFiles(searchPath, "*")
                        File.Delete(fileName)
                    Next
                    success = True
                    Return success
                Catch uae As UnauthorizedAccessException
                    Console.WriteLine("The caller does not have the required permission.")
                    Console.WriteLine(uae.Message)
                    Console.WriteLine(uae.StackTrace)
                Catch dnfe As DirectoryNotFoundException
                    Console.WriteLine("The specified path is invalid, such as being on an unmapped drive.")
                    Console.WriteLine(dnfe.Message)
                    Console.WriteLine(dnfe.StackTrace)
                End Try
                Console.WriteLine()
                Return False
            End While
            Return success
        End Function

    End Class

    ' Public class members based on native methods using Platform Invoke
    Public NotInheritable Class NativeMethods
        Private Sub New()
        End Sub

        <DllImport("kernel32.dll", CharSet:=CharSet.Auto)> _
        Friend Shared Function GetDiskFreeSpace(ByVal drive As String, _
            <Out()> ByRef spc As Integer, <Out()> ByRef bps As Integer, _
            <Out()> ByRef nfc As Integer, <Out()> ByRef tnc As Integer) As Integer
        End Function

        <DllImport("kernel32.dll", CharSet:=CharSet.Auto)> _
        Friend Shared Function GetVolumeInformation(ByVal drive As String, _
            ByVal volumeName As StringBuilder, ByVal size As Integer, _
            ByVal par1 As IntPtr, ByVal som As Integer, _
            ByVal flags As Integer, ByVal par2 As IntPtr, _
            ByVal size2 As Integer) As Boolean
        End Function

        <DllImport("kernel32.dll", CharSet:=CharSet.Auto)> _
        Friend Shared Function SetVolumeLabel(ByVal drive As String, _
            ByVal volName As String) As Boolean
        End Function

        <DllImport("kernel32.dll", CharSet:=CharSet.Auto)> _
        Friend Shared Function SetErrorMode(ByVal mode As Integer) As Integer
        End Function
    End Class

End Namespace