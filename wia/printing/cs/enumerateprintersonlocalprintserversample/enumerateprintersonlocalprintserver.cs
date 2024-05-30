//---------------------------------------------------------------------
//  This file is part of the Microsoft .NET Framework SDK Code Samples.
// 
//  Copyright (C) Microsoft Corporation.  All rights reserved.
// 
//This source code is intended only as a supplement to Microsoft
//Development Tools and/or on-line documentation.  See these other
//materials for detailed information regarding Microsoft code samples.
// 
//THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
//KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//PARTICULAR PURPOSE.
//---------------------------------------------------------------------
using System;
using System.Printing;
using System.Printing.PrintSubSystem;
using System.Printing.Configuration;
using Microsoft.Printing.DeviceCapabilities;
using Microsoft.Printing.JobTicket;
using System.Collections;


namespace PrintSystemSample
{
    /// <summary>
    /// TestPrintSystemClass class implements sample the code for 
    /// enumerating local printers on the local print server.
    /// </summary>
    class TestPrintSystemClass
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("This sample enumerates local printers on the local server and prints some of the properties.");

            Console.WriteLine("\nPress <Enter> to continue:");
            Console.ReadLine();

            EnumeratePrintersonLocalPrintServer();

			Console.ReadLine();
        }

        /// <summary>
        /// Create enumerate the local print queues on the local print server and list the
        /// location information and the .
        /// </summary>
        static public void EnumeratePrintersonLocalPrintServer()
        {
            try
            {
                PrintQueueProperty[] printerProperties = {
                    PrintQueueProperty.Name, 
                    PrintQueueProperty.Location, 
                    PrintQueueProperty.DefaultJobTicket
                };

                LocalPrintServer printServer = new LocalPrintServer();

                PrintQueueCollection printQueuesOnLocalServer = printServer.GetPrintQueues(printerProperties);

                Console.WriteLine("Enumerate local print queues:");

                foreach (PrintQueue printer in printQueuesOnLocalServer)
                {
                    Console.WriteLine("--- Printer '" + printer.Name + "' located at '" + printer.Location + "' ---");

                    // Construct a JobTicket object using the printer's default job ticket.
                    JobTicket jt = new JobTicket(printer.DefaultJobTicket);
                    
                    // Acquire the printer's device capabilities and construct a DeviceCapabilities object.
                    DeviceCapabilities devcap = new DeviceCapabilities(printer.AcquireDeviceCapabilities(printer.DefaultJobTicket));

                    // Check if the printer can print color.                    
                    Console.WriteLine((devcap.CanPrintColor ? "Can" : "Cannot") + " print color documents.");

                    // Query for device capability of document duplexing.
                    Console.WriteLine("DocumentDuplex Capability:");
                    if (devcap.SupportsCapability(PrintSchema.Features.DocumentDuplex))
                    {
                        foreach (DocumentDuplexCapability.DuplexOption option in devcap.DocumentDuplexCap.DuplexOptions)
                        {
                            Console.WriteLine("   {0} (constrained by: {1})",
                                              option.Value, option.Constrained);
                        }
                    }
                    else
                    {
                        Console.WriteLine("   not supported");
                    }
                    
                    // Query for device capability of page resolution.
                    Console.WriteLine("PageResolution Capability:");
                    if (devcap.SupportsCapability(PrintSchema.Features.PageResolution))
                    {
                        foreach (PageResolutionCapability.Resolution option in devcap.PageResolutionCap.Resolutions)
                        {
                            Console.WriteLine("   {0}x{1} (constrained by: {2})",
                                              option.ResolutionX, option.ResolutionY, option.Constrained);
                        }
                    }
                    else
                    {
                        Console.WriteLine("   not supported");
                    }
                    
                    // Query for device capability of page canvas size.
                    Console.WriteLine("PageCanvasSize Capability:");
                    if (devcap.SupportsCapability(PrintSchema.Features.PageCanvasSize))
                    {
                        // Set the length unit type to Inch in order to get length values in inches.
                        devcap.LengthUnitType = PrintSchema.LengthUnitTypes.Inch;
                        
                        // If the printer's device capability doesn't specify CanvasSizeX or CanvasSizeY,
                        // then the value will be PrintSchema.UnspecifiedDecimalValue.
                        if (devcap.PageCanvasSizeCap.CanvasSizeX != PrintSchema.UnspecifiedDecimalValue)
                        {
                            Console.WriteLine("   CanvasSizeX = {0}", devcap.PageCanvasSizeCap.CanvasSizeX);
                        }
                        else
                        {
                            Console.WriteLine("   CanvasSizeX not specified");
                        }

                        if (devcap.PageCanvasSizeCap.CanvasSizeY != PrintSchema.UnspecifiedDecimalValue)
                        {
                            Console.WriteLine("   CanvasSizeY = {0}", devcap.PageCanvasSizeCap.CanvasSizeY);
                        }
                        else
                        {
                            Console.WriteLine("   CanvasSizeY not specified");
                        }
                    }
                    else
                    {
                        Console.WriteLine("   not supported");
                    }

                    Console.WriteLine("Printer-default Job Ticket Settings:");
                    Console.WriteLine("   PageMediaSize = {0}", jt.PageMediaSize.Value);
                    Console.WriteLine("   PageOrientation = {0}", jt.PageOrientation.Value);
                }
            }
            catch (PrintSystemException printException)
            {
                Console.Write(printException.Message);                
            }
        }

    }
}