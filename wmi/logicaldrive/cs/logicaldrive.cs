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
using System.Globalization;
using System.Text;
using System.IO ;
using System.Runtime.InteropServices;
using System.Management.Instrumentation ;

namespace Microsoft.LogicalDrive
{
	/// <summary>
	/// The instrumented class implements a logical drive, exposing information such as the amount of free space on the disk
	/// </summary>
	// Use the Folder attribute at the type level to indicate that the type is instrumented and where in the management namespace it should be placed
	[Folder (Uri="#System/Drive")] 
	public class Drive
	{
		// Private fields
		private		string	drive ;
		private		int		sectorsPerCluster ;
		private		int		bytesPerSector ;
		private		int		freeClusters ;
		private		int		totalClusters ;
		private		string	volumeLabel ;

		// Use the Probe attribute at the member level to expose the member for management purposes
		// Empty set code required to include this property in the return XSD of the URI to this class due to limitations of current implementation

		// Volume label
		public string VolumeLabel
		{
			[Probe] get { return volumeLabel ; }
			set { ; }
		}

		// Number of sectors per cluster
		public int SectorsPerCluster 
		{
			[Probe] get { return sectorsPerCluster ; }
			set { ; }
		}

		// Number of bytes per sector
		public int BytesPerSector
		{
			[Probe] get { return bytesPerSector ; }
			set { ; }
		}

		// Number of free clusters
		public int FreeClusters 
		{
			[Probe] get { return freeClusters; }
			set { ; }
		}

		// Total number of clusters
		public int TotalClusters
		{
			[Probe] get { return totalClusters ; }
			set { ; }
		}

		// Percentage of free clusters
		public int PercentFree
		{
			[Probe]	get 
					{ 
						if ( TotalClusters == 0 )
						{
							return 0 ;
						}
						double free = ((double)FreeClusters/(double)TotalClusters)*100;
						return (int) free ; 
					}
			set { ; }
		}

		// Name of the logical drive
		// Use the Key attribute to identify this member as the means of identifying instances of the class within a collection.
		[Key]
		public string DriveName
		{
			[Probe] get { return drive; }
			set { ; }
		}

		/// <summary>
		/// Retrieves the Drive object, given the name of the logical drive on this computer in the form "Drive_Letter:\".
		/// </summary>
		/// <param name="name">name of the logical drive on this computer in the form "Drive_Letter:\".</param>
		/// <returns>Drive object</returns>
		[Probe(Uri="Name=_")]
		public static Drive GetLogicalDrive ( string name ) 
		{
			Drive retVal = null ;
			NativeMethods.SetErrorMode ( 1 ) ;
			string[] driveStrs = Directory.GetLogicalDrives ( );
			foreach ( string driveString in driveStrs )
			{
				if ( string.Compare ( name, driveString, true, CultureInfo.InvariantCulture ) == 0 )
				{
					int tmp=0, tmp2=0, tmp3=0 ;
					StringBuilder volName = new StringBuilder (255) ;

					// retrieves volume whose drive name is specified, for example the C drive as "C:\".
					NativeMethods.GetVolumeInformation ( driveString, volName, 255, IntPtr.Zero, tmp, tmp2, IntPtr.Zero, tmp3 ) ;
					Drive drive = new Drive ( ) ;
					drive.volumeLabel = volName.ToString ();
					drive.drive = driveString ;

					// retrieves information about the specified disk, including the amount of free space on the disk.
					NativeMethods.GetDiskFreeSpace ( drive.DriveName, out drive.sectorsPerCluster, out drive.bytesPerSector, out drive.freeClusters, out drive.totalClusters ) ;
					retVal= drive ;
					break ;
				}
			}
			return retVal ;
		}

		/// <summary>
		/// Retrieves all the Drive objects corresponding to the logical drives on this computer
		/// </summary>
		/// <returns>Array of Drive objects</returns>
		[Probe(Uri="GetAll", ResultType=typeof(Drive))]
		public static Drive[] GetLogicalDrives ( ) 
		{
			NativeMethods.SetErrorMode ( 1 ) ;
			string[] driveStrs = Directory.GetLogicalDrives ( );
			Drive[] retVal = new Drive[driveStrs.Length] ;
			int index=0;
			foreach ( string driveString in driveStrs )
			{
				int tmp=0, tmp2=0, tmp3=0 ;
				StringBuilder volName = new StringBuilder (255) ;

				// retrieves volume whose drive name is specified, for example the C drive as "C:\".
				NativeMethods.GetVolumeInformation ( driveString, volName, 255, IntPtr.Zero, tmp, tmp2, IntPtr.Zero, tmp3 ) ;
				Drive drive = new Drive ( ) ;
				drive.volumeLabel = volName.ToString ();
				drive.drive = driveString ;

				// retrieves information about the specified disk, including the amount of free space on the disk.
				NativeMethods.GetDiskFreeSpace ( drive.DriveName, out drive.sectorsPerCluster, out drive.bytesPerSector, out drive.freeClusters, out drive.totalClusters ) ;
				retVal[index++] = drive ;
			}
			return retVal ;
		}
		
		/// <summary>
		/// The SetVolumeLabel function sets the label of a file system volume.
		/// </summary>
		/// <param name="label">a string specifying a name for the volume</param>
		/// <returns>true if success</returns>
		[Probe (Uri="VolumeLabel=_")]
		public bool SetVolumeLabel ( string label )
		{
			return NativeMethods.SetVolumeLabel ( drive, label ) ;			
		}

		/// <summary>
		/// Deletes all the file in the specified directory.
		/// </summary>
		/// <returns>true if success</returns>
		[Probe (Uri="Cleanup")]
		public bool Cleanup ( )
		{
			bool success = false ;
			while ( success == false )
			{
				string searchPath = DriveName+"temp" ;
				try
				{
					foreach ( string fileName in Directory.GetFiles ( searchPath, "*" ) )
					{
						File.Delete ( fileName ) ;
					}
					success = true ;
					return success;
				}
				catch (UnauthorizedAccessException uae)
				{
					Console.WriteLine("The caller does not have the required permission.");
					Console.WriteLine(uae.Message);
					Console.WriteLine(uae.StackTrace);
				}
				catch (DirectoryNotFoundException dnfe)
				{
					Console.WriteLine("The specified path is invalid, such as being on an unmapped drive.");
					Console.WriteLine(dnfe.Message);
					Console.WriteLine(dnfe.StackTrace);
				}
				Console.WriteLine();
				return false;
			}
			return success;
		}
	}

	/// <summary>
	/// Public class members based on native methods using Platform Invoke
	/// </summary>
	public sealed class NativeMethods
	{
		private NativeMethods() {}

		[DllImport ("kernel32.dll", CharSet=CharSet.Auto)]
		internal static extern int GetDiskFreeSpace ( string drive, [Out] out int spc, [Out] out int bps, [Out] out int nfc, [Out] out int tnc ) ;

		[DllImport ("kernel32.dll", CharSet=CharSet.Auto)]
		internal static extern bool GetVolumeInformation ( string drive, StringBuilder volumeName, int size, IntPtr par1, [Out] int som, [Out] int flags, IntPtr par2, int size2  ) ;
		
		[DllImport ("kernel32.dll", CharSet=CharSet.Auto)]
		internal static extern bool SetVolumeLabel ( string drive, string volName  ) ;

		[DllImport ("kernel32.dll", CharSet=CharSet.Auto)]
		internal static extern int SetErrorMode ( int mode ) ;
	}
}