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
Imports System.Reflection
Imports System.Runtime.InteropServices

<Assembly: CLSCompliant(True)> 
<Assembly: ComVisible(False)> 

' RSSimporter sample
'
' A console application that imports an RSS feed as messages
' in the Communication History.
Namespace Microsoft.Samples.Communications
    Module RssSample
        Sub Main(ByVal args() As String)
            If args.Length <> 0 Then
                RssImporter.Import(args(0))
            Else
                Dim defaultSource As String = "http://msdn.microsoft.com/rss.xml"

                Console.WriteLine("No source specified. Using default.")
                Console.WriteLine()
                Console.WriteLine("To download a different RSS feed, run the sample like this:")
                Console.WriteLine("    RssImporterSample RssFile")
                Console.WriteLine()
                RssImporter.Import(defaultSource)
            End If
        End Sub

    End Module
End Namespace
