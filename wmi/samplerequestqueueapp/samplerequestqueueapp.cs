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
using System.Collections;
using System.Management.Instrumentation;

// Use the assembly level ApplicationProbe attribute to specify where in the management namespace probes from this application should be placed.  This URI must include one placeholder '_' to identify the instance of the application (ie., pid).
[assembly:ApplicationProbe ("#System/Apps/MyApp/MyId=_")]

namespace Microsoft.SampleRequestQueueApp
{
	/// <summary>
	/// The application contains a queue of integers which can be manipulated from the cmdline and inspected via instrumentation.
	/// </summary>
	[Folder]
	public class MyApp
	{
		private static RequestQueues queue = new RequestQueues();

		/// <summary>
		/// Exposes the Queue for the application
		/// </summary>
		/// <returns></returns>
		[Probe(Uri="RequestQueue")]
		public static RequestQueues GetQueue
		{
			get { return queue ; }
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			// Register the application instance with the instrumentation system using the default instance id (process id).
			Application.Register() ;

			Console.WriteLine("Hit (a) to add to queue, (d) to delete from queue, (q) to quit ") ;
			ReadEvalPrintLoop();

			// Unregisters the application
			Application.UnRegister ( ) ;
		}
	

		/// <summary>
		/// Handles the command line options
		///		'a' to add an integer to the end of the Queue
		///		'd' to remove the integer at the beginning of the Queue
		///		'p' to print out the current queue contents
		///		'q' to exit the application
		/// </summary>
		private static void ReadEvalPrintLoop() 
		{
			while (true) 
			{
				char c = (char) Console.Read();
				if (c == 'a') 
				{
					queue.AddItem();
				} 
				else if (c == 'd') 
				{
					queue.DeleteItem();
				}
				else if (c == 'p') 
				{
					object[] values = queue.Contents();
					if (values.Length == 0) 
					{
						Console.WriteLine("empty");
					} 
					else 
					{
						for (int i = 0; i < values.Length; i++) 
						{
							Console.Write(values[i]);
							if (i+1 < values.Length) 
							{
								Console.Write(",");
							}
						}
						Console.WriteLine();
					}
				}
				else if (c == 'q') 
				{
					break;
				}
			}
		}
	}

	/// <summary>
	/// The instrumented class implements a queue to be manipulated by application
	/// </summary>
	[Folder]
	public class RequestQueues
	{
		// Private fields
		private Queue contents = new Queue();
		private int maxConcurrentRequests = 10;
		private Random randomGenerator = new Random();

		/// <summary>
		/// Exposes the maximum number of queue elements
		/// </summary>
		[Probe]
		public int MaxConcurrentRequests 
		{ 
			get { return maxConcurrentRequests; } 
			set { maxConcurrentRequests = value; }
		}
		
		/// <summary>
		/// Exposes the Queue elements as an array
		/// </summary>
		/// <returns></returns>
		// Use the ResultType attribute on a Probe to indicate tell instrumentation what types are contained in the array in the event that the return type is too generic (ie. object in this case)
		[Probe(ResultType=typeof(int))]
		public object[] Contents() 
		{
			return contents.ToArray();
		}

		/// <summary>
		/// Adds a random number to the end of the Queue.
		/// </summary>
		public void AddItem() 
		{
			if (contents.Count < maxConcurrentRequests) 
			{
				contents.Enqueue(randomGenerator.Next(100));
			}
		}

		/// <summary>
		/// Removes the number at the beginning of the Queue.
		/// </summary>
		public void DeleteItem() 
		{
			if (contents.Count > 0) 
			{
				contents.Dequeue();
			}
		}
	}
}