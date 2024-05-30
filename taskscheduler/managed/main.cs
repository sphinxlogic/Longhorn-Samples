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


/****************************************************************************
*                                                                           *
* Main.cs - Sample application for Task Scheduler V2 managed API            *
*                                                                           *
* Component: Task Scheduler                                                 *
*                                                                           *
* Copyright (c) 2002 - 2003, Microsoft Corporation                          *
*                                                                           *
****************************************************************************/



using System;
using System.Management.TaskScheduler;

using System.Collections;
using System.Xml;
using System.Xml.Schema;
using System.Runtime.InteropServices;


namespace ConsoleApplication1
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class Class1
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{

			// Check if the user name is passed
			if( args.Length == 0 )
			{
				Console.WriteLine("Usage: CS_Sample <UserName> " );
				return;
			}
			string UserName = args[0];

			string UserPassword;
			UserPassword = String.Empty;

			//Get the password from the console
			Console.WriteLine("\nEnter the Password for the user : " );

			char Char;
			while( true )
			{
				Char = (char) ReadConsole.NativeMethod.GetCh();
				if( Char == '\r' )
					break;
				UserPassword += Char;
			}


			const string JobName = "TestJob";

			// Do the initialization to do impersonation.

			JobsInitialize InitObj = new JobsInitialize();


			//Get the FolderObject for the root folder

			JobFolder JobFolderObj = JobService.RootFolder;

			//If the job already exist, remove the nob
			if (RegisteredJob.Exists( JobName ))
			{
				JobFolderObj.RemoveJob( JobName );
			}


			//Create a Time Trigger Object and set the start time to 10 seconds from now

			TimeTrigger TimeTriggerObj = new TimeTrigger();			
			DateTime date = new DateTime();
			date = DateTime.Now;

			date = date.AddSeconds( 60 );
			TimeTriggerObj.StartTime = date;

			//Create a executable step to run calc.exe
			string ExecutablePath = Environment.SystemDirectory + "\\Calc.exe";

			ExecutableStep ExecStep = new ExecutableStep( "Calc", ExecutablePath );

			//Create job object and add the steps and triggers to it
			Job job = new Job();	

			job.Triggers.Add( TimeTriggerObj );
			job.Steps.Add( ExecStep );

			//Set the user name and password
			job.SetCredentials( UserName, UserPassword );

			Console.WriteLine( "Setting StartTime for Job as {0}", date  );
			
			//Register the job in root folder
			RegisteredJob RegJob = job.Register( JobName, "" );
		
			Console.WriteLine("Success !!! Job registered: " );
		}
	}


	public class JobsInitialize
	{

		public JobsInitialize()
		{
			//Initialize COM
			UInt32 hResult = NativeMethod.CoInitialize( IntPtr.Zero );

			//Initialize to COM Security ( Impersonate )
			hResult = NativeMethod.CoInitializeSecurity(
				IntPtr.Zero,
				-1,
				IntPtr.Zero,
				IntPtr.Zero,
				4, //RPC_C_AUTHN_LEVEL_PKT
				3, //RPC_C_IMP_LEVEL_IMPERSONATE
				IntPtr.Zero,
				0x3020,
				IntPtr.Zero);
		}

		internal class NativeMethod
		{
			[DllImport("OLE32.DLL")]
			public static extern UInt32 CoInitializeSecurity(
				[In] IntPtr securityDescriptor, 
				[In] Int32 cAuth,
				[In] IntPtr asAuthSvc,
				[In] IntPtr reserved,
				[In] UInt32 AuthLevel,
				[In] UInt32 ImpLevel,
				[In] IntPtr pAuthList,
				[In] UInt32 Capabilities,
				[In] IntPtr reserved3
				);

			[DllImport("OLE32.DLL")]
			public static extern UInt32 CoInitialize(
				[In] IntPtr reserved3
				);

		}

	}

        public class ReadConsole
        {
			internal class NativeMethod
			{
				[DllImport("msvcrt.dll", EntryPoint="_getch")]
				public static extern int GetCh();
			}
        }

}




