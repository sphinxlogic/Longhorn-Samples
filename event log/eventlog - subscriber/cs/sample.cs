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
// using System.Xml;
using System.Collections;
using System.Threading;
using System.Diagnostics.Events;


namespace Microsoft.Sample.Subscriber
{
	public class Sample
	{
		static public void Main(string[] args)
		{
			Console.WriteLine("Subscribe to events asynchronously.");

			EventFilterExpression filter = new EventFilterExpression("Global/Application/*");
			filter.EventMatched += new EntryWrittenEventHandler(EventHandler);

			EventQuery query = new EventQuery();
			query.Selectors.Add(filter);

			int timeout = 100;

			using(EventLog globalApp = new EventLog())
			{
				using(globalApp.AsyncSubscription(query))
				{
					// loop for 100 seconds waiting for events to occur then time out

					Console.WriteLine("Waiting for events to occur...");

					for (int i=0; i < timeout; i++)
					{
						Thread.Sleep(1000);	
					}
				}
			}

		}

		private static void EventHandler(object sender, EntryWrittenEventArgs e)
		{
			string textNode = e.Entry.XmlNode.OuterXml;

			Console.WriteLine("Recieved an event:");
			Console.WriteLine(textNode);
			Console.WriteLine();
		}

	}
}
